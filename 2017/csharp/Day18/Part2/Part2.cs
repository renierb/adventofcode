// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task Tests()
        {
            string[] instructions =
            {
                "snd 1",
                "snd 2",
                "snd p",
                "rcv a",
                "rcv b",
                "rcv c",
                "rcv d",
            };
            await TotalSendsOf(instructions, 3);
        }

        private static async Task TotalSendsOf(string[] input, int expected)
        {
            Assert.Equal(expected, await Compute(input.Select(ParseInput).ToArray()));
        }

        private static async Task<long> Compute(Instruction[] input)
        {
            var program0 = new Program(0);
            var program1 = new Program(1);

            program0.OtherProgram = program1;
            program1.OtherProgram = program0;

            var task0 = Task.Run(() => program0.Execute(input));
            var task1 = Task.Run(() => program1.Execute(input));

            await Task.WhenAll(task0, task1);
            
            return program1.SendCount;
        }

        private static Instruction ParseInput(string line)
        {
            var parts = line.Split(' ');
            return new Instruction(parts[1], parts[0], parts.Length > 2 ? parts[2] : null);
        }

        [Fact]
        public async Task Answer()
        {
            Instruction[] input = File.ReadAllLines("./input1.txt").Select(ParseInput).ToArray();
            var answer = await Compute(input);
            _output.WriteLine($"Part1: {answer}");
        }
    }
}