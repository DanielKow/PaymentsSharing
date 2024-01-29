using MediatR;
using PaymentsSharing.Persons;

namespace PaymentsSharing.Payments;

internal record AddPayment(
    IReadOnlyCollection<Person> Payers,
    IReadOnlyCollection<Person> Consumers,
    uint Amount,
    uint? AmountForMeat = null,
    string Description = "") : IRequest;