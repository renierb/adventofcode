// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System.Collections.Generic;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace Day09
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
            TotalGroupsOf("", 0);
            TotalGroupsOf("{}", 1);
            TotalGroupsOf("{", 0);
            TotalGroupsOf("}", 0);
            TotalGroupsOf("{{}", 1);
            TotalGroupsOf("{}{}", 2);
            TotalGroupsOf("{{{}}}", 6);
            TotalGroupsOf("{{},{}}", 5);
            TotalGroupsOf("{{{},{},{{}}}}", 16);
            TotalGroupsOf("{<a>,<a>,<a>,<a>}", 1);
            TotalGroupsOf("{{<ab>},{<ab>},{<ab>},{<ab>}}", 9);
            TotalGroupsOf("{{<!!>},{<!!>},{<!!>},{<!!>}}", 9);
            TotalGroupsOf("{{<a!>},{<a!>},{<a!>},{<ab>}}", 3);
        }

        private static void TotalGroupsOf(string input, int expected)
        {
            Assert.Equal(expected, Compute(input));
        }

        private static int Compute(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;
            int groups = 0;
            bool isNegatorToggled = false;
            bool collectingGarbage = false;
            var brackets = new Stack<char>();
            foreach (var c in input)
            {
                if (isNegatorToggled)
                {
                    isNegatorToggled = false;
                    continue;
                }
                if (c == '!')
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
                if (!collectingGarbage)
                    if (IsGroupOpening(c))
                    {
                        brackets.Push(c);
                    }
                    else if (IsGroupClosing(brackets, c))
                    {
                        groups += brackets.Count;
                        brackets.Pop();
                    }
            }
            while (groups > 0 && brackets.Count > 0)
            {
                brackets.Pop();
                groups--;
            }
            return groups;
        }

        private static bool IsGarbageOpening(char c)
        {
            return c == '<';
        }

        private static bool IsGarbageClosing(char c)
        {
            return c == '>';
        }

        private static bool IsGroupOpening(char c)
        {
            return c == '{';
        }

        private static bool IsGroupClosing(Stack<char> brackets, char c)
        {
            return c == '}' && brackets.TryPeek(out var x) && x == '{';
        }

        [Fact]
        public void Answer()
        {
            string input = File.ReadAllText("./input1.txt");
            _output.WriteLine($"Part1: {Compute(input)}");
        }
    }
}