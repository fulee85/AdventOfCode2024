using AdventOfCode2024.Common;
using AdventOfCode2024.Day15.SecondPart;

namespace AdventOfCode2024.Day15;

public class PuzzleSolver : PuzzleSolverBase
{
    public PuzzleSolver(IInput input) : base(input)
    {
    }

    public override string GetFirstSolution()
    {
        List<string> map = input.TakeWhile(l => !string.IsNullOrEmpty(l)).ToList();
        var warehouse = new Warehouse(map);
        List<string> moveLines = input.SkipWhile(l => !string.IsNullOrEmpty(l)).ToList();
        char previousChar = ' ';
        bool canMovedPreviously = true;

        //Console.WriteLine(warehouse);
        foreach (var moveLine in moveLines)
        {
            foreach (var moveChar in moveLine)
            {
                if (moveChar == previousChar && !canMovedPreviously)
                {
                    continue;
                }
                canMovedPreviously = warehouse.MoveRobot(moveChar);
                previousChar = moveChar;
                //Console.WriteLine($"Move: {previousChar}");
                //Console.WriteLine(warehouse);
            }
        }

        return warehouse.GetSumOfBoxGPSValues().ToString();
    }
    public override string GetSecondSolution()
    {
        List<string> map = input.TakeWhile(l => !string.IsNullOrEmpty(l)).ToList();
        var warehouse = new ScaledUpWarehouse(map);
        List<string> moveLines = input.SkipWhile(l => !string.IsNullOrEmpty(l)).ToList();
        char previousChar = ' ';
        bool canMovedPreviously = true;

        //Console.WriteLine(warehouse);
        foreach (var moveLine in moveLines)
        {
            foreach (var moveChar in moveLine)
            {
                if (moveChar == previousChar && !canMovedPreviously)
                {
                    continue;
                }
                canMovedPreviously = warehouse.MoveRobot(moveChar);
                previousChar = moveChar;
                //Console.WriteLine($"Move: {previousChar}");
                //Console.WriteLine(warehouse);
                //Console.ReadKey();
            }
        }

        return warehouse.GetSumOfScaledUpBoxGPSValues().ToString();
    }
}
