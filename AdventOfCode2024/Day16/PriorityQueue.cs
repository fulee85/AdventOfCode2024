using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day16;

public class Queue
{
    private readonly HashSet<(Directions, Node)> values = new();
    public void Enqueue(Directions direction, Node node) => values.Add((direction, node));

    public (Directions, Node) GetMinimumVertice(Distances distances)
    {
        Directions minDirection = Directions.Down;
        Node minNode = null!;
        var minDist = int.MaxValue;
        foreach (var value in values)
        {
            var dist = distances.GetDistance(value.Item1, value.Item2);
            if (dist < minDist)
            {
                minDirection = value.Item1;
                minNode = value.Item2;
                minDist = dist;
            }
        }

        return (minDirection, minNode);
    }

    public bool IsNotEmpty() => values.Count > 0;
    public void Remove((Directions direction, Node currentNode) value) =>
        values.Remove(value);
}
