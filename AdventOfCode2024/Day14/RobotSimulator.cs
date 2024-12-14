namespace AdventOfCode2024.Day14;

public class RobotSimulator
{
    private List<int> RobotCountInSectors = [0, 0, 0, 0];
    private int widthHalfPoint = PuzzleSolver.Width / 2;
    private int heightHalfPoint = PuzzleSolver.Height / 2;

    public void Simulate100Move(Robot robot)
    {
        var position = MoveRobot(robot, 100);
        int quarter = position switch
        {
            _ when position.xEnd < widthHalfPoint && position.yEnd < heightHalfPoint => 0,
            _ when position.xEnd > widthHalfPoint && position.yEnd < heightHalfPoint => 1,
            _ when position.xEnd < widthHalfPoint && position.yEnd > heightHalfPoint => 2,
            _ when position.xEnd > widthHalfPoint && position.yEnd > heightHalfPoint => 3,
            // Add additional cases here if needed
            _ => -1
        };

        if (quarter != -1)
        {
            RobotCountInSectors[quarter]++;
        }
    }

    public int GetSafetyFactor() => RobotCountInSectors.Aggregate(1, (a, b) => a * b);

    public (int xEnd, int yEnd) MoveRobot(Robot robot, int stepsCount)
    {
        var xEnd = (robot.X + (robot.Vx * stepsCount)) % PuzzleSolver.Width;
        if (xEnd < 0)
        {
            xEnd += PuzzleSolver.Width;
        }
        var yEnd = (robot.Y + (robot.Vy * stepsCount)) % PuzzleSolver.Height;
        if (yEnd < 0)
        {
            yEnd += PuzzleSolver.Height;
        }

        return (xEnd, yEnd);
    }
}
