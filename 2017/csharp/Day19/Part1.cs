// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day19
{
    public class Part1
    {
        private readonly ITestOutputHelper _output;

        public Part1(ITestOutputHelper output)
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
            string input = @"
       |          
       |  +--+    
       A  |  C    
   F---|----E|--+ 
       |  |  |  D 
       +B-+  +--+ 
                  ";

            LettersCapturedOf(input.Split("\r\n").Skip(1).ToArray(), "ABCDEF");
        }

        private static void LettersCapturedOf(string[] input, string expected)
        {
            Assert.Equal(expected, Compute(input));
        }

        private const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private static string Compute(string[] grid)
        {
            var collects = new List<char>();

            var pos = GetStartPosition(grid);
            while (pos != null)
            {
                (int x, int y, Direction d) = pos.Value;
                var scanned = grid[x][y];
                if (HasCapturedLetter(scanned))
                    collects.Add(scanned);
                if (IsEndOfLine(scanned))
                    pos = GetNextDirection(grid, pos.Value);
                else if (IsVerticalTraverse(d))
                    pos = ScanCol(grid, y, $"{Letters}+", d, Increment(x, d), false);
                else if (IsHorizontalTraverse(d))
                    pos = ScanRow(grid, x, $"{Letters}+", d, Increment(y, d), false);
            }

            return string.Concat(collects);
        }

        private static bool HasCapturedLetter(char scanned)
        {
            return Letters.ToList().Contains(scanned);
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
            return null;
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
            string[] grid, int rowIndex, string scanFor = "", Direction dir = Direction.Right, int colIndex = 0, bool ignoreSpaces = true)
        {
            var row = grid[rowIndex];
            for (int y = colIndex; IsLegal(y, row.Length, dir); y = Increment(y, dir))
            {
                var scanned = row[y];
                if (!ignoreSpaces && scanned == ' ')
                    return null;
                if (scanFor.ToList().Contains(scanned))
                    return (rowIndex, y, dir);
            }
            return null;
        }

        private static (int x, int y, Direction d)? ScanCol(
            string[] grid, int colIndex, string scanFor = "", Direction dir = Direction.Down, int rowIndex = 0, bool ignoreSpaces = true)
        {
            for (int x = rowIndex; IsLegal(x, grid.Length, dir); x = Increment(x, dir))
            {
                var scanned = grid[x][colIndex];
                if (!ignoreSpaces && scanned == ' ')
                    return null;
                if (scanFor.ToList().Contains(scanned))
                    return (x, colIndex, dir);
            }
            return null;
        }

        private static bool IsLegal(int index, int maxIndex, Direction direction)
        {
            if (direction == Direction.Up || direction == Direction.Left)
                return index >= 0;
            return index < maxIndex;
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