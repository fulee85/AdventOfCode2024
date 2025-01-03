﻿using AdventOfCode2024.Day21Old;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day21;
public class KeypadConundrum
{
    private readonly Keypad keypad;

    public KeypadConundrum(Keypad keypad)
    {
        this.keypad = keypad;
    }

    public long GetShortestSequenceLength(string line)
    {
        return keypad.GetShortestPathLength(line);
    }

    public string GetShortestSequence(string line)
    {
        var shortestPath = keypad.GetShortestPath(line);
        Console.WriteLine(shortestPath);
        return shortestPath;
    }
}
