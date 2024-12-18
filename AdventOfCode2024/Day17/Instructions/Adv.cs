namespace AdventOfCode2024.Day17.Instructions;

/// <summary>
/// The adv instruction (opcode 0) performs division. 
/// The numerator is the value in the A register. 
/// The denominator is found by raising 2 to the power of the instruction's combo operand. 
/// (So, an operand of 2 would divide A by 4 (2^2); an operand of 5 would divide A by 2^B.) 
/// The result of the division operation is truncated to an integer and then written to the A register.
/// </summary>
public class Adv : InstructionBase
{
    protected override void PerformInstruction() => state.A /= 1 << ComboOperand;
}
