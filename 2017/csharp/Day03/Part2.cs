using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day03
{
    public class Part2
    {
        private readonly ITestOutputHelper _output;

        public Part2(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void BuildSpiralTests()
        {
            Assert.Equal(2, SearchSpiral(1));
            Assert.Equal(26, SearchSpiral(25));
            Assert.Equal(806, SearchSpiral(747));
        }

        private static int SearchSpiral(int value, List<List<int>> spiral = null)
        {
            if (spiral == null)
                return SearchSpiral(value, GrowSpiral(new List<List<int>>(1) {new List<int>(1) {1}}));

            var answer = IterateSpiral(spiral, value);
            if (answer > 0)
                return answer;

            return SearchSpiral(value, GrowSpiral(spiral));
        }

        private static int IterateSpiral(List<List<int>> spiral, int value)
        {
            var spiralSize = spiral.Count;
            (int row, int col) start = GetStartPosition(spiralSize);

            for (int row = start.row; row >= 0; row--)
            {
                var sum = SumNeighbors(spiral, (row, start.col));
                if (sum > value)
                    return sum;
                spiral[row][start.col] = sum;
            }
            for (int col = spiral.Count - 2; col >= 0; col--)
            {
                var sum = SumNeighbors(spiral, (0, col));
                if (sum > value)
                    return sum;
                spiral[0][col] = sum;
            }
            for (int row = 1; row < spiralSize; row++)
            {
                var sum = SumNeighbors(spiral, (row, 0));
                if (sum > value)
                    return sum;
                spiral[row][0] = sum;
            }
            for (int col = 1; col < spiralSize; col++)
            {
                var sum = SumNeighbors(spiral, (spiralSize - 1, col));
                if (sum > value)
                    return sum;
                spiral[spiralSize - 1][col] = sum;
            }
            return 0;
        }

        private static (int, int) GetStartPosition(int spiralSize)
        {
            if (spiralSize == 3)
                return (spiralSize / 2, spiralSize - 1);
            return (spiralSize - 2, spiralSize - 1);
        }

        private static int SumNeighbors(List<List<int>> spiral, (int row, int col) pos)
        {
            List<(int row, int col)> neighbors = new List<(int, int)>(8)
            {
                (pos.row - 1, pos.col + 1),
                (pos.row - 1, pos.col),
                (pos.row - 1, pos.col - 1),
                (pos.row, pos.col + 1),
                (pos.row, pos.col - 1),
                (pos.row + 1, pos.col + 1),
                (pos.row + 1, pos.col),
                (pos.row + 1, pos.col - 1),
            };
            return neighbors.Where(p => IsLegal(spiral, p)).Select(p => spiral[p.row][p.col]).Sum();
        }

        private static bool IsLegal(List<List<int>> spiral, (int row, int col) pos)
        {
            try
            {
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                spiral.ElementAt(pos.row).ElementAt(pos.col);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static List<List<int>> GrowSpiral(List<List<int>> spiral)
        {
            var count = spiral.Count + 2;
            return spiral
                .Select(AddColumns)
                .Prepend(new List<int>(Enumerable.Repeat(0, count)))
                .Append(new List<int>(Enumerable.Repeat(0, count)))
                .ToList();
        }

        private static List<int> AddColumns(List<int> row)
        {
            return row.Prepend(0).Append(0).ToList();
        }

        [Fact]
        public void Answer()
        {
            string input = File.ReadAllText("./input2.txt");
            _output.WriteLine($"Part2: {SearchSpiral(int.Parse(input))}");
        }
    }
}