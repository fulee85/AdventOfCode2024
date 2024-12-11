namespace AdventOfCode2024.Day11;

internal class RuleWhenNoneApply : RuleBase
{
    public override long Apply(long stoneNumber, int count)
    {
        if (count == 0)
        {
            return 1;
        }

        if (keyValuePairs.TryGetValue((stoneNumber, count), out var sum))
        {
            return sum;
        }

        sum = FirstRule?.Apply(stoneNumber * 2024, count - 1) ?? throw new Exception();
        keyValuePairs[(stoneNumber, count)] = sum;
        return sum;
    }
}
