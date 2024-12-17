using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day16;
public class Vertex
{
    public required Node Node { get; init; }

    public required Directions InDirection { get; init; }

    public List<VertexEdge> InEdges { get; } = new List<VertexEdge> { };

    public Dictionary<Directions, VertexEdge> OutEdges { get; } = new Dictionary<Directions, VertexEdge>();

    public List<VertexEdge> PreviousEdges { get; set; } = new List<VertexEdge>();

    public int Distance { get; set; } = int.MaxValue;

    public override string ToString()
    {
        return $"{Node} dist:{Distance}";
    }
}
