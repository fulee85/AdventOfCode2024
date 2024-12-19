using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day19;

public class DesignVariationCounter
{
    private List<string> availableTowels;
    private readonly Dictionary<string, long> countCache = new();

    public DesignVariationCounter(List<string> availableTowels)
    {
        this.availableTowels = availableTowels;
        countCache[""] = 1;
    }

    public long PossiblArrangementCount(string design)
    {
        if (countCache.TryGetValue(design, out var count))
        {
            return count;
        }

        count = 0;
        foreach (var towel in availableTowels)
        {
            if (design.StartsWith(towel))
            {
                count += PossiblArrangementCount(design.Substring(towel.Length));
            }
        }

        countCache[design] = count;
        return count;
    }
}
