using AdventOfCode2024.Common;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2024.Day11;

public class PuzzleSolver : PuzzleSolverBase
{
    private IRule ruleOfChain;

    public PuzzleSolver(IInput input) : base(input)
    {
        RuleWhenZero ruleWhenZero = new();
        RuleWhenHaveEvenDigits ruleWhenHaveEvenDigits = new();
        RuleWhenNoneApply ruleWhenNoneApply = new();

        ruleOfChain = RuleBase.CreateRuleOfChain(ruleWhenZero, ruleWhenHaveEvenDigits, ruleWhenNoneApply);
    }

    public override string GetFirstSolution()
    {
        return GetSolution(25);
    }
    public override string GetSecondSolution()
    {
        return GetSolution(75);
    }

    private string GetSolution(int repeat)
    {
        var inputNumbers = input.GetRawInput().Split().Select(int.Parse);
        long sum = 0;
        foreach (var inputNumber in inputNumbers)
        {
            sum += ruleOfChain.Apply(inputNumber, repeat);
        }
        return sum.ToString();
    }
}
