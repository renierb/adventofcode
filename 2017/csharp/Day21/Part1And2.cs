// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day21
{
    public class Part1And2
    {
        private readonly ITestOutputHelper _output;

        public Part1And2(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void PatternTests()
        {
            string[] patterns2 =
            {
                "#./..",
                ".#/..",
                "../#.",
                "../.#",
            };

            IsSamePattern(patterns2[0], patterns2[0]);
            IsSamePattern(patterns2[0], patterns2[1]);
            IsSamePattern(patterns2[0], patterns2[2]);
            IsSamePattern(patterns2[1], patterns2[2]);
            IsSamePattern(patterns2[0], patterns2[3]);

            string[] patterns3 =
            {
                ".#./..#/###",
                ".#./#../###",
                "###/..#/.#.",
                "#../#.#/##.",
            };

            IsSamePattern(patterns3[0], patterns3[0]);
            IsSamePattern(patterns3[0], patterns3[1]);
            IsSamePattern(patterns3[0], patterns3[2]);
            IsSamePattern(patterns3[0], patterns3[3]);
            IsSamePattern(patterns3[1], patterns3[2]);
            IsSamePattern(patterns3[1], patterns3[3]);
            IsSamePattern(patterns3[2], patterns3[3]);
        }

        private static void IsSamePattern(string pattern1, string pattern2)
        {
            Assert.True(new Pattern(pattern1.Split('/')).Equals(new Pattern(pattern2.Split('/'))));
        }

        [Fact]
        public void Tests()
        {
            string[] rules =
            {
                "../.# => ##./#../...",
                ".#./..#/### => #..#/..../..../#..#",
            };

            TotalPixelsOn(rules, 12);
        }

        private void TotalPixelsOn(string[] rules, int expected)
        {
            Assert.Equal(expected, Compute(rules.Select(ParseInput).ToArray(), 2));
        }

        private static int Compute((Pattern input, Pattern output)[] rules, int iterations = 5)
        {
            var pattern = new Pattern(new[] {".#.", "..#", "###"});

            for (int i = 0; i < iterations; i++)
            {
                pattern = Combine(pattern.GetInnerPatterns().Select(EnhancePattern(rules)).ToArray());
            }

            return pattern.GetHashCode();
        }

        private static Func<Pattern, Pattern> EnhancePattern((Pattern input, Pattern output)[] rules)
        {
            return pattern =>
            {
                var rule = rules.FirstOrDefault(r => r.input.Equals(pattern));
                return rule.output;
            };
        }

        private static Pattern Combine(Pattern[] patterns)
        {
            var rowCount = patterns[0].Rows.Length;
            var patternRows = new List<string>(rowCount);
            foreach (var rows in patterns.Buffer((int) Math.Sqrt(patterns.Length)))
            {
                for (int i = 0; i < rowCount; i++)
                {
                    patternRows.Add(string.Concat(rows.Select(r => r.Rows[i])));
                }
            }
            return new Pattern(patternRows.ToArray());
        }

        // ReSharper disable UnusedTupleComponentInReturnValue
        private static (Pattern, Pattern) ParseInput(string line)
        {
            var patterns = line.Split(" => ").Select(p => new Pattern(p.Split('/'))).ToArray();
            return (patterns[0], patterns[1]);
        }

        [Fact]
        public void Answer()
        {
            (Pattern, Pattern)[] input = File.ReadAllLines("./input1.txt").Select(ParseInput).ToArray();
            _output.WriteLine($"Part1: {Compute(input)}");
            _output.WriteLine($"Part2: {Compute(input, 18)}");
        }
    }
}