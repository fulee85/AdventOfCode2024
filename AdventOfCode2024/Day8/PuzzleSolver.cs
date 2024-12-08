using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day8;

public class PuzzleSolver : PuzzleSolverBase
{
    private readonly SafeMatrix<char> antennaMap;
    private readonly Dictionary<char, List<Position>> antennaPositions;

    public PuzzleSolver(IInput input) : base(input)
    {
        antennaMap = new SafeMatrix<char>(input.ToCharMatrix());
        antennaPositions = GetAntennaPositions();
    }

    public override string GetFirstSolution()
    {
        SafeMatrix<char> antinodeMap = new SafeMatrix<char>(input.ToCharMatrix());

        var count = 0;
        foreach (var item in antennaPositions.Values)
        {
            for (int i = 0; i < item.Count - 1; i++)
            {
                for (int j = i + 1; j < item.Count; j++)
                {
                    Position antennaA = item[i];
                    Position antennaB = item[j];
                    Position antinode1 = antennaA + (antennaA - antennaB);
                    antinodeMap.SetValue(antinode1, '#');
                    count++;
                    Position antinode2 = antennaB + (antennaB - antennaA);
                    antinodeMap.SetValue(antinode2, '#');
                    count++;
                }
            }
        }

        return antinodeMap.Count('#').ToString();
    }

    private Dictionary<char, List<Position>> GetAntennaPositions()
    {
        Dictionary<char, List<Position>> antennaPositions = [];

        for (int i = 0; i < antennaMap.RowCount; i++)
        {
            for (int j = 0; j < antennaMap.ColumnCount; j++)
            {
                if (antennaMap[i,j] != '.')
                {
                    if (antennaPositions.TryGetValue(antennaMap[i, j], out var antennaPositionsList))
                    {
                        antennaPositionsList.Add(new Position(i, j));
                    }
                    else
                    {
                        antennaPositions[antennaMap[i, j]] = [new Position(i, j)];
                    }
                }
            }
        }

        return antennaPositions;
    }

    public override string GetSecondSolution()
    {
        SafeMatrix<char> antinodeMap = new SafeMatrix<char>(input.ToCharMatrix());

        foreach (var item in antennaPositions.Values)
        {
            for (int i = 0; i < item.Count - 1; i++)
            {
                for (int j = i + 1; j < item.Count; j++)
                {
                    Position antennaA = item[i];
                    Position antennaB = item[j];
                    Position difference = antennaA - antennaB;

                    int multiplier = 0;
                    while (antinodeMap.SetValue(antennaA + (multiplier * difference), '#'))
                    {
                        multiplier++;
                    }

                    multiplier = -1;
                    while (antinodeMap.SetValue(antennaA + (multiplier * difference), '#'))
                    {
                        multiplier--;
                    }
                }
            }
        }

        return antinodeMap.Count('#').ToString();
    }
}
