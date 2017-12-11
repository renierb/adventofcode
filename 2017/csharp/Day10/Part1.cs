using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day10
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
            HashOf(5, new[] {3, 4, 1, 5}, 12);
        }

        private static void HashOf(int hashLength, int[] lengths, int expected)
        {
            Assert.Equal(expected, Compute(hashLength, lengths));
        }

        private static int Compute(int hashLength, int[] lengths)
        {
            int[] hashed = Enumerable.Range(0, hashLength).ToArray();
            int index = 0;
            int skip = 0;
            foreach (var length in lengths)
            {
                ReverseSection(hashed, index, length);

                index = (index + length + skip) % hashed.Length;
                skip = skip + 1;
            }
            return hashed[0] * hashed[1];
        }

        private static void ReverseSection(int[] hashed, int index, int length)
        {
            if (index + length >= hashed.Length)
            {
                var tailLength = hashed.Length - index;
                var headLength = length - tailLength;
                var reversed = hashed.Skip(index).Take(tailLength).Concat(hashed.Take(headLength)).Reverse().ToArray();
                Update(hashed, reversed, index, length);
            }
            else
            {
                var reversed = hashed.Skip(index).Take(length).Reverse().ToArray();
                Update(hashed, reversed, index, length);
            }
        }

        private static void Update(int[] hashed, int[] reversed, int index, int length)
        {
            for (int i = index; i < index + length; i++)
            {
                hashed[i % hashed.Length] = reversed[i - index];
            }
        }

        [Fact]
        public void Answer()
        {
            int[] input = File.ReadAllText("./input1.txt").Split(',').Select(int.Parse).ToArray();
            _output.WriteLine($"Part1: {Compute(256, input)}");
        }
    }
}