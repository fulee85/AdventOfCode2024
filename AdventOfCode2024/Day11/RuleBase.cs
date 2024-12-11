using System.Numerics;

namespace AdventOfCode2024.Day11;

internal abstract class RuleBase : IRule
{
    protected IRule? nextRule;
    public static IRule? FirstRule;

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
        FirstRule = rules[0];
        return rules[0];
    }

    protected readonly Dictionary<(long, int), long> keyValuePairs = [];
    public abstract long Apply(long number, int count);
}
