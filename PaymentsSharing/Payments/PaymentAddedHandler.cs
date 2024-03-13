using MediatR;

namespace PaymentsSharing.Payments;

internal class PaymentAddedHandler(Payments payments) : INotificationHandler<PaymentAdded>
{
    public Task Handle(PaymentAdded @event, CancellationToken cancellationToken)
    { 
        payments.Add(new Payment(
            @event.CreatedAt,
            @event.Payers,
            @event.Consumers,
            @event.Amount,
            @event.AmountForMeat,
            @event.Description));
        
        return Task.CompletedTask;
    }
}