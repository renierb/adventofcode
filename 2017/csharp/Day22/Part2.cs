// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day22
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
            string[] grid =
            {
                "..#",
                "#..",
                "...",
            };

            BurstsCausingInfectionOf(grid, 100, 26);
            BurstsCausingInfectionOf(grid, 10000000, 2511944);
        }

        private static void BurstsCausingInfectionOf(string[] input, int bursts, int expected)
        {
            var grid = input.Select(row => row.ToCharArray().ToList()).ToList();
            Assert.Equal(expected, Compute(grid, bursts));
        }

        private static int Compute(List<List<char>> grid, int bursts)
        {
            var preInfected = grid.SelectMany(GetInfectedNodes).ToHashSet();
            var postInfected = new HashSet<(int r, int c)>();

            int infected = 0;

            Direction direction = Direction.Up;
            var mid = grid.Count / 2;
            (int r, int c) node = (mid, mid);
            for (int i = 0; i < bursts; i++)
            {
                if (IsClean(grid, node))
                {
                    direction = TurnLeft(direction);
                    grid[node.r][node.c] = 'W';
                    
                }
                else if (IsWeakened(grid, node))
                {
                    grid[node.r][node.c] = '#';
                    if (!preInfected.Contains(node))
                        infected += postInfected.Add(node) ? 1 : 0;
                }
                else if (IsInfected(grid, node))
                {
                    direction = TurnRight(direction);
                    grid[node.r][node.c] = 'F';
                }
                else if (IsFlagged(grid, node))
                {
                    direction = ReverseDirection(direction);
                    grid[node.r][node.c] = '.';
                    preInfected.Remove(node);
                    postInfected.Remove(node);
                }
                node = MoveForward(node, direction);
                if (!IsLegalPosition(grid, node))
                {
                    node = NormalizePosition(node);
                    grid = IncreaseGrid(grid, grid.Count + 2);
                    preInfected = NormalizeNodes(preInfected);
                    postInfected = NormalizeNodes(postInfected);
                }
            }
            return infected;
        }

        private static Direction ReverseDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Left;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction));
            }
        }

        private static bool IsFlagged(List<List<char>> grid, (int r, int c) node)
        {
            return grid[node.r][node.c] == 'F';
        }

        private static bool IsWeakened(List<List<char>> grid, (int r, int c) node)
        {
            return grid[node.r][node.c] == 'W';
        }

        private static bool IsClean(List<List<char>> grid, (int r, int c) node)
        {
            return grid[node.r][node.c] == '.';
        }

        private static HashSet<(int, int)> NormalizeNodes(IEnumerable<(int r, int c)> nodes)
        {
            return nodes.Select(n => (n.r + 1, n.c + 1)).ToHashSet();
        }

        private static IEnumerable<(int r, int c)> GetInfectedNodes(List<char> nodes, int r)
        {
            return nodes.Select(GetInfectedNode(r)).Where(NotNull).OfType<(int, int)>();
        }

        private static Func<char, int, (int, int)?> GetInfectedNode(int r)
        {
            return (node, c) => node == '#' ? (r, c) : ((int, int)?) null;
        }

        private static bool NotNull((int, int)? value)
        {
            return value != null;
        }

        private static (int r, int c) MoveForward((int r, int c) node, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return (node.r - 1, node.c);
                case Direction.Down:
                    return (node.r + 1, node.c);
                case Direction.Left:
                    return (node.r, node.c - 1);
                case Direction.Right:
                    return (node.r, node.c + 1);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction));
            }
        }

        private static (int r, int c) NormalizePosition((int r, int c) node)
        {
            return (node.r + 1, node.c + 1);
        }

        private static List<List<char>> IncreaseGrid(List<List<char>> grid, int count)
        {
            return grid.Select(AddColumns)
                .Prepend(new List<char>(Enumerable.Repeat('.', count)))
                .Append(new List<char>(Enumerable.Repeat('.', count)))
                .ToList();
        }

        private static bool IsLegalPosition(List<List<char>> grid, (int r, int c) node)
        {
            return node.r >= 0 && node.c >= 0 && node.r < grid.Count && node.c < grid.Count;
        }

        private static List<char> AddColumns(List<char> row)
        {
            return row.Prepend('.').Append('.').ToList();
        }

        private static Direction TurnLeft(Direction current)
        {
            switch (current)
            {
                case Direction.Up:
                    return Direction.Left;
                case Direction.Down:
                    return Direction.Right;
                case Direction.Left:
                    return Direction.Down;
                case Direction.Right:
                    return Direction.Up;
                default:
                    throw new ArgumentOutOfRangeException(nameof(current));
            }
        }

        private static Direction TurnRight(Direction current)
        {
            switch (current)
            {
                case Direction.Up:
                    return Direction.Right;
                case Direction.Down:
                    return Direction.Left;
                case Direction.Left:
                    return Direction.Up;
                case Direction.Right:
                    return Direction.Down;
                default:
                    throw new ArgumentOutOfRangeException(nameof(current));
            }
        }

        private static bool IsInfected(List<List<char>> grid, (int r, int c) node)
        {
            return grid[node.r][node.c] == '#';
        }

        [Fact]
        public void Answer()
        {
            string[] input = File.ReadAllLines("./input1.txt");
            var grid = input.Select(row => row.ToCharArray().ToList()).ToList();
            _output.WriteLine($"Part1: {Compute(grid, 10000000)}");
        }
    }
}