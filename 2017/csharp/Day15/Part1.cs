// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day15
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
            MatchesOf(65, 8921, 588);
        }

        private static void MatchesOf(int genA, int genB, int expected)
        {
            Assert.Equal(expected, Compute(genA, genB));
        }

        private static int Compute(int genA, int genB)
        {
            var generatorA = Generator(genA, 16807);
            var generatorB = Generator(genB, 48271);
            return generatorA.Zip(generatorB, (a, b) => (a << 48) ^ (b << 48)).Take(40000000).Count(result => result == 0);
        }

        private static IEnumerable<long> Generator(long start, int multiplier)
        {
            return EnumerableEx.Generate(start, _ => true, next => (next * multiplier) % int.MaxValue, _ => _);
        }

        [Fact]
        public void Answer()
        {
            _output.WriteLine($"Part1: {Compute(289, 629)}");
        }
    }
}