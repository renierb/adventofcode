using System;
using System.IO;
using System.Linq;
using Xunit;

namespace Day01
{
    public class Part1
    {
        [Fact]
        public void Tests()
        {
            InverseCaptchaOf("", 0);
            InverseCaptchaOf("0", 0);
            InverseCaptchaOf("00", 0);
            InverseCaptchaOf("1", 1);
            InverseCaptchaOf("11", 2);
            InverseCaptchaOf("1111", 4);
            InverseCaptchaOf("12", 0);
            InverseCaptchaOf("1234", 0);
            InverseCaptchaOf("1122", 3);
            InverseCaptchaOf("91212129", 9);
        }

        [Fact]
        public void Answer()
        {
            string input = File.ReadAllText("./input1.txt");
            Console.WriteLine($"Part1: {ComputeInverse(input)}");
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private void InverseCaptchaOf(string input, int expected)
        {
            Assert.Equal(expected, ComputeInverse(input));
        }

        private static int ComputeInverse(string input)
        {
            var numbers = input.Select(c => int.Parse(c.ToString())).ToArray();
            
            int sum = 0;
            for (var i = 0; i < numbers.Length; i++)
            {
                if (i == numbers.Length - 1 && numbers[0] == numbers[i])
                    sum += numbers[0];
                if (i > 0 && numbers[i] == numbers[i - 1])
                    sum += numbers[i];
            }
            return sum;
        }
    }
}