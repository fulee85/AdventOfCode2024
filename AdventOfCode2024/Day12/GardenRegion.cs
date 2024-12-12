using AdventOfCode2024.Common;
using System;

namespace AdventOfCode2024.Day12;

internal class GardenRegion
{
    private readonly List<GardenPlot> gardenPlots = [];
    internal void AddGardenPlot(GardenPlot gardenPlot) => gardenPlots.Add(gardenPlot);
    internal int GetFencingCost() => gardenPlots.Count * fences.Count;

    private readonly List<Fence> fences = [];
    public void AddFence(Fence fence)
    {
        fences.Add(fence);
    }

    internal int GetDiscountedFencingCost() => gardenPlots.Count * GetFenceSidesCount();

    private int GetFenceSidesCount()
    {
        HashSet<Fence> fencesSet = fences.ToHashSet();
        int count = 0;
        while (fencesSet.Count > 0)
        {
            var fence = fencesSet.First();
            fencesSet.Remove(fence);
            if (fence.FenceType is FenceType.Top or FenceType.Bottom)
            {
                RemoveFencesToDirection(Directions.Left, fence, fencesSet);
                RemoveFencesToDirection(Directions.Right, fence, fencesSet);
            }
            else
            {
                RemoveFencesToDirection(Directions.Up, fence, fencesSet);
                RemoveFencesToDirection(Directions.Down, fence, fencesSet);
            }
            count++;
        }

        return count;
    }

    private void RemoveFencesToDirection(Directions direction, Fence fence, HashSet<Fence> fencesSet)
    {
        int removedElementCount = 0;
        Position searchPosition = fence.Position;
        do
        {
            searchPosition = searchPosition.GetPositionInDirection(direction);
            removedElementCount = fencesSet.RemoveWhere(f => f.FenceType == fence.FenceType && f.Position == searchPosition);
        }
        while (removedElementCount > 0);
    }
}
