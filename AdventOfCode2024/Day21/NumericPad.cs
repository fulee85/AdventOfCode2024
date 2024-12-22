using AdventOfCode2024.Common;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2024.Day21;

public class NumericPad : Pad
{
    private readonly Dictionary<(char, char), char> numericPadEdgesMap = new()
        {
            { ('A', '0'), 'l' },
            { ('A', '3'), 'u' },
            { ('0', 'A'), 'r' },
            { ('0', '2'), 'u' },
            { ('1', '4'), 'u' },
            { ('1', '2'), 'r' },
            { ('2', '0'), 'd' },
            { ('2', '3'), 'r' },
            { ('2', '5'), 'u' },
            { ('2', '1'), 'l' },
            { ('3', 'A'), 'd' },
            { ('3', '2'), 'l' },
            { ('3', '6'), 'u' },
            { ('4', '1'), 'd' },
            { ('4', '5'), 'r' },
            { ('4', '7'), 'u' },
            { ('5', '8'), 'u' },
            { ('5', '6'), 'r' },
            { ('5', '2'), 'd' },
            { ('5', '4'), 'l' },
            { ('6', '9'), 'u' },
            { ('6', '5'), 'l' },
            { ('6', '3'), 'd' },
            { ('7', '8'), 'r' },
            { ('7', '4'), 'd' },
            { ('8', '9'), 'r' },
            { ('8', '5'), 'd' },
            { ('8', '7'), 'l' },
            { ('9', '6'), 'd' },
            { ('9', '8'), 'l' },
        };

    private readonly Dictionary<char, char[]> numericPadEdges = new()
    {
        {'A', ['0','3']},
        {'0', ['A','2']},
        {'1', ['2','4']},
        {'2', ['0','1','3','5']},
        {'3', ['A','2','6']},
        {'4', ['1','5','7']},
        {'5', ['2','4','6','8']},
        {'6', ['3','5','9']},
        {'7', ['4','8']},
        {'8', ['5','7','9']},
        {'9', ['6','8']}
    };

    private readonly List<char> numericPadChars = ['A', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];

    public NumericPad(Pad pad): base(pad)
    {
        StartChar = 'A';
        base.chars = numericPadChars;
        base.edgesMap = numericPadEdgesMap;
        base.edges = numericPadEdges;
        PopulateDictionary();
    }

    public void AddNewNextPad()
    {
        var newPad = new DirectionalPad(this.next);
        next = newPad;
        RecalculateShortestPaths();
    }

    private void RecalculateShortestPaths()
    {
        foreach (var startChar in chars)
        {
            foreach (var endChar in chars.Where(ch => ch != startChar))
            {
                var fromToKey = string.Concat(startChar, endChar);
                List<string> shortestPaths = allPaths[fromToKey];
                dictionary[fromToKey] = GetBestShortestPath(shortestPaths);
            }
        }
    }

    public override string GetShortestPath(string input, bool withCache = true)
    {
        StringBuilder stringBuilder = new StringBuilder();
        var extendedInput = StartChar + input;
        for (int i = 0; i < extendedInput.Length - 1; i++)
        {
            stringBuilder.Append(next.GetShortestPath(dictionary[extendedInput.Substring(i, 2)], withCache));
        }

        return stringBuilder.ToString();
    }

    public override long GetShortestPathLength(string input) 
    {
        long length = 0;
        var extendedInput = StartChar + input;
        for (int i = 0; i < extendedInput.Length - 1; i++)
        {
            length += next.GetShortestPathLength(dictionary[extendedInput.Substring(i, 2)]);
        }

        return length;
    } 
}
