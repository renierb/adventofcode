using System.Collections.Generic;
using System.Numerics;

namespace Day18.Part2
{
    public class Program
    {
        private Dictionary<string, BigInteger> Registers { get; } = new Dictionary<string, BigInteger>();

        public Program(int programId)
        {
            ProgramId = programId;
            Registers["p"] = programId;
        }

        public int ProgramId { get; }
        public long InstructionIndex { get; set; }
        public long SendCount { get; set; }

        public BigInteger GetRegisterValue(string name)
        {
            if (!Registers.TryGetValue(name, out var value))
                Registers.Add(name, 0);
            return value;
        }

        public void SetRegisterValue(string name, BigInteger value)
        {
            Registers[name] = value;
        }

        public BigInteger GetValue(string value)
        {
            return BigInteger.TryParse(value, out var result) ? result : GetRegisterValue(value);
        }
    }
}