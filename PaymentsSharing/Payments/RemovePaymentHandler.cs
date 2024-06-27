using MediatR;

namespace PaymentsSharing.Payments;

internal class RemovePaymentHandler(Payments payments, IPublisher publisher) : IRequestHandler<RemovePayment>
{
    public async Task Handle(RemovePayment command, CancellationToken cancellationToken)
    {
        if (payments.Contains(command.Payment))
        {
            await publisher.Publish(new PaymentRemoved(command.Payment), cancellationToken);
        }
    }
}