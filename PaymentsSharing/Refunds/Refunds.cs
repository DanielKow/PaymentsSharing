using System.Collections;
using MediatR;
using PaymentsSharing.Payments;
using PaymentsSharing.Persons;

namespace PaymentsSharing.Refunds;

internal class Refunds(Payments.Payments payments) : INotificationHandler<PaymentAdded>, IEnumerable<Refund>
{
    private readonly List<Refund> _refunds =
    [
        new Refund("Andrzej", "Mikołaj", 12),
        new Refund("Andrzej", "Natalia", 22),
        new Refund("Andrzej", "Mikołaj+Natalia", 50),
        new Refund("Mikołaj", "Natalia", 2)
    ];

    public Task Handle(PaymentAdded paymentAdded, CancellationToken cancellationToken)
    {
        var refunds = new Dictionary<InvolvedInRefund, decimal>();

        foreach (Payment payment in payments.All)
        {
            int numberOfConsumers = payment.Consumers.Count;
            decimal amountPerConsumer = (decimal)payment.Amount / numberOfConsumers;

            foreach (Person consumer in payment.Consumers)
            {
                if (consumer.IsMeatEater)
                {
                    amountPerConsumer += (decimal)(payment.AmountForMeat ?? 0) / numberOfConsumers;
                }

                if (payment.Payers.Contains(consumer))
                {
                    continue;
                }

                string payers = string.Join('+',
                    payment.Payers.OrderByDescending(payer => payer.Name).Select(payer => payer.Name));

                var involvedInRefund = new InvolvedInRefund(consumer.Name, payers);
                refunds.TryAdd(involvedInRefund, 0);

                refunds[involvedInRefund] += amountPerConsumer;
            }
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