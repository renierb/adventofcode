using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day06
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
            RedistributionCyclesOf(new[] {0}, 0);
            RedistributionCyclesOf(new[] {1}, 1);
            RedistributionCyclesOf(new[] {2}, 1);
            RedistributionCyclesOf(new[] {0, 2, 7, 0}, 5);
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private void RedistributionCyclesOf(int[] input, int repeatAfter)
        {
            _configurations.Clear();
            Assert.Equal(repeatAfter, Iterate(input));
        }

        private readonly Dictionary<int, List<int[]>> _configurations = new Dictionary<int, List<int[]>>();

        private int Iterate(int[] distribution)
        {
            int cycles = 0;
            while (true)
            {
                (int value, int index) max = distribution.Select((x, i) => (x, i)).Aggregate((0, 0), GetMax, x => x);
                if (max.value == 0)
                    return 0;

                if (IsRepeatedConfiguration(distribution))
                    return cycles;
                AddConfiguration(distribution);

                var redistribution = Redistribute((int[]) distribution.Clone(), max);
                distribution = redistribution;
                cycles = cycles + 1;
            }
        }

        // ReSharper disable UnusedTupleComponentInReturnValue
        private static (int, int) GetMax((int value, int index) x, (int value, int index) y)
        {
            return x.value >= y.value ? x : y;
        }

        private static int[] Redistribute(int[] distribution, (int blocks, int index) bank)
        {
            var totalBanks = distribution.Length;
            var totalBlocks = bank.blocks;

            var index = bank.index;
            distribution[index++] = 0;
            while (totalBlocks > 0)
            {
                distribution[index++ % totalBanks]++;
                totalBlocks--;
            }

            return distribution;
        }

        private void AddConfiguration(int[] redistribution)
        {
            int key = redistribution[0];
            if (_configurations.ContainsKey(key))
                _configurations[key].Add(redistribution);
            else
                _configurations.Add(key, new List<int[]> {redistribution});
        }

        private bool IsRepeatedConfiguration(int[] redistribution)
        {
            var key = redistribution[0];
            if (!_configurations.ContainsKey(key))
                return false;
            return _configurations[key].Any(distribution => IsSameDistribution(redistribution, distribution));
        }

        private static bool IsSameDistribution(int[] redistribution, int[] distribution)
        {
            return distribution.Zip(redistribution, (x, y) => Math.Abs(x - y)).Sum() == 0;
        }

        [Fact]
        public void Answer()
        {
            int[] input = File.ReadAllText("./input1.txt").Split('\t').Select(int.Parse).ToArray();
            _output.WriteLine($"Part1: {Iterate(input)}");
        }
    }
}