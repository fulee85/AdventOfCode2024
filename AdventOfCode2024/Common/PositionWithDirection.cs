namespace AdventOfCode2024.Common;

public record PositionWithDirection(int Row, int Column, Directions Direction) : Position(Row, Column)
{
    public PositionWithDirection GetNextPosition() => Direction switch
    {
        Directions.Up => this with { Row = this.Row - 1 },
        Directions.Down => this with { Row = this.Row + 1 },
        Directions.Left => this with { Column = this.Column - 1 },
        Directions.Right => this with { Column = this.Column + 1 },
        _ => throw new NotImplementedException(),
    };
}
