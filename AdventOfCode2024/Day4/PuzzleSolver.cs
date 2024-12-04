using AdventOfCode2024.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day4;

public class PuzzleSolver : PuzzleSolverBase
{
    private readonly SafeMatrix<char> safeMatrix;
    private const string SearchedString = "XMAS";
    private const string ReversedSearchedString = "SAMX";

    public PuzzleSolver(IInput input) : base(input) => safeMatrix = new SafeMatrix<char>(input.Select(l => l.ToList()).ToList(), '.');

    public override string GetFirstSolution()
    {
        int XmasCount = 0;
        // RowSearch
        foreach (var row in safeMatrix.GetRows().Select(chars => new string(chars.ToArray())))
        {
            XmasCount += Regex.Matches(row, SearchedString).Count;
            XmasCount += Regex.Matches(row, ReversedSearchedString).Count;
        }

        // ColumnSearch
        foreach (var row in safeMatrix.GetColumns().Select(chars => new string(chars.ToArray())))
        {
            XmasCount += Regex.Matches(row, SearchedString).Count;
            XmasCount += Regex.Matches(row, ReversedSearchedString).Count;
        }

        // Left-Right Diagonal Search
        foreach (var row in safeMatrix.GetLeftRightDiagonalLines().Select(chars => new string(chars.ToArray())))
        {
            XmasCount += Regex.Matches(row, SearchedString).Count;
            XmasCount += Regex.Matches(row, ReversedSearchedString).Count;
        }

        // Right-Left Diagonal Search
        foreach (var row in safeMatrix.GetRightLeftDiagonalLines().Select(chars => new string(chars.ToArray())))
        {
            XmasCount += Regex.Matches(row, SearchedString).Count;
            XmasCount += Regex.Matches(row, ReversedSearchedString).Count;
        }


        return XmasCount.ToString();
    }
    public override string GetSecondSolution()
    {
        int X_MasCount = 0;

        for (int i = 0; i < safeMatrix.RowCount; i++)
        {
            for (int j = 0; j < safeMatrix.ColumnCount; j++)
            {
                if (safeMatrix[i, j] == 'A')
                {
                    if (CheckForX(i, j))
                    {
                        X_MasCount++;
                    }
                }
            }
        }

        return X_MasCount.ToString();
    }

    private bool CheckForX(int i, int j) =>
        ((safeMatrix[i - 1, j - 1] == 'M' && safeMatrix[i + 1, j + 1] == 'S') || (safeMatrix[i - 1, j - 1] == 'S' && safeMatrix[i + 1, j + 1] == 'M'))
        &&
        ((safeMatrix[i - 1, j + 1] == 'M' && safeMatrix[i + 1, j - 1] == 'S') || (safeMatrix[i - 1, j + 1] == 'S' && safeMatrix[i + 1, j - 1] == 'M'));
}

