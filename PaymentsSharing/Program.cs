using MediatR;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using PaymentsSharing;
using PaymentsSharing.EventStore;
using PaymentsSharing.Payments;
using PaymentsSharing.Refunds;
using PaymentsSharing.Summary;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddMudServices()
    .AddMediatR(config => { config.RegisterServicesFromAssembly(typeof(Program).Assembly); })
    .AddPayments()
    .AddSummary()
    .AddRefunds()
    .AddEventStore();


builder.Services.AddDbContext<EventsContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

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
var eventsGetter = scope.ServiceProvider.GetRequiredService<EventsGetter>();
var sender = scope.ServiceProvider.GetRequiredService<ISender>();

await eventsGetter.GetEvents();

app.Run();