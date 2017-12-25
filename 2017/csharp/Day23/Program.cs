using System.Collections.Generic;
using System.Numerics;

namespace Day23
{
    public class Program
    {
        private Dictionary<string, BigInteger> Registers { get; } = new Dictionary<string, BigInteger>();

        public Program(int programId)
        {
            ProgramId = programId;
        }

        public int ProgramId { get; }
        public long InstructionIndex { get; set; }
        public long Count { get; set; }

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
    }
}