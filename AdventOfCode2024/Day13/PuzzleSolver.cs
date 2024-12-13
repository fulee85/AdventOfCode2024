using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day13;

public class PuzzleSolver : PuzzleSolverBase
{
    public PuzzleSolver(IInput input) : base(input)
    {
    }

    public override string GetFirstSolution()
    {
        return GetSolution(false);
    }
    public override string GetSecondSolution()
    {
        return GetSolution(true);
    }

    private string GetSolution(bool withCorrection)
    {
        var inputLines = input.ToArray();
        var equations = new List<Equation>();
        var startIndex = 0;
        while (startIndex < inputLines.Length)
        {
            equations.Add(new Equation(new Span<string>(inputLines, startIndex, 3), withCorrection: withCorrection));
            startIndex += 4;
        }

        long tokensCount = 0;
        foreach (var result in equations.Select(e => e.GetResult()))
        {
            if (result.Success)
            {
                tokensCount += result.A * 3 + result.B;
            }
        }

        return tokensCount.ToString();
    }
}
