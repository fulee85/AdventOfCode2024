using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day12;

public class PuzzleSolver : PuzzleSolverBase
{
    private Garden garden;

    public PuzzleSolver(IInput input) : base(input)
    {
        garden = new Garden(input);
    }

    public override string GetFirstSolution()
    {
        return garden.GardenRegions.Sum(r => r.GetFencingCost()).ToString();
    }
    public override string GetSecondSolution()
    {
        return garden.GardenRegions.Sum(r => r.GetDiscountedFencingCost()).ToString();
    }
}
