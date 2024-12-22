namespace AdventOfCode2024.Day21;
public class NumericKeypadFactory
{
    private readonly Dictionary<string, List<string>> shortestPaths;

    public NumericKeypadFactory(Dictionary<string, List<string>> shortestPaths)
    {
        this.shortestPaths = shortestPaths;
    }

    public NumericKeypad Create(Keypad next) => new NumericKeypad(shortestPaths, next);
}
