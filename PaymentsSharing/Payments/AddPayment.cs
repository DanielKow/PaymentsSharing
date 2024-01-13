using MediatR;
using PaymentsSharing.Persons;

namespace PaymentsSharing.Payments;

internal record AddPayment(
    IReadOnlyCollection<Person> Payers,
    IReadOnlyCollection<Person> Consumers,
    decimal Amount,
    decimal? AmountForMeat = null,
    string Description = "") : IRequest;