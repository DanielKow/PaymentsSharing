using MediatR;
using PaymentsSharing.Persons;
using PaymentsSharing.Time;

namespace PaymentsSharing.Payments;

internal class Payments(IPublisher mediator)
{
    private readonly List<Payment> _payments = [];

    public async Task Add(Payment payment)
    {
        _payments.Add(payment);
        await mediator.Publish(new PaymentAdded(
            payment.CreatedAt,
            payment.Payers,
            payment.Consumers,
            payment.Amount,
            payment.AmountForMeat,
            payment.Description));
    }
    
    public IEnumerable<Payment> All => _payments;
    
    public IEnumerable<Payment> FromMonth(Person person, MonthAndYear monthAndYear)
    {
        return _payments.Where(payment => (payment.Payers.Contains(person) || payment.Consumers.Contains(person))
                                          && payment.CreatedAt.Month == monthAndYear.Month
                                          && payment.CreatedAt.Year == monthAndYear.Year);
    }
}