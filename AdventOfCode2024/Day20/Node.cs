using AdventOfCode2024.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day20;

public class Node
{
    public Node(Position position)
    {
        Distance = int.MaxValue;
        IsInTheQueue = false;
        Position = position;
    }
    public Position Position { get; init; }
    public int Distance { get; set; }
    public bool IsInTheQueue { get; set; }

    public IEnumerable<Node> GetNeighbors(Dictionary<Position, Node> trackNodesDict)
    {
        foreach (var neighborPosition in Position.GetNeighborPositions())
        {
            if (trackNodesDict.TryGetValue(neighborPosition, out var neighbor))
            {
                yield return neighbor;
            }
        }
    }

    public override string ToString() => $"{Position}, {Distance}";
}
