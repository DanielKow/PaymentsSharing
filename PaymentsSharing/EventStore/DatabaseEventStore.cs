using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PaymentsSharing.EventStore;

internal class DatabaseEventStore(EventsContext context) : IEventStore
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
    
    public async Task<IEnumerable<T?>> GetEvents<T>(CancellationToken cancellationToken = default) where T :INotification
    {
        List<Event> events = await context.Events.Where(row => row.Type == typeof(T).AssemblyQualifiedName).ToListAsync(cancellationToken);
        
        return events.Select(row => JsonSerializer.Deserialize<T>(row.Data)).AsEnumerable();
    }
}