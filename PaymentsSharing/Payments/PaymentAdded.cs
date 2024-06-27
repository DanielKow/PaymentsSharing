using MediatR;

namespace PaymentsSharing.Payments;

internal record PaymentAdded(
    DateTime Date,
    uint Amount,
    uint? AmountForMeat = null,
    string Description = "") : INotification;