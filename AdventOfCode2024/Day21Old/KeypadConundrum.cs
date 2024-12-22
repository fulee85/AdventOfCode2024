using System.Numerics;

namespace AdventOfCode2024.Day21Old;

public class KeypadConundrum
{
    private MostOuterPad outerPad;
    private NumericPad numericPad;

    public KeypadConundrum()
    {
        outerPad = new MostOuterPad();
        numericPad = new NumericPad(outerPad);
    }

    public string GetShortestSequence(string line)
    {
        return numericPad.GetShortestPath(line, true);
    }

    public long GetShortestSequenceLength(string line)
    {
        return numericPad.GetShortestPathLength(line);
    }

    internal void AddExtraRobot() => numericPad.AddNewNextPad();
}
