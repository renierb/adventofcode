// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace Day09
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
            TotalGroupsOf("", 0);
            TotalGroupsOf("<>", 0);
            TotalGroupsOf("<random characters>", 17);
            TotalGroupsOf("<<<<>", 3);
            TotalGroupsOf("<{!>}>", 2);
            TotalGroupsOf("<!!>", 0);
            TotalGroupsOf("<!!!>>", 0);
            TotalGroupsOf("<{o\"i!a,<{i<a>", 10);
            TotalGroupsOf("{<a>,<a>,<a>,<a>}", 4);
            TotalGroupsOf("{{<ab>},{<ab>},{<ab>},{<ab>}}", 8);
            TotalGroupsOf("{{<!!>},{<!!>},{<!!>},{<!!>}}", 0);
            TotalGroupsOf("{{<a!>},{<a!>},{<a!>},{<ab>}}", 17);
        }

        private static void TotalGroupsOf(string input, int expected)
        {
            Assert.Equal(expected, Compute(input));
        }

        private static int Compute(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;
            int garbage = 0;
            bool isNegatorToggled = false;
            bool collectingGarbage = false;
            foreach (var c in input)
            {
                if (isNegatorToggled)
                {
                    isNegatorToggled = false;
                    continue;
                }
                if (IsNegator(c))
                {
                    isNegatorToggled = true;
                    continue;
                }
                if (!collectingGarbage && IsGarbageOpening(c))
                {
                    collectingGarbage = true;
                    continue;
                }
                if (IsGarbageClosing(c))
                {
                    collectingGarbage = false;
                    continue;
                }
                if (collectingGarbage)
                {
                    garbage++;
                }
            }
            return garbage;
        }

        private static bool IsNegator(char c)
        {
            return c == '!';
        }

        private static bool IsGarbageOpening(char c)
        {
            return c == '<';
        }

        private static bool IsGarbageClosing(char c)
        {
            return c == '>';
        }

        [Fact]
        public void Answer()
        {
            string input = File.ReadAllText("./input1.txt");
            _output.WriteLine($"Part1: {Compute(input)}");
        }
    }
}