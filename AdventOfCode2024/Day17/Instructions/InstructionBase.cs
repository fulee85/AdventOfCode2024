namespace AdventOfCode2024.Day17.Instructions;

public abstract class InstructionBase
{
    private int operand;
    protected ComputerState state = new(0,0,0,0);

    public void PerformInstructionOnState(ComputerState state, int operand)
    {
        this.operand = operand;
        this.state = state;

        PerformInstruction();
        StepInstructionPointer();
    }

    protected abstract void PerformInstruction();

    protected virtual void StepInstructionPointer()
    {
        state.InstructionPointer += 2;
    }

    protected int ComboOperand => operand switch
    {
        < 4 => operand,
        4 => state.A,
        5 => state.B,
        6 => state.C,
        > 6 => throw new Exception()
    };

    protected int LiteralOperand => operand;
}
