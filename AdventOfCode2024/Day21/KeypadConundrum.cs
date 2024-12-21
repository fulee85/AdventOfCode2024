using System.Numerics;

namespace AdventOfCode2024.Day21;

public class KeypadConundrum
{
    private MostOuterPad outerPad;
    private DirectionalPad directionalPad1;
    private DirectionalPad directionalPad2;
    private NumericPad numericPad;

    public KeypadConundrum()
    {
        outerPad = new MostOuterPad();
        directionalPad1 = new DirectionalPad(outerPad);
        directionalPad2 = new DirectionalPad(directionalPad1);
        numericPad = new NumericPad(directionalPad2);
    }

    public string GetShortestSequence(string line, int extraDirPads = 0)
    {
        for (int i = 0; i < extraDirPads; i++)
        {
            directionalPad1.AddNewNextPad();
        }
        return numericPad.GetShortestPath(line);
    }
}
