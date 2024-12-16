namespace AdventOfCode2024.Day16;

public class PreviousEdges
{
    private readonly Dictionary<Node, HashSet<Edge>> previousEdges = new Dictionary<Node, HashSet<Edge>>();

    public void SetPreviousAsEqual(Node node, Edge edge) => previousEdges[node].Add(edge);

    public void SetPrevious(Node node, Edge edge) => previousEdges[node] = new HashSet<Edge> { edge };

    public IEnumerable<Edge> GetEdges(Node node) => previousEdges[node];
}
