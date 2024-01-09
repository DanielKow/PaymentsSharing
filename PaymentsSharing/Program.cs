using MudBlazor.Services;
using PaymentsSharing.Components;
using PaymentsSharing.EventStore;
using PaymentsSharing.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddEventStore()
    .AddMudServices()
    .AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly))
    .AddUsers();

builder.Services.AddScoped<SignedInUserInfoViewModel>();

var app = builder.Build();

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

app.Run();