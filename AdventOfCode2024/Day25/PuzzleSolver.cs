using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day25;

public class PuzzleSolver : PuzzleSolverBase
{
    public PuzzleSolver(IInput input) : base(input)
    {
    }

    public override string GetFirstSolution()
    {
        List<SafeMatrix<char>> locksAndkeys = new();
        var enumerator = input.GetEnumerator();
        bool notEndOfFile = enumerator.MoveNext();
        while (notEndOfFile)
        {
            List<List<char>> matrix = new();
            while (notEndOfFile && enumerator.Current != string.Empty)
            {
                matrix.Add(enumerator.Current.ToList());
                notEndOfFile = enumerator.MoveNext();
            }
            locksAndkeys.Add(new SafeMatrix<char>(matrix));
            if (notEndOfFile)
            {
                enumerator.MoveNext();
            }
        }

        List<List<int>> locks = new();
        List<List<int>> keys = new();
        foreach (var item in locksAndkeys)
        {
            List<int> heights = new();
            foreach (var column in item.GetColumns())
            {
                heights.Add(column.Count(ch => ch == '#') - 1);
            }

            if (item.GetRow(0).All(ch => ch == '#'))
            {
                // It's a lock
                locks.Add(heights);
            }
            else
            {
                keys.Add(heights);
            }
        }

        var matchWidouthOverlapping = 0;
        foreach (var key in keys)
        {
            foreach (var doorLock in locks)
            {
                if (IsKeyFitToLock(key, doorLock))
                {
                    matchWidouthOverlapping++;
                }
            }
        }

        return matchWidouthOverlapping.ToString();
    }

    private bool IsKeyFitToLock(List<int> key, List<int> doorLock)
    {
        for (int i = 0; i < key.Count; i++)
        {
            if (key[i] + doorLock[i] > 5)
            {
                return false;
            }
        }

        return true;
    }

    public override string GetSecondSolution()
    {
        return "";
    }
}
