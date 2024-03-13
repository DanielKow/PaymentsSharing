using System.Text;
using System.Text.Json;
using MediatR;
using PaymentsSharing.Payments;
using PaymentsSharing.Persons;

namespace PaymentsSharing.EventStore;

internal class EventsHandler<T>(EventsContext context): INotificationHandler<T> where T : INotification
{
    public async Task Handle(T @event, CancellationToken cancellationToken)
    {
        if (@event is PersonSignedIn || @event is PaymentAdded)
        {
            return;
        }
        
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