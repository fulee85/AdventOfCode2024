﻿using AdventOfCode2024.Day8;
using FluentAssertions;

namespace SolverTests.Day8;

public class SolverTests
{
    [Fact]
    public void FirstSolutionShouldGiveCorrectResultOnDemoInput()
    {
        //Arrange
        string input = @"............
........0...
.....0......
.......0....
....0.......
......A.....
............
............
........A...
.........A..
............
............";
        string expectedResult = "14";
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
        string input = @"............
........0...
.....0......
.......0....
....0.......
......A.....
............
............
........A...
.........A..
............
............";
        string expectedResult = "34";
        PuzzleSolver puzzleSolver = new PuzzleSolver(new StringInput(input));

        //Act
        var solution = puzzleSolver.GetSecondSolution();

        //Assert
        solution.Should().Be(expectedResult);
    }
}
