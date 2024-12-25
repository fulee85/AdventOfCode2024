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
        shortestPaths["pp"] = [""];
        shortestPaths["uu"] = [""];
        shortestPaths["rr"] = [""];
        shortestPaths["dd"] = [""];
        shortestPaths["ll"] = [""];
    }

    private readonly Dictionary<string, string> stringCache = new();
    public override string GetShortestPath(string input)
    {
        if (stringCache.TryGetValue(input, out var result))
        {
            return result;
        }

        StringBuilder stringBuilder = new StringBuilder();
        var extendedInput = StartChar + input + StartChar;
        for (int i = 0; i < extendedInput.Length - 1; i++)
        {
            var minLength = int.MaxValue;
            string minPathString = "";
            foreach (var item in shortestPaths[extendedInput.Substring(i, 2)])
            {
                var actString = next.GetShortestPath(item);
                if (actString.Length < minLength)
                {
                    minLength = actString.Length;
                    minPathString = actString;
                }
            }
            stringBuilder.Append(minPathString);
        }
        result = stringBuilder.ToString();

       
        stringCache[input] = result;
        return result;
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
                length += 1;
            }
            else
            {
                length += GetMinLength(substring);
            }

        }

        cache[input] = length;
        return length;
    }
}
