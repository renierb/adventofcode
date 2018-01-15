// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local
// ReSharper disable AccessToModifiedClosure
// ReSharper disable PossibleMultipleEnumeration

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day13.Part2
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
            string[] input = new[]
            {
                "0: 3",
                "1: 2",
                "4: 4",
                "6: 4",
            };
            AfterDelayOf(input, 10);
        }

        private static void AfterDelayOf(string[] input, int expected)
        {
            Assert.Equal(expected, Compute(ParseInput(input)));
        }

        private static int Compute(List<Scanner> scanners)
        {
            return EnumerableEx.Generate(0, i => MoreDelay(scanners, i), i => i + 1, i => i + 1).Last();
        }

        private static bool MoreDelay(List<Scanner> scanners, int delay)
        {
            return scanners.Any(scanner =>
            {
                var repeat = (scanner.Range - 1) * 2;
                return (delay + scanner.Depth) % repeat == 0;
            });
        }

        private static List<Scanner> ParseInput(IEnumerable<string> lines)
        {
            return lines.Select(line => line.Split(": ").Select(int.Parse).ToArray()).Aggregate(new List<Scanner>(), BuildFirewall);
        }

        private static List<Scanner> BuildFirewall(List<Scanner> scanners, int[] scanner)
        {
            scanners.Add(new Scanner(scanner[0], scanner[1]));
            return scanners;
        }

        [Fact]
        public void Answer()
        {
            string[] input = File.ReadAllLines("./input1.txt");
            var answer = Compute(ParseInput(input));
            _output.WriteLine($"Part2: {answer}");
        }
    }
}