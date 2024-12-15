using AdventOfCode2024.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day15;

public class Box : MoveableItem
{
    public Box(Position position) : base(position, 'O')
    {
    }

    public int GPS => position.Row * 100 + position.Column;

    public override string ToString() => base.ToString();
}
