using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day23;

public class PuzzleSolver : PuzzleSolverBase
{
    public PuzzleSolver(IInput input) : base(input)
    {
    }

    public override string GetFirstSolution()
    {
        Dictionary<string, HashSet<string>> LANGraph = CreateGraph();
        HashSet<InterConnectedComputers> interconnectedComputers = new();
        foreach (var computer in LANGraph.Where(kv => kv.Key.StartsWith('t')))
        {
            var listOfConnectedComps = computer.Value.ToArray();
            for (int i = 0; i < listOfConnectedComps.Length - 1; i++)
            {
                for (int j = i + 1; j < listOfConnectedComps.Length; j++)
                {
                    if (LANGraph[listOfConnectedComps[i]].Contains(listOfConnectedComps[j]))
                    {
                        interconnectedComputers
                            .Add(new InterConnectedComputers(computer.Key, listOfConnectedComps[i], listOfConnectedComps[j]));
                    }
                }
            }
        }

        return interconnectedComputers.Count.ToString();
    }

    private Dictionary<string, HashSet<string>> CreateGraph()
    {
        Dictionary<string, HashSet<string>> graph = new();
        foreach (var line in input)
        {
            var computers = line.Split('-');
            AddToGraph(computers[0], computers[1], graph);
            AddToGraph(computers[1], computers[0], graph);
        }

        return graph;
    }

    private void AddToGraph(string v1, string v2, Dictionary<string, HashSet<string>> graph)
    {
        if (graph.TryGetValue(v1, out var connectedPCs))
        {
            connectedPCs.Add(v2);
        }
        else
        {
            graph[v1] = new HashSet<string>() { v2 };
        }
    }

    public override string GetSecondSolution()
    {
        Node.LANGraph = CreateGraph();
        var startNode = new Node([], Node.LANGraph.Keys.ToList());
        HashSet<Node> visitedNodes = new HashSet<Node>();
        Stack<Node> nodesStack = new();
        nodesStack.Push(startNode);

        while (nodesStack.Count > 0)
        {
            var node = nodesStack.Pop();
            visitedNodes.Add(node);
            foreach (var neighborNode in node.GetNeighborNodes())
            {
                if (!visitedNodes.Contains(neighborNode))
                {
                    nodesStack.Push(neighborNode);
                }
            }
        }

        return visitedNodes.MaxBy(n => n.Depth)!.Label;
    }
}
