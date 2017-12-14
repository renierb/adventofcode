namespace Day13.Part2
{
    public class Scanner
    {
        public Scanner(int depth, int range)
        {
            Depth = depth;
            Range = range;
        }

        public int Depth { get; }
        public int Range { get; }
        public int Position { get; private set; }
        private int Direction { get; set; }

        public void Move()
        {
            if (Position == 0)
                Direction = 1;
            else if (Position == Range - 1)
                Direction = -1;
            Position += Direction;
        }

        public void Reset()
        {
            Position = 0;
            Direction = 1;
        }
    }
}