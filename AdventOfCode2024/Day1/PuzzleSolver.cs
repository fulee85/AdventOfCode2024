using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day1;

public class PuzzleSolver : PuzzleSolverBase
{
    private readonly Lists lists = new();
    public PuzzleSolver(IInput input) : base(input)
    {
        foreach (var line in input)
        {
            var splittedLine = line.Split([" ", Environment.NewLine], StringSplitOptions.RemoveEmptyEntries);
            lists.AddToLists(int.Parse(splittedLine[0]), int.Parse(splittedLine[1]));
        }
    }

    public override string GetFirstSolution()
    {
        return lists.GetDifferences();
    }

    public override string GetSecondSolution()
    {
        return lists.GetSimilarityScore();
    }
}
