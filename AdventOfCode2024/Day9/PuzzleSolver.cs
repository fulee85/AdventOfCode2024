using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day9;

public class PuzzleSolver : PuzzleSolverBase
{
    public PuzzleSolver(IInput input) : base(input)
    {
    }

    public override string GetFirstSolution()
    {
        long checkSum = 0;
        List<int> numbers = input.GetRawInput().Select(ch => int.Parse(ch.ToString())).ToList();
        int index = 0;

        Spaces spaces = new Spaces(numbers);

        foreach (var fileId in spaces)
        {
            checkSum += index * fileId;
            index++;
        }

        return checkSum.ToString();
    }
    public override string GetSecondSolution()
    {
        long checkSum = 0;
        List<int> numbers = input.GetRawInput().Select(ch => int.Parse(ch.ToString())).ToList();
        int index = 0;

        SpacesWithoutFragmentation spaces = new SpacesWithoutFragmentation(numbers);

        foreach (var fileId in spaces)
        {
            checkSum += index * fileId;
            index++;
        }

        return checkSum.ToString();
    }
}
