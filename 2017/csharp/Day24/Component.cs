namespace Day24
{
    public class Component
    {
        public Component(int id, int port1, int port2)
        {
            Id = id;
            Port1 = port1;
            Port2 = port2;
        }

        public int Id { get; }
        public int Port1 { get; }
        public int Port2 { get; }
        public int Strength => Port1 + Port2;

        public int OtherPort(int port)
        {
            return Port1 == port ? Port2 : Port1;
        }

        public override string ToString()
        {
            return $"{Port1}/{Port2}";
        }
    }
}