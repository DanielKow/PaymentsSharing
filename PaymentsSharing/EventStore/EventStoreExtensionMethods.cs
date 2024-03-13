namespace PaymentsSharing.EventStore;

internal static class EventStoreExtensionMethods
{
    public static IServiceCollection AddEventStore(this IServiceCollection services)
    {
        services.AddScoped<IEventStore, AzureEventStore>();
        return services;
    }
}