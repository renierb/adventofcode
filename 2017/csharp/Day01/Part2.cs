using System;
using System.IO;
using System.Linq;
using Xunit;

namespace Day01
{
    public class Part2
    {
        [Fact]
        public void Tests()
        {
            InverseCaptchaOf("", 0);
            InverseCaptchaOf("00", 0);
            InverseCaptchaOf("11", 2);
            InverseCaptchaOf("1212", 6);
            InverseCaptchaOf("1221", 0);
            InverseCaptchaOf("123425", 4);
            InverseCaptchaOf("123123", 12);
            InverseCaptchaOf("12131415", 4);
        }

        [Fact]
        public void Answer()
        {
            string input = File.ReadAllText("./input2.txt");
            Console.WriteLine($"Part2: {ComputeInverse(input)}");
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private void InverseCaptchaOf(string input, int expected)
        {
            Assert.Equal(expected, ComputeInverse(input));
        }

        private static int ComputeInverse(string input)
        {
            var numbers = input.Select(c => int.Parse(c.ToString())).ToArray();

            int steps = numbers.Length / 2;
            return numbers.Where((n, i) => numbers[i] == numbers[(i + steps) % numbers.Length ]).Sum();
        }
    }
}