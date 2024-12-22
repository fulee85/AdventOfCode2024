using AdventOfCode2024.Common;
using System.Text;
using Edge = (char Label, char directionChar);

namespace AdventOfCode2024.Day21;

public static class DijkstraAlgorithm
{
    public static Dictionary<string, List<string>> CreatePathsDictionary(SafeMatrix<char> safeMatrix)
    {
        var allPaths = new Dictionary<string, List<string>>();
        var allpositions = safeMatrix
            .EnumerateAllPositions()
            .Where(p => safeMatrix.GetValue(p) != '#');
        var edges = GetEdges(allpositions, safeMatrix);
        var chars = allpositions.Select(safeMatrix.GetValue);

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
                    if (alt < distances[neighbor.Label])
                    {
                        distances[neighbor.Label] = alt;
                        previous[neighbor.Label] = [minChar];
                    }
                    else if (alt == distances[neighbor.Label])
                    {
                        previous[neighbor.Label].Add(minChar);
                    }
                }
            }

            foreach (var endChar in chars.Where(ch => ch != startChar))
            {
                var fromToKey = string.Concat(startChar, endChar);
                List<string> shortestPaths = CalculatePathStrings(endChar, previous).ToList();
                allPaths[fromToKey] = shortestPaths.Select(p => Translate(p,edges)).ToList();
            }
        }

        return allPaths;
    }

    private static string Translate(string path, Dictionary<char, List<Edge>> edges)
    {
        StringBuilder stringBuilder = new();
        for (int i = 0; i < path.Length - 1; i++)
        {
            stringBuilder.Append(edges[path[i]].Find(e => e.Label == path[i + 1]).directionChar);
        }

        return stringBuilder.ToString();
    }

    private static Dictionary<char, List<Edge>> GetEdges(IEnumerable<Position> allpositions, SafeMatrix<char> safeMatrix)
    {
        var edges = new Dictionary<char, List<Edge>>();

        foreach (var position in allpositions)
        {
            edges[safeMatrix.GetValue(position)] = DirectionExtensions.EnumerateDirections()
                .Select(d => new Edge(safeMatrix.GetValue(position.GetPositionInDirection(d)), DirectionToChar(d)))
                .Where(ch => ch.Label != '#')
                .ToList();
        }

        return edges;
    }

    private static char DirectionToChar(Directions d) => d switch
    {
        Directions.Down => 'd',
        Directions.Up => 'u',
        Directions.Left => 'l',
        Directions.Right => 'r',
        _ => throw new NotImplementedException(),
    };


    private static IEnumerable<string> CalculatePathStrings(char endChar, Dictionary<char, List<char>> previous)
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
}
