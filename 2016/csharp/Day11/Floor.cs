using System.Collections.Generic;
using System.Collections.Immutable;

namespace Day11
{
    public class Floor
    {
        public Floor(int number, string[] items)
        {
            Number = number;
            Items = ImmutableHashSet.Create(items);
        }

        public Floor(int number, ImmutableHashSet<string> items)
        {
            Number = number;
            Items = items;
        }
        
        public int Number { get; }
        public ImmutableHashSet<string> Items { get; }
    }
}