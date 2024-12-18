using AdventOfCode2024.Common;
using System.Linq;
using System.Numerics;

namespace AdventOfCode2024.Day17;

public class PuzzleSolver : PuzzleSolverBase
{
    private int aRegisterValue;
    private int bRegisterValue;
    private int cRegisterValue;
    private int[] program;
    public PuzzleSolver(IInput input) : base(input)
    {
        var inputLines = input.ToArray();
        aRegisterValue = inputLines[0].Replace("Register A: ", "").ToInt();
        bRegisterValue = inputLines[1].Replace("Register B: ", "").ToInt();
        cRegisterValue = inputLines[2].Replace("Register C: ", "").ToInt();
        program = inputLines[4].Replace("Program: ", "").Split(',').Select(int.Parse).ToArray();
    }

    public override string GetFirstSolution()
    {
        var computer = new Computer(aRegisterValue, bRegisterValue, cRegisterValue, program);
        var output = computer.RunProgram();

        return output;
    }

    public override string GetSecondSolution()
    {
        List<int> firstNumbers = new List<int>();
        for (int a = 0; a < 3000; a = a + 1)
        {
            var computer = new Computer(a, bRegisterValue, cRegisterValue, program);
            var output = computer.RunProgram();
            var firstNum = int.Parse(output.First().ToString());
            firstNumbers.Add(firstNum);
        }

        var repetation = FindRepetation(firstNumbers);

        List<int>[] repetitionDictionary = [[], [], [], [], [], [], [], []];
        for (int i = 0; i < repetation.Count; i++)
        {
            repetitionDictionary[repetation[i]].Add(i);
        }

        List<IntervalFinder> intervalFinders = Enumerable
            .Range(0, program.Length)
            .Zip(program)
            .Select(tuple => new IntervalFinder(tuple.First, tuple.Second, repetitionDictionary, repetation.Count))
            .Reverse()
            .ToList();

        BigInteger answer = 0;
        bool answerFound = false;
        while (!answerFound)
        {
            answerFound = true;
            foreach (var intervalFinder in intervalFinders)
            {
                var closestInterval = intervalFinder.GetClosestInterval(answer);
                if (!closestInterval.Contains(answer))
                {
                    answer = closestInterval.From;
                    answerFound = false;
                }
            }
        }

        return answer.ToString();
    }

    private List<int> FindRepetation(List<int> firstNumbers)
    {
        for (int i = 2; i < firstNumbers.Count / 2; i++)
        {
            bool possibleRepeatation = true;
            for (int j = 0; j < i; j++)
            {
                if (firstNumbers[j] != firstNumbers[i + j])
                {
                    possibleRepeatation = false;
                    break;
                }
            }

            if (possibleRepeatation)
            {
                return firstNumbers.Take(i).ToList(); ;
            }
        }

        return [];
    }
}