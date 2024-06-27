using MediatR;

namespace PaymentsSharing.Payments;

internal record AddPayment(
    DateTime Date,
    uint Amount,
    uint? AmountForMeat = null,
    string Description = "") : IRequest;