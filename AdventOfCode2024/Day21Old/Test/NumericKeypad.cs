using AdventOfCode2024.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day21Old.Test;

public class NumericKeypad
{
    private readonly SafeMatrix<char> safeMatrix;
    private readonly Dictionary<char, Position> charDict;

    public NumericKeypad()
    {
        safeMatrix = new SafeMatrix<char>(new List<List<char>>()
        {
            new(){'7', '8', '9' },
            new(){'4', '5', '6' },
            new(){'1', '2', '3' },
            new(){'#', '0', 'A' },
        });

        charDict = safeMatrix.EnumerateAllPositions().ToDictionary(p => safeMatrix.GetValue(p));
    }

    public char GetCharAtDirection(char ch, Directions direction) 
        => safeMatrix.GetValue(charDict[ch].GetPositionInDirection(direction));
}
