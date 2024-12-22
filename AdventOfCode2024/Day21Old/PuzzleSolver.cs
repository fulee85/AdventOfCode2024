using AdventOfCode2024.Common;
using System.Numerics;

namespace AdventOfCode2024.Day21Old;

public class PuzzleSolver : PuzzleSolverBase
{
    private KeypadConundrum keypadConundrum;

    public PuzzleSolver(IInput input) : base(input)
    {
        keypadConundrum = new KeypadConundrum();
    }

    public override string GetFirstSolution()
    {
        List<BigInteger> lengths = new();
        for (int i = 0; i < 2; i++)
        {
            lengths.Add(GetSum());
            keypadConundrum.AddExtraRobot();
        }
        return GetSum().ToString();
    }
    public override string GetSecondSolution()
    {
        List<BigInteger> lengths = new();
        for (int i = 0; i < 25; i++)
        {
            lengths.Add(GetSum());
            keypadConundrum.AddExtraRobot();
        }
        return GetSum().ToString();
    }

    private BigInteger GetSum()
    {
        BigInteger sum = 0L;
        foreach (var line in input)
        {
            var numValue = BigInteger.Parse(line.TrimStart('0')[..^1]);
            var shortestSequence = keypadConundrum.GetShortestSequenceLength(line);
            sum += numValue * shortestSequence;
        }

        return sum;
    }
}
