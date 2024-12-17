using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day16;

public class Queue
{
    private readonly HashSet<Vertex> values = new();
    public void Enqueue(Vertex vertex) => values.Add(vertex);

    public Vertex GetMinimumVertex()
    {
        return values.MinBy(v => v.Distance)!;
    }

    public bool IsNotEmpty() => values.Count > 0;
    public void Remove(Vertex value) =>
        values.Remove(value);
}
