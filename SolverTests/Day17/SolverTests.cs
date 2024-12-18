using AdventOfCode2024.Day17;
using AdventOfCode2024.Day17.Instructions;
using FluentAssertions;

namespace SolverTests.Day17;

public class SolverTests
{
    [Fact]
    public void FirstSolutionShouldGiveCorrectResultOnDemoInput()
    {
        //Arrange
        string input = @"Register A: 729
Register B: 0
Register C: 0

Program: 0,1,5,4,3,0";
        string expectedResult = "4,6,3,5,6,3,5,2,1,0";
        PuzzleSolver puzzleSolver = new PuzzleSolver(new StringInput(input));

        //Act
        var solution = puzzleSolver.GetFirstSolution();

        //Assert
        solution.Should().Be(expectedResult);
    }

    [Fact]
    public void TestBst()
    {
        var bst = new Bst();
        var state = new ComputerState(0, 0, 9, 0);
        bst.PerformInstructionOnState(state, 6);

        state.B.Should().Be(1);
        state.InstructionPointer.Should().Be(2);
    }

    [Fact]
    //If register B contains 29, the program 1,7 would set register B to 26.
    public void TestBxl()
    {
        var bxl = new Bxl();
        var state = new ComputerState(0, 29, 0, 0);
        bxl.PerformInstructionOnState(state, 7);

        state.B.Should().Be(26);
        state.InstructionPointer.Should().Be(2);
    }

    [Fact]
    //If register B contains 2024 and register C contains 43690,
    //the program 4,0 would set register B to 44354.
    public void TestBxc()
    {
        var bxc = new Bxc();
        var state = new ComputerState(0, 2024, 43690, 0);
        bxc.PerformInstructionOnState(state, 0);

        state.B.Should().Be(44354);
        state.InstructionPointer.Should().Be(2);
    }

    [Fact]
    //If register A contains 10, the program 5,0,5,1,5,4 would output 0,1,2.
    public void TestProgram()
    {
        var computer = new Computer(10, 0, 0, [5, 0, 5, 1, 5, 4]);
        var output = computer.RunProgram();
        output.Should().Be("0,1,2");
    }

    [Fact]
    //If register A contains 2024, the program 0,1,5,4,3,0
    //would output 4,2,5,6,7,7,7,7,3,1,0 and leave 0 in register A.
    public void TestProgram2()
    {
        var computer = new Computer(2024, 0, 0, [0, 1, 5, 4, 3, 0]);
        var output = computer.RunProgram();
        output.Should().Be("4,2,5,6,7,7,7,7,3,1,0");
    }

    [Fact]
    //If register A contains 2024, the program 0,1,5,4,3,0
    //would output 4,2,5,6,7,7,7,7,3,1,0 and leave 0 in register A.
    public void TestProgram3()
    {
        var computer = new Computer(117440, 0, 0, [0, 3, 5, 4, 3, 0]);
        var output = computer.RunProgram();
        output.Should().Be("0,3,5,4,3,0");
    }
}
