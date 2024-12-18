namespace AdventOfCode2024.Day17.Instructions;

/// <summary>
/// The cdv instruction (opcode 7) works exactly like the adv instruction
/// except that the result is stored in the C register. (The numerator is still read from the A register.)
/// </summary>
public class Cdv : InstructionBase
{
    protected override void PerformInstruction() => state.C = state.A / (1 << ComboOperand);
}