namespace Day24
{
    public class BridgeNode
    {
        public BridgeNode(int port, Component component)
        {
            Port = port;
            Component = component;
        }

        public int Port { get; }
        public Component Component { get; }
        public BridgeNode Parent { get; set; }

        public int Strength => Component.Strength + (Parent?.Strength ?? 0);
        public int Length => 1 + (Parent?.Length ?? 0);

        public override string ToString()
        {
            return Component.ToString();
        }
    }
}