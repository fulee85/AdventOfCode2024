using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day11;

internal interface IRule
{
    void Apply(Stone stone);
    void SetNext(IRule next);
}
