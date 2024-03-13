namespace PaymentsSharing.EventStore;

internal static class EventStoreExtensionMethods
{
    public static IServiceCollection AddEventStore(this IServiceCollection services)
    {
        services.AddSingleton<IEventStore, AzureEventStore>();
        return services;
    }
}