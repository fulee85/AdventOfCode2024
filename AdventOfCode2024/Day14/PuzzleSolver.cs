using AdventOfCode2024.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day14;

public class PuzzleSolver : PuzzleSolverBase
{
    private readonly List<Robot> robots;
    public static int Width = 101;
    public static int Height = 103;

    public PuzzleSolver(IInput input) : base(input)
    {
        robots = input.Select(l => new Robot(l)).ToList();
    }

    public override string GetFirstSolution()
    {
        var robotSimulator = new RobotSimulator();
        robots.ForEach(robotSimulator.Simulate100Move);

        return robotSimulator.GetSafetyFactor().ToString();
    }
    public override string GetSecondSolution()
    {
        var robotSimulator = new RobotSimulator();
        var steps = 0;
        char keyChar = ' ';
        bool possibleChristmassTree = false;
        while (keyChar != 'x')
        {
            possibleChristmassTree = false;
            while (!possibleChristmassTree)
            {
                List<Position> positions = [];
                steps++;
                foreach (var robot in robots)
                {
                    var position = robotSimulator.MoveRobot(robot, steps);
                    positions.Add(new Position(position.yEnd, position.xEnd));
                }
                possibleChristmassTree = IsPossibleChristmassTree(positions);
            }

            // Write Out
            var matrix = new SafeMatrix<char>(rowCount: Height, columnCount: Width, ' ');
            foreach (var robot in robots)
            {
                var position = robotSimulator.MoveRobot(robot, steps);
                matrix[position.yEnd, position.xEnd] = '#';
            }

            Console.Clear();
            Console.WriteLine(matrix);
            Console.WriteLine();
            Console.WriteLine($"Steps: {steps}");
            keyChar = Console.ReadKey().KeyChar;
        }

        return steps.ToString();
    }

    private static long maxValue = 0L;

    private bool IsPossibleChristmassTree(List<Position> positions)
    {
        var sumRow = 0;
        var sumColumn = 0;
        int[] columnCount = new int[Width];
        int[] rowCount = new int[Height];
        foreach (var position in positions)
        {
            columnCount[position.Column]++;
            rowCount[position.Row]++;
        }

        sumRow = rowCount.Sum(v => v * v * v);
        sumColumn = columnCount.Sum(v => v * v * v);

        if (maxValue <= sumRow + sumColumn)
        {
            maxValue = sumRow + sumColumn;
            return true;
        }
        return false;
    }
}
