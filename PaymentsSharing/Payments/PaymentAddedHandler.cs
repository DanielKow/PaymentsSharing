using MediatR;
using PaymentsSharing.Refunds;

namespace PaymentsSharing.Payments;

internal class PaymentAddedHandler(Refunds.Refunds refunds) : INotificationHandler<PaymentAdded>
{
    public Task Handle(PaymentAdded notification, CancellationToken cancellationToken)
    {
        refunds.Recalculate();
        return Task.CompletedTask;
    }
}