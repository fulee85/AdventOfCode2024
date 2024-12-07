using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day6;

public class PuzzleSolver : PuzzleSolverBase
{
    private readonly Map map;
    public PuzzleSolver(IInput input) : base(input)
    {
        map = new Map(input);
    }

    public override string GetFirstSolution()
    {
        return map.GetVisitedPositionsCount().ToString();
    }
    public override string GetSecondSolution()
    {
        return map.GetPossibleObstaclesCount().ToString();
    }
}
