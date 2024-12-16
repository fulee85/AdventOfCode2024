using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day16;

public class Edge
{
    public required Node ArrivingNode { get; init; }

    public required Directions ArrivingDirection { get; init; }

    public required int PointValue { get; init; }
    public required int EffectiveLength { get; init; }
}
