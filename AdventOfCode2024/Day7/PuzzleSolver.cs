using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day7;

public class PuzzleSolver : PuzzleSolverBase
{
    private Func<long, long, long>[] operations = [
        (a,b) => a + b,
        (a,b) => a * b,
        ];

    private Func<long, long, long>[] operations2 = [
        (a,b) => a + b,
        (a,b) => a * b,
        (a,b) => long.Parse(a.ToString() + b.ToString()),
        ];


    public PuzzleSolver(IInput input) : base(input)
    {
    }

    public override string GetFirstSolution()
    {
        long sum = 0;
        List<Equation> equations = input.Select(l => new Equation(l, operations)).ToList();

        foreach (Equation equation in equations)
        { 
            if (equation.CanBeSolved())
            {
                sum += equation.TestValue;
            }
        }

        return sum.ToString();
    }
    public override string GetSecondSolution()
    {
        long sum = 0;
        List<Equation> equations = input.Select(l => new Equation(l, operations2)).ToList();

        foreach (Equation equation in equations)
        {
            if (equation.CanBeSolved())
            {
                sum += equation.TestValue;
            }
        }

        return sum.ToString();
    }
}
