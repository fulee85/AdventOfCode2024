namespace AdventOfCode2024.Day17;

public class ComputerState(int a, int b, int c, int instructionPointer)
{
    public int A { get; set; } = a;
    public int B { get; set; } = b;
    public int C { get; set; } = c;
    public int InstructionPointer { get; set; } = instructionPointer;

    private readonly List<int> output = new List<int>();

    public void OutputValue(int value) => output.Add(value);
    public string Output => string.Join(',', output);
}
