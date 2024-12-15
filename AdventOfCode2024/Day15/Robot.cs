using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day15;

public class Robot : MoveableItem
{
    public Robot(Position position) : base(position, '@')
    {
    }

    public override string ToString() => base.ToString();
}
