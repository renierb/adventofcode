// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day12
{
    public class Part12
    {
        private readonly ITestOutputHelper _output;

        public Part12(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Tests()
        {
            var graph = new QuickUnionGraph(2);
            TotalGroupsAfter(graph, "0 <-> 2", 1);
            TotalGroupsAfter(graph, "1 <-> 1", 2);
            TotalGroupsAfter(graph, "2 <-> 0, 3, 4", 2);
            TotalGroupsAfter(graph, "3 <-> 2, 4", 2);
            TotalGroupsAfter(graph, "4 <-> 2, 3, 6", 2);
            TotalGroupsAfter(graph, "5 <-> 6", 2);
            TotalGroupsAfter(graph, "6 <-> 4, 5", 2);
        }

        private void TotalGroupsAfter(QuickUnionGraph graph, string line, int expected)
        {
            AddNodes(ParseLine(line), graph);
            Assert.Equal(expected, graph.TotalGroups());
        }

        private static int[] ParseLine(string line)
        {
            var parts = line.Split(" <-> ");
            return GetTail(parts).Prepend(GetHead(parts)).ToArray();
        }

        private static int GetHead(IReadOnlyList<string> parts)
        {
            return int.Parse(parts[0]);
        }

        private static IEnumerable<int> GetTail(IReadOnlyList<string> parts)
        {
            return parts[1].Split(',').Select(int.Parse);
        }

        private static void AddNodes(int[] nodes, QuickUnionGraph graph)
        {
            for (int i = 1; i < nodes.Length; i++)
            {
                graph.Union(nodes[0], nodes[i]);
            }
        }

        private QuickUnionGraph BuildGraph(string[] lines)
        {
            var graph = new QuickUnionGraph(1000);
            lines.Select(ParseLine).ForEach(nodes => { AddNodes(nodes, graph); });
            return graph;
        }

        [Fact]
        public void Answer()
        {
            string[] input = File.ReadAllLines("./input1.txt");
            _output.WriteLine($"Part2: {BuildGraph(input).TotalGroups()}");
        }
    }
}