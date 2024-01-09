using EventStore.Client;

namespace PaymentsSharing.EventStore;

internal static class EventStoreExtensionMethods
{
    public static IServiceCollection AddEventStore(this IServiceCollection services)
    {
        const string connectionString = "esdb://admin:changeit@localhost:2113?tls=false&tlsVerifyCert=false";
        var settings = EventStoreClientSettings.Create(connectionString);
        var client = new EventStoreClient(settings);
        
        services.AddSingleton(client);
        services.AddSingleton<IEventBus, MonthlyEventBus>();
        
        return services;
    }
}