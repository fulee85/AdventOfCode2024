namespace AdventOfCode2024.Common;

public enum Directions
{
    Left,
    Right,
    Up,
    Down
}

public static class DirectionExtensions
{
    private static Directions[] DirectionsArray = [Directions.Up, Directions.Down, Directions.Right, Directions.Left];
    public static IEnumerable<Directions> EnumerateDirections() => DirectionsArray;
}
