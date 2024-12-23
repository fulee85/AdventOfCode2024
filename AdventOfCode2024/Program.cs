using AdventOfCode2024.Common;
using System.Diagnostics;

var day = DateTime.Now.Day;
Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();
//for (int i = 1; i <= 25; i++)
//{
    //var day = i;
    var fullyQualifiedName = $"AdventOfCode2024.Day{day}.PuzzleSolver";
    Type t = Type.GetType(fullyQualifiedName)!;
    string inputPath = Environment.CurrentDirectory + $"\\Day{day}\\input.txt";
    var solver = Activator.CreateInstance(t, new FileInput(inputPath)) as PuzzleSolverBase;


    Console.WriteLine($"Solutions for day {day}");
    Console.WriteLine($"First solution: {solver?.GetFirstSolution()}");
    Console.WriteLine($"Second solution: {solver?.GetSecondSolution()}");
//}
stopwatch.Stop();
Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");