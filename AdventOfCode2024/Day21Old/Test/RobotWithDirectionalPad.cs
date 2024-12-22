using AdventOfCode2024.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day21Old.Test;

public class RobotWithDirectionalPad : IRobot
{
    private readonly DirectionalKeypad directionalKeypad;
    private readonly IRobot directedRobot;
    private readonly int i;

    public RobotWithDirectionalPad(DirectionalKeypad directionalKeypad, IRobot robot, int i = 0)
    {
        this.directionalKeypad = directionalKeypad;
        this.directedRobot = robot;
        this.i = i;
    }

    public char ActualPosition { get; set; } = 'A';

    public void MoveRobotArm(Directions direction)
    {
        var prev = ActualPosition;
        ActualPosition = directionalKeypad.GetCharAtDirection(ActualPosition, direction);
        if (i == 1)
        {
            //Console.WriteLine($"Robot change position: {prev} -> {ActualPosition}");
        }
    }

    public void PressButton()
    {
        //Console.WriteLine("ButtonPressed: " + ActualPosition);
        if (ActualPosition == 'A')
        {
            directedRobot.PressButton();
        }
        else
        {
            directedRobot.MoveRobotArm(ActualPosition switch
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
