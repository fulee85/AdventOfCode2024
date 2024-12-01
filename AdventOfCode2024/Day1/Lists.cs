namespace AdventOfCode2024.Day1;

internal class Lists
{
    private readonly List<int> leftList = [];
    private readonly List<int> rightList = [];

    internal void AddToLists(int left, int right)
    {
        leftList.Add(left);
        rightList.Add(right);
    }

    internal string GetDifferences()
    {
        leftList.Sort();
        rightList.Sort();
        int sum = 0;
        for (int i = 0; i < leftList.Count; i++)
        {
            sum += Math.Abs(leftList[i] - rightList[i]);
        }

        return sum.ToString();
    }

    internal string GetSimilarityScore()
    {
        long similarityScore = 0;
        for (int i = 0; i < leftList.Count; i++)
        {
            similarityScore += leftList[i] * rightList.Count(n => n == leftList[i]);
        }

        return similarityScore.ToString();
    }
}
