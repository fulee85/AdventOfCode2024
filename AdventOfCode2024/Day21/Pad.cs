using AdventOfCode2024.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day21;

public abstract class Pad
{
    protected char StartChar;
    protected Dictionary<string, string> dictionary = new Dictionary<string, string>();
    protected Pad next;
    protected List<char> chars;
    protected Dictionary<char, char[]> edges;
    protected Dictionary<(char, char), char> edgesMap;

    protected Pad()
    {
    }

    protected Pad(Pad next)
    {
        this.next = next;
    }

    public abstract string GetShortestPath(string input, bool withCache = true);

    public abstract long GetShortestPathLength(string line);

    protected void PopulateDictionary()
    {
        foreach (var startChar in chars)
        {
            var distances = chars.ToDictionary(c => c, c => int.MaxValue);
            var previous = chars.ToDictionary(c => c, c => new List<char>());
            LinkedList<char> queue = new LinkedList<char>(chars);
            distances[startChar] = 0;

            while (queue.Count > 0)
            {
                var minChar = queue.GetAndRemoveMin(ch => distances[ch]);

                foreach (var neighbor in edges[minChar])
                {
                    var alt = distances[minChar] + 1;
                    if (alt < distances[neighbor])
                    {
                        distances[neighbor] = alt;
                        previous[neighbor] = [minChar];
                    }
                    else if (alt == distances[neighbor])
                    {
                        previous[neighbor].Add(minChar);
                    }
                }
            }

            Dictionary<char, List<string>> shortestPathsTo = new Dictionary<char, List<string>>();
            foreach (var endChar in chars.Where(ch => ch != startChar))
            {
                List<string> shortestPaths = CalculatePathStrings(endChar, previous).ToList();
                dictionary[string.Concat(startChar, endChar)] = GetBestShortestPath(shortestPaths);
            }
        }
    }

    private IEnumerable<string> CalculatePathStrings(char endChar, Dictionary<char, List<char>> previous)
    {
        if (previous[endChar].Count == 0)
        {
            yield return endChar.ToString();
        }
        else
        {
            foreach (var previousChar in previous[endChar])
            {
                foreach (var pathString in CalculatePathStrings(previousChar, previous))
                {
                    yield return pathString + endChar;
                }
            }
        }
    }

    private string GetBestShortestPath(List<string> shortestPaths)
    {
        return shortestPaths.Select(TranslateToMoves).MinBy(p => next.GetShortestPath(p, withCache: false).Length)!;
    }

    private string TranslateToMoves(string path)
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < path.Length - 1; i++)
        {
            stringBuilder.Append(edgesMap[(path[i], path[i + 1])]);
        }

        return stringBuilder.ToString();
    }
}