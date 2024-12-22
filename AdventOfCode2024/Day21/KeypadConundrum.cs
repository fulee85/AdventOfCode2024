using System.Numerics;

namespace AdventOfCode2024.Day21;

public class KeypadConundrum
{
    private MostOuterPad outerPad;
    private DirectionalPad directionalPad1;
    private DirectionalPad directionalPad2;
    private DirectionalPad directionalPad3;
    private DirectionalPad directionalPad4;
    private NumericPad numericPad;

    public KeypadConundrum()
    {
        outerPad = new MostOuterPad();
        directionalPad1 = new DirectionalPad(outerPad);
        directionalPad2 = new DirectionalPad(directionalPad1);
        directionalPad3 = new DirectionalPad(directionalPad2);
        directionalPad4 = new DirectionalPad(directionalPad3);
        numericPad = new NumericPad(directionalPad4);
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
