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
    public int GetDistance(Node node)
    {
        var distance = int.MaxValue;
        foreach (var direction in DirectionExtensions.EnumerateDirections())
        {
            if (distances.TryGetValue((direction,node), out var dist))
            {
                distance = Math.Min(dist, distance);
            }
        }
        return distance;
    }

    public IEnumerable<Edge> GetPreviousEdges(Node node)
    {
        var distance = GetDistance(node);
        if (distance == 0)
        {
            return Enumerable.Empty<Edge>();
        }
        return distances
            .Where(d => d.Value == distance && d.Key.Item2 == node)
            .Select(x => node.Edges[x.Key.Item1.GetInvertDirection()]);
    }
}
