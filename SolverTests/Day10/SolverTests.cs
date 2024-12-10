using AdventOfCode2024.Day10;
using FluentAssertions;

namespace SolverTests.Day10;

public class SolverTests
{
    [Fact]
    public void FirstSolutionShouldGiveCorrectResultOnDemoInput()
    {
        //Arrange
        string input = @"89010123
78121874
87430965
96549874
45678903
32019012
01329801
10456732";
        string expectedResult = "36";
        PuzzleSolver puzzleSolver = new PuzzleSolver(new StringInput(input));

        //Act
        var solution = puzzleSolver.GetFirstSolution();

        //Assert
        solution.Should().Be(expectedResult);
    }

    [Fact]
    public void SecondSolutionShouldGiveCorrectResultOnDemoInput()
    {
        //Arrange
        string input = @"89010123
78121874
87430965
96549874
45678903
32019012
01329801
10456732";
        string expectedResult = "81";
        PuzzleSolver puzzleSolver = new PuzzleSolver(new StringInput(input));

        //Act
        var solution = puzzleSolver.GetSecondSolution();

        //Assert
        solution.Should().Be(expectedResult);
    }
}
