namespace Day25
{
    public enum Move
    {
        Left = -1,
        Right = 1,
    }

    public class Action
    {
        public Action(int write, Move move, string state)
        {
            Write = write;
            Move = move;
            State = state;
        }

        public int Write { get; }
        public Move Move { get; }
        public string State { get; }
    }
}