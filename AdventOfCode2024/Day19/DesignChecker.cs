namespace AdventOfCode2024.Day19;

public class DesignChecker
{
    private readonly List<string> availableTowels;
    private readonly Dictionary<string, bool> cache = new Dictionary<string, bool>();

    public DesignChecker(List<string> availableTowels)
    {
        this.availableTowels = availableTowels;
        cache[""] = true;
    }

    public bool IsDesignPossible(string design)
    {
        if (cache.TryGetValue(design, out var result))
        {
            return result;
        }

        bool isDesignPossible = false;
        foreach (var towelDesign in availableTowels)
        {
            if (design.StartsWith(towelDesign))
            {
                isDesignPossible = IsDesignPossible(design.Substring(towelDesign.Length));
            }

            if (isDesignPossible)
            {
                break;
            }
        }

        cache[design] = isDesignPossible;
        return isDesignPossible;
    }
}
