using AdventOfCode2024.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day6
{
    internal class Map
    {
        private const char OutsideChar = '$';

        private readonly SafeMatrix<char> safeMatrix;
        private readonly Position guardStartPosition;

        public Map(IInput input)
        {
            this.safeMatrix = new SafeMatrix<char>(input.ToCharMatrix(), OutsideChar);
            guardStartPosition = GetGuardStartPosition();
        }

        private Position GetGuardStartPosition()
        {
            for (int row = 0; row < safeMatrix.RowCount; row++)
            {
                for (int column = 0; column < safeMatrix.ColumnCount; column++)
                {
                    if (safeMatrix[row, column] == '^')
                    {
                        return new Position(row, column);
                    }
                }
            }
            return new Position(0, 0);
        }

        public int GetVisitedPositionsCount()
        {
            HashSet<Position> visitedPositions = new();
            Directions guardDirection = Directions.Up;
            Position guardPosition = guardStartPosition;
            while (safeMatrix.GetValue(guardPosition) != OutsideChar)
            {
                visitedPositions.Add(guardPosition);

                var nextPosition = guardPosition.GetPositionInDirection(guardDirection);
                while (safeMatrix.GetValue(nextPosition) == '#')
                {
                    guardDirection = TurnRight(guardDirection);
                    nextPosition = guardPosition.GetPositionInDirection(guardDirection);
                }

                guardPosition = guardPosition.GetPositionInDirection(guardDirection);
            }

            return visitedPositions.Count;
        }

        public int GetPossibleObstaclesCount()
        {
            int possibleObsticlesPositionCount = 0;
            List<Position> emptyPositions = GetEmptyPositions();
            foreach (var newObsticlePosition in emptyPositions)
            {
                PositionWithDirection guardPosition = new PositionWithDirection(guardStartPosition.Row, guardStartPosition.Column, Directions.Up);
                HashSet<PositionWithDirection> VisitedPositionWithDirections = [];
                while (safeMatrix.GetValue(guardPosition) != OutsideChar && !VisitedPositionWithDirections.Contains(guardPosition))
                {
                    VisitedPositionWithDirections.Add(guardPosition);

                    var nextPosition = guardPosition.GetNextPosition();
                    while (safeMatrix.GetValue(nextPosition) == '#' || 
                        (nextPosition.Row == newObsticlePosition.Row && nextPosition.Column == newObsticlePosition.Column))
                    {
                        var guardNextDirection = TurnRight(guardPosition.Direction);
                        guardPosition = guardPosition with { Direction = guardNextDirection };
                        nextPosition = guardPosition.GetNextPosition();
                    }

                    guardPosition = nextPosition;
                }

                if (VisitedPositionWithDirections.Contains(guardPosition))
                {
                    possibleObsticlesPositionCount++;
                }
            }

            return possibleObsticlesPositionCount;
        }

        private List<Position> GetEmptyPositions()
        {
            List<Position> emptyPositions = [];
            
            for (int row = 0; row < safeMatrix.RowCount; row++)
            {
                for (int column = 0; column < safeMatrix.ColumnCount; column++)
                {
                    if (safeMatrix[row,column] == '.')
                    {
                        emptyPositions.Add(new Position(row, column));
                    }
                }
            }

            return emptyPositions;
        }

        private Directions TurnRight(Directions guardDirection) => guardDirection switch
        {
            Directions.Up => Directions.Right,
            Directions.Right => Directions.Down,
            Directions.Down => Directions.Left,
            Directions.Left => Directions.Up,
            _ => throw new Exception()
        };
    }
}
