using System;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day04
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
            PassphraseOf("", isValid: false);
            PassphraseOf("aa", isValid: true);
            PassphraseOf("aa aa", isValid: false);
            PassphraseOf("aa bb cc dd ee", isValid: true);
            PassphraseOf("aa bb cc dd aa", isValid: false);
            PassphraseOf("aa bb cc dd aaa", isValid: true);
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private static void PassphraseOf(string input, bool isValid)
        {
            Assert.Equal(isValid, IsValidPassphrase(input));
        }

        private static bool IsValidPassphrase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            return input.Split(' ').GroupBy(word => word).All(groups => groups.Count() == 1);
        }

        [Fact]
        public void Answer()
        {
            string[] input = File.ReadAllLines("./input1.txt");
            var count = input.Where(IsValidPassphrase).Count();
            _output.WriteLine($"Part1: {count}");
        }
    }
}