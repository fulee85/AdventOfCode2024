namespace AdventOfCode2024.Day17.Instructions;

/// <summary>
/// The bdv instruction (opcode 6) works exactly like the adv instruction
/// except that the result is stored in the B register. (The numerator is still read from the A register.)
/// </summary>
public class Bdv : InstructionBase
{
    protected override void PerformInstruction() => state.B = state.A / (1 << ComboOperand);
}
