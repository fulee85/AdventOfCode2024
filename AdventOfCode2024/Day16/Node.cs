using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day16;

public class Node
{
    public required Position Position { get; init; }

    public Dictionary<Directions, Edge> Edges = [];

    public readonly Dictionary<Directions, int> DistancesFromInDirections = [];

    public int Distance => DistancesFromInDirections.Count > 0 ? DistancesFromInDirections.Values.Min() : int.MaxValue;
}
