namespace AdventOfCode2024.Common;
public interface IInput : IEnumerable<string>
{
    string GetRawInput();

    List<List<char>> ToCharMatrix()
    {
        return GetRawInput().Split(Environment.NewLine).Select(l => l.ToList()).ToList();
    }
}