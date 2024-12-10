namespace AdventOfCode2024.Common;
public interface IInput : IEnumerable<string>
{
    string GetRawInput();

    List<List<char>> ToCharMatrix()
    {
        return GetRawInput().Split(Environment.NewLine).Select(l => l.ToList()).ToList();
    }

    List<List<int>> ToIntMatrix()
    {
        return GetRawInput().Split(Environment.NewLine).Select(l => l.Select(c => int.Parse(c.ToString())).ToList()).ToList();
    }
}