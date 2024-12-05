namespace AdventOfCode2024.Day5;
internal class PageComparer : IComparer<int>
{
    Dictionary<int, HashSet<int>> keySmallerThanValuesDict = new();
    public PageComparer(List<Rule> rules)
    {
        foreach (Rule rule in rules)
        {
            if (!keySmallerThanValuesDict.ContainsKey(rule.Smaller))
            {
                keySmallerThanValuesDict[rule.Smaller] = new();
            }

            if (!keySmallerThanValuesDict.ContainsKey(rule.BiggerNumber))
            {
                keySmallerThanValuesDict[rule.BiggerNumber] = new();
            }

            keySmallerThanValuesDict[rule.Smaller].Add(rule.BiggerNumber);
        }
    }

    public int Compare(int x, int y)
    {
        if (keySmallerThanValuesDict[x].Contains(y)) //x < y
        {
            return -1;
        }
        if (keySmallerThanValuesDict[y].Contains(x)) // x > y)
        {
            return 1;
        }
        return 0;
    }
}
