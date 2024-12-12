using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day12;

internal class GardenPlot
{
    public GardenPlot(Position position, char plantType)
    {
        Position = position;
        PlantType = plantType;
    }

    public Position Position { get; }
    public char PlantType { get; }

    public override string ToString() => $"'{PlantType}' {Position.Row};{Position.Column}";
}
