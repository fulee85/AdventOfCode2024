using AdventOfCode2024.Common;
using AdventOfCode2024.Day15.SecondPart;

namespace AdventOfCode2024.Day15;

public class MapItem
{
    public MapItemType Type { get; init; }
    public MapItem(char ch) => Type = ch switch
    {
        '#' => MapItemType.Wall,
        '.' => MapItemType.EmptySpace,
        '@' => MapItemType.Robot,
        'O' => MapItemType.Box,
        _ => throw new NotImplementedException()
    };

    public override string ToString() => Type switch
    {
        MapItemType.Wall => "#",
        MapItemType.EmptySpace => ".",
        MapItemType.Robot => "@",
        MapItemType.Box => "O",
        _ => throw new NotImplementedException(),
    };

    public static MapItem Wall = new MapItem('#');
    public static MapItem EmptySpace = new MapItem('.');

    public virtual bool CanMove(Directions direction, SafeMatrix<MapItem> mapOfItems, List<ScaledUpBox> moveableBoxes)
    {
        if (Type == MapItemType.Wall)
        {
            return false;
        }
        return true;
    }
}
