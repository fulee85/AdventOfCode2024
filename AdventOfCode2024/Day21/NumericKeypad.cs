namespace AdventOfCode2024.Day21;

public class NumericKeypad : Keypad
{
    public NumericKeypad(Dictionary<string, List<string>> shortestPaths, Keypad next) : base(shortestPaths, next)
    {
        StartChar = 'A';
    }

    public override long GetShortestPathLength(string input)
    {
        long length = 0;
        var extendedInput = StartChar + input;
        for (int i = 0; i < extendedInput.Length - 1; i++)
        {
            var substring = extendedInput.Substring(i, 2);
            length += GetMinLength(substring);
        }

        return length;
    }
}
