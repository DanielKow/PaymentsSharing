using MediatR;
using PaymentsSharing.EventStore;

namespace PaymentsSharing.Payments;

internal class PaymentAddedHandler(Payments payments, IEventStore eventStore) : INotificationHandler<PaymentAdded>
{
    public Task Handle(PaymentAdded @event, CancellationToken cancellationToken)
    { 
        payments.Add(new Payment(
            @event.CreatedAt,
            @event.Payers,
            @event.Consumers,
            @event.Amount,
            @event.AmountForMeat,
            @event.Description));

        eventStore.SaveEvent(@event, cancellationToken);
        
        return Task.CompletedTask;
    }
}