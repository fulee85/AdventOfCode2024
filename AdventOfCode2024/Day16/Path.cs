using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day16;

public class Path
{
    public Directions Direction { get; init; }
    public Node Node { get; init; }
    public HashSet<Node> VisitedNodes { get; init; }
    public int Score { get; init; }

    public List<Edge> Edges { get; init; }

    public override string ToString() => Score.ToString();
}
