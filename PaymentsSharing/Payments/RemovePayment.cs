using MediatR;

namespace PaymentsSharing.Payments;

internal record RemovePayment(Payment Payment) : IRequest;