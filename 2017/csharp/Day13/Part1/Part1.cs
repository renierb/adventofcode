// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day13.Part1
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
            string[] input = new[]
            {
                "0: 3",
                "1: 2",
                "4: 4",
                "6: 4",
            };
            SeverityOf(input, 24);
        }

        private static void SeverityOf(string[] input, int expected)
        {
            Assert.Equal(expected, Compute(ParseInput(input)));
        }

        private static int Compute(List<Scanner> initial)
        {
            int travelDepth = 0;
            var maxDepth = initial.Max(_ => _.Depth);
            var generator = EnumerableEx.Generate(initial, state => travelDepth <= maxDepth, MoveScanners, _ => _);

            int severity = 0;
            foreach (var scanners in generator)
            {
                var index = scanners.FindIndex(scanner => scanner.Depth == travelDepth && scanner.Position == 0);
                if (index >= 0)
                    severity += initial[index].Depth * initial[index].Range;
                travelDepth++;
            }
            return severity;
        }

        private static List<Scanner> MoveScanners(List<Scanner> scanners)
        {
            scanners.ForEach(scanner => scanner.Move());
            return scanners;
        }

        private static List<Scanner> ParseInput(IEnumerable<string> lines)
        {
            return lines.Select(line => line.Split(": ").Select(int.Parse).ToArray()).Aggregate(new List<Scanner>(), BuildFirewall);
        }

        private static List<Scanner> BuildFirewall(List<Scanner> firewall, int[] scanner)
        {
            int depth = scanner[0];
            int range = scanner[1];
            firewall.Add(new Scanner(depth, range));
            return firewall;
        }

        [Fact]
        public void Answer()
        {
            string[] input = File.ReadAllLines("./input1.txt");
            _output.WriteLine($"Part1: {Compute(ParseInput(input))}");
        }
    }
}