using MediatR;

namespace PaymentsSharing.Payments;

internal record PaymentAdded(
    DateTime CreatedAt,
    uint Amount,
    uint? AmountForMeat = null,
    string Description = "") : INotification;