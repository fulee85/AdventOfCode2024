using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day21;

public class MostOuterPad : Pad
{
    public MostOuterPad()
    {
    }

    public override string GetShortestPath(string input, bool withCache) => input + 'p';
    public override long GetShortestPathLength(string input) => input.Length + 1;
}
