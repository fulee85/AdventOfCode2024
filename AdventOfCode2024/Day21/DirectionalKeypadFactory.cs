namespace AdventOfCode2024.Day21;
public class DirectionalKeypadFactory
{
    private readonly Dictionary<string, List<string>> shortestPaths;

    public DirectionalKeypadFactory(Dictionary<string, List<string>> shortestPaths)
    {
        this.shortestPaths = shortestPaths;
    }

    public DirectionalKeypad Create(Keypad next) => new DirectionalKeypad(shortestPaths, next);
}
