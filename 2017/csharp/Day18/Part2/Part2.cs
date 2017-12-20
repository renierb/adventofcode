// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day18.Part2
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
            string[] instructions =
            {
                "snd 1", "snd 2", "snd p", "rcv a", "rcv b", "rcv c", "rcv d", "jgz a -2"
            };
            TotalSendsOf(instructions, 3);
            instructions = new[]
            {
                "snd 1", "rcv a", "snd 2", "rcv b", "snd p", "rcv c", "rcv d", "jgz a -2"
            };
            TotalSendsOf(instructions, 3);
            instructions = new[]
            {
                "snd 1", "rcv a", "snd 2", "rcv b", "snd p", "rcv c", "jgz a -1"
            };
            TotalSendsOf(instructions, 3);
            instructions = new[]
            {
                "snd 1", "rcv a", "snd 2", "jgz a 3", "rcv b", "snd p", "rcv c", "jgz a -1"
            };
            TotalSendsOf(instructions, 2);
            instructions = new[]
            {
                "snd 1", "rcv a", "snd 2", "jgz a 4", "jgz a 1", "rcv b", "jgz a 4", "snd p", "rcv c", "jgz a -5", "rcv d"
            };
            TotalSendsOf(instructions, 3);
        }

        private static void TotalSendsOf(string[] input, long expected)
        {
            Assert.Equal(expected, Compute(input.Select(ParseInput).ToArray()));
        }

        private static long Compute(Instruction[] instructions)
        {
            var program0 = new Program(0);
            var program1 = new Program(1);

            var cpu = new Cpu(new[] {program0, program1});
            cpu.Execute(instructions);

            return program1.SendCount;
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
            _output.WriteLine($"Part2: {Compute(input)}");
        }
    }
}