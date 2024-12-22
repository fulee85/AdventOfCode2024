using AdventOfCode2024.Day22;
using FluentAssertions;

namespace SolverTests.Day22;

public class SolverTests
{

    [Fact]
    public void FirstSolutionShouldGiveCorrectResultOnDemoInput()
    {
        //Arrange
        string input = @"1
10
100
2024";
        string expectedResult = "37327623";
        PuzzleSolver puzzleSolver = new PuzzleSolver(new StringInput(input));

        //Act
        var solution = puzzleSolver.GetFirstSolution();

        //Assert
        solution.Should().Be(expectedResult);
    }

    [Fact]
    public void TestMix()
    {
        int secret = 42;
        int value = 15;

        var result = PuzzleSolver.Mix(42, 15);

        result.Should().Be(37);
    }

    [Fact]
    public void TestPrune()
    {
        int secret = 100000000;

        var result = PuzzleSolver.Prune(secret);

        result.Should().Be(16113920);
    }

    [Fact]
    public void SecondSolutionShouldGiveCorrectResultOnDemoInput()
    {
        //Arrange
        string input = @"1
2
3
2024";
        string expectedResult = "23";
        PuzzleSolver puzzleSolver = new PuzzleSolver(new StringInput(input));

        //Act
        var solution = puzzleSolver.GetSecondSolution();

        //Assert
        solution.Should().Be(expectedResult);
    }
}
