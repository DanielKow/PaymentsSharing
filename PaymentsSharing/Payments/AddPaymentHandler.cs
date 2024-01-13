using MediatR;

namespace PaymentsSharing.Payments;

internal class AddPaymentHandler : IRequestHandler<AddPayment>
{
    public Task Handle(AddPayment request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}