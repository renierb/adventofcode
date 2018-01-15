// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day14
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
            SquaresUsedOf("flqrgnkx", 8108);
        }

        private static void SquaresUsedOf(string input, int expected)
        {
            Assert.Equal(expected, Compute(input));
        }

        private static int Compute(string input)
        {
            var hashes = Enumerable.Range(0, 128).Select(i => KnotHash.GetHash($"{input}-{i}"));
            var grid = hashes.Select(hash => string.Concat(hash.Select(GetBinary)));
            return grid.Select(row => row.Count(c => c == '1')).Sum();
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

        [Fact]
        public void Answer()
        {
            string input = File.ReadAllText("./input1.txt");
            _output.WriteLine($"Part1: {Compute(input)}");
        }
    }
}