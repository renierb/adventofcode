using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day08
{
    public static class InputParser
    {
        public static IEnumerable<Instruction> NextInstruction(IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                var parts = line.Split(" if ");
                yield return GetInstruction(parts[0], parts[1]);
            }
        }

        private static Instruction GetInstruction(string action, string condition)
        {
            var act = Regex.Split(action, @"(\w+)\s(inc|dec)\s(-?\d+)").Skip(1).ToArray();
            var cond = Regex.Split(condition, @"(\w+)\s([!|<|>|=]+)\s(-?\d+)").Skip(1).ToArray();
            return new Instruction(act[0], IsIncrease(act[1]), GetAmount(act[2]), GetCondition(cond));
        }

        private static bool IsIncrease(string value)
        {
            return value.Equals("inc", StringComparison.OrdinalIgnoreCase);
        }

        private static int GetAmount(string value)
        {
            return int.Parse(value);
        }

        private static Condition GetCondition(string[] condition)
        {
            return new Condition(condition[0], condition[1], GetAmount(condition[2]));
        }
    }
}