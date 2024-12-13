using AdventOfCode2024.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day13;

public class Equation
{
    private const long Correction = 10_000_000_000_000;
    private long a, b, c, d, e, f;

    public Equation(Span<string> input, bool withCorrection)
    {
        var lineOnePattern = @"Button A: X\+(?'a'\d+), Y\+(?'d'\d+)";
        var lineTwoPattern = @"Button B: X\+(?'b'\d+), Y\+(?'e'\d+)";
        var lineThreePattern = @"Prize: X=(?'c'\d+), Y=(?'f'\d+)";

        var match = Regex.Match(input[0], lineOnePattern);
        a = long.Parse(match.GetNamedValue("a"));
        d = long.Parse(match.GetNamedValue("d"));

        match = Regex.Match(input[1], lineTwoPattern);
        b = long.Parse(match.GetNamedValue("b"));
        e = long.Parse(match.GetNamedValue("e"));

        match = Regex.Match(input[2], lineThreePattern);
        c = long.Parse(match.GetNamedValue("c"));
        f = long.Parse(match.GetNamedValue("f"));

        if (withCorrection)
        {
            c += Correction;
            f += Correction;
        }
    }

    public Result GetResult()
    {
        var numerator = (a * f) - (d * c);
        var denominator = (a * e) - (d * b);
        if (numerator % denominator != 0)
        {
            return Result.FailedResult;
        }
        var B = numerator / denominator;

        var numeratorA = c - (b * B);
        if (numeratorA % a != 0)
        {
            return Result.FailedResult;
        }
        var A = numeratorA / a;

        return new Result(A, B);
    }
}
