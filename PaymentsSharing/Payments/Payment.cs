using PaymentsSharing.Persons;

namespace PaymentsSharing.Payments;

internal readonly record struct Payment(
    DateTime CreatedAt,
    IEnumerable<Person> Payers,
    IEnumerable<Person> Consumers,
    uint Amount,
    uint? AmountForMeat = null,
    string Description = "");