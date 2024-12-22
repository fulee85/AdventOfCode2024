using AdventOfCode2024.Common;
using System.Numerics;

namespace AdventOfCode2024.Day21;

public class PuzzleSolver : PuzzleSolverBase
{

    public PuzzleSolver(IInput input) : base(input)
    {
    }

    public override string GetFirstSolution()
    {
        KeypadConundrumFactory keypadConundrumFactory = CreateKeypadConundrumFactory();
        var keypadConundrum = keypadConundrumFactory.Create(2);
        return GetSum(keypadConundrum).ToString();
    }
    public override string GetSecondSolution()
    {
        KeypadConundrumFactory keypadConundrumFactory = CreateKeypadConundrumFactory();
        var keypadConundrum = keypadConundrumFactory.Create(25);
        return GetSum(keypadConundrum).ToString();
    }

    private BigInteger GetSum(KeypadConundrum keypadConundrum)
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

    private KeypadConundrumFactory CreateKeypadConundrumFactory()
    {
        NumericKeypadFactory numericKeypadFactory = new NumericKeypadFactory(DijkstraAlgorithm.CreatePathsDictionary(KeypadConfigurations.NumericKeypad));
        DirectionalKeypadFactory directionalKeypadFactory = new DirectionalKeypadFactory(DijkstraAlgorithm.CreatePathsDictionary(KeypadConfigurations.DirectionalKeypad));
        return new KeypadConundrumFactory(directionalKeypadFactory, numericKeypadFactory);
    }
}
