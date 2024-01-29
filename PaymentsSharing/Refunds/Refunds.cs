using System.Collections;
using MediatR;
using PaymentsSharing.Payments;
using PaymentsSharing.Persons;

namespace PaymentsSharing.Refunds;

internal class Refunds(Payments.Payments payments) : IEnumerable<Refund>
{
    private readonly List<Refund> _refunds = [];

    public void Recalculate()
    {
        var refunds = new Dictionary<InvolvedInRefund, decimal>();

        foreach (Payment payment in payments.All)
        {
            var numberOfConsumers = (uint) payment.Consumers.Count();
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

                refunds[involvedInRefund] += (uint)amountPerConsumer;
            }
        }
        
        _refunds.Clear();
        foreach (KeyValuePair<InvolvedInRefund, decimal> refund in refunds)
        {
            _refunds.Add(new Refund(refund.Key.From, refund.Key.To, (uint)refund.Value));
        }
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