using System;
using System.Collections.Generic;

namespace Day18.Part1
{
    public class Program
    {
        private readonly Dictionary<string, long> Registers = new Dictionary<string, long>();
        private long Index { get; set; }
        private long Sound { get; set; }

        private static readonly Dictionary<string, Action<Program, string, string>> Actions = new Dictionary<string, Action<Program, string, string>>(7)
        {
            {"snd", Snd},
            {"set", Set},
            {"add", Add},
            {"mul", Mul},
            {"mod", Mod},
            {"rcv", Rcv},
            {"jgz", Jgz},
        };

        public long RecoveredSound { get; private set; }

        public void Execute(Instruction[] instructions)
        {
            while (Index >= 0 && Index < instructions.Length)
            {
                Execute(instructions[Index]);
            }
        }

        private void Execute(Instruction instr)
        {
            Actions[instr.Action](this, instr.Register, instr.Value);
        }

        private static void Snd(Program program, string registerX, string value)
        {
            program.Sound = program.GetRegisterValue(registerX);
            program.Index++;
        }

        private static void Set(Program program, string register, string value)
        {
            program.SetRegisterValue(register, program.GetValue(value));
            program.Index++;
        }

        private static void Add(Program program, string registerX, string valueY)
        {
            var x = program.GetRegisterValue(registerX);
            var y = program.GetValue(valueY);
            program.SetRegisterValue(registerX, x + y);
            program.Index++;
        }

        private static void Mul(Program program, string registerX, string valueY)
        {
            var x = program.GetRegisterValue(registerX);
            var y = program.GetValue(valueY);
            program.SetRegisterValue(registerX, x * y);
            program.Index++;
        }

        private static void Mod(Program program, string registerX, string valueY)
        {
            var x = program.GetRegisterValue(registerX);
            var y = program.GetValue(valueY);
            program.SetRegisterValue(registerX, x % y);
            program.Index++;
        }

        private static void Rcv(Program program, string registerX, string valueY = null)
        {
            var x = program.GetRegisterValue(registerX);
            if (x != 0)
            {
                program.RecoveredSound = program.Sound;
                program.Index = -1;
                return;
            }

            program.Index++;
        }

        private static void Jgz(Program program, string registerX, string valueY)
        {
            var x = program.GetRegisterValue(registerX);
            if (x > 0)
                program.Index += program.GetValue(valueY);
            else
                program.Index++;
        }

        private long GetValue(string value)
        {
            return long.TryParse(value, out var result) ? result : GetRegisterValue(value);
        }

        private long GetRegisterValue(string name)
        {
            if (!Registers.TryGetValue(name, out var value))
                Registers.Add(name, 0);
            return value;
        }

        private void SetRegisterValue(string name, long value)
        {
            Registers[name] = value;
        }
    }
}