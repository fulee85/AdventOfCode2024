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
                return new Edge { ArrivingDirection = direction, ArrivingNode = nextNode, PointValue = pointValue, EffectiveLength = effectiveLength };
            }
        }
    }

    public override string GetSecondSolution()
    {
        CreateNodes();
        CreateEdges();
        var startPosition = maze.FindPositions('S').First();
        var endPosition = maze.FindPositions('E').First();
        var paths = new LinkedList<Path>();
        var startNode = nodes[startPosition];
        var endNode = nodes[endPosition];
        startNode.DistancesFromInDirections[Directions.Right] = 0;
        paths.AddFirst(new Path { Direction = Directions.Right, Node = startNode, VisitedNodes = [], Score = 0, Edges = [] });

        List<Path> shortestPaths = [];
        while (paths.Count > 0)
        {
            var path = paths.GetMin(p => p.Score);
            paths.Remove(path!);

            if (path.Value.Node == endNode)
            {
                while (path.Value.Node == endNode)
                {
                    shortestPaths.Add(path.Value);
                    path = paths.GetMin(p => p.Score);
                    paths.Remove(path!);
                }
                break;
            }

            foreach (var edge in path!.Value.Node.Edges)
            {
                if (!path.Value.VisitedNodes.Contains(edge.Value.ArrivingNode))
                {
                    var score = path.Value.Score + edge.Value.PointValue + (edge.Key == path.Value.Direction ? 0 : 1000);
                    var oldPath = paths.Find(p => p.Direction == edge.Value.ArrivingDirection && p.Node == edge.Value.ArrivingNode);
                    if (oldPath != null)
                    {
                        if (oldPath.Value.Score > score)
                        {
                            paths.Remove(oldPath);
                            paths.AddLast(new Path()
                            {
                                Direction = edge.Value.ArrivingDirection,
                                Node = edge.Value.ArrivingNode,
                                VisitedNodes = new HashSet<Node>(path.Value.VisitedNodes) { path.Value.Node },
                                Score = score,
                                Edges = new List<Edge>(path.Value.Edges) { edge.Value }
                            });
                        }
                        else if (oldPath.Value.Score == score)
                        {
                            paths.AddLast(new Path()
                            {
                                Direction = edge.Value.ArrivingDirection,
                                Node = edge.Value.ArrivingNode,
                                VisitedNodes = new HashSet<Node>(path.Value.VisitedNodes) { path.Value.Node },
                                Score = score,
                                Edges = new List<Edge>(path.Value.Edges) { edge.Value }
                            });
                        }
                    }
                    else
                    {
                        paths.AddLast(new Path()
                        {
                            Direction = edge.Value.ArrivingDirection,
                            Node = edge.Value.ArrivingNode,
                            VisitedNodes = new HashSet<Node>(path.Value.VisitedNodes) { path.Value.Node },
                            Score = score,
                            Edges = new List<Edge>(path.Value.Edges) { edge.Value }
                        });
                    }
                }
            }
        }

        return shortestPaths.SelectMany(s => s.Edges).ToHashSet().Sum(e => e.EffectiveLength).ToString();
    }
}
