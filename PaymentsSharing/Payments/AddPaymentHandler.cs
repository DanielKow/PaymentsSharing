using MediatR;

namespace PaymentsSharing.Payments;

internal class AddPaymentHandler(IPublisher publisher) : IRequestHandler<AddPayment>
{
    public async Task Handle(AddPayment command, CancellationToken cancellationToken)
    {
        
        if (command.Amount + (command.AmountForMeat ?? 0) <= 0)
        {
            throw new InvalidPaymentException("Amount must be greater than 0");
        }

        await publisher.Publish(
            new PaymentAdded(
                DateTime.Now,
                command.Amount,
                command.AmountForMeat,
                command.Description), cancellationToken);
    }
}