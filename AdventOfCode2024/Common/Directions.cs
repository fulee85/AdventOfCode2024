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

    public static Directions GetDirectionAfterTurnLeft(this Directions direction) => direction switch
    {
        Directions.Down => Directions.Right,
        Directions.Left => Directions.Down,
        Directions.Right => Directions.Up,
        Directions.Up => Directions.Left,
        _ => throw new NotImplementedException()
    };

    public static Directions GetDirectionAfterTurnRight(this Directions direction) => direction switch
    {
        Directions.Up => Directions.Right,
        Directions.Right => Directions.Down,
        Directions.Down => Directions.Left,
        Directions.Left => Directions.Up,
        _ => throw new NotImplementedException()
    };

    public static Directions GetInvertDirection(this Directions direction) => direction switch
    {
        Directions.Up => Directions.Down,
        Directions.Down => Directions.Up,
        Directions.Right => Directions.Left,
        Directions.Left => Directions.Right,
        _ => throw new NotImplementedException()
    };
}


