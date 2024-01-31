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
        var refundsGraph = new RefundsGraph();
        
        foreach (Payment payment in payments.FromCurrentMonth)
        {
            string payer = string.Join('+', payment.Payers.Select(payer => payer.Name));
            foreach (Person consumer in payment.Consumers)
            {
                if (payment.Payers.Contains(consumer)) continue;
                
                refundsGraph.AddEdge(consumer.Name, payer, (decimal) payment.Amount / payment.Consumers.Count());
                
                if (payment.AmountForMeat is not null && consumer.IsMeatEater)
                {
                    refundsGraph.AddEdge(consumer.Name, payer, (decimal) (payment.AmountForMeat ?? 0) / payment.Consumers.Count());
                }
            }
        }
        
        refundsGraph.MergeBidirectionalEdges();
        
        _refunds.Clear();
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