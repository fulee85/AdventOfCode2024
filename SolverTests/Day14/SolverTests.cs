using AdventOfCode2024.Day14;
using FluentAssertions;

namespace SolverTests.Day14;

public class SolverTests
{
    [Fact]
    public void FirstSolutionShouldGiveCorrectResultOnDemoInput()
    {
        //Arrange
        string input = @"p=0,4 v=3,-3
p=6,3 v=-1,-3
p=10,3 v=-1,2
p=2,0 v=2,-1
p=0,0 v=1,3
p=3,0 v=-2,-2
p=7,6 v=-1,-3
p=3,0 v=-1,-2
p=9,3 v=2,3
p=7,3 v=-1,2
p=2,4 v=2,-3
p=9,5 v=-3,-3";
        string expectedResult = "12";
        PuzzleSolver puzzleSolver = new PuzzleSolver(new StringInput(input));
        PuzzleSolver.Width = 11;
        PuzzleSolver.Height = 7;

        //Act
        var solution = puzzleSolver.GetFirstSolution();

        //Assert
        solution.Should().Be(expectedResult);
    }
}
