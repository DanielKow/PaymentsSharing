using System.Collections;
using MediatR;
using PaymentsSharing.Payments;

namespace PaymentsSharing.Refunds;

internal class Refunds(Payments.Payments payments) : INotificationHandler<PaymentAdded>, IEnumerable<Refund>
{
    private readonly List<Refund> _refunds = [
        new Refund("Andrzej", "Mikołaj", 12),
        new Refund("Andrzej", "Natalia", 22),
        new Refund("Andrzej", "Mikołaj+Natalia", 50),
        new Refund("Mikołaj", "Natalia", 2)
    ];
    
    public Task Handle(PaymentAdded paymentAdded, CancellationToken cancellationToken)
    {
        foreach (Payment payment in payments.All)
        {
            
        }

        return Task.CompletedTask;
    }

    public IEnumerator<Refund> GetEnumerator()
    {
        return _refunds.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}