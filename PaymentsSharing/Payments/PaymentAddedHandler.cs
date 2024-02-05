using MediatR;

namespace PaymentsSharing.Payments;

internal class PaymentAddedHandler : INotificationHandler<PaymentAdded>
{
    public Task Handle(PaymentAdded notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}