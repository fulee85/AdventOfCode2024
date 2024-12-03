using AdventOfCode2024.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day3;

public class PuzzleSolver : PuzzleSolverBase
{
    public PuzzleSolver(IInput input) : base(input)
    {
    }

    public override string GetFirstSolution()
    {
        return CalculateProduct(input.GetRawInput()).ToString();
    }
    public override string GetSecondSolution()
    {
        var rawInput = input.GetRawInput();
        var splittedStrings = rawInput.Split("do()", StringSplitOptions.RemoveEmptyEntries);
        int product = 0;
        foreach (var splittedString in splittedStrings)
        {
            product += CalculateProduct(splittedString.Split("don't()", 2).First());
        }

        return product.ToString();
    }

    private int CalculateProduct(string input)
    {
        string pattern = @"mul\((?'Num1'\d{1,3}),(?'Num2'\d{1,3})\)";
        Regex regex = new Regex(pattern);

        var matches = regex.Matches(input);
        var product = 0;
        foreach (Match match in matches)
        {
            product += int.Parse(match.Groups["Num1"].Value) * int.Parse(match.Groups["Num2"].Value);
        }

        return product;
    }
}
