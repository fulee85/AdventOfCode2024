namespace AdventOfCode2024.Day11;

internal class RuleWhenZero : RuleBase
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

        if (stoneNumber == 0)
        {
            sum = FirstRule?.Apply(1, count - 1) ?? throw new Exception();
            keyValuePairs[(stoneNumber, count)] = sum;
            return sum;
        }
        else
        {
            return nextRule?.Apply(stoneNumber, count) ?? throw new Exception();
        }
    }
}
