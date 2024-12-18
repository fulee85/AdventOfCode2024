using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day17.Instructions;

/// <summary>
/// The bxl instruction (opcode 1) calculates the bitwise XOR of register B 
/// and the instruction's literal operand, then stores the result in register B.
/// </summary>
public class Bxl : InstructionBase
{
    protected override void PerformInstruction() => state.B ^= LiteralOperand;
}