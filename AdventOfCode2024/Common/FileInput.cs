using System.Collections;

namespace AdventOfCode2024.Common;
public class FileInput : IInput
{
    private readonly string inputPath;

    public FileInput(string inputPath)
    {
        this.inputPath = inputPath;
    }

    public string GetRawInput() => File.ReadAllText(inputPath);

    public IEnumerator<string> GetEnumerator() => File.ReadLines(inputPath).GetEnumerator();

    public IEnumerable<string> ReadInput() => File.ReadLines(inputPath);

    IEnumerator IEnumerable.GetEnumerator() => File.ReadLines(inputPath).GetEnumerator();
}
