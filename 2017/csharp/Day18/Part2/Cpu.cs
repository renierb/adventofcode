using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Day18.Part2
{
    public class Cpu
    {
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

        private int Index { get; set; }
        private static readonly List<Queue<long>> Queues = new List<Queue<long>>(Enumerable.Repeat(new Queue<long>(), 2));
        
        public void Execute(Instruction[] instructions, Program[] programs)
        {
            var program = programs[Index];
            while (program.Index >= 0 && program.Index < instructions.Length)
            {
                Execute(program, instructions[program.Index]);
            }
        }

        private static void Execute(Program program, Instruction instr)
        {
            Actions[instr.Action](program, instr.Register, instr.Value);
        }

        private static void Snd(Program program, string registerX, string value)
        {
            var x = program.GetRegisterValue(registerX);
            Queues[(program.ProgramId + 1) % Queues.Count].Enqueue(x);
            program.SendCount++;
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
            checked
            {
                program.SetRegisterValue(registerX, x + y);
            }
            program.Index++;
        }

        private static void Mul(Program program, string registerX, string valueY)
        {
            var x = program.GetRegisterValue(registerX);
            var y = program.GetValue(valueY);
            checked
            {
                program.SetRegisterValue(registerX, x * y);
            }
            program.Index++;
        }

        private static void Mod(Program program, string registerX, string valueY)
        {
            var x = program.GetRegisterValue(registerX);
            var y = program.GetValue(valueY);
            program.SetRegisterValue(registerX, Math.Abs(x % y));
            program.Index++;
        }

        private static void Rcv(Program program, string registerX, string valueY = null)
        {
            do
            {
                if (program.MessageQueue.TryDequeue(out var value))
                {
                    program.SetRegisterValue(registerX, value);
                    program.Index++;
                    return;
                }

                program.OtherProgram.Signal.Set();
                if (program.MessageQueue.IsEmpty && program.OtherProgram.MessageQueue.IsEmpty)
                {
                    program.Index = -1;
                    return;
                }

                program.Signal.WaitOne();
            } while (true);
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

        private void Send(long value)
        {
            MessageQueue.Enqueue(value);
        }
    }
}