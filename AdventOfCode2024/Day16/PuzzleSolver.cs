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
        if (DirectionExtensions.EnumerateDirections().Count(d => maze.GetValue(position.GetPositionInDirection(d)) != '#') >= 3)
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

    private readonly List<Edge> edges = new List<Edge>();

    private void SetEdges(Node node)
    {
        foreach (var direction in DirectionExtensions.EnumerateDirections())
        {
            if (maze.GetValue(node.Position.GetPositionInDirection(direction)) != '#')
            {
                var edge = CreateEdgeInDirection(node.Position, direction);
                if (edge != null)
                {
                    node.Edges[direction] = edge;
                    edges.Add(edge);
                }
            }
        }
    }

    private Edge? CreateEdgeInDirection(Position position, Directions direction)
    {
        var departureNode = nodes.GetValueOrDefault(position);
        var departureDirection = direction;
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
                return new Edge {
                    ArrivingDirection = direction, 
                    ArrivingNode = nextNode, 
                    PointValue = pointValue, 
                    EffectiveLength = effectiveLength - 1, 
                    DepartureNode = departureNode!,
                    DepartureDirection = departureDirection
                };
            }
        }
    }

    public override string GetSecondSolution()
    {
        CreateNodes();
        CreateEdges();
        CreateVertices();
        CreateVertexEdges();

        var queue = new Queue();
        vertices.ForEach(queue.Enqueue);
        var visitedVertices = new HashSet<Vertex>();

        while (queue.IsNotEmpty())
        {
            var minVertex = queue.GetMinimumVertex();
            queue.Remove(minVertex);
            visitedVertices.Add(minVertex);
            int currentDistance = minVertex.Distance;
            if (currentDistance == int.MaxValue)
            {
                break;
            }
            foreach (var edge in minVertex.OutEdges.Where(e => !visitedVertices.Contains(e.Value.To)))
            {
                var distance = currentDistance + edge.Value.PointValue + (minVertex.InDirection == edge.Key ? 0 : 1000);
                if (distance == edge.Value.To.Distance)
                {
                    edge.Value.To.PreviousEdges.Add(edge.Value);
                    edge.Value.To.Distance = distance;
                }
                else if (distance < edge.Value.To.Distance)
                {
                    edge.Value.To.PreviousEdges = new List<VertexEdge> { edge.Value };
                    edge.Value.To.Distance = distance;
                }
            }
        }

        var endPosition = maze.FindPositions('E').First();
        var endNode = nodes[endPosition];
        var endVertices = vertices.Where(v => v.Node == endNode).ToList();
        var minDist = endVertices.Min(v => v.Distance);
        var minEndVertices = endVertices.Where(v => v.Distance == minDist).ToList();
        HashSet<VertexEdge> pathEdges = new HashSet<VertexEdge>();
        foreach (var minEndVertex in minEndVertices)
        {
            pathEdges.UnionWith(AllPathEdges(minEndVertex));
        }
        HashSet<Node> pathNodes = pathEdges.Select(p => p.From.Node).ToHashSet();
        pathNodes.UnionWith(pathEdges.Select(p => p.To.Node));
        pathNodes.ToList().ForEach(Console.WriteLine);
        return (pathEdges.Sum(e => e.EffectiveLength) + pathNodes.Count).ToString();
    }

    private void CreateVertexEdges()
    {
        foreach (var edge in edges)
        {
            var endVertex = vertices.Find(v => v.InDirection == edge.ArrivingDirection && v.Node == edge.ArrivingNode);
            foreach (var fromVertex in vertices.FindAll(v => v.Node == edge.DepartureNode && v.InDirection != edge.DepartureDirection.GetInvertDirection()))
            {
                var vertexEdge = new VertexEdge {
                    From = fromVertex,
                    To = endVertex!,
                    EffectiveLength = edge.EffectiveLength,
                    PointValue = edge.PointValue,
                };
                endVertex!.InEdges.Add(vertexEdge);
                fromVertex.OutEdges[edge.DepartureDirection] = vertexEdge;
            }
        }
    }

    private readonly List<Vertex> vertices = new List<Vertex>();
    private Vertex firstVertex;
    private void CreateVertices()
    {
        foreach (var node in nodes)
        {
            foreach (var edge in node.Value.Edges)
            {
                vertices.Add(new Vertex
                {
                    InDirection = edge.Key.GetInvertDirection(),
                    Node = node.Value,
                });
            }
        }
        var startPosition = maze.FindPositions('S').First();
        var startNode = nodes[startPosition];
        firstVertex = new Vertex { InDirection = Directions.Right, Distance = 0, Node = startNode };
        vertices.Add(firstVertex);
    }

    private IEnumerable<VertexEdge> AllPathEdges(Vertex vertex)
    {
        List<VertexEdge> previousEdges = vertex.PreviousEdges;
        HashSet<VertexEdge> edges = new HashSet<VertexEdge>();
        foreach (var edge in previousEdges)
        {
            edges.Add(edge);
            edges.UnionWith(AllPathEdges(edge.From));
        }

        return edges;
    }
}
