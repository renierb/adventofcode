namespace Day18
{
    public struct Instruction
    {
        public Instruction(string register, string action, string value)
        {
            Register = register;
            Action = action;
            Value = value;
        }

        public string Register { get; }
        public string Action { get; }
        public string Value { get; }
    }
}