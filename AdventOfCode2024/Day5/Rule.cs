using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day5;
internal class Rule
{
    private readonly int smallerNumber;
    private readonly int biggerNumber;

    public Rule(string input)
    {
        var parts = input.Split('|');
        smallerNumber = int.Parse(parts[0]);
        biggerNumber = int.Parse(parts[1]);
    }

    public int Smaller => smallerNumber;
    public int BiggerNumber => biggerNumber;

    public bool IsRuleSatisfiedBySection(IEnumerable<int> section)
    {
        bool smallerNumFound = false;
        bool biggerNumFound = false;
        foreach (var num in section)
        {
            if (num == smallerNumber)
            {
                if (biggerNumFound) 
                { 
                    return false;
                }
                smallerNumFound = true;
            }
            else if (num == biggerNumber)
            {
                biggerNumFound = true;
            }

            if (smallerNumFound && biggerNumFound)
            {
                return true;
            }
        }

        return true;
    }
}
