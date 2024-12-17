using AdventOfCode2024.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day16;
public class VertexEdge
{
    public Vertex From { get; set; }
    public Vertex To { get; set; }

    public required int PointValue { get; init; }
    public required int EffectiveLength { get; init; }

    public override string ToString()
    {
        return $"From: {From} To:{To} Len: {EffectiveLength}";
    }

    public override int GetHashCode()
    {
        return From.Node.GetHashCode() + To.Node.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj is VertexEdge other)
        {
            return other.From.Node == From.Node && other.To.Node == To.Node &&
                other.To.Distance == To.Distance;
        }
        return false;
    }
}
