using AdventOfCode2024.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day15.SecondPart;

public class ScaledUpRobot : MapItem
{
    private Position position;

    public ScaledUpRobot(Position position) : base('@') => this.position = position;

    public bool MoveToDirection(Directions direction, SafeMatrix<MapItem> mapOfItems)
    {
        var nextPosition = position.GetPositionInDirection(direction);
        var nextItemInDirection = mapOfItems.GetValue(nextPosition);
        if (nextItemInDirection.Type == MapItemType.Wall)
        {
            return false;
        }
        else if (nextItemInDirection.Type == MapItemType.EmptySpace)
        {
            mapOfItems.SetValue(position, EmptySpace);
            mapOfItems.SetValue(nextPosition, this);
            position = nextPosition;
            return true;
        }
        else //box
        {
            List<ScaledUpBox> scaledUpBoxes = new();
            var nextMoveableItem = nextItemInDirection as ScaledUpBox;
            var canMove = nextMoveableItem!.CanMove(direction, mapOfItems, scaledUpBoxes);
            if (canMove is true)
            {
                MoveBoxes(scaledUpBoxes, direction, mapOfItems);
                mapOfItems.SetValue(position, EmptySpace);
                mapOfItems.SetValue(nextPosition, this);
                position = nextPosition;
                return true;
            }

            return false;
        }
    }

    private void MoveBoxes(List<ScaledUpBox> scaledUpBoxes, Directions direction, SafeMatrix<MapItem> mapOfItems)
    {
        foreach (var box in scaledUpBoxes)
        {
            box.MoveToDirection(direction,mapOfItems);
        }
    }
}
