// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace Day20
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
            string[] particles =
            {
                "p=<-6,0,0>, v=< 3,0,0>, a=< 0,0,0>",
                "p=<-4,0,0>, v=< 2,0,0>, a=< 0,0,0>",
                "p=<-2,0,0>, v=< 1,0,0>, a=< 0,0,0>",
                "p=< 3,0,0>, v=<-1,0,0>, a=< 0,0,0>",
            };

            ParticlesRemainingOf(particles, 1);
        }

        private static void ParticlesRemainingOf(string[] input, int expected)
        {
            Assert.Equal(expected, Compute(input.Select(ParseInput).ToArray()));
        }

        // ReSharper disable PossibleMultipleEnumeration
        private static long Compute(Particle[] particles)
        {
            var tick0 = particles.Distinct();
            var tick1 = MoveParticles(tick0);

            while (AnyConverging(tick0, tick1))
            {
                tick0 = tick1;
                tick1 = MoveParticles(tick0);
            }

            return tick1.Count();
        }
        // ReSharper restore PossibleMultipleEnumeration

        private static bool AnyConverging(IEnumerable<Particle> tick0, IEnumerable<Particle> tick1)
        {
            return tick0
                .Join(tick1, p => p.Id, p => p.Id, (p1, p2) => p1.MinDistance > p2.MinDistance ? p2 : null)
                .Any(p => p != null);
        }

        private static IEnumerable<Particle> MoveParticles(IEnumerable<Particle> particles)
        {
            return particles.Select(p => p.Move()).Distinct().ToArray().Where(p => p.MinDistance > 0);
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
            _output.WriteLine($"Part2: {Compute(input)}");
        }
    }
}