// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System.IO;
using System.Linq;
using System.Numerics;
using Xunit;
using Xunit.Abstractions;

namespace Day23
{
    public class Part2
    {
        private readonly ITestOutputHelper _output;

        public Part2(ITestOutputHelper output)
        {
            _output = output;
        }

        // ReSharper disable UnusedMember.Local
        private static BigInteger Compute(Instruction[] instructions)
        {
            var program0 = new Program(0);
            program0.SetRegisterValue("a", 1);

            var cpu = new Cpu(new[] {program0});
            cpu.Execute(instructions);

            return program0.GetRegisterValue("h");
        }

        private static BigInteger Compute()
        {
            var b = 57;
            b = b * 100;
            b = b + 100000;
            var c = b;
            c = c + 17000;
            var h = 0;
            do
            {
                var f = 1;
                for (var d = 2; d <= (b / 2); d++)
                {
                    for (var e = 2; e <= (b / d); e++)
                    {
                        if (d * e == b)
                        {
                            f = 0;
                            break;
                        }
                    }
                    if (f == 0)
                        break;
                }
                if (f == 0)
                    h++;
                b += 17;
            } while (b <= c);
            return h;
        }

        private static Instruction ParseInput(string line)
        {
            var parts = line.Split(' ');
            return new Instruction(parts[0], parts[1], parts[2]);
        }

        [Fact]
        public void DebugTest()
        {
            Instruction[] input = File.ReadAllLines("./input2.txt").Select(ParseInput).ToArray();
            _output.WriteLine($"Part2: {Compute(input)}");
        }

        [Fact]
        public void Answer()
        {
            _output.WriteLine($"Part2: {Compute()}");
        }
    }
}