using MediatR;
using PaymentsSharing.Persons;

namespace PaymentsSharing.Payments;

internal record AddPayment(
    IEnumerable<Person> Payers,
    IEnumerable<Person> Consumers,
    uint Amount,
    uint? AmountForMeat = null,
    string Description = "") : IRequest;