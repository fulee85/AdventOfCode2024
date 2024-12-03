using AdventOfCode2024.Common;
using AdventOfCode2024.Day1;
using System.Runtime.CompilerServices;

namespace AdventOfCode2024.Day2;

public class PuzzleSolver : PuzzleSolverBase
{
    List<List<int>> inputLists = new List<List<int>>();
    public PuzzleSolver(IInput input) : base(input)
    {
        foreach (var line in input)
        {
            var numbers = line.Split(' ').Select(int.Parse).ToList();
            inputLists.Add(numbers);
        }
    }

    public override string GetFirstSolution()
    {
        int safeCount = 0;
        foreach (var list in inputLists)
        {
            bool isSafe = IsSafe(list.ToArray());

            if (isSafe)
            {
                safeCount++;
            }
        }

        return safeCount.ToString();
    }

    public override string GetSecondSolution()
    {
        int safeCount = 0;
        foreach (var list in inputLists)
        {
            if (IsSafe(list.ToArray()))
            {
                safeCount++;
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (IsSafe(EnumerateExceptIndex(list, i)))
                    {
                        safeCount++;
                        break;
                    }
                }
            }
        }

        return safeCount.ToString();
    }

    private IEnumerable<int> EnumerateExceptIndex(List<int> list, int index)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (i == index)
            {
                continue;
            }
            yield return list[i];
        }
    }

    private bool IsSafe(IEnumerable<int> enumerable) 
    {
        List<int> list = enumerable.ToList();
        bool isIncreasing = list[0] < list[1];
        for (int i = 0; i < list.Count - 1; i++)
        {
            var diff = Math.Abs(list[i] - list[i + 1]);

            if (diff == 0 || diff > 3)
            {
                return false;
            }

            if (isIncreasing ^ list[i] < list[i + 1])
            {
                return false;
            }
        }

        return true;
    }
}