// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day11
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
            StepsAwayOf("ne", 1);
            StepsAwayOf("ne,ne,ne", 3);
            StepsAwayOf("ne,sw", 0);
            StepsAwayOf("ne,ne,sw,sw", 0);
            StepsAwayOf("ne,ne,s,s", 2);
            StepsAwayOf("se,sw,se,sw,sw", 3);
            StepsAwayOf("n,n,s,s,sw", 1);
            StepsAwayOf("n,se,s,sw,nw,n,ne", 1);
        }

        private static void StepsAwayOf(string input, int expected)
        {
            Assert.Equal(expected, Compute(input.Split(',')));
        }

        private static int Compute(IEnumerable<string> steps)
        {
            (int x, int y) pos = (0, 0);
            steps.ForEach(step =>
            {
                if (step == "n")
                {
                    pos.x -= 1;
                    pos.y += 1;
                }
                else if (step == "s")
                {
                    pos.x += 1;
                    pos.y -= 1;
                }
                else if (step == "nw")
                {
                    pos.x -= 1;
                }
                else if (step == "se")
                {
                    pos.x += 1;
                }
                else if (step == "ne")
                {
                    pos.y += 1;
                }
                else if (step == "sw")
                {
                    pos.y -= 1;
                }
            });
            return GetDistance(pos);
        }

        private static int GetDistance((int x, int y) pos)
        {
            return Math.Max(Math.Abs(pos.x), Math.Abs(pos.y));
        }

        [Fact]
        public void Answer()
        {
            string input = File.ReadAllText("./input1.txt");
            _output.WriteLine($"Part1: {Compute(input.Split(','))}");
        }
    }
}