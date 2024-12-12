using AdventOfCode2024.Common;
using System.Collections.Generic;

namespace AdventOfCode2024.Day12;

internal class Garden
{
    private readonly SafeMatrix<char> gardenMap;

    public Garden(IInput input)
    {
        gardenMap = new SafeMatrix<char>(input.ToCharMatrix(), '#');
        gardenRegions = new Lazy<List<GardenRegion>>(() => GetGardenRegions().ToList());
    }

    private Lazy<List<GardenRegion>> gardenRegions;
    public List<GardenRegion> GardenRegions => gardenRegions.Value;

    private IEnumerable<GardenRegion> GetGardenRegions()
    {
        Dictionary<Position, GardenPlot> gardenPlots = [];
        for (int row = 0; row < gardenMap.RowCount; row++)
        {
            for (int column = 0; column < gardenMap.ColumnCount; column++)
            {
                gardenPlots.Add(new Position(row, column), new GardenPlot(new Position(row, column), gardenMap[row, column]));
            }
        }

        while (gardenPlots.Count > 0)
        {
            GardenRegion gardenRegion = new();
            var gardenPlot = gardenPlots.First().Value;
            gardenRegion.AddGardenPlot(gardenPlot);
            gardenPlots.Remove(gardenPlot.Position);
            Expand(gardenPlot, gardenPlots, gardenRegion);

            yield return gardenRegion;
        }
    }

    private void Expand(GardenPlot gardenPlot, Dictionary<Position, GardenPlot> gardenPlots, GardenRegion gardenRegion)
    {
        foreach (var direction in DirectionExtensions.EnumerateDirections())
        {
            Position neighbourPosition = gardenPlot.Position.GetPositionInDirection(direction);
            if (gardenMap.GetValue(neighbourPosition) == gardenPlot.PlantType)
            {
                if (gardenPlots.TryGetValue(neighbourPosition, out var neighbourPlot))
                {
                    gardenRegion.AddGardenPlot(neighbourPlot);
                    gardenPlots.Remove(neighbourPosition);
                    Expand(neighbourPlot, gardenPlots, gardenRegion);
                }
            }
            else
            {
                FenceType fenceType = direction switch
                {
                    Directions.Up => FenceType.Top,
                    Directions.Down => FenceType.Bottom,
                    Directions.Left => FenceType.Left,
                    Directions.Right => FenceType.Right,
                    _ => throw new NotImplementedException()
                };

                gardenRegion.AddFence(new Fence(fenceType, gardenPlot.Position));
            }
        }
    }
}
