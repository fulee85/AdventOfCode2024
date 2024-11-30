using System.Text;

namespace AdventOfCode2024.Common
{
    public class InfiniteMatrix<T>
    {
        private readonly List<List<T>> values;

        public InfiniteMatrix(List<List<T>> values) => this.values = values;

        public T this[int x, int y] => GetValueAt(x, y);

        private T GetValueAt(int row, int column)
        {
            var r = row % RowCount;
            var c = column % ColumnCount;
            if (r < 0)
            {
                if (c < 0)
                {
                    return values[r + RowCount][c + ColumnCount];
                }
                else
                {
                    return values[r + RowCount][c];
                }
            }
            else
            {
                if (c < 0)
                {
                    return values[r][c + ColumnCount];
                }
                else
                {
                    return values[r][c];
                }
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
                    stringBuilder.Append(values[i][j]?.ToString());
                }
                stringBuilder.Append(Environment.NewLine);
            }

            return stringBuilder.ToString();
        }
    }
}
