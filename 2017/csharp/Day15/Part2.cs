// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day15
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
            MatchesOf(65, 8921, 309);
        }

        private static void MatchesOf(int genA, int genB, int expected)
        {
            Assert.Equal(expected, Compute(genA, genB));
        }

        private static int Compute(int genA, int genB)
        {
            var generatorA = Generator(genA, 16807, n => n % 4 == 0);
            var generatorB = Generator(genB, 48271, n => n % 8 == 0);
            return generatorA.Zip(generatorB, (a, b) => (a << 48) ^ (b << 48)).Take(5000000).Count(result => result == 0);
        }

        private static IEnumerable<long> Generator(long start, int multiplier, Func<long, bool> condition)
        {
            return EnumerableEx.Generate(start, _ => true, next => (next * multiplier) % int.MaxValue, _ => _).Where(condition);
        }

        [Fact]
        public void Answer()
        {
            _output.WriteLine($"Part2: {Compute(289, 629)}");
        }
    }
}