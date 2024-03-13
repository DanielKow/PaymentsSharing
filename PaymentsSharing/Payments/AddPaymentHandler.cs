using MediatR;
using PaymentsSharing.Persons;

namespace PaymentsSharing.Payments;

internal class AddPaymentHandler(Persons.Persons persons, IPublisher publisher) : IRequestHandler<AddPayment>
{
    public async Task Handle(AddPayment command, CancellationToken cancellationToken)
    {
        if (!persons.VerifyPersons(command.Payers))
        {
            throw new InvalidPaymentException("Payers are not valid");
        }

        if (!persons.VerifyPersons(command.Consumers))
        {
            throw new InvalidPaymentException("Consumers are not valid");
        }

        if (command.Amount + (command.AmountForMeat ?? 0) <= 0)
        {
            throw new InvalidPaymentException("Amount must be greater than 0");
        }

        await publisher.Publish(
            new PaymentAdded(
                DateTime.Now, 
                command.Payers,
                command.Consumers,
                command.Amount,
                command.AmountForMeat,
                command.Description), cancellationToken);
    }
}