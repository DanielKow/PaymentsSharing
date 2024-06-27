using MediatR;

namespace PaymentsSharing.Payments;

internal record PaymentRemoved(Payment Payment) : INotification;