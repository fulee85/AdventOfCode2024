using System.Numerics;

namespace AdventOfCode2024.Day11;

internal class RuleWhenHaveEvenDigits : RuleBase
{
    public override void Apply(Stone stone)
    {
        var stoneNumberAsString = stone.Number.ToString();
        if (stoneNumberAsString.Length % 2 == 0)
        {
            var firstHalf = stoneNumberAsString[..(stoneNumberAsString.Length / 2)];
            var newStone = new Stone { Number = long.Parse(firstHalf), Previous = stone.Previous, Next = stone };
            if (stone.Previous == null)
            {
                Stone.FirstStone = newStone;
            }
            else
            {
                stone.Previous.Next = newStone;
            }

            var secondHalf = stoneNumberAsString[(stoneNumberAsString.Length / 2)..];
            stone.Number = long.Parse(secondHalf);
            stone.Previous = newStone;
        }
        else
        {
            base.Apply(stone);
        }
    }
}
