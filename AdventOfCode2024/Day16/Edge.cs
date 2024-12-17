using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day16;

public class Edge
{
    public required Node ArrivingNode { get; init; }
    public required Node DepartureNode { get; init; }

    public required Directions ArrivingDirection { get; init; }
    public required Directions DepartureDirection { get; init; }

    public required int PointValue { get; init; }
    public required int EffectiveLength { get; init; }

    public override int GetHashCode() => ArrivingNode.GetHashCode() + DepartureNode.GetHashCode();

    public override bool Equals(object? obj)
    {
        if (obj is not null && obj is Edge otherEdge)
        {
            return otherEdge.DepartureNode == this.DepartureNode && otherEdge.ArrivingNode == this.ArrivingNode ||
                otherEdge.DepartureNode == this.ArrivingNode && otherEdge.ArrivingNode == this.DepartureNode;
        }
        return false;
    }
}
