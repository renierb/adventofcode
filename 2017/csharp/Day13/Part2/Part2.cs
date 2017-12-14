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

        private static int Compute(List<Scanner> initialScanners)
        {
            var initial = initialScanners.ToDictionary(_ => _.Depth, _ => _);

            int delay = 0;
            while (IsCaught(initial, delay))
                delay++;
            return delay;
        }

        private static bool IsCaught(Dictionary<int, Scanner> initial, int delay)
        {
            int travelDepth = 0;
            var maxDepth = initial.Keys.Max();

            var generator = EnumerableEx.Generate(initial, _ => true, MoveScanners, _ => _);
            foreach (var scanners in generator.Skip(delay))
            {
                if (scanners.TryGetValue(travelDepth, out var scanner) && scanner.Position == 0)
                    break;
                travelDepth++;
                if (travelDepth > maxDepth)
                    return false;
            }
            initial.Values.ForEach(scanner => scanner.Reset());
            return true;
        }

        private static Dictionary<int, Scanner> MoveScanners(Dictionary<int, Scanner> scanners)
        {
            scanners.Values.ForEach(scanner => scanner.Move());
            return scanners;
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
            _output.WriteLine($"Part2: {Compute(ParseInput(input))}");
        }
    }
}