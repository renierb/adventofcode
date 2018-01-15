// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day22
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
            string[] grid =
            {
                "..#",
                "#..",
                "...",
            };

            BurstsCausingInfectionOf(grid, 7, 5);
            BurstsCausingInfectionOf(grid, 70, 41);
            BurstsCausingInfectionOf(grid, 10000, 5587);
        }

        private static void BurstsCausingInfectionOf(string[] input, int bursts, int expected)
        {
            var grid = input.Select(row => row.ToCharArray()).ToArray();
            Assert.Equal(expected, Compute(grid, bursts));
        }

        private static int Compute(char[][] grid, int bursts)
        {
            HashSet<(int r, int c)> infectedNodes = grid.SelectMany(GetInfectedNodes).ToHashSet();

            int infected = 0;
            int gridSize = grid.Length;

            Direction direction = Direction.Up;
            var mid = gridSize / 2;
            (int r, int c) node = (mid, mid);
            for (int i = 0; i < bursts; i++)
            {
                if (IsInfected(infectedNodes, node))
                {
                    direction = TurnRight(direction);
                    infectedNodes.Remove(node);
                }
                else
                {
                    direction = TurnLeft(direction);
                    if (!infectedNodes.Contains(node))
                        infected += infectedNodes.Add(node) ? 1 : 0;
                }
                node = MoveForward(node, direction);
                if (!IsLegalPosition(node, gridSize))
                {
                    gridSize += 2;
                    node = NormalizePosition(node);
                    infectedNodes = NormalizeNodes(infectedNodes);
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

        private static HashSet<(int, int)> NormalizeNodes(IEnumerable<(int r, int c)> nodes)
        {
            return nodes.Select(n => (n.r + 1, n.c + 1)).ToHashSet();
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

        private static bool IsLegalPosition((int r, int c) node, int gridSize)
        {
            return node.r >= 0 && node.c >= 0 && node.r < gridSize && node.c < gridSize;
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

        private static bool IsInfected(HashSet<(int r, int c)> infectedNodes, (int r, int c) node)
        {
            return infectedNodes.Contains(node);
        }

        [Fact]
        public void Answer()
        {
            string[] input = File.ReadAllLines("./input1.txt");
            var grid = input.Select(row => row.ToCharArray()).ToArray();
            _output.WriteLine($"Part1: {Compute(grid, 10000)}");
        }
    }
}