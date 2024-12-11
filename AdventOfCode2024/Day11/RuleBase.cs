using System.Numerics;

namespace AdventOfCode2024.Day11;

internal abstract class RuleBase : IRule
{
    private IRule? nextRule;

    public virtual void Apply(Stone stone) => nextRule?.Apply(stone);

    public void SetNext(IRule next)
    {
        nextRule = next;
    }

    public static IRule CreateRuleOfChain(params IRule[] rules)
    {
        for (int i = 0; i < rules.Length - 1; i++)
        {
            rules[i].SetNext(rules[i + 1]);
        }

        return rules[0];
    }
}
