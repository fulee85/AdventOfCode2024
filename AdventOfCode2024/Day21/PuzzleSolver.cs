using AdventOfCode2024.Common;
using AdventOfCode2024.Day21.Test;

namespace AdventOfCode2024.Day21;

public class PuzzleSolver : PuzzleSolverBase
{
    private KeypadConundrum keypadConundrum;

    public PuzzleSolver(IInput input) : base(input)
    {
        keypadConundrum = new KeypadConundrum();
    }

    public override string GetFirstSolution()
    {
        return GetSumWithExtraPads().ToString(); 

    }
    public override string GetSecondSolution()
    {
        return GetSumWithExtraPads().ToString();
    }

    private long GetSumWithExtraPads(int extraPadCount = 0)
    {
        var sum = 0L;
        foreach (var line in input)
        {
            var numValue = int.Parse(line.TrimStart('0')[..^1]);
            string shortestSequence = keypadConundrum.GetShortestSequence(line);
            sum += numValue * shortestSequence.Length;
        }

        return sum;
    }
}
