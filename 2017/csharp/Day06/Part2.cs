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
            int[] configuration = input;
            while (true)
            {
                (int blocks, int index) bank = FindFirstBankWithMaxBlocks(configuration);
                if (bank.blocks == 0)
                    return 0;

                var repeatCycles = GetCyclesSinceRepeat(configuration, cycles);
                if (repeatCycles > 0)
                    return repeatCycles;
                AddConfiguration(configuration, cycles);

                configuration = Redistribute((int[]) configuration.Clone(), bank);
                cycles = cycles + 1;
            }
        }

        private static (int, int) FindFirstBankWithMaxBlocks(int[] configuration)
        {
            return configuration.Select(ToTuple).Aggregate((0, 0), GetMax, x => x);
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

        private static int[] Redistribute(int[] configuration, (int blocks, int index) bank)
        {
            var totalBanks = configuration.Length;
            var totalBlocks = bank.blocks;

            var index = bank.index;
            configuration[index++] = 0;
            while (totalBlocks > 0)
            {
                configuration[index++ % totalBanks]++;
                totalBlocks--;
            }

            return configuration;
        }

        private void AddConfiguration(int[] configuration, int cycles)
        {
            int key = configuration[0];
            if (_configurations.ContainsKey(key))
                _configurations[key].Add((configuration, cycles));
            else
                _configurations.Add(key, new List<(int[], int)> {(configuration, cycles)});
        }

        private int GetCyclesSinceRepeat(int[] distribution, int cycles)
        {
            var key = distribution[0];
            if (!_configurations.ContainsKey(key))
                return 0;
            var repeated = GetRepeatedConfiguration(distribution);
            if (repeated.configuration != null)
                return cycles - repeated.cycle;
            return 0;
        }

        private (int[] configuration, int cycle) GetRepeatedConfiguration(int[] configuration)
        {
            var key = configuration[0];
            return _configurations[key].FirstOrDefault(IsRepeatedDistribution(configuration));
        }

        private static Func<(int[] configuration, int cycle), bool> IsRepeatedDistribution(int[] configuration)
        {
            return other => configuration.Zip(other.configuration, (x, y) => Math.Abs(x - y)).Sum() == 0;
        }

        [Fact]
        public void Answer()
        {
            int[] input = File.ReadAllText("./input1.txt").Split('\t').Select(int.Parse).ToArray();
            _output.WriteLine($"Part1: {Iterate(input)}");
        }
    }
}