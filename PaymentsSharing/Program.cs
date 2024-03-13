using MediatR;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using PaymentsSharing;
using PaymentsSharing.EventStore;
using PaymentsSharing.Payments;
using PaymentsSharing.Persons;
using PaymentsSharing.Refunds;
using PaymentsSharing.SignIn;
using PaymentsSharing.Summary;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddMudServices()
    .AddMediatR(config => { config.RegisterServicesFromAssembly(typeof(Program).Assembly); })
    .AddPayments()
    .AddPersons()
    .AddSignIn()
    .AddSummary()
    .AddRefunds()
    .AddEventStore();


builder.Services.AddDbContext<EventsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL")));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

using IServiceScope scope = app.Services.CreateScope();
var eventStore = scope.ServiceProvider.GetRequiredService<IEventStore>();
var payments = scope.ServiceProvider.GetRequiredService<Payments>();

Console.WriteLine("Getting payments");
IEnumerable<PaymentAdded?> events = await eventStore.GetEvents<PaymentAdded>();

foreach (PaymentAdded? @event in events)
{
    if (@event is null)
    {
        continue;
    }
    
    payments.Add(new Payment(
        @event.CreatedAt,
        @event.Payers,
        @event.Consumers,
        @event.Amount,
        @event.AmountForMeat,
        @event.Description));
}

Console.WriteLine("Got events");

app.Run();
