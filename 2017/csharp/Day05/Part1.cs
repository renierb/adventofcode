using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day05
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
            StepsToExitOf(new[] {0}, 2);
            StepsToExitOf(new[] {1}, 1);
            StepsToExitOf(new[] {0, 3, 0, 1, -3}, 5);
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private static void StepsToExitOf(int[] input, int steps)
        {
            Assert.Equal(steps, Iterate(input));
        }

        private static int Iterate(int[] input)
        {
            int atIndex = 0;
            int steps = 0;
            while (true)
            {
                if (atIndex >= input.Length)
                    return steps;

                var offset = input[atIndex];
                input[atIndex]++;

                atIndex = atIndex + offset;
                steps = steps + 1;
            }
        }

        [Fact]
        public void Answer()
        {
            int[] input = File.ReadAllLines("./input1.txt").Select(int.Parse).ToArray();
            _output.WriteLine($"Part1: {Iterate(input)}");
        }
    }
}