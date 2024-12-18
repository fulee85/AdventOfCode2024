using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day17.Instructions;

/// <summary>
/// The bst instruction (opcode 2) calculates the value of its combo operand modulo 8 (thereby keeping only its lowest 3 bits),
/// then writes that value to the B register.
/// </summary>
public class Bst : InstructionBase
{
    protected override void PerformInstruction() => state.B = ComboOperand & 7;
}
