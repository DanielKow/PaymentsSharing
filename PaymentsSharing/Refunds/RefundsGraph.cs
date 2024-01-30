namespace PaymentsSharing.Refunds;

internal class RefundsGraph
{
    private readonly Dictionary<string, Dictionary<string, decimal>> _graph = [];

    public void AddNode(string name)
    {
        _graph[name] = [];
    }
    
    public void AddEdge(string from, string to, decimal amount)
    {
        if (!_graph.TryGetValue(from, out Dictionary<string, decimal>? edges) || !_graph.ContainsKey(to)) return;
        
        if (!edges.TryAdd(to, amount))
        {
            edges[to] += amount;
        }
    }
    
    public void RemoveEdge(string from, string to)
    {
        if (!_graph.TryGetValue(from, out Dictionary<string, decimal>? edges) || !_graph.ContainsKey(to)) return;
        
        edges.Remove(to);
    }

    public void MergeBidirectionalEdges()
    {

    }
}