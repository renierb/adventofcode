using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day10
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
            HashOf(256, "", "a2582a3a0e66e6e86e3812dcb672a272");
            HashOf(256, "AoC 2017", "33efeb34ea91902bb2f59c9920caa6cd");
            HashOf(256, "1,2,3", "3efbe78a8d82f29979031a4aa0b16a9d");
            HashOf(256, "1,2,4", "63960835bcdc130f0b66d7ff4f6a5a8e");
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private static void HashOf(int hashLength, string input, string expected)
        {
            Assert.Equal(expected, Compute(hashLength, GetInputLengths(input)));
        }

        private static int[] GetInputLengths(string input)
        {
            return input.Select(c => (int) c).Concat(new[] {17, 31, 73, 47, 23}).ToArray();
        }

        private static string Compute(int hashLength, int[] lengths)
        {
            int[] hash = Enumerable.Range(0, hashLength).ToArray();

            int index = 0;
            int skip = 0;
            for (int i = 0; i < 64; i++)
            {
                ComputeHash(lengths, hash, ref index, ref skip);
            }
            return GetDenseHash(GetSparseHash(hash));
        }

        private static void ComputeHash(int[] lengths, int[] hash, ref int index, ref int skip)
        {
            foreach (var length in lengths)
            {
                ReverseSection(hash, index, length);
                index = (index + length + skip) % hash.Length;
                skip = skip + 1;
            }
        }

        private static int[] GetSparseHash(int[] hash)
        {
            return hash.Buffer(16).Select(buffer => buffer.Aggregate(0, (acc, x) => acc ^ x)).ToArray();
        }

        private static string GetDenseHash(int[] hash)
        {
            return string.Concat(hash.Select(x => $"{x:x2}"));
        }

        private static void ReverseSection(int[] hash, int index, int length)
        {
            var hashLength = hash.Length;
            if (IsWrapping(index, length, hashLength))
            {
                var tailLength = hashLength - index;
                var headLength = length - tailLength;
                var reversed = hash.Skip(index).Take(tailLength).Concat(hash.Take(headLength)).Reverse().ToArray();
                UpdateHash(hash, reversed, index, length);
            }
            else
            {
                var reversed = hash.Skip(index).Take(length).Reverse().ToArray();
                UpdateHash(hash, reversed, index, length);
            }
        }

        private static bool IsWrapping(int index, int length, int hashLength)
        {
            return index + length >= hashLength;
        }

        private static void UpdateHash(int[] hashed, int[] reversed, int index, int length)
        {
            for (int i = index; i < index + length; i++)
            {
                hashed[i % hashed.Length] = reversed[i - index];
            }
        }

        [Fact]
        public void Answer()
        {
            string input = File.ReadAllText("./input1.txt");
            _output.WriteLine($"Part2: {Compute(256, GetInputLengths(input))}");
        }
    }
}