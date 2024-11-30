namespace AdventOfCode2024.Common;

public abstract class PuzzleSolverBase
{
    protected readonly IInput input;
    public PuzzleSolverBase(IInput input)
    {
        this.input = input;
    }

    public abstract object GetFirstSolution();
    public abstract object GetSecondSolution();
}
