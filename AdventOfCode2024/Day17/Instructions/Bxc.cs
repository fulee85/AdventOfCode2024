namespace AdventOfCode2024.Day17.Instructions;

/// <summary>
/// The bxc instruction (opcode 4) calculates the bitwise XOR of register B and register C, then stores the result in register B.
/// (For legacy reasons, this instruction reads an operand but ignores it.)
/// </summary>
public class Bxc : InstructionBase
{
    protected override void PerformInstruction() => state.B ^= state.C;
}
