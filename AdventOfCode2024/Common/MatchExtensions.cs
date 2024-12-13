using System.Text.RegularExpressions;

namespace AdventOfCode2024.Common;

public static class MatchExtensions
{
    public static string GetNamedValue(this Match match, string name) => match.Groups.GetValueOrDefault(name)?.Value ?? string.Empty;
}
