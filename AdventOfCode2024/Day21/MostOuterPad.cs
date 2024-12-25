namespace AdventOfCode2024.Day21;
public class MostOuterPad : Keypad
{
    public MostOuterPad() : base([])
    {
    }

    public override string GetShortestPath(string input) => input + 'p';
    public override long GetShortestPathLength(string input) => input.Length + 1;
}
