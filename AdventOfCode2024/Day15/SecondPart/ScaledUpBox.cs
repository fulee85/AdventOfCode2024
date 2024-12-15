using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day15.SecondPart;

public class ScaledUpBox : MapItem
{
    private Position positionFirst;
    private Position positionSecond;

    public ScaledUpBox(Position positionFirst) : base('O')
    {
        this.positionFirst = positionFirst;
        this.positionSecond = positionFirst.GetPositionInDirection(Directions.Right);
    }
    public int GPS => positionFirst.Row * 100 + positionFirst.Column;

    public override string ToString() => "+";
    internal void MoveToDirection(Directions direction, SafeMatrix<MapItem> mapOfItems)
    {
        if (direction == Directions.Left) // <-
        {
            mapOfItems.SetValue(positionSecond, MapItem.EmptySpace);
        }
        else if (direction == Directions.Right) // ->
        {
            mapOfItems.SetValue(positionFirst, MapItem.EmptySpace);
        }
        else
        {
            mapOfItems.SetValue(positionFirst, MapItem.EmptySpace);
            mapOfItems.SetValue(positionSecond, MapItem.EmptySpace);
        }
        positionFirst = positionFirst.GetPositionInDirection(direction);
        positionSecond = positionSecond.GetPositionInDirection(direction);
        mapOfItems.SetValue(positionFirst, this);
        mapOfItems.SetValue(positionSecond, this);
    }

    public override bool CanMove(Directions direction, SafeMatrix<MapItem> mapOfItems, List<ScaledUpBox> moveableBoxes)
    {
        bool canMove = false;
        if (direction == Directions.Left) // <-
        {
            var nextItem = mapOfItems.GetValue(positionFirst.GetPositionInDirection(direction));
            canMove = nextItem.CanMove(direction, mapOfItems, moveableBoxes);
        }
        else if (direction == Directions.Right) // ->
        {
            var nextItem = mapOfItems.GetValue(positionSecond.GetPositionInDirection(direction));
            canMove = nextItem.CanMove(direction, mapOfItems, moveableBoxes);
        }
        else
        {
            var nextItemFirst = mapOfItems.GetValue(positionFirst.GetPositionInDirection(direction));
            var nextItemSecond = mapOfItems.GetValue(positionSecond.GetPositionInDirection(direction));
            canMove = nextItemFirst.CanMove(direction, mapOfItems, moveableBoxes) && nextItemSecond.CanMove(direction, mapOfItems, moveableBoxes);
        }

        if (canMove && !moveableBoxes.Contains(this))
        {
            moveableBoxes.Add(this);
        }
        return canMove;
    }
}
