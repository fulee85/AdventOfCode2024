using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day18;

public class PuzzleSolver : PuzzleSolverBase
{
    public static int maxPosition = 70;
    public static int corruptedPositionsCount = 1024;
    private List<Position> corruptedPositions;

    public PuzzleSolver(IInput input) : base(input)
    {
        corruptedPositions = input.Select(l => l.Split(',').Select(int.Parse).ToArray()).Select(a => new Position(a[0], a[1])).ToList();
    }

    public override string GetFirstSolution()
    {
        List<List<Node>> nodes = Enumerable.Range(0, maxPosition + 1).Select(i => Enumerable.Range(0, maxPosition + 1).Select(j => new Node()).ToList()).ToList();
        SafeMatrix<Node> grid = new SafeMatrix<Node>(nodes);
        SetNeighboursAndDistances(grid);

        foreach (var position in corruptedPositions.Take(corruptedPositionsCount))
        {
            grid.GetValue(position).IsCorrupted = true;
        }

        LinkedList<Node> queue = new LinkedList<Node>();
        HashSet<Node> visitedNodes = new HashSet<Node>();
        Node startNode = grid[0, 0];
        Node endNode = grid[maxPosition, maxPosition];
        startNode.Distance = 0;
        queue.AddLast(startNode);
        while (queue.Count > 0)
        {
            var currentNode = queue.GetAndRemoveMin(n => n.Distance);
            if (currentNode == endNode)
            {
                break;
            }

            visitedNodes.Add(currentNode);

            var distance = currentNode.Distance + 1;
            foreach (var neighbor in currentNode.GetNeighbors().Where(n => !visitedNodes.Contains(n)))
            {
                if (distance < neighbor.Distance)
                {
                    neighbor.Distance = distance;
                    if (!neighbor.IsInTheQueue)
                    {
                        queue.AddLast(neighbor);
                        neighbor.IsInTheQueue = true;
                    }
                }
            }
        }

        return grid[maxPosition,maxPosition].Distance.ToString();
    }

    public override string GetSecondSolution()
    {
        for (int i = corruptedPositionsCount; i < corruptedPositions.Count; i++)
        {
            corruptedPositionsCount = i;
            var firstSolution = GetFirstSolution();
            if (firstSolution == int.MaxValue.ToString())
            {
                break;
            }
        }

        var corruptedPositionThatBlock = corruptedPositions[corruptedPositionsCount - 1];
        return $"{corruptedPositionThatBlock.Row},{corruptedPositionThatBlock.Column}";
    }

    private void SetNeighboursAndDistances(SafeMatrix<Node> grid)
    {
        foreach (var position in grid.EnumerateAllPositions())
        {
            var nodeInPosition = grid.GetValue(position);
            nodeInPosition.Distance = int.MaxValue;
            nodeInPosition.SetNeighbors(position.GetNeighbourPositions().Select(grid.GetValue));
        }
    }
}
