using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day16;

public class PuzzleSolver : PuzzleSolverBase
{
    SafeMatrix<char> maze;
    public PuzzleSolver(IInput input) : base(input) => maze = new SafeMatrix<char>(input.ToCharMatrix());

    public override string GetFirstSolution()
    {
        CreateNodes();
        CreateEdges();
        var startPosition = maze.FindPositions('S').First();
        var endPosition = maze.FindPositions('E').First();
        var priorityQueue = new List<Node>();
        var startNode = nodes[startPosition];
        var endNode = nodes[endPosition];
        startNode.DistancesFromInDirections[Directions.Right] = 0;
        priorityQueue.Add(startNode);

        while (priorityQueue.Count > 0)
        {
            var currentNode = priorityQueue.MinBy(n => n.Distance);
            if (currentNode == endNode)
            {
                return endNode.Distance.ToString();
            }
            var currentMinDistance = currentNode!.DistancesFromInDirections.MinBy(x => x.Value);
            currentNode.DistancesFromInDirections.Remove(currentMinDistance.Key);

            foreach (var edge in currentNode.Edges)
            {
                var distance = currentMinDistance.Value + edge.Value.PointValue + (currentMinDistance.Key == edge.Key ? 0 : 1000);
                if (!edge.Value.ArrivingNode.DistancesFromInDirections.ContainsKey(edge.Value.ArrivingDirection) || distance <= edge.Value.ArrivingNode.DistancesFromInDirections[edge.Value.ArrivingDirection])
                {
                    edge.Value.ArrivingNode.DistancesFromInDirections[edge.Value.ArrivingDirection] = distance;
                    if (!priorityQueue.Contains(edge.Value.ArrivingNode))
                    {
                        priorityQueue.Add(edge.Value.ArrivingNode);
                    }
                }
            }
        }

        return endNode.Distance.ToString();
    }

    private readonly Dictionary<Position, Node> nodes = [];
    private void CreateNodes()
    {
        foreach (var position in maze.EnumerateAllPositions())
        {
            if (IsJunction(position))
            {
                nodes[position] = new Node { Position = position };
            }
        }
    }

    private bool IsJunction(Position position)
    {
        if (maze.GetValue(position) == '#')
        {
            return false;
        }
        if (maze.GetValue(position) == 'S' || maze.GetValue(position) == 'E')
        {
            return true;
        }
        if (DirectionExtensions.EnumerateDirections().Count(d => maze.GetValue(position.GetPositionInDirection(d)) == '.') >= 3)
        {
            return true;
        }
        return false;
    }
    private void CreateEdges()
    {
        foreach (var node in nodes.Values)
        {
            SetEdges(node);
        }
    }

    private void SetEdges(Node node)
    {
        foreach (var direction in DirectionExtensions.EnumerateDirections())
        {
            if (maze.GetValue(node.Position.GetPositionInDirection(direction)) == '.')
            {
                var edge = CreateEdgeInDirection(node.Position, direction);
                if (edge != null)
                {
                    node.Edges[direction] = edge;
                }
            }
        }
    }

    private Edge? CreateEdgeInDirection(Position position, Directions direction)
    {
        var departureNode = nodes.GetValueOrDefault(position);
        var pointValue = 0;
        var effectiveLength = 0;
        while (true)
        {
            var nextPosition = position.GetPositionInDirection(direction);
            if (maze.GetValue(nextPosition) == '#')
            {
                var directionAfterTurnLeft = direction.GetDirectionAfterTurnLeft();
                var leftPosition = position.GetPositionInDirection(directionAfterTurnLeft);
                if (maze.GetValue(leftPosition) == '#')
                {
                    var directionAfterTurnRight = direction.GetDirectionAfterTurnRight();
                    var rightPosition = position.GetPositionInDirection(directionAfterTurnRight);
                    if (maze.GetValue(rightPosition) == '#')
                    {
                        return null;
                    }
                    else
                    {
                        nextPosition = rightPosition;
                        direction = directionAfterTurnRight;
                        pointValue += 1000;
                    }
                }
                else
                {
                    nextPosition = leftPosition;
                    direction = directionAfterTurnLeft;
                    pointValue += 1000;
                }
            }

            position = nextPosition;
            pointValue++;
            effectiveLength++;

            if (nodes.TryGetValue(position, out var nextNode))
            {
                nextNode.DistancesFromInDirections[direction] = int.MaxValue;
                return new Edge { ArrivingDirection = direction, ArrivingNode = nextNode, PointValue = pointValue, EffectiveLength = effectiveLength, DepartureNode = departureNode! };
            }
        }
    }

    public override string GetSecondSolution()
    {
        CreateNodes();
        CreateEdges();
        var startPosition = maze.FindPositions('S').First();
        var endPosition = maze.FindPositions('E').First();
        var queue = new Queue();
        var distances = new Distances();
        var startNode = nodes[startPosition];
        var endNode = nodes[endPosition];
        distances.SetDistance(Directions.Right, startNode, 0);
        queue.Enqueue(Directions.Right, startNode);
        var visitedVertices = new HashSet<(Directions, Node)>();

        while (queue.IsNotEmpty())
        {
            (var direction, var currentNode) = queue.GetMinimumVertice(distances);
            queue.Remove((direction, currentNode));
            visitedVertices.Add((direction,currentNode));
            int currentDistance = distances.GetDistance(direction, currentNode);
            foreach (var edge in currentNode.Edges.Where(e => !visitedVertices.Contains((e.Value.ArrivingDirection,e.Value.ArrivingNode))))
            {
                var distance = currentDistance + edge.Value.PointValue + (direction == edge.Key ? 0 : 1000);
                if (distance < distances.GetDistance(edge.Value.ArrivingDirection, edge.Value.ArrivingNode))
                {
                    distances.SetDistance(edge.Value.ArrivingDirection, edge.Value.ArrivingNode, distance);
                }
                queue.Enqueue(edge.Value.ArrivingDirection, edge.Value.ArrivingNode);
            }
        }

        HashSet<Edge> pathEdges = new HashSet<Edge>();
        pathEdges.UnionWith(AllPathEdges(endNode, distances));

        return pathEdges.Sum(e => e.EffectiveLength).ToString();
    }

    private IEnumerable<Edge> AllPathEdges(Node node, Distances distances)
    {
        List<Edge> previousEdges = distances.GetPreviousEdges(node).ToList();
        HashSet<Edge> edges = new HashSet<Edge>();
        foreach (var edge in previousEdges)
        {
            edges.Add(edge);
            edges.UnionWith(AllPathEdges(edge.ArrivingNode, distances));
        }

        return edges;
    }
}
