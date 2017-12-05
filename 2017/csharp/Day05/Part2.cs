using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day05
{
    public class Part2
    {
        private readonly ITestOutputHelper _output;

        public Part2(ITestOutputHelper output)
        {
            _output = output;
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
                SetNewOffset(input, atIndex, offset);

                atIndex = atIndex + offset;
                steps = steps + 1;
            }
        }

        private static void SetNewOffset(int[] input, int atIndex, int offset)
        {
            if (offset >= 3)
                input[atIndex]--;
            else
                input[atIndex]++;
        }

        [Fact]
        public void Answer()
        {
            int[] input = File.ReadAllLines("./input1.txt").Select(int.Parse).ToArray();
            _output.WriteLine($"Part1: {Iterate(input)}");
        }
    }
}