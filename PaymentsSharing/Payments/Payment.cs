using PaymentsSharing.Persons;

namespace PaymentsSharing.Payments;

internal readonly record struct Payment(
    DateTime CreatedAt,
    IReadOnlyCollection<Person> Payers,
    IReadOnlyCollection<Person> Consumers,
    uint Amount,
    uint? AmountForMeat = null,
    string Description = "");