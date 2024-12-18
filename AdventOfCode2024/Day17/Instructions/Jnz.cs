namespace AdventOfCode2024.Day17.Instructions;

/// <summary>
/// The jnz instruction (opcode 3) does nothing if the A register is 0. 
/// However, if the A register is not zero, it jumps by setting the instruction pointer to the value of its literal operand;
/// if this instruction jumps, the instruction pointer is not increased by 2 after this instruction.
/// </summary>
public class Jnz : InstructionBase
{
    protected override void PerformInstruction()
    {
    }

    protected override void StepInstructionPointer()
    {
        if (state.A == 0)
        {
            base.StepInstructionPointer();
        }
        else
        {
            state.InstructionPointer = LiteralOperand;
        }
    }
}
