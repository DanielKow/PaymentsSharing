using System.Collections;
using PaymentsSharing.Collections;
using PaymentsSharing.Payments;
using PaymentsSharing.Persons;
using PaymentsSharing.Time;

namespace PaymentsSharing.Refunds;

internal class Refunds(Payments.Payments payments)
{
    public IEnumerable<Refund> FromMonth(MonthAndYear monthAndYear)
    {
        List<Refund> refunds = [];
        var refundsGraph = new RefundsGraph();
        
        foreach (Payment payment in payments.FromMonth(monthAndYear))
        {
            string payer = string.Join('+', payment.Payers.Select(payer => payer.Name).OrderByDescending(name => name));
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
            refunds.Add(refund.WithIntegerAmount());
        }

        return refunds;
    }
}