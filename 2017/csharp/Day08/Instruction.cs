using System;
using System.Collections.Generic;

namespace Day08
{
    public struct Instruction
    {
        public Instruction(string register, bool increase, int amount, Condition condition)
        {
            Register = register;
            Increase = increase;
            Amount = amount;
            Condition = condition;
        }

        public string Register { get; }
        public bool Increase { get; }
        public int Amount { get; }
        public Condition Condition { get; }
    }

    [Flags]
    public enum Operator
    {
        NotEquals = 0,
        Equals = 1,
        LessThan = 2,
        GreaterThan = 4
    }

    public struct Condition
    {
        private static readonly Dictionary<string, Operator> Operators = new Dictionary<string, Operator>(6)
        {
            {"!=", Operator.NotEquals},
            {"==", Operator.Equals},
            {"<", Operator.LessThan},
            {">", Operator.GreaterThan},
            {"<=", Operator.LessThan | Operator.Equals},
            {">=", Operator.GreaterThan | Operator.Equals}
        };

        public Condition(string register, string comparison, int value)
        {
            Register = register;
            Value = value;
            Operator = Operators[comparison];
        }

        public string Register { get; }
        public Operator Operator { get; }
        public int Value { get; }
    }
}