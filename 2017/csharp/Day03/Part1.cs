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
            BottomRightOf(10, isSquare: 25);
            BottomRightOf(25, isSquare: 25);
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
            return GetStepsTaken(IterateSpiral(fromSquare, spiralSize), spiralSize);
        }

        private static int GetSpiralSize(int fromSquare)
        {
            return (int) Math.Sqrt(GetBottomRightSquare(fromSquare));
        }

        private static (int, int) IterateSpiral(int value, int spiralSize = 3)
        {
            if (IsRightSideNumber(value, spiralSize))
            {
                return IterateRightSide(value, spiralSize);
            }

            if (IsTopSideNumber(value, spiralSize))
            {
                return IterateTopSide(value, spiralSize);
            }

            if (IsLeftSideNumber(value, spiralSize))
            {
                return IterateLeftSide(value, spiralSize);
            }

            return IterateBottomSide(value, spiralSize);
        }

        private static bool IsRightSideNumber(int value, int spiralSize)
        {
            return value <= GetSquare(spiralSize, 3);
        }

        private static bool IsTopSideNumber(int value, int spiralSize)
        {
            return value <= GetSquare(spiralSize, 2);
        }

        private static bool IsLeftSideNumber(int value, int spiralSize)
        {
            return value <= GetSquare(spiralSize, 1);
        }

        private static (int row, int col) IterateRightSide(int value, int spiralSize)
        {
            int square = GetSquare(spiralSize, 4) + 1;
            (int row, int col) pos = (spiralSize - 2, spiralSize - 1);
            for (int row = pos.row; row >= 0; row--)
            {
                if (square++ == value)
                    return (row, pos.col);
            }
            throw new Exception("Uh oh!");
        }

        private static (int, int) IterateTopSide(int value, int spiralSize)
        {
            int square = GetSquare(spiralSize, 3) + 1;
            for (int col = spiralSize - 2; col >= 0; col--)
            {
                if (square++ == value)
                    return (0, col);
            }
            throw new Exception("Uh oh!");
        }

        private static (int, int) IterateLeftSide(int value, int spiralSize)
        {
            int square = GetSquare(spiralSize, 2) + 1;
            for (int row = 1; row < spiralSize; row++)
            {
                if (square++ == value)
                    return (row, 0);
            }
            throw new Exception("Uh oh!");
        }

        private static (int, int) IterateBottomSide(int value, int spiralSize)
        {
            int square = GetSquare(spiralSize, 1) + 1;
            for (int col = 1; col < spiralSize; col++)
            {
                if (square++ == value)
                    return (spiralSize - 1, col);
            }
            throw new Exception("Uh oh!");
        }

        private static int GetSquare(int spiralSize, int offset)
        {
            return spiralSize * spiralSize - offset * (spiralSize - 1);
        }

        private static int GetStepsTaken((int row, int col) pos, int size)
        {
            (int midRow, int midCol) = (size / 2, size / 2);
            return Math.Abs(pos.row - midRow) + Math.Abs(pos.col - midCol);
        }

        [Fact]
        public void Answer()
        {
            string input = File.ReadAllText("./input1.txt");
            _output.WriteLine($"Part1: {GetStepsTaken(int.Parse(input))}");
        }
    }
}