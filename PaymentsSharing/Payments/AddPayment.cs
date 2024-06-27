using MediatR;

namespace PaymentsSharing.Payments;

internal record AddPayment(
    uint Amount,
    uint? AmountForMeat = null,
    string Description = "") : IRequest;