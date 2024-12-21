using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day21.Test;

public class RobotWithNumericKeypad : IRobot
{
    private readonly NumericKeypad numericKeypad;

    public RobotWithNumericKeypad(NumericKeypad numericKeypad) => this.numericKeypad = numericKeypad;

    public char ActualPosition { get; set; } = '3';

    public void MoveRobotArm(Directions direction)
    {
        char prev = ActualPosition;
        ActualPosition = numericKeypad.GetCharAtDirection(ActualPosition, direction);
        Console.WriteLine();
        Console.WriteLine($"Robot change position: {prev} -> {ActualPosition}");
    }

    public void PressButton()
    {
        Console.WriteLine();
        Console.WriteLine("ButtonPressed: " + ActualPosition);
    }
}
