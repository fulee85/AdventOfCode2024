using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day16;

public class Distances
{
    private readonly Dictionary<(Directions, Node), int> distances = new Dictionary<(Directions, Node), int>();

    public int GetDistance(Directions direction, Node node)
    {
        if (distances.TryGetValue((direction, node), out var distance))
        {
            return distance;
        }
        return int.MaxValue;
    }

    public void SetDistance(Directions direction, Node node, int distance) => distances[(direction, node)] = distance;
    public int GetDistance(Node endNode)
    {
        var distance = int.MaxValue;
        foreach (var direction in DirectionExtensions.EnumerateDirections())
        {
            if (distances.TryGetValue((direction,endNode), out var dist))
            {
                distance = Math.Min(dist, distance);
            }
        }
        return distance;
    }
}
