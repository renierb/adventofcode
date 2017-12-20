// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace Day20
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
            string[] particles =
            {
                "p=< 3,0,0>, v=< 2,0,0>, a=<-1,0,0>",
                "p=< 4,0,0>, v=< 0,0,0>, a=<-2,0,0>",
            };

            ClosestParticleOf(particles, 0);
        }

        private static void ClosestParticleOf(string[] input, int expected)
        {
            Assert.Equal(expected, Compute(input.Select(ParseInput).ToArray()));
        }

        private static long Compute(Particle[] particles)
        {
            var particle = particles.MinBy(p => p.AbsAcceleration).FirstOrDefault();
            return particle?.Id ?? -1;
        }

        private static Particle ParseInput(string line, int index)
        {
            var coordinates = Regex.Matches(line, @"((?:-?\d+,?)+)")
                .Select(m => m.Value.Split(',').Select(int.Parse).ToArray())
                .Select(c => new Coordinate(c[0], c[1], c[2]))
                .ToArray();
            return new Particle(index, coordinates[0], coordinates[1], coordinates[2]);
        }

        [Fact]
        public void Answer()
        {
            Particle[] input = File.ReadAllLines("./input1.txt").Select(ParseInput).ToArray();
            _output.WriteLine($"Part1: {Compute(input)}");
        }
    }
}