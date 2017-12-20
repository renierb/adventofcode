// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local
// ReSharper disable AccessToModifiedClosure

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
            string[] input =
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
            var maxDepth = initial.Max(_ => _.Depth);

            int travelDepth = 0;
            var generator = EnumerableEx.Generate(initial, state => travelDepth <= maxDepth, MoveScanners, _ => _);

            int severity = 0;
            foreach (var scanners in generator)
            {
                var index = scanners.FindIndex(scanner => scanner.Depth == travelDepth && scanner.Position == 0);
                if (index >= 0)
                    severity += GetSeverity(scanners[index]);
                travelDepth++;
            }

            return severity;
        }

        private static int GetSeverity(Scanner scanner)
        {
            return scanner.Depth * scanner.Range;
        }

        private static List<Scanner> MoveScanners(List<Scanner> scanners)
        {
            scanners.ForEach(scanner => scanner.Move());
            return scanners;
        }

        private static List<Scanner> ParseInput(IEnumerable<string> lines)
        {
            return lines.Select(line => line.Split(": ").Select(int.Parse).ToArray()).Aggregate(new List<Scanner>(), AddScanner);
        }

        private static List<Scanner> AddScanner(List<Scanner> scanners, int[] scanner)
        {
            int depth = scanner[0];
            int range = scanner[1];
            scanners.Add(new Scanner(depth, range));
            return scanners;
        }

        [Fact]
        public void Answer()
        {
            string[] input = File.ReadAllLines("./input1.txt");
            _output.WriteLine($"Part1: {Compute(ParseInput(input))}");
        }
    }
}