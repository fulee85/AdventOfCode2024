using System.Text;

namespace AdventOfCode2024.Common;
internal class SafeMatrix<T>
{
    private readonly List<List<T>> values;
    private readonly Func<T> defaultFunc;

    public SafeMatrix(List<List<T>> values)
    {
        this.values = values;
        defaultFunc = () => default;
    }

    public SafeMatrix(List<List<T>> values, Func<T> defaultFunc)
    {
        this.values = values;
        this.defaultFunc = defaultFunc;
    }

    public SafeMatrix(List<List<T>> values, T defaultValue)
    {
        this.values = values;
        defaultFunc = () => defaultValue;
    }

    public IEnumerable<T> GetNeighbours(int row, int column)
    {
        yield return this[row + 1, column];
        yield return this[row, column + 1];
        yield return this[row - 1, column];
        yield return this[row, column - 1];
    }

    public T GetValue(Position position) => this[position.Row, position.Column];

    public T this[int x, int y]
    {
        get
        {
            if (x < 0 || x >= values.Count)
                return defaultFunc();
            if (y < 0 || y >= values[x].Count)
                return defaultFunc();
            return values[x][y];
        }
        set
        {
            values[x][y] = value;
        }
    }

    public List<T> GetRow(int i) => values[i];
    public List<T> GetColumn(int i) => values.Select(l => l[i]).ToList();

    public int RowCount => values.Count;
    public int ColumnCount => RowCount > 0 ? values[0].Count : 0;

    public IEnumerable<IEnumerable<T>> GetColumns()
    {
        for (int i = 0; i < ColumnCount; i++)
        {
            yield return GetColumn(i);
        }
    }

    public IEnumerable<IEnumerable<T>> GetRows() => values;

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColumnCount; j++)
            {
                stringBuilder.Append(values[i][j]?.ToString()?.PadLeft(4));
            }
            stringBuilder.Append(Environment.NewLine);
        }

        return stringBuilder.ToString();
    }
}
