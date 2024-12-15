using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day15;

public class Warehouse
{
    private readonly SafeMatrix<MapItem> mapOfItems;
    private readonly Robot robot;
    private readonly List<Box> boxes = new List<Box>();

    public Warehouse(List<string> input)
    {
        List<List<MapItem>> mapItems = new List<List<MapItem>>();
        for (int i = 0; i < input.Count; i++)
        {
            var row = new List<MapItem>();
            for (int j = 0; j < input[0].Length; j++)
            {
                var mapItem = input[i][j] switch
                {
                    '.' or '#' => new MapItem(input[i][j]),
                    'O' => CreateAndAddBox(new Position(i, j)),
                    '@' => robot = new Robot(new Position(i, j)),
                    _ => throw new NotImplementedException(),
                };

                row.Add(mapItem);
            }
            mapItems.Add(row);
        }

        this.mapOfItems = new SafeMatrix<MapItem>(mapItems);
    }

    private MapItem CreateAndAddBox(Position position)
    {
        var box = new Box(position);
        boxes.Add(box);
        return box;
    }

    public bool MoveRobot(char ch)
    {
        Directions direction = ch switch
        {
            '^' => Directions.Up,
            'v' => Directions.Down,
            '>' => Directions.Right,
            '<' => Directions.Left,
            _ => throw new NotImplementedException(),
        };
        return robot.MoveToDirection(direction, mapOfItems);
    }

    public int GetSumOfBoxGPSValues() => boxes.Sum(b => b.GPS);

    public override string ToString() => mapOfItems.ToString();
}
