using System.Collections;
using MediatR;
using PaymentsSharing.Persons;
using PaymentsSharing.Time;

namespace PaymentsSharing.Payments;

internal class Payments : INotificationHandler<PaymentAdded>
{
    private readonly List<Payment> _payments =
    [
        new Payment(
            new DateTime(2024, 1, 2),
            new[] { new Person("Natalia", false) },
            new[] { new Person("Natalia", false), new Person("Mikołaj", true), new Person("Andrzej", true) },
            100,
            null,
            "Warzywka"),
        new Payment(
            new DateTime(2024, 1, 3),
            new[] { new Person("Natalia", false), new Person("Mikołaj", true) },
            new[] { new Person("Natalia", false), new Person("Mikołaj", true), new Person("Andrzej", true) },
            100,
            null,
            "Pepco"),
        new Payment(
            new DateTime(2024, 1, 4),
            new[] { new Person("Mikołaj", false) },
            new[] { new Person("Natalia", false), new Person("Mikołaj", true), new Person("Andrzej", true) },
            22,
            12),
        new Payment(
            new DateTime(2024, 1, 4),
            new[] { new Person("Mikołaj", false) },
            new[] { new Person("Natalia", false), new Person("Mikołaj", true) },
            40)
    ];

    public Task Handle(PaymentAdded notification, CancellationToken cancellationToken)
    {
        _payments.Add(new Payment(
            notification.CreatedAt,
            notification.Payers,
            notification.Consumers,
            notification.Amount,
            notification.AmountForMeat,
            notification.Description));

        return Task.CompletedTask;
    }
    
    public IEnumerable<Payment> All => _payments;
    
    public IEnumerable<Payment> FromMonth(Person person, MonthAndYear monthAndYear)
    {
        return _payments.Where(payment => (payment.Payers.Contains(person) || payment.Consumers.Contains(person))
                                          && payment.CreatedAt.Month == monthAndYear.Month
                                          && payment.CreatedAt.Year == monthAndYear.Year);
    }
}