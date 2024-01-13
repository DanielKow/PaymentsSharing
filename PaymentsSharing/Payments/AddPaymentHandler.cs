using MediatR;
using PaymentsSharing.Persons;

namespace PaymentsSharing.Payments;

internal class AddPaymentHandler(Persons.Persons persons, IPublisher mediator) : IRequestHandler<AddPayment>
{

    public async Task Handle(AddPayment addPayment, CancellationToken cancellationToken)
    {
        if (!persons.VerifyPersons(addPayment.Payers))
        {
            throw new InvalidPaymentException("Payers are not valid");
        }
        
        if (!persons.VerifyPersons(addPayment.Consumers))
        {
            throw new InvalidPaymentException("Consumers are not valid");
        }
        
        if (addPayment.Amount <= 0)
        {
            throw new InvalidPaymentException("Amount must be greater than 0");
        }
        
        if (addPayment.AmountForMeat is <= 0)
        {
            throw new InvalidPaymentException("Amount for meat must be greater than 0");
        }
        
        await mediator.Publish(new PaymentAdded(
            DateTime.Now,
            addPayment.Payers,
            addPayment.Consumers,
            addPayment.Amount,
            addPayment.AmountForMeat,
            addPayment.Description), cancellationToken);
    }
}