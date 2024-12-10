using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day10;

public class PuzzleSolver : PuzzleSolverBase
{
    private readonly SafeMatrix<int> safeMatrix;
    private readonly List<Directions> directions = [Directions.Up, Directions.Down, Directions.Left, Directions.Right];
    public PuzzleSolver(IInput input) : base(input)
    {
        safeMatrix = new SafeMatrix<int>(input.ToIntMatrix());
    }

    public override string GetFirstSolution()
    {
        var zeroPositions = safeMatrix.FindPositions(0);

        var scoresSum = 0;
        foreach (var zero in zeroPositions)
        {
            var reachableNinePositions = new HashSet<Position>();
            var queue = new Queue<Position>();
            queue.Enqueue(zero);
            while (queue.Count > 0)
            {
                var actPosition = queue.Dequeue();
                var actValue = safeMatrix.GetValue(actPosition);
                foreach (var nextPosition in directions.Select(actPosition.GetPositionInDirection))
                {
                    var nextPositionValue = safeMatrix.GetValue(nextPosition);
                    if (nextPositionValue - actValue == 1)
                    {
                        if (nextPositionValue == 9)
                        {
                            reachableNinePositions.Add(nextPosition);
                        }
                        else
                        {
                            queue.Enqueue(nextPosition);
                        }
                    }
                }
            }

            scoresSum += reachableNinePositions.Count;
        }

        return scoresSum.ToString();
    }
    public override string GetSecondSolution()
    {
        var zeroPositions = safeMatrix.FindPositions(0);

        var scoresSum = 0;
        foreach (var zero in zeroPositions)
        {
            var reachableNinePositions = new List<Position>();
            var queue = new Queue<Position>();
            queue.Enqueue(zero);
            while (queue.Count > 0)
            {
                var actPosition = queue.Dequeue();
                var actValue = safeMatrix.GetValue(actPosition);
                foreach (var nextPosition in directions.Select(actPosition.GetPositionInDirection))
                {
                    var nextPositionValue = safeMatrix.GetValue(nextPosition);
                    if (nextPositionValue - actValue == 1)
                    {
                        if (nextPositionValue == 9)
                        {
                            reachableNinePositions.Add(nextPosition);
                        }
                        else
                        {
                            queue.Enqueue(nextPosition);
                        }
                    }
                }
            }

            scoresSum += reachableNinePositions.Count;
        }

        return scoresSum.ToString();
    }
}
