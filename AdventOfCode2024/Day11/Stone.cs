using System.Numerics;

namespace AdventOfCode2024.Day11;

internal class Stone
{
    public static Stone? FirstStone;
    public static int StonesCreated = 0;

    public Stone()
    {
        FirstStone ??= this;
        StonesCreated++;
    }

    public required long Number { get; set; }
    public Stone? Previous { get; set; }
    public Stone? Next { get; set; }
}
