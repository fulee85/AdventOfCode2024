using System.Numerics;

namespace AdventOfCode2024.Day11;

internal class RuleWhenHaveEvenDigits : RuleBase
{
    public override long Apply(long stoneNumber, int count)
    {
        if (count == 0)
        {
            return 1;
        }

        if (keyValuePairs.TryGetValue((stoneNumber,count), out var sum))
        {
            return sum;
        }

        var stoneNumberAsString = stoneNumber.ToString();
        if (stoneNumberAsString.Length % 2 == 0)
        {
            var firstHalf = long.Parse(stoneNumberAsString[..(stoneNumberAsString.Length / 2)]);
            sum = FirstRule?.Apply(firstHalf, count - 1) ?? throw new Exception();
            var secondHalf = long.Parse(stoneNumberAsString[(stoneNumberAsString.Length / 2)..]);
            sum += FirstRule.Apply(secondHalf, count - 1);
            keyValuePairs[(stoneNumber, count)] = sum;
            return sum;
        }
        else
        {
            return nextRule?.Apply(stoneNumber, count) ?? throw new Exception();
        }
    }
}
