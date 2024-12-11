namespace AdventOfCode2024.Day11;

internal class RuleWhenZero : RuleBase
{
    public override void Apply(Stone stone)
    {
        if (stone.Number == 0)
        {
            stone.Number = 1;
        }
        else
        {
            base.Apply(stone);
        }
    }
}
