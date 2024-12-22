using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day21;
public class DirectionalKeypad : Keypad
{
    private readonly Dictionary<string, long> cache = new();
    public DirectionalKeypad(Dictionary<string, List<string>> shortestPaths, Keypad? next = null) : base(shortestPaths, next)
    {
        StartChar = 'p';
    }

    public override long GetShortestPathLength(string input)
    {
        if (cache.TryGetValue(input, out var length))
        {
            return length;
        }

        length = 0;
        var extendedInput = StartChar + input + StartChar;
        for (int i = 0; i < extendedInput.Length - 1; i++)
        {
            var substring = extendedInput.Substring(i, 2);
            if (substring[0] == substring[1])
            {
                continue;
            }

            length += GetMinLength(substring);
        }

        cache[input] = length;
        return length;
    }
}
