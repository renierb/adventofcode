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
            var grid = input.Select(row => row.ToCharArray()).ToArray();
            Assert.Equal(expected, Compute(grid, bursts));
        }

        private static int Compute(char[][] grid, int bursts)
        {
            IDictionary<(int r, int c), char> nodes = grid.SelectMany(GetInfectedNodes).ToDictionary(_ => _, _ => '#');

            int infected = 0;
            int gridSize = grid.Length;

            Direction direction = Direction.Up;
            var mid = gridSize / 2;
            (int r, int c) node = (mid, mid);
            for (int i = 0; i < bursts; i++)
            {
                if (IsClean(nodes, node))
                {
                    direction = TurnLeft(direction);
                    nodes[node] = 'W';
                }
                else if (IsWeakened(nodes, node))
                {
                    nodes[node] = '#';
                    infected++;
                }
                else if (IsInfected(nodes, node))
                {
                    direction = TurnRight(direction);
                    nodes[node] = 'F';
                }
                else if (IsFlagged(nodes, node))
                {
                    direction = ReverseDirection(direction);
                    nodes.Remove(node);
                }
                node = MoveForward(node, direction);
                if (!IsLegalPosition(node, gridSize))
                {
                    gridSize += 2;
                    node = NormalizePosition(node);
                    nodes = NormalizeNodes(nodes);
                }
            }
            return infected;
        }

        private static IEnumerable<(int r, int c)> GetInfectedNodes(char[] nodes, int r)
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

        private static bool IsClean(IDictionary<(int r, int c), char> nodes, (int r, int c) node)
        {
            return !nodes.ContainsKey(node);
        }

        private static bool IsWeakened(IDictionary<(int r, int c), char> nodes, (int r, int c) node)
        {
            return nodes.TryGetValue(node, out var value) && value == 'W';
        }

        private static bool IsInfected(IDictionary<(int r, int c), char> nodes, (int r, int c) node)
        {
            return nodes.TryGetValue(node, out var value) && value == '#';
        }

        private static bool IsFlagged(IDictionary<(int r, int c), char> nodes, (int r, int c) node)
        {
            return nodes.TryGetValue(node, out var value) && value == 'F';
        }

        private static bool IsLegalPosition((int r, int c) node, int gridSize)
        {
            return node.r >= 0 && node.c >= 0 && node.r < gridSize && node.c < gridSize;
        }

        private static (int r, int c) NormalizePosition((int r, int c) node)
        {
            return (node.r + 1, node.c + 1);
        }

        private static IDictionary<(int r, int c), char> NormalizeNodes(IDictionary<(int r, int c), char> nodes)
        {
            return nodes.ToDictionary(pair => (pair.Key.r + 1, pair.Key.c + 1), pair => pair.Value);
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

        [Fact]
        public void Answer()
        {
            string[] input = File.ReadAllLines("./input1.txt");
            var grid = input.Select(row => row.ToCharArray()).ToArray();
            _output.WriteLine($"Part1: {Compute(grid, 10000000)}");
        }
    }
}