// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day18.Part1
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
            string[] instructions =
            {
                "set a 1",
                "add a 2",
                "mul a a",
                "mod a 5",
                "snd a",
                "set a 0",
                "rcv a",
                "jgz a -1",
                "set a 1",
                "jgz a -2",
            };

            LastFrequencyOf(instructions, 4);
        }

        private static void LastFrequencyOf(string[] input, int expected)
        {
            Assert.Equal(expected, Compute(input.Select(ParseInput).ToArray()));
        }

        private static long Compute(Instruction[] input)
        {
            var program = new Program();
            program.Execute(input);
            return program.RecoveredSound;
        }

        private static Instruction ParseInput(string line)
        {
            var parts = line.Split(' ');
            return new Instruction(parts[1], parts[0], parts.Length > 2 ? parts[2] : null);
        }

        [Fact]
        public void Answer()
        {
            Instruction[] input = File.ReadAllLines("./input1.txt").Select(ParseInput).ToArray();
            _output.WriteLine($"Part1: {Compute(input)}");
        }
    }
}