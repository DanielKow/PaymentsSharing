using MediatR;
using PaymentsSharing.Persons;

namespace PaymentsSharing.Payments;

internal record PaymentAdded(
    DateTime CreatedAt,
    IReadOnlyCollection<Person> Payers,
    IReadOnlyCollection<Person> Consumers,
    decimal Amount,
    decimal? AmountForMeat = null,
    string Description = "") : INotification;