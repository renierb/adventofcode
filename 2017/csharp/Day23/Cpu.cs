using System;
using System.Collections.Generic;
using System.Linq;

namespace Day23
{
    public class Cpu
    {
        private static readonly Dictionary<string, Action<Cpu, Instruction>> Actions =
            new Dictionary<string, Action<Cpu, Instruction>>(4)
            {
                {"set", Set},
                {"sub", Sub},
                {"mul", Mul},
                {"mod", Mod},
                {"jnz", Jump},
            };

        public Cpu(Program[] programs)
        {
            Programs = programs;
            ProgramIndex = 0;
        }

        private int ProgramIndex { get; set; }
        private Program[] Programs { get; }

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
            Actions[instr.Action](this, instr);
        }

        private static void Set(Cpu cpu, Instruction instr)
        {
            var y = instr.ValueY ?? cpu.CurrentProgram.GetRegisterValue(instr.RegisterY);
            cpu.CurrentProgram.SetRegisterValue(instr.RegisterX, y);
        }

        private static void Sub(Cpu cpu, Instruction instr)
        {
            var x = cpu.CurrentProgram.GetRegisterValue(instr.RegisterX);
            var y = instr.ValueY ?? cpu.CurrentProgram.GetRegisterValue(instr.RegisterY);
            cpu.CurrentProgram.SetRegisterValue(instr.RegisterX, x - y);
        }

        private static void Mul(Cpu cpu, Instruction instr)
        {
            var x = cpu.CurrentProgram.GetRegisterValue(instr.RegisterX);
            var y = instr.ValueY ?? cpu.CurrentProgram.GetRegisterValue(instr.RegisterY);
            cpu.CurrentProgram.SetRegisterValue(instr.RegisterX, x * y);

            cpu.CurrentProgram.Count++;
        }

        private static void Mod(Cpu cpu, Instruction instr)
        {
            var x = cpu.CurrentProgram.GetRegisterValue(instr.RegisterX);
            var y = instr.ValueY ?? cpu.CurrentProgram.GetRegisterValue(instr.RegisterY);
            cpu.CurrentProgram.SetRegisterValue(instr.RegisterX, x % y);
        }

        private static void Jump(Cpu cpu, Instruction instr)
        {
            var x = instr.ValueX ?? cpu.CurrentProgram.GetRegisterValue(instr.RegisterX);
            if (x != 0)
            {
                var y = instr.ValueY ?? cpu.CurrentProgram.GetRegisterValue(instr.RegisterY);
                cpu.CurrentProgram.InstructionIndex += (long) y - 1L;
            }
        }
    }
}