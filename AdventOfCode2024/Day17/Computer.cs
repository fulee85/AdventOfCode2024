using AdventOfCode2024.Day17.Instructions;

namespace AdventOfCode2024.Day17;

public class Computer
{
    private readonly ComputerState state;
    private readonly List<int> program;
    private readonly Dictionary<int, InstructionBase> instructions = new Dictionary<int, InstructionBase>
    {
        {0 , new Adv()},
        {1 , new Bxl()},
        {2 , new Bst()},
        {3 , new Jnz()},
        {4 , new Bxc()},
        {5 , new Out()},
        {6 , new Bdv()},
        {7 , new Cdv()},
    };

    public Computer(int aRegisterValue, int bRegisterValue, int cRegisterValue, int[] program)
    {
        state = new ComputerState(aRegisterValue, bRegisterValue, cRegisterValue, 0);
        this.program = program.ToList();
    }

    public string RunProgram()
    {
        while (state.InstructionPointer < program.Count)
        {
            var opCode = program[state.InstructionPointer];
            var operand = program[state.InstructionPointer + 1];
            instructions[opCode].PerformInstructionOnState(state, operand);
        }

        return state.Output;
    }
}
