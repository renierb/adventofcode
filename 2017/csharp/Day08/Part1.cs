// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day08
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
            var cpu = new Cpu();
            LargestCpuRegisterAfter(cpu, "b inc 5 if a > 1", 0);
            LargestCpuRegisterAfter(cpu, "a inc 1 if b < 5", 1);
            LargestCpuRegisterAfter(cpu, "c dec -10 if a >= 1", 10);
            LargestCpuRegisterAfter(cpu, "c inc -20 if c == 10", 1);
        }

        private static void LargestCpuRegisterAfter(Cpu cpu, string instruction, int expected)
        {
            Assert.Equal(expected, Compute(Enumerable.Repeat(instruction, 1), cpu));
        }

        private static int Compute(IEnumerable<string> input, Cpu cpu = null)
        {
            cpu = cpu ?? new Cpu();
            foreach (var instruction in InputParser.NextInstruction(input))
            {
                if (cpu.CheckCondition(instruction.Condition))
                {
                    cpu.Execute(instruction);
                }
            }

            return cpu.GetMax();
        }

        [Fact]
        public void Answer()
        {
            IEnumerable<string> input = File.ReadLines("./input1.txt");
            _output.WriteLine($"Part1: {Compute(input)}");
        }
    }
}