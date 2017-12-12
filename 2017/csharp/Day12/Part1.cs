// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day12
{
    public class Part1
    {
        private readonly ITestOutputHelper _output;

        public Part1(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Tests()
        {
            var graph = new QuickUnionGraph(2);
            TotalInGroupAfter(graph, "0 <-> 2", 2);
            TotalInGroupAfter(graph, "1 <-> 1", 2);
            TotalInGroupAfter(graph, "2 <-> 0, 3, 4", 4);
            TotalInGroupAfter(graph, "3 <-> 2, 4", 4);
            TotalInGroupAfter(graph, "4 <-> 2, 3, 6", 5);
            TotalInGroupAfter(graph, "5 <-> 6", 6);
            TotalInGroupAfter(graph, "6 <-> 4, 5", 6);
        }

        private void TotalInGroupAfter(QuickUnionGraph graph, string line, int expected)
        {
            AddNodes(ParseLine(line), graph);
            Assert.Equal(expected, graph.TotalConnected(to: 0));
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

        private static void AddNodes(int[] line, QuickUnionGraph graph)
        {
            for (int i = 1; i < line.Length; i++)
            {
                graph.Union(line[0], line[i]);
            }
        }

        private static QuickUnionGraph BuildGraph(string[] lines)
        {
            var graph = new QuickUnionGraph(1000);
            lines.Select(ParseLine).ForEach(line => { AddNodes(line, graph); });
            return graph;
        }

        [Fact]
        public void Answer()
        {
            string[] input = File.ReadAllLines("./input1.txt");
            _output.WriteLine($"Part1: {BuildGraph(input).TotalConnected(to: 0)}");
        }
    }
}