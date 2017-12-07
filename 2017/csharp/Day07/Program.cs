using System.Collections.Generic;
using System.Linq;

namespace Day07
{
    public class Program
    {
        public string Name { get; }
        public int Weight { get; }
        public string[] ChildrenNames { get; }
        public Program[] Children { get; private set; }
        public Program Parent { get; set; }

        public Program(string name, int weight, string[] childrenNames)
        {
            Name = name;
            Weight = weight;
            ChildrenNames = childrenNames;
        }

        public void ExtractChildren(Dictionary<string, Program> lookup)
        {
            Children = ChildrenNames.Select(name =>
            {
                var child = lookup[name];
                child.Parent = this;
                return child;
            }).ToArray();
        }

        public int GetCumulativeWeight()
        {
            return Children.Sum(c => c.GetCumulativeWeight()) + Weight;
        }
    }
}