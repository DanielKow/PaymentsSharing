using System.Text.Json;
using MediatR;

namespace PaymentsSharing.EventStore;

internal class AzureEventStore(EventsContext context) : IEventStore
{
    public async Task SaveEvent<T>(T @event, CancellationToken cancellationToken) where T : INotification
    {
        string eventJson = JsonSerializer.Serialize(@event);

        string? eventType = @event.GetType().AssemblyQualifiedName;

        if (eventType is null)
        {
            throw new Exception($"Cannot get event type name for event: {@event.GetType().Name}");
        }
        
        var toStore = new Event(Guid.NewGuid(), eventType, eventJson);

        context.Events.Add(toStore);
        await context.SaveChangesAsync(cancellationToken);
    }
}