using System.Text.Json;
using EventStore.Client;

namespace PaymentsSharing.EventStore;

internal class MonthlyEventBus : IEventBus
{
    private readonly EventStoreClient _client;

    public MonthlyEventBus(EventStoreClient client)
    {
        _client = client;
    }

    public Task Append<TEvent>(TEvent @event) where TEvent : notnull
    {
        var streamName = DateTime.Now.ToString("YYYYMM");
        var eventData = new EventData(Uuid.NewUuid(), typeof(TEvent).Name, JsonSerializer.SerializeToUtf8Bytes(@event));

        return _client.AppendToStreamAsync(
            streamName,
            StreamState.Any,
            new[] { eventData });
    }
}