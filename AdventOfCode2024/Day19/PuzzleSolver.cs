using AdventOfCode2024.Common;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day19;

public class PuzzleSolver : PuzzleSolverBase
{
    private readonly List<string> availableTowels;
    private readonly List<string> desiredDesigns;

    public PuzzleSolver(IInput input) : base(input)
    {
        availableTowels = input.First().Split(", ").ToList();
        desiredDesigns = input.Skip(2).ToList();
    }

    public override string GetFirstSolution()
    {
        var designChecker = new DesignChecker(availableTowels);
        return desiredDesigns.Count(designChecker.IsDesignPossible).ToString();
    }
    public override string GetSecondSolution()
    {
        DesignVariationCounter designVariationCounter = new(availableTowels);

        return desiredDesigns.Sum(designVariationCounter.PossiblArrangementCount).ToString();
    }
}
