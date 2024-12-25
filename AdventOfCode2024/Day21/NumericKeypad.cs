using System.Text;

namespace AdventOfCode2024.Day21;

public class NumericKeypad : Keypad
{
    public NumericKeypad(Dictionary<string, List<string>> shortestPaths, Keypad next) : base(shortestPaths, next)
    {
        StartChar = 'A';
    }

    public override string GetShortestPath(string input)
    {
        StringBuilder stringBuilder = new StringBuilder();
        var extendedInput = StartChar + input;
        for (int i = 0; i < extendedInput.Length - 1; i++)
        {
            var minLength = int.MaxValue;
            string minPathString = "";
            foreach (var item in shortestPaths[extendedInput.Substring(i, 2)])
            {
                var actString = next.GetShortestPath(item);
                if (actString.Length < minLength)
                {
                    minLength = actString.Length;
                    minPathString = actString;
                }
            }
            stringBuilder.Append(minPathString);
        }

        return stringBuilder.ToString();
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
