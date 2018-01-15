using System.Numerics;

namespace Day23
{
    public struct Instruction
    {
        public Instruction(string action, string valueX, string valueY)
        {
            Action = action;
            ValueY = null;
            if (BigInteger.TryParse(valueX, out var x))
            {
                ValueX = x;
                RegisterX = null;
            }
            else
            {
                ValueX = null;
                RegisterX = valueX;
            }
            if (BigInteger.TryParse(valueY, out var y))
            {
                ValueY = y;
                RegisterY = null;
            }
            else
            {
                ValueY = null;
                RegisterY = valueY;
            }
        }

        public string Action { get; }
        public string RegisterX { get; }
        public string RegisterY { get; }
        public BigInteger? ValueX { get; }
        public BigInteger? ValueY { get; }

        public override string ToString()
        {
            return $"{Action} {ValueX?.ToString() ?? RegisterX} {ValueY?.ToString() ?? RegisterY}";
        }
    }
}