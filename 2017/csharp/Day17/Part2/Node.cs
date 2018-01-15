using System.Diagnostics;

namespace Day17.Part2
{
    [DebuggerDisplay("Value = {" + nameof(Value) + "}")]
    public sealed class Node<T>
    {
        internal Node(T item)
        {
            Value = item;
        }
        
        public T Value { get; }

        public Node<T> Next { get; internal set; }
    }
}