// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local
// ReSharper disable PossibleMultipleEnumeration

using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day02
{
    public class Part1
    {
        private readonly ITestOutputHelper _output;

        public Part1(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Tests()
        {
            ChecksumOf("", 0);
            ChecksumOf("1", 0);
            ChecksumOf("1\n1", 0);
            ChecksumOf("1\n1 2", 1);
            ChecksumOf("5 1 9 5\n7 5 3\n2 4 6 8", 18);
        }

        private static void ChecksumOf(string input, int expected)
        {
            Assert.Equal(expected, Compute(input));
        }

        private static int Compute(string input)
        {
            var rows = input.Split('\n').Where(row => row.Length > 0);
            var cols = rows.Select(r => r.Split(' ', '\t').Select(int.Parse)).Select(ns => ns.OrderBy(i => i));
            return cols.Select(ns => ns.Last() - ns.First()).Sum();
        }

        [Fact]
        public void Answer()
        {
            string input = File.ReadAllText("./input1.txt");
            _output.WriteLine($"Part1: {Compute(input)}");
        }
    }
}