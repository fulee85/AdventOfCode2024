using AdventOfCode2024.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day21.Test;

public class DirectionalKeypad
{
    private readonly SafeMatrix<char> safeMatrix;
    private readonly Dictionary<char, Position> charDict;

    public DirectionalKeypad()
    {
        safeMatrix = new SafeMatrix<char>(new List<List<char>>()
        {
            new(){'#', '^', 'A' },
            new(){'<', 'v', '>' },
        });

        charDict = safeMatrix.EnumerateAllPositions().ToDictionary(p => safeMatrix.GetValue(p));
    }

    public char GetCharAtDirection(char ch, Directions direction)
        => safeMatrix.GetValue(charDict[ch].GetPositionInDirection(direction));
}
