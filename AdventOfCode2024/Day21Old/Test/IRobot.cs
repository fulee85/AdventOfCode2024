using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day21Old.Test;

public interface IRobot
{
    public void MoveRobotArm(Directions direction);

    public void PressButton();
}