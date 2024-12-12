namespace AdventOfCode2024.Common;

public record Position(int Row, int Column)
{
    internal Position GetPositionInDirection(Directions direction) => direction switch
    {
        Directions.Up => this with { Row = this.Row - 1 },
        Directions.Down => this with { Row = this.Row + 1 },
        Directions.Left => this with { Column = this.Column - 1 },
        Directions.Right => this with { Column = this.Column + 1 },
        _ => throw new NotImplementedException(),
    };

    public static Position operator +(Position a, Position b) => new Position(a.Row + b.Row, a.Column + b.Column);
    public static Position operator -(Position a, Position b) => new Position(a.Row - b.Row, a.Column - b.Column);
    public static Position operator *(int c, Position b) => new Position(c * b.Row, c * b.Column);

    internal IEnumerable<Position> GetNeighbourPositions()
    {
        yield return this with { Row = this.Row - 1 };
        yield return this with { Row = this.Row + 1 };
        yield return this with { Column = this.Column - 1 };
        yield return this with { Column = this.Column + 1 };
    }
}
