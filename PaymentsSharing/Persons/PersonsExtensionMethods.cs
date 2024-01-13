namespace PaymentsSharing.Persons;

internal static class PersonsExtensionMethods
{
    public static IServiceCollection AddPersons(this IServiceCollection services)
    {
        services.AddSingleton<Persons>();
        return services;
    }
}