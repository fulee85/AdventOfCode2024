namespace AdventOfCode2024.Day23;

public class InterConnectedComputers
{
    private readonly string compNames;

    public InterConnectedComputers(params string[] compNames)
    {
        Count = compNames.Length;
        this.compNames = string.Join(',', compNames.Order());
    }

    public string ComputersNames => compNames;

    public int Count { get; }

    public override int GetHashCode() => compNames.GetHashCode();

    public override bool Equals(object? obj)
    {
        if (obj is InterConnectedComputers computers)
        {
            return this.compNames == computers.compNames;
        }
        return false;
    }
}
