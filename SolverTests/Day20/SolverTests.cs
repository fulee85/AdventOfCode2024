using AdventOfCode2024.Day20;
using FluentAssertions;

namespace SolverTests.Day20;

public class SolverTests
{
    string input = @"###############
#...#...#.....#
#.#.#.#.#.###.#
#S#...#.#.#...#
#######.#.#.###
#######.#.#...#
#######.#.###.#
###..E#...#...#
###.#######.###
#...###...#...#
#.#####.#.###.#
#.#...#.#.#...#
#.#.#.#.#.#.###
#...#...#...###
###############";

    [Fact]
    public void FirstSolutionShouldGiveCorrectResultOnDemoInput()
    {
        //Arrange
        PuzzleSolver.leastCheatDistance = 10;
        string expectedResult = "10";
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
        PuzzleSolver.leastCheatDistance = 70;
        string expectedResult = "41";
        PuzzleSolver puzzleSolver = new PuzzleSolver(new StringInput(input));

        //Act
        var solution = puzzleSolver.GetSecondSolution();

        //Assert
        solution.Should().Be(expectedResult);
    }
}
