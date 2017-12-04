using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day04
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
            PassphraseOf("", isValid: false);
            PassphraseOf("aa", isValid: true);
            PassphraseOf("ab ba", isValid: false);
            PassphraseOf("abcde fghij", isValid: true);
            PassphraseOf("abcde xyz ecdab", isValid: false);
            PassphraseOf("a ab abc abd abf abj", isValid: true);
            PassphraseOf("iiii oiii ooii oooi oooo", isValid: true);
            PassphraseOf("oiii ioii iioi iiio", isValid: false);
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
            return input.Split(' ').GroupBy(word => string.Concat(word.OrderBy(c => c))).All(groups => groups.Count() == 1);
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