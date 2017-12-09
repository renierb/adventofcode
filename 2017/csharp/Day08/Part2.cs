// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day08
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
            var cpu = new Cpu();
            LargestEverCpuRegisterAfter(cpu, "b inc 5 if a > 1", 0);
            LargestEverCpuRegisterAfter(cpu, "a inc 1 if b < 5", 1);
            LargestEverCpuRegisterAfter(cpu, "c dec -10 if a >= 1", 10);
            LargestEverCpuRegisterAfter(cpu, "c inc -20 if c == 10", 10);
        }

        private static void LargestEverCpuRegisterAfter(Cpu cpu, string instruction, int expected)
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

            return cpu.GetMaxEver();
        }

        [Fact]
        public void Answer()
        {
            IEnumerable<string> input = File.ReadLines("./input1.txt");
            _output.WriteLine($"Part1: {Compute(input)}");
        }
    }
}