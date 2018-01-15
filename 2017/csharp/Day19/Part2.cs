// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day19
{
    public class Part2
    {
        private readonly ITestOutputHelper _output;

        public Part2(ITestOutputHelper output)
        {
            _output = output;
        }

        private enum Direction
        {
            Up = 0,
            Down = 1,
            Left = 2,
            Right = 3
        }

        [Fact]
        public void Tests()
        {
            string[] input =
            {
                "     |          ",
                "     |  +--+    ",
                "     A  |  C    ",
                " F---|----E|--+ ",
                "     |  |  |  D ",
                "     +B-+  +--+ ",
                "                ",
            };

            TotalStepsOf(input, 38);
        }

        private static void TotalStepsOf(string[] input, int expected)
        {
            Assert.Equal(expected, Compute(input));
        }

        private const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private static int Compute(string[] grid)
        {
            int steps = 0;

            var pos = GetStartPosition(grid);
            while (pos != null)
            {
                (int x, int y, Direction d) = pos.Value;
                var scanned = grid[x][y];
                if (IsEndOfLine(scanned))
                    pos = GetNextDirection(grid, pos.Value);
                else if (IsVerticalTraverse(d))
                    pos = ScanCol(grid, y, $"{Letters}+", d, Increment(x, d), false);
                else if (IsHorizontalTraverse(d))
                    pos = ScanRow(grid, x, $"{Letters}+", d, Increment(y, d), false);

                if (pos != null)
                    steps += Math.Abs(pos.Value.x - x) + Math.Abs(pos.Value.y - y);
            }

            return steps + 1;
        }

        private static bool IsEndOfLine(char scanned)
        {
            return scanned == '+';
        }

        private static bool IsVerticalTraverse(Direction direction)
        {
            return direction == Direction.Down || direction == Direction.Up;
        }

        private static bool IsHorizontalTraverse(Direction direction)
        {
            return direction == Direction.Left || direction == Direction.Right;
        }

        private static (int x, int y, Direction d)? GetStartPosition(string[] input)
        {
            var pos = ScanRow(input, 0, "|");
            if (pos != null)
                return (pos.Value.x, pos.Value.y, Direction.Down);
            pos = ScanRow(input, input.Length - 1, "|");
            if (pos != null)
                return (pos.Value.x, pos.Value.y, Direction.Up);
            pos = ScanCol(input, 0, "-");
            if (pos != null)
                return (pos.Value.x, pos.Value.y, Direction.Right);
            pos = ScanCol(input, input[0].Length - 1, "-");
            if (pos != null)
                return (pos.Value.x, pos.Value.y, Direction.Left);
            throw new Exception("uh oh!");
        }

        private static (int x, int y, Direction d)? GetNextDirection(string[] grid, (int x, int y, Direction d) pos)
        {
            if (IsVerticalTraverse(pos.d))
                return ScanRow(grid, pos.x, $"{Letters}-", Direction.Left, pos.y - 1, false) ??
                       ScanRow(grid, pos.x, $"{Letters}-", Direction.Right, pos.y + 1, false);
            return ScanCol(grid, pos.y, $"{Letters}|", Direction.Down, pos.x + 1, false) ??
                   ScanCol(grid, pos.y, $"{Letters}|", Direction.Up, pos.x - 1, false);
        }

        private static (int x, int y, Direction d)? ScanRow(
            string[] grid, int rowIndex, string scanFor, Direction dir = Direction.Right, int startIndex = 0, bool ignoreSpaces = true)
        {
            var row = grid[rowIndex];
            for (int y = startIndex; IsLegal(y, row.Length, dir); y = Increment(y, dir))
            {
                var scanned = grid[rowIndex][y];
                if (!ignoreSpaces && scanned == ' ')
                    return null;
                if (scanFor.ToList().Contains(scanned))
                    return (rowIndex, y, dir);
            }
            return null;
        }

        private static (int x, int y, Direction d)? ScanCol(
            string[] grid, int colIndex, string scanFor, Direction dir = Direction.Down, int startIndex = 0, bool ignoreSpaces = true)
        {
            for (int x = startIndex; IsLegal(x, grid.Length, dir); x = Increment(x, dir))
            {
                var scanned = grid[x][colIndex];
                if (!ignoreSpaces && scanned == ' ')
                    return null;
                if (scanFor.ToList().Contains(scanned))
                    return (x, colIndex, dir);
            }
            return null;
        }

        private static bool IsLegal(int index, int length, Direction direction)
        {
            if (direction == Direction.Up || direction == Direction.Left)
                return index >= 0;
            return index < length;
        }

        private static int Increment(int index, Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                    return index + 1;
                case Direction.Up:
                    return index - 1;
                case Direction.Left:
                    return index - 1;
                case Direction.Right:
                    return index + 1;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        [Fact]
        public void Answer()
        {
            string[] input = File.ReadAllLines("./input1.txt");
            var answer = Compute(input);
            _output.WriteLine($"Part1: {answer}");
        }
    }
}