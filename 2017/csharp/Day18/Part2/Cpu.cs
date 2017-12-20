using System;
using System.Collections.Generic;
using System.Linq;

namespace Day18.Part2
{
    public class Cpu
    {
        private static readonly Dictionary<string, Action<Cpu, string, string>> Actions =
            new Dictionary<string, Action<Cpu, string, string>>(7)
            {
                {"snd", Snd},
                {"set", Set},
                {"add", Add},
                {"mul", Mul},
                {"mod", Mod},
                {"rcv", Rcv},
                {"jgz", Jgz},
            };

        public Cpu(Program[] programs)
        {
            Programs = programs;
            Queues = new List<Queue<long>>(programs.Length);
            Programs.ForEach((_, i) => Queues.Add(new Queue<long>()));
        }

        private int ProgramIndex { get; set; }
        private Program[] Programs { get; }
        private List<Queue<long>> Queues { get; }

        private Program CurrentProgram => Programs?[ProgramIndex];

        public void Execute(Instruction[] instructions)
        {
            while (Programs.Any(program => program.InstructionIndex >= 0 && program.InstructionIndex < instructions.Length))
            {
                Execute(instructions[CurrentProgram.InstructionIndex++]);
            }
        }

        private void Execute(Instruction instr)
        {
            Actions[instr.Action](this, instr.Register, instr.Value);
        }

        private void SendMessage(long value)
        {
            var programId = (CurrentProgram.ProgramId + 1) % Programs.Length;
            Queues[programId].Enqueue(value);
            CurrentProgram.SendCount++;
        }

        private static void Snd(Cpu cpu, string valueX, string _)
        {
            var x = cpu.CurrentProgram.GetValue(valueX);
            cpu.SendMessage(x);
        }

        private static void Set(Cpu cpu, string registerX, string valueY)
        {
            var y = cpu.CurrentProgram.GetValue(valueY);
            cpu.CurrentProgram.SetRegisterValue(registerX, y);
        }

        private static void Add(Cpu cpu, string registerX, string valueY)
        {
            var x = cpu.CurrentProgram.GetRegisterValue(registerX);
            var y = cpu.CurrentProgram.GetValue(valueY);
            checked
            {
                cpu.CurrentProgram.SetRegisterValue(registerX, x + y);
            }
        }

        private static void Mul(Cpu cpu, string registerX, string valueY)
        {
            var x = cpu.CurrentProgram.GetRegisterValue(registerX);
            var y = cpu.CurrentProgram.GetValue(valueY);
            checked
            {
                cpu.CurrentProgram.SetRegisterValue(registerX, x * y);
            }
        }

        private static void Mod(Cpu cpu, string registerX, string valueY)
        {
            var x = cpu.CurrentProgram.GetRegisterValue(registerX);
            var y = cpu.CurrentProgram.GetValue(valueY);
            cpu.CurrentProgram.SetRegisterValue(registerX, Math.Abs(x % y));
        }

        private static void Rcv(Cpu cpu, string registerX, string valueY = null)
        {
            if (cpu.Queues[cpu.CurrentProgram.ProgramId].TryDequeue(out var value))
            {
                cpu.CurrentProgram.SetRegisterValue(registerX, value);
                return;
            }

            if (cpu.Queues.All(_ => _.IsEmpty()))
            {
                cpu.CurrentProgram.InstructionIndex = -1;
            }

            SwitchProgram(cpu);
        }

        private static void SwitchProgram(Cpu cpu)
        {
            cpu.ProgramIndex = (cpu.ProgramIndex + 1) % cpu.Programs.Length;
        }

        private static void Jgz(Cpu cpu, string registerX, string valueY)
        {
            var x = cpu.CurrentProgram.GetRegisterValue(registerX);
            if (x > 0)
                cpu.CurrentProgram.InstructionIndex += cpu.CurrentProgram.GetValue(valueY) - 1;
        }
    }
}