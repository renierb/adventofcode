using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace Day03
{
    public class Part1
    {
        private readonly ITestOutputHelper _output;

        public Part1(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void BottomRightTests()
        {
            BottomRightOf(2, isSquare: 9);
            BottomRightOf(3, isSquare: 9);
            BottomRightOf(4, isSquare: 9);
            BottomRightOf(5, isSquare: 9);
            BottomRightOf(6, isSquare: 9);
            BottomRightOf(7, isSquare: 9);
            BottomRightOf(8, isSquare: 9);
            BottomRightOf(9, isSquare: 9);
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private static void BottomRightOf(int fromSquare, int isSquare)
        {
            Assert.Equal(isSquare, GetBottomRightSquare(fromSquare));
        }

        private static int GetBottomRightSquare(int fromSquare)
        {
            var sqrt = Math.Sqrt(fromSquare);
            return (int) Math.Pow(GetUpperSqrt((int) sqrt, sqrt), 2.0);
        }

        private static int GetUpperSqrt(int rounded, double sqrt)
        {
            if (rounded % 2 == 0)
                return rounded + 1;
            return IsSquareNumber(sqrt) ? rounded : rounded + 2;
        }

        private static bool IsSquareNumber(double sqrt)
        {
            return Math.Abs(sqrt - (int) sqrt) < double.Epsilon;
        }

        [Fact]
        private void TakeStepsTests()
        {
            FromSquareSteps(2, steps: 1);
            FromSquareSteps(3, steps: 2);
            FromSquareSteps(4, steps: 1);
            FromSquareSteps(5, steps: 2);
            FromSquareSteps(6, steps: 1);
            FromSquareSteps(7, steps: 2);
            FromSquareSteps(8, steps: 1);
            FromSquareSteps(9, steps: 2);
            FromSquareSteps(10, steps: 3);
            FromSquareSteps(11, steps: 2);
            FromSquareSteps(12, steps: 3);
            FromSquareSteps(23, steps: 2);
            FromSquareSteps(1024, steps: 31);
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private static void FromSquareSteps(int fromSquare, int steps)
        {
            Assert.Equal(steps, GetStepsTaken(fromSquare));
        }

        private static int GetStepsTaken(int fromSquare)
        {
            var spiralSize = GetSpiralSize(fromSquare);
            var spiral = BuildSpiral(spiralSize);
            var squarePosition = GetPosition(fromSquare, spiral);
            var stepsFromPosition = GetStepsTaken(squarePosition, spiralSize);
            return stepsFromPosition;
        }

        private static int GetSpiralSize(int fromSquare)
        {
            return (int) Math.Sqrt(GetBottomRightSquare(fromSquare));
        }

        private static int GetStepsTaken((int row, int col) pos, int size)
        {
            (int midRow, int midCol) = (size / 2, size / 2);
            return Math.Abs(pos.row - midRow) + Math.Abs(pos.col - midCol);
        }

        private static (int, int) GetPosition(int fromSquare, int[][] spiral)
        {
            int len = spiral.Length;
            var bottomRight = spiral[len - 1][len - 1];

            var distance = (int) Math.Sqrt(bottomRight) - 1;
            var bottomLeft = bottomRight - distance;
            var topLeft = bottomLeft - distance;
            var topRight = topLeft - distance;

            if (fromSquare <= topRight)
                return FindInColumn(fromSquare, spiral, len - 1);
            if (fromSquare <= topLeft)
                return FindInRow(fromSquare, spiral, 0);
            if (fromSquare <= bottomLeft)
                return FindInColumn(fromSquare, spiral, 0);
            return FindInRow(fromSquare, spiral, len - 1);
        }

        private static (int, int) FindInRow(int fromSquare, int[][] spiral, int row)
        {
            for (int col = 0; col < spiral.Length; col++)
            {
                if (spiral[row][col] == fromSquare)
                    return (row, col);
            }
            throw new Exception("Not found");
        }

        private static (int, int) FindInColumn(int fromSquare, int[][] spiral, int col)
        {
            for (int row = 0; row < spiral.Length; row++)
            {
                if (spiral[row][col] == fromSquare)
                    return (row, col);
            }
            throw new Exception("Not found");
        }

        [Fact]
        public void BuildSpiralTests()
        {
            Assert.Equal(
                new[]
                {
                    new[] {5, 4, 3},
                    new[] {6, 1, 2},
                    new[] {7, 8, 9}
                }, BuildSpiral(3));
            Assert.Equal(
                new[]
                {
                    new[] {17, 16, 15, 14, 13},
                    new[] {18, 5, 4, 3, 12},
                    new[] {19, 6, 1, 2, 11},
                    new[] {20, 7, 8, 9, 10},
                    new[] {21, 22, 23, 24, 25}
                }, BuildSpiral(5));
        }

        private static int[][] BuildSpiral(int size)
        {
            int[][] spiral = new int[size][];
            for (int i = 0; i < size; i++)
            {
                spiral[i] = new int[size];
            }
            spiral[size / 2][size / 2] = 1;
            PopulateSpiral(spiral, size, size * size);
            return spiral;
        }

        private static void PopulateSpiral(int[][] spiral, int size, int corner, int i = 0)
        {
            if (size == 1)
                return;
            if (corner == size * size)
            {
                int row = size - 1 + i;
                for (int col = size - 1 + i; col >= i; col--)
                {
                    spiral[row][col] = corner--;
                }
                PopulateSpiral(spiral, size, corner, i);
                return;
            }
            if (corner == size * size - size)
            {
                int col = i;
                for (int row = size - 2 + i; row >= i; row--)
                {
                    spiral[row][col] = corner--;
                }
                PopulateSpiral(spiral, size, corner, i);
                return;
            }
            if (corner == (size * size - (size * 2)) + 1)
            {
                int row = i;
                for (int col = i + 1; col < size + i; col++)
                {
                    spiral[row][col] = corner--;
                }
                PopulateSpiral(spiral, size, corner, i);
                return;
            }
            {
                int col = size - 1 + i;
                for (int row = i + 1; row < size - 1 + i; row++)
                {
                    spiral[row][col] = corner--;
                }
                PopulateSpiral(spiral, size - 2, corner, i + 1);
            }
        }

        [Fact]
        public void Answer()
        {
            string input = File.ReadAllText("./input1.txt");
            _output.WriteLine($"Part1: {GetStepsTaken(int.Parse(input))}");
        }
    }
}