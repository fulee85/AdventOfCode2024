using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day20;

public class PuzzleSolver : PuzzleSolverBase
{
    public static int leastCheatDistance = 100;
    private int cheatTime;
    public PuzzleSolver(IInput input) : base(input)
    {
    }

    public override string GetFirstSolution()
    {
        cheatTime = 2;
        var maze = new SafeMatrix<char>(input.ToCharMatrix(), '#');
        var startNode = new Node(maze.FindPositions('S').First());
        var endNode = new Node(maze.FindPositions('E').First());
        var trackNodes = maze.FindPositions('.').Select(p => new Node(p)).ToHashSet();
        trackNodes.Add(startNode);
        trackNodes.Add(endNode);
        Dictionary<Position, Node> trackNodesDict = trackNodes.ToDictionary(n => n.Position);

        LinkedList<Node> queue = new LinkedList<Node>();
        HashSet<Node> visitedNodes = new HashSet<Node>();
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
            foreach (var neighbor in currentNode.GetNeighbors(trackNodesDict).Where(n => !visitedNodes.Contains(n)))
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

        List<int> cheats = new List<int>();
        foreach (var wallPosition in maze.FindPositions('#'))
        {
            var upPosition = wallPosition.GetPositionInDirection(Directions.Up);
            var downPosition = wallPosition.GetPositionInDirection(Directions.Down);
            if (maze.GetValue(upPosition) != '#' && maze.GetValue(downPosition) != '#')
            {
                cheats.Add(GetCheatDistance(trackNodesDict[upPosition],trackNodesDict[downPosition]));
            }

            var leftPosition = wallPosition.GetPositionInDirection(Directions.Left);
            var rightPosition = wallPosition.GetPositionInDirection(Directions.Right);
            if (maze.GetValue(leftPosition) != '#' && maze.GetValue(rightPosition) != '#')
            {
                cheats.Add(GetCheatDistance(trackNodesDict[leftPosition], trackNodesDict[rightPosition]));
            }
        }

        return cheats.Count(c => c >= leastCheatDistance).ToString();
    }

    private int GetCheatDistance(Node node1, Node node2)
    {
        return Math.Abs(node1.Distance - node2.Distance) - cheatTime;
    }

    public override string GetSecondSolution()
    {
        cheatTime = 20;
        var maze = new SafeMatrix<char>(input.ToCharMatrix(), '#');
        var startNode = new Node(maze.FindPositions('S').First());
        var endNode = new Node(maze.FindPositions('E').First());
        var trackNodes = maze.FindPositions('.').Select(p => new Node(p)).ToList();
        trackNodes.Add(startNode);
        trackNodes.Add(endNode);
        Dictionary<Position, Node> trackNodesDict = trackNodes.ToDictionary(n => n.Position);

        LinkedList<Node> queue = new LinkedList<Node>();
        HashSet<Node> visitedNodes = new HashSet<Node>();
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
            foreach (var neighbor in currentNode.GetNeighbors(trackNodesDict).Where(n => !visitedNodes.Contains(n)))
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

        List<int> cheats = new List<int>();
        for (int i = 0; i < trackNodes.Count - 1; i++)
        {
            for (int j = i + 1; j < trackNodes.Count; j++)
            {
                var manhattanDistance = trackNodes[i].Position.GetDistanceFrom(trackNodes[j].Position);
                if (manhattanDistance <= 20)
                {
                    var cheatDistance = GetCheatDistance(trackNodes[i], trackNodes[j], manhattanDistance);
                    if (cheatDistance >= leastCheatDistance)
                    {
                        cheats.Add(GetCheatDistance(trackNodes[i], trackNodes[j]));
                    }
                }
            }
        }

        return cheats.Count.ToString();
    }

    private int GetCheatDistance(Node node1, Node node2, int cheatTime)
    {
        return Math.Abs(node1.Distance - node2.Distance) - cheatTime;
    }
}
