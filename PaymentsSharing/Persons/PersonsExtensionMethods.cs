namespace PaymentsSharing.Persons;

internal static class PersonsExtensionMethods
{
    public static IServiceCollection AddPersons(this IServiceCollection services)
    {
        services.AddSingleton<Persons>();
        services.AddScoped<CurrentPerson>();
        services.AddTransient<SignedInPersonViewModel>();
        return services;
    }
}