namespace PaymentsSharing.Refunds;

internal class RefundsGraph
{
    private readonly List<Refund> _graph = [];

    public IEnumerable<string> Nodes =>
        _graph.Select(edge => edge.From).Concat(_graph.Select(edge => edge.To)).Distinct();

    public IEnumerable<Refund> Edges => _graph;

    public IEnumerable<Refund> EdgesFrom(string from) => _graph.Where(edge => edge.From == from);

    public IEnumerable<Refund> EdgesTo(string to) => _graph.Where(edge => edge.To == to);

    public decimal Weight(string from, string to) => Edge(from, to)?.Amount ?? 0;
    
    public Refund? Edge(string from, string to) => _graph.FirstOrDefault(edge => edge.From == from && edge.To == to);

    public void AddEdge(string from, string to, decimal amount)
    {
        Refund? existingEdge = _graph.FirstOrDefault(edge => edge.From == from && edge.To == to);

        if (existingEdge is null)
        {
            _graph.Add(new Refund(from, to, amount));
            return;
        }

        _graph.Remove(existingEdge);
        _graph.Add(new Refund(from, to, existingEdge.Amount + amount));
    }

    public void RemoveEdge(string from, string to)
    {
        Refund? toRemove = _graph.FirstOrDefault(edge => edge.From == from && edge.To == to);
        if (toRemove is not null)
        {
            _graph.Remove(toRemove);
        }
    }

    public void Clear()
    {
        _graph.Clear();
    }

    public void MergeBidirectionalEdges()
    {
        List<Refund> merged = [];

        foreach (Refund edge in _graph)
        {
            Refund? oppositeEdge = _graph.FirstOrDefault(e => e.From == edge.To && e.To == edge.From);

            if (oppositeEdge is null)
            {
                merged.Add(edge);
                continue;
            }

            if (edge.Amount > oppositeEdge.Amount)
            {
                merged.Add(edge with { Amount = edge.Amount - oppositeEdge.Amount });
            }
        }

        Clear();
        _graph.AddRange(merged);
    }
}