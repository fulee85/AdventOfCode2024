namespace AdventOfCode2024.Day11;

internal class RuleWhenNoneApply : RuleBase
{
    public override void Apply(Stone stone) => stone.Number *= 2024;
}
