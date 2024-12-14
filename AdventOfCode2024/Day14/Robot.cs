using AdventOfCode2024.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day14;

public class Robot
{
    private const string inputPattern = @"p=(?'x'\-?\d+),(?'y'\-?\d+) v=(?'vx'\-?\d+),(?'vy'\-?\d+)";
    private int x;
    private int y;
    private int vx;
    private int vy;

    public Robot(string input)
    {
        var match = Regex.Match(input, inputPattern);
        this.x = match.GetNamedValue("x").ToInt();
        this.y = match.GetNamedValue("y").ToInt();
        this.vx = match.GetNamedValue("vx").ToInt();
        this.vy = match.GetNamedValue("vy").ToInt();
    }

    public int X => x;

    public int Y => y;

    public int Vx => vx;

    public int Vy => vy;
}
