using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day17.Instructions;

/// <summary>
/// The out instruction (opcode 5) calculates the value of its combo operand modulo 8, then outputs that value. 
/// (If a program outputs multiple values, they are separated by commas.)
/// </summary>
public class Out : InstructionBase
{
    protected override void PerformInstruction() => state.OutputValue(ComboOperand & 7);
}
