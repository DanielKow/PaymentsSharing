using System.Collections;
using PaymentsSharing.Collections;
using PaymentsSharing.Payments;
using PaymentsSharing.Persons;

namespace PaymentsSharing.Refunds;

internal class Refunds(Payments.Payments payments) : IEnumerable<Refund>
{
    private readonly List<Refund> _refunds = [];

    public void Recalculate()
    {
        _refunds.Clear();
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
                    refundsGraph.AddEdge(consumer.Name, payer, (decimal) (payment.AmountForMeat ?? 0) / payment.Consumers.Count(person => person.IsMeatEater));
                }
            }
        }
        
        refundsGraph.MergeBidirectionalEdges();
        
        foreach (string node in refundsGraph.Nodes)
        {
            IEnumerable<string> edgesToNode = refundsGraph.EdgesTo(node).Select(refund => refund.From);

            IEnumerable<Pair<string>> variations = edgesToNode.TwoElementsVariations();
            
            foreach ((string first, string second) in variations)
            {
                Refund? neighboursEdge = refundsGraph.Edge(first, second);
                
                if (neighboursEdge is null) continue;

                Refund firstToNode = refundsGraph.Edge(neighboursEdge.From, node)!;
                Refund secondToNode = refundsGraph.Edge(neighboursEdge.To, node)!;

                if (secondToNode <= neighboursEdge)
                {
                    firstToNode.Increase(secondToNode.Amount);
                    neighboursEdge.Decrease(secondToNode.Amount);
                    secondToNode.Reset();
                    continue;
                }

                firstToNode.Increase(neighboursEdge.Amount);
                secondToNode.Decrease(neighboursEdge.Amount);
                neighboursEdge.Reset();
            }
        }
        
        refundsGraph.RemoveZeroEdges();
        
        foreach (Refund refund in refundsGraph.Edges)
        {
            _refunds.Add(refund.WithIntegerAmount());
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