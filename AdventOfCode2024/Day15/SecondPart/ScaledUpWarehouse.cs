using AdventOfCode2024.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day15.SecondPart;

public class ScaledUpWarehouse
{
    private readonly SafeMatrix<MapItem> mapOfItems;
    private readonly ScaledUpRobot robot;
    private readonly List<ScaledUpBox> boxes = new List<ScaledUpBox>();

    public ScaledUpWarehouse(List<string> input)
    {
        List<List<MapItem>> mapItems = new List<List<MapItem>>();
        for (int row = 0; row < input.Count; row++)
        {
            var rowList = new List<MapItem>();
            for (int column = 0; column < input[0].Length; column++)
            {
                char ch = input[row][column];
                if (ch == '#')
                {
                    rowList.AddRange([MapItem.Wall, MapItem.Wall]);
                }
                else if (ch == '.')
                {
                    rowList.AddRange([MapItem.EmptySpace, MapItem.EmptySpace]);
                }
                else if (ch == '@')
                {
                    robot = new ScaledUpRobot(new Position(row, rowList.Count));
                    rowList.AddRange([robot, MapItem.EmptySpace]);
                }
                else
                {
                    ScaledUpBox scaledUpBox = new ScaledUpBox(new Position(row, rowList.Count));
                    boxes.Add(scaledUpBox);
                    rowList.AddRange([scaledUpBox, scaledUpBox]);
                }
            }
            mapItems.Add(rowList);
        }

        this.mapOfItems = new SafeMatrix<MapItem>(mapItems);
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

    public int GetSumOfScaledUpBoxGPSValues() => boxes.Sum(b => b.GPS);

    public override string ToString() => mapOfItems.ToString();
}
