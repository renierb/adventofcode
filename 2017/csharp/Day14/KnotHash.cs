using System.Linq;

namespace Day14
{
    public static class KnotHash
    {
        public static string GetHash(string input, int hashLength = 256)
        {
            int[] hash = Enumerable.Range(0, hashLength).ToArray();

            int index = 0;
            int skip = 0;
            for (int i = 0; i < 64; i++)
            {
                ComputeHash(GetInputLengths(input), hash, ref index, ref skip);
            }
            return GetDenseHash(GetSparseHash(hash));
        }

        private static int[] GetInputLengths(string input)
        {
            return input.Select(c => (int) c).Concat(new[] {17, 31, 73, 47, 23}).ToArray();
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
    }
}