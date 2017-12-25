// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day23
{
    public class Part1
    {
        private readonly ITestOutputHelper _output;

        public Part1(ITestOutputHelper output)
        {
            _output = output;
        }

        private static long Compute(Instruction[] instructions)
        {
            var program0 = new Program(0);

            var cpu = new Cpu(new[] {program0});
            cpu.Execute(instructions);

            return program0.Count;
        }

        private static Instruction ParseInput(string line)
        {
            var parts = line.Split(' ');
            return new Instruction(parts[0], parts[1], parts.Length > 2 ? parts[2] : null);
        }

        [Fact]
        public void Answer()
        {
            Instruction[] input = File.ReadAllLines("./input1.txt").Select(ParseInput).ToArray();
            _output.WriteLine($"Part1: {Compute(input)}");
        }
    }
}