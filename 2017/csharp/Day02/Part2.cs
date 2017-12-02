using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day02
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
            ChecksumOf("", 0);
            ChecksumOf("1 1", 1);
            ChecksumOf("1 2\n4 3 2", 4);
            ChecksumOf("5 9 2 8\n9 4 7 3\n3 8 6 5", 9);
        }

        [Fact]
        public void Answer()
        {
            string input = File.ReadAllText("./input2.txt");
            _output.WriteLine($"Part2: {Compute(input)}");
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private static void ChecksumOf(string input, int expected)
        {
            Assert.Equal(expected, Compute(input));
        }

        private static int Compute(string input)
        {
            var rows = input.Split('\n').Where(row => row.Length > 0);
            return rows
                .Select(ParseNumbers)
                .Select(GetRowResult).Sum();
        }

        private static IEnumerable<int> ParseNumbers(string row)
        {
            return row.Split(' ', '\t').Select(int.Parse);
        }

        private static int GetRowResult(IEnumerable<int> row)
        {
            var numbers = row.OrderByDescending(n => n).ToArray();
            return numbers.Select(GetDivideResult(numbers)).Sum();
        }

        private static Func<int, int, int> GetDivideResult(int[] numbers)
        {
            return (n, index) =>
            {
                var denominator = numbers.Skip(index + 1).FirstOrDefault(cur => n % cur == 0);
                return denominator > 0 ? n / denominator : 0;
            };
        }
    }
}