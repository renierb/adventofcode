// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day13
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
            StepsAwayOf("", 0);
        }

        private static void StepsAwayOf(string input, int expected)
        {
            Assert.Equal(expected, Compute(input.Split(',')));
        }

        private static int Compute(IEnumerable<string> steps)
        {
            return 0;
        }

        //[Fact]
        public void Answer()
        {
            string input = File.ReadAllText("./input1.txt");
            _output.WriteLine($"Part1: {Compute(input.Split(','))}");
        }
    }
}