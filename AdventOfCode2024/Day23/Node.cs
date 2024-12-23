using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day23;

public class Node
{
    public static Dictionary<string, HashSet<string>>? LANGraph;
    private readonly List<string> computerIds;
    private readonly List<string> commonNeighbors;

    public Node(List<string> computerIds, List<string> commonNeighbors)
    {
        this.computerIds = computerIds;
        this.commonNeighbors = commonNeighbors;
        Label = string.Join(',', computerIds.Order());
        Depth = computerIds.Count;
    }
    public int Depth { get; init; }

    public string Label { get; init; }

    public override int GetHashCode() => Label.GetHashCode();

    public override bool Equals(object? obj)
    {
        if (obj is Node other)
        {
            return other.Label == Label;
        }
        return false;
    }

    public IEnumerable<Node> GetNeighborNodes()
    {
        foreach (var neighbor in commonNeighbors)
        {
            yield return new Node(new List<string>(computerIds) { neighbor }, Intersect(commonNeighbors, LANGraph![neighbor]));
        }
    }

    public static List<string> Intersect(List<string> first, HashSet<string> second)
    {
        var result = new List<string>();
        foreach (var item in first)
        {
            if (second.Contains(item))
            {
                result.Add(item);
            }
        }
        return result;
    }
}
