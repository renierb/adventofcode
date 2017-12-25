// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day24
{
    public class Part2
    {
        private readonly ITestOutputHelper _output;

        public Part2(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Tests()
        {
            StrongestBridgeOf(new[] {"0/2", "2/2", "2/3", "3/4", "3/5", "0/1", "10/1", "9/10"}, 19);
        }

        private void StrongestBridgeOf(string[] input, int expected)
        {
            Assert.Equal(expected, Compute(input.Select(ParseLine).ToArray()));
        }

        private static long Compute(Component[] components)
        {
            if (components.Length == 0)
                return 0;

            var lookup = GetLookupDictionary(components);

            lookup.TryGetValue(0, out var rootComponents);
            if (rootComponents != null)
            {
                var start = rootComponents.Select(c => new BridgeNode(0, c));
                var explorer = start.Expand(node =>
                {
                    var otherPort = node.Component.OtherPort(node.Port);
                    var nextComponents = GetNextComponents(node, lookup[otherPort]);
                    return nextComponents.Select(GetBridgeNode(node, otherPort)).Where(NotNull);
                });

                var longest = explorer.MaxBy(node => node.Length);
                return longest.MaxBy(node => node.Strength).FirstOrDefault()?.Strength ?? 0;
            }

            return 0L;
        }

        private static IEnumerable<Component> GetNextComponents(BridgeNode parent, IEnumerable<Component> components)
        {
            return components.Where(c => c.Id != parent.Component.Id);
        }

        private static bool NotNull(BridgeNode l)
        {
            return l != null;
        }

        private static Func<Component, BridgeNode> GetBridgeNode(BridgeNode parent, int port)
        {
            return component =>
            {
                var link = new BridgeNode(port, component) {Parent = parent};
                return !IsCircular(link, component.Id) ? link : null;
            };
        }

        private static bool IsCircular(BridgeNode link, int id)
        {
            var parent = link.Parent;
            while (parent != null)
            {
                if (parent.Component.Id == id)
                    return true;
                parent = parent.Parent;
            }
            return false;
        }

        private static Dictionary<int, List<Component>> GetLookupDictionary(Component[] components)
        {
            var lookup = new Dictionary<int, List<Component>>();
            foreach (var component in components)
            {
                if (component.Port1 != component.Port2)
                {
                    AddComponent(lookup, component.Port1, component);
                    AddComponent(lookup, component.Port2, component);
                }
                else
                {
                    AddComponent(lookup, component.Port1, component);
                }
            }
            return lookup;
        }

        private static void AddComponent(Dictionary<int, List<Component>> lookup, int port, Component component)
        {
            if (lookup.ContainsKey(port))
                lookup[port].Add(component);
            else
                lookup.Add(port, new List<Component>(new[] {component}));
        }

        private static Component ParseLine(string line, int index)
        {
            var ports = line.Split('/').Select(int.Parse).ToArray();
            return new Component(index, ports[0], ports[1]);
        }

        [Fact]
        public void Answer()
        {
            string[] input = File.ReadAllLines("./input1.txt");
            _output.WriteLine($"Part2: {Compute(input.Select(ParseLine).ToArray())}");
        }
    }
}