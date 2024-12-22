using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day21;
public static class KeypadConfigurations
{
    public static SafeMatrix<char> NumericKeypad { get; } = new SafeMatrix<char>(new List<List<char>>()
        {
            new (){ '7', '8', '9' },
            new (){ '4', '5', '6' },
            new() { '1', '2', '3' },
            new() { '#', '0', 'A' },
        }, '#');

    public static SafeMatrix<char> DirectionalKeypad { get; } = new SafeMatrix<char>(new List<List<char>>()
        {
            new(){'#', 'u', 'p' },
            new(){'l', 'd', 'r' },
        }, '#');
}
