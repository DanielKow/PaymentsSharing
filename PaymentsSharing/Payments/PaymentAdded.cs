using MediatR;
using PaymentsSharing.Persons;

namespace PaymentsSharing.Payments;

internal record PaymentAdded(
    DateTime CreatedAt,
    IEnumerable<Person> Payers,
    IEnumerable<Person> Consumers,
    uint Amount,
    uint? AmountForMeat = null,
    string Description = "") : INotification;