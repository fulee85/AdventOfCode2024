using AdventOfCode2024.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day21Old.Test;

public class Human
{
    private readonly IRobot directedRobot;

    public Human(IRobot directedRobot)
    {
        this.directedRobot = directedRobot;
    }
    public void PressButtons(string input)
    {
        foreach (var ch in input)
        {
            Console.Write(ch);
            if (ch == 'A')
            {
                directedRobot.PressButton();
            }
            else
            {
                directedRobot.MoveRobotArm(ch switch
                {
                    '^' => Directions.Up,
                    'v' => Directions.Down,
                    '>' => Directions.Right,
                    '<' => Directions.Left,
                    _ => throw new NotImplementedException(),
                });
            }
        }
    }
}
