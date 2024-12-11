using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day11;

public class PuzzleSolver : PuzzleSolverBase
{
    public PuzzleSolver(IInput input) : base(input)
    {
    }

    public override string GetFirstSolution()
    {
        Stone? previousStone = null;
        foreach (var number in input.GetRawInput().Split().Select(long.Parse))
        {
            var newStone = new Stone { Number = number, Previous = previousStone };
            if (previousStone != null)
            {
                previousStone.Next = newStone;
            }
            previousStone = newStone;
        }

        RuleWhenZero ruleWhenZero = new();
        RuleWhenHaveEvenDigits ruleWhenHaveEvenDigits = new();
        RuleWhenNoneApply ruleWhenNoneApply = new();

        IRule ruleOfChain = RuleBase.CreateRuleOfChain(ruleWhenZero, ruleWhenHaveEvenDigits, ruleWhenNoneApply);
        for (int blinking = 0; blinking < 25; blinking++)
        {
            var actStone = Stone.FirstStone;
            while (actStone != null)
            {
                ruleOfChain.Apply(actStone);
                actStone = actStone.Next;
            }
        }

        return Stone.StonesCreated.ToString();
    }
    public override string GetSecondSolution()
    {
        return "";
    }
}
