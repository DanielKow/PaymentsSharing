namespace PaymentsSharing.EventStore;

internal static class EventStoreExtensionMethods
{
    public static IServiceCollection AddEventStore(this IServiceCollection services)
    {
        services.AddScoped<IEventStore, DatabaseEventStore>();
        services.AddScoped<EventsGetter>();
        return services;
    }
}