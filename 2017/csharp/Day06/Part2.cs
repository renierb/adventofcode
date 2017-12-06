using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day06
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
            RedistributionCyclesOf(new[] {0}, 0);
            RedistributionCyclesOf(new[] {1}, 1);
            RedistributionCyclesOf(new[] {2}, 1);
            RedistributionCyclesOf(new[] {0, 2, 7, 0}, 4);
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private void RedistributionCyclesOf(int[] input, int repeatAfter)
        {
            _configurations.Clear();
            Assert.Equal(repeatAfter, Iterate(input));
        }

        private readonly Dictionary<int, List<(int[] conf, int cycle)>> _configurations = new Dictionary<int, List<(int[], int)>>();

        private int Iterate(int[] input)
        {
            int cycles = 0;
            int[] distribution = input;
            while (true)
            {
                (int value, int index) max = distribution.Select(ToTuple).Aggregate((0, 0), GetMax, x => x);
                if (max.value == 0)
                    return 0;

                var repeatCycles = GetCyclesSinceRepeat(distribution, cycles);
                if (repeatCycles > 0)
                    return repeatCycles;
                AddConfiguration(distribution, cycles);

                distribution = Redistribute((int[]) distribution.Clone(), max);
                cycles = cycles + 1;
            }
        }

        // ReSharper disable UnusedTupleComponentInReturnValue
        private static (int, int) ToTuple(int x, int i)
        {
            return (x, i);
        }

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

        private void AddConfiguration(int[] redistribution, int cycles)
        {
            int key = redistribution[0];
            if (_configurations.ContainsKey(key))
                _configurations[key].Add((redistribution, cycles));
            else
                _configurations.Add(key, new List<(int[], int)> {(redistribution, cycles)});
        }

        private int GetCyclesSinceRepeat(int[] redistribution, int cycles)
        {
            var key = redistribution[0];
            if (!_configurations.ContainsKey(key))
                return 0;
            var repeated = _configurations[key].FirstOrDefault(distribution => IsSameDistribution(redistribution, distribution.conf));
            if (repeated.conf != null)
                return cycles - repeated.cycle;
            return 0;
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