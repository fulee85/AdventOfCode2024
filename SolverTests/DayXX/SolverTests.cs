﻿using AdventOfCode2024.DayXX;
using FluentAssertions;

namespace SolverTests.DayXX;

public class SolverTests
{
    private readonly string input = @"";

    [Fact]
    public void FirstSolutionShouldGiveCorrectResultOnDemoInput()
    {
        //Arrange
        string expectedResult = "";
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
        string expectedResult = "";
        PuzzleSolver puzzleSolver = new PuzzleSolver(new StringInput(input));

        //Act
        var solution = puzzleSolver.GetSecondSolution();

        //Assert
        solution.Should().Be(expectedResult);
    }
}
