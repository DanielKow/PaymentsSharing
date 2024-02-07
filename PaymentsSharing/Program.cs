using MediatR;
using MudBlazor.Services;
using PaymentsSharing;
using PaymentsSharing.Components;
using PaymentsSharing.EventStore;
using PaymentsSharing.Payments;
using PaymentsSharing.Persons;
using PaymentsSharing.Refunds;
using PaymentsSharing.SignIn;
using PaymentsSharing.Summary;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddEventStore()
    .AddMudServices()
    .AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly))
    .AddPayments()
    .AddPersons()
    .AddSignIn()
    .AddSummary()
    .AddRefunds();


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

AddTestPayments().Wait();

app.Run();

async Task AddTestPayments()
{
    var persons = app.Services.GetRequiredService<Persons>();

    var mediator = app.Services.GetRequiredService<ISender>();
    await mediator.Send(new AddPayment(
        persons.Everyone.Where(person => person.Name is "Natalia"),
        persons.Everyone,
        100,
        null,
        "Pizza"));

    await mediator.Send(new AddPayment(
        persons.Everyone.Where(person => person.Name is "Natalia" or "Mikołaj"),
        persons.Everyone,
        200,
        null,
        "Lidl"));

    await mediator.Send(new AddPayment(
        persons.Everyone.Where(person => person.Name is "Mikołaj"),
        persons.Everyone,
        50,
        50,
        "Dino"));

    await mediator.Send(new AddPayment(
        persons.Everyone.Where(person => person.Name is "Andrzej"),
        persons.Everyone,
        13,
        null,
        "Chleb"));

    await mediator.Send(new AddPayment(
        persons.Everyone.Where(person => person.Name is "Natalia"),
        persons.Everyone.Where(person => person.Name is "Natalia" or "Mikołaj"),
        300,
        null,
        "Pepco"));

    await mediator.Send(new AddPayment(
        persons.Everyone.Where(person => person.Name is "Mikołaj"),
        persons.Everyone.Where(person => person.Name is "Natalia" or "Mikołaj"),
        100,
        null,
        "Aldi"));
}