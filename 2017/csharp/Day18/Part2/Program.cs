using System.Collections.Generic;

namespace Day18.Part2
{
    public class Program
    {
        private Dictionary<string, long> Registers { get; } = new Dictionary<string, long>();

        public Program(int programId)
        {
            ProgramId = programId;
            Registers["p"] = programId;
        }

        public int ProgramId { get; }
        public long InstructionIndex { get; set; }
        public long SendCount { get; set; }

        public long GetRegisterValue(string name)
        {
            if (!Registers.TryGetValue(name, out var value))
                Registers.Add(name, 0);
            return value;
        }

        public void SetRegisterValue(string name, long value)
        {
            Registers[name] = value;
        }

        public long GetValue(string value)
        {
            return long.TryParse(value, out var result) ? result : GetRegisterValue(value);
        }
    }
}