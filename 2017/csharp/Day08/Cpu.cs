using System;
using System.Collections.Generic;
using System.Linq;

namespace Day08
{
    public class Cpu
    {
        private readonly Dictionary<string, int> _registers = new Dictionary<string, int>();
        private int _max;

        public bool CheckCondition(Condition cond)
        {
            _registers.TryGetValue(cond.Register, out var value);
            switch (cond.Operator)
            {
                case Operator.NotEquals:
                    return value != cond.Value;
                case Operator.Equals:
                    return value == cond.Value;
                case Operator.LessThan:
                    return value < cond.Value;
                case Operator.LessThan | Operator.Equals:
                    return value <= cond.Value;
                case Operator.GreaterThan:
                    return value > cond.Value;
                case Operator.GreaterThan | Operator.Equals:
                    return value >= cond.Value;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Execute(Instruction instr)
        {
            if (!_registers.TryGetValue(instr.Register, out var value))
                _registers.Add(instr.Register, 0);
            if (instr.Increase)
                SetRegisterValue(instr.Register, value + instr.Amount);
            else
                SetRegisterValue(instr.Register, value - instr.Amount);
        }

        private void SetRegisterValue(string name, int value)
        {
            if (value > _max)
                _max = value;
            _registers[name] = value;
        }

        public int GetMax()
        {
            if (_registers.Count == 0)
                return 0;
            return _registers.Values.Max();
        }

        public int GetMaxEver()
        {
            return _max;
        }
    }
}