using MediatR;
using PaymentsSharing.Persons;

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

    public IReadOnlyCollection<Payment> All => _payments;

    public IReadOnlyCollection<Payment> FromCurrentMonth =>
        _payments.Where(payment => payment.CreatedAt.Month == DateTime.Now.Month).ToArray();

    public IReadOnlyCollection<Payment> FromLastMonth =>
        _payments.Where(payment => payment.CreatedAt.Month == DateTime.Now.Month - 1).ToArray();

    public IReadOnlyCollection<Payment> ForPayer(Person person) =>
        _payments.Where(payment => payment.Payers.Contains(person)).ToArray();
    
    
}