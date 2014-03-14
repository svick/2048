using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace _2048
{
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    public class Board
    {
        public static readonly int Size = 4;

        private readonly Random random;

        public ObservableCollection<ObservableCollection<int?>> Tiles { get; private set; }

        public Board()
        {
            Tiles = new ObservableCollection<ObservableCollection<int?>>
            {
                new ObservableCollection<int?> { 0, 0, null, null },
                new ObservableCollection<int?> { null, null, null, null },
                new ObservableCollection<int?> { null, null, null, null },
                new ObservableCollection<int?> { null, null, null, null }
            };
            random = new Random();
        }

        public void Move(Direction direction)
        {
            var xs = Enumerable.Range(0, Size);
            if (direction == Direction.Down)
                xs = xs.Reverse();
            var ys = Enumerable.Range(0, Size);
            if (direction == Direction.Right)
                ys = ys.Reverse();

            foreach (int x in xs)
            {
                foreach (int y in ys)
                {
                    var target = GetTarget(x, y, direction);

                    if (target == null)
                        continue;

                    int? sourceValue = Tiles[x][y];
                    int? targetValue = Tiles[target.Item1][target.Item2];

                    if (targetValue != null && targetValue != sourceValue)
                        continue;

                    int? targetNewValue;

                    if (targetValue == null)
                        targetNewValue = sourceValue;
                    else
                        targetNewValue = targetValue + 1;

                    Tiles[x][y] = null;
                    Tiles[target.Item1][target.Item2] = targetNewValue;
                }
            }

            var incomingPosition = GetRandomIncomingPosition(direction);
            if (incomingPosition != null)
            {
                int maxValue = Tiles.Max(row => row.Max()).Value;
                int incomingValue = random.Next(0, maxValue);
                Tiles[incomingPosition.Item1][incomingPosition.Item2] = incomingValue;
            }
        }

        private static Tuple<int, int> GetTarget(int x, int y, Direction direction)
        {
            int xTarget = x;
            int yTarget = y;

            switch (direction)
            {
            case Direction.Up:
                xTarget -= 1;
                break;
            case Direction.Right:
                yTarget += 1;
                break;
            case Direction.Down:
                xTarget += 1;
                break;
            case Direction.Left:
                yTarget -= 1;
                break;
            }

            if (xTarget < 0 || xTarget >= Size || yTarget < 0 || yTarget >= Size)
                return null;

            return Tuple.Create(xTarget, yTarget);
        }

        private Tuple<int, int> GetRandomIncomingPosition(Direction direction)
        {
            IEnumerable<Tuple<int, int>> positions;

            if (direction == Direction.Up || direction == Direction.Down)
            {
                int row = direction == Direction.Down ? 0 : Size - 1;
                positions = Enumerable.Range(0, Size).Select(column => Tuple.Create(row, column));
            }
            else
            {
                int column = direction == Direction.Right ? 0 : Size - 1;
                positions = Enumerable.Range(0, Size).Select(row => Tuple.Create(row, column));
            }

            var validPositions = positions.Where(pos => Tiles[pos.Item1][pos.Item2] == null).ToList();

            if (!validPositions.Any())
                return null;

            return validPositions[random.Next(0, validPositions.Count)];
        }
    }
}