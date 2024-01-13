using PaymentsSharing.Persons;

namespace PaymentsSharing.Payments;

internal readonly record struct Payment(
    DateTime CreatedAt,
    IReadOnlyCollection<Person> Payers,
    IReadOnlyCollection<Person> Consumers,
    decimal Amount,
    decimal? AmountForMeat = null,
    string Description = "");