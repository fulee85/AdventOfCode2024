using AdventOfCode2024.Common;
using SolverTests.ExtensionMethods;
using System.Collections;

namespace SolverTests;
internal class StringInput(string input) : IInput
{
    public IEnumerator<string> GetEnumerator() => input.GetLines().GetEnumerator();

    public string GetRawInput() => input;

    IEnumerator IEnumerable.GetEnumerator() => input.GetLines().GetEnumerator();
}
