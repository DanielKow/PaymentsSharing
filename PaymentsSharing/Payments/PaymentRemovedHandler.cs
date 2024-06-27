using MediatR;
using PaymentsSharing.EventStore;

namespace PaymentsSharing.Payments;

internal class PaymentRemovedHandler(Payments payments, IEventStore eventStore) : INotificationHandler<PaymentRemoved>
{
    public async Task Handle(PaymentRemoved @event, CancellationToken cancellationToken)
    {
        payments.Remove(@event.Payment);
        await eventStore.SaveEvent(@event, cancellationToken);
    }
}