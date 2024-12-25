using AdventOfCode2024.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day21;
public abstract class Keypad
{
    protected Dictionary<string, long> dictionary = new();
    protected readonly Dictionary<string, List<string>> shortestPaths;
    protected readonly Keypad? next;
    protected char StartChar;

    protected Keypad(Dictionary<string, List<string>> shortestPaths, Keypad? next = null)
    {
        this.shortestPaths = shortestPaths;
        this.next = next;
    }

    public abstract long GetShortestPathLength(string input);

    public abstract string GetShortestPath(string input);

    protected long GetMinLength(string substring)
    {
        if (dictionary.TryGetValue(substring, out var minLength))
        {
            return minLength;
        }

        minLength = long.MaxValue;
        foreach (var possiblePath in shortestPaths[substring])
        {
            var possibleMinLength = next!.GetShortestPathLength(possiblePath);
            if (possibleMinLength < minLength)
            {
                minLength = possibleMinLength;
            }
        }

        dictionary[substring] = minLength;
        return minLength;
    }
}
