using AdventOfCode2024.Day21;
using FluentAssertions;

namespace SolverTests.Day21;

public class SolverTests
{
    private readonly string input = @"029A
980A
179A
456A
379A";

    [Fact]
    public void FirstSolutionShouldGiveCorrectResultOnDemoInput()
    {
        //Arrange
        string expectedResult = "126384";
        PuzzleSolver puzzleSolver = new PuzzleSolver(new StringInput(input));

        //Act
        var solution = puzzleSolver.GetFirstSolution();

        //Assert
        solution.Should().Be(expectedResult);
    }
}
