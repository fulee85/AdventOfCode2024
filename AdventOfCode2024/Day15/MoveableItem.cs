using AdventOfCode2024.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day15;

public class MoveableItem(Position position, char ch) : MapItem(ch)
{
    public Position position = position;

    public bool MoveToDirection(Directions direction, SafeMatrix<MapItem> safeMatrix)
    {
        var nextPosition = position.GetPositionInDirection(direction);
        var nextItemInDirection = safeMatrix.GetValue(nextPosition);
        if (nextItemInDirection.Type == MapItemType.Wall)
        {
            return false;
        }
        else if (nextItemInDirection.Type == MapItemType.EmptySpace)
        {
            safeMatrix[position.Row, position.Column] = nextItemInDirection;
            safeMatrix[nextPosition.Row, nextPosition.Column] = this;
            position = nextPosition;
            return true;
        }
        else
        {
            var nextMoveableItem = nextItemInDirection as MoveableItem;
            var canMove = nextMoveableItem!.MoveToDirection(direction, safeMatrix);
            if (canMove is true)
            {
                safeMatrix[position.Row, position.Column] = safeMatrix.GetValue(nextPosition);
                safeMatrix[nextPosition.Row, nextPosition.Column] = this;
                position = nextPosition;
                return true;
            }

            return false;
        }
    }

    public override string ToString() => base.ToString();
}
