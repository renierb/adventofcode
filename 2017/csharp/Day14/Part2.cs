using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day14
{
    public class Part2
    {
        private readonly ITestOutputHelper _output;

        public Part2(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Tests()
        {
            RegionsOf("flqrgnkx", 1242);
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private static void RegionsOf(string input, int expected)
        {
            Assert.Equal(expected, Compute(input));
        }

        private static int Compute(string input)
        {
            var graph = new QuickUnionGraph(128 * 128);

            var grid = GetGrid(input);
            for (int row = 0; row < 128; row++)
            {
                for (int col = 0; col < 128; col++)
                {
                    var index = GetIndex(row, col);
                    if (grid[row][col] == '1')
                        graph.AddIndex(index);
                    GetConnectedNeighbors(grid, (row, col)).ForEach(pos => graph.Union(index, GetIndex(pos.row, pos.col)));
                }
            }
            return graph.TotalGroups();
        }

        private static int GetIndex(int row, int col)
        {
            return row * 128 + col;
        }

        private static string[] GetGrid(string input)
        {
            var hashes = Enumerable.Range(0, 128).Select(i => KnotHash.GetHash($"{input}-{i}"));
            return hashes.Select(hash => string.Concat(hash.Select(GetBinary))).ToArray();
        }

        private static string GetBinary(char hex)
        {
            switch (hex)
            {
                case '0': return "0000";
                case '1': return "0001";
                case '2': return "0010";
                case '3': return "0011";
                case '4': return "0100";
                case '5': return "0101";
                case '6': return "0110";
                case '7': return "0111";
                case '8': return "1000";
                case '9': return "1001";
                case 'a': return "1010";
                case 'b': return "1011";
                case 'c': return "1100";
                case 'd': return "1101";
                case 'e': return "1110";
                case 'f': return "1111";
                default: throw new Exception("uh oh!");
            }
        }

        private static IEnumerable<(int row, int col)> GetConnectedNeighbors(string[] grid, (int row, int col) pos)
        {
            if (grid[pos.row][pos.col] == '0')
                return Enumerable.Empty<(int, int)>();

            List<(int row, int col)> neighbors = new List<(int, int)>(4)
            {
                (pos.row - 1, pos.col),
                (pos.row, pos.col + 1),
                (pos.row, pos.col - 1),
                (pos.row + 1, pos.col),
            };

            return neighbors.Where(IsLegal).Where(p => grid[p.row][p.col] == '1');
        }

        private static bool IsLegal((int row, int col) pos)
        {
            return pos.row >= 0 && pos.col >= 0 && pos.row < 128 && pos.col < 128;
        }

        [Fact]
        public void Answer()
        {
            string input = File.ReadAllText("./input1.txt");
            _output.WriteLine($"Part2: {Compute(input)}");
        }
    }
}