using AdventOfCode2024.Common;
using System.Text;

namespace AdventOfCode2024.Day21;

public class DirectionalPad : Pad
{
    private static Dictionary<string, string>? staticDictionary;

    private static readonly Dictionary<(char, char), char> directionalPadEdgesMap = new()
        {
            { ('p', 'u'), 'l' },
            { ('p', 'r'), 'd' },
            { ('u', 'p'), 'r' },
            { ('u', 'd'), 'd' },
            { ('r', 'p'), 'u' },
            { ('r', 'd'), 'l' },
            { ('d', 'u'), 'u' },
            { ('d', 'r'), 'r' },
            { ('d', 'l'), 'l' },
            { ('l', 'd'), 'r' },
        };

    private static readonly Dictionary<char, char[]> directionalPadEdges = new()
    {
        {'p', ['u','r']},
        {'u', ['p','d']},
        {'r', ['p','d']},
        {'d', ['u','r','l']},
        {'l', ['d']}
    };

    private static readonly List<char> directionalPadChars = ['p', 'u', 'r', 'd', 'l'];

    public DirectionalPad(Pad pad): base(pad)
    {
        StartChar = 'p';
        base.chars = directionalPadChars;
        base.edgesMap = directionalPadEdgesMap;
        base.edges = directionalPadEdges;
        PopulateDictionary();
        dictionary["pp"] = "";
        dictionary["uu"] = "";
        dictionary["rr"] = "";
        dictionary["dd"] = "";
        dictionary["ll"] = "";
    }

    private readonly Dictionary<string, string> cache = new Dictionary<string, string>();

    public override string GetShortestPath(string input, bool withCache = true)
    {
        if (withCache && cache.TryGetValue(input, out var result))
        {
            return result;
        }

        StringBuilder stringBuilder = new StringBuilder();
        var extendedInput = StartChar + input + StartChar;
        for (int i = 0; i < extendedInput.Length - 1; i++)
        {
            stringBuilder.Append(next.GetShortestPath(dictionary[extendedInput.Substring(i, 2)], withCache));
        }
        result = stringBuilder.ToString();

        if (withCache)
        {
            cache[input] = result;
        }
        return result;
    }

    private readonly Dictionary<string, long> lenghtsCache = new Dictionary<string, long>();
    public override long GetShortestPathLength(string input)
    {
        if (lenghtsCache.TryGetValue(input, out var result))
        {
            return result;
        }

        result = 0L;
        var extendedInput = StartChar + input + StartChar;
        for (int i = 0; i < extendedInput.Length - 1; i++)
        {
            result += next.GetShortestPathLength(dictionary[extendedInput.Substring(i, 2)]);
        }

        lenghtsCache[input] = result;
        return result;
    }
}
