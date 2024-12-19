
namespace AdventOfCode2024.Day18;

public class Node
{
    List<Node> neighborNodes = new List<Node>();

    public int Distance { get; set; }

    public void SetNeighbors(IEnumerable<Node> enumerable) => neighborNodes = enumerable.Where(n => n != null).ToList();
    internal IEnumerable<Node> GetNeighbors() => neighborNodes.Where(n => !n.IsCorrupted);

    public bool IsCorrupted { get; set; }

    public bool IsInTheQueue { get; set; }
}
