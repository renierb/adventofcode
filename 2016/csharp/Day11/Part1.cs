using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day11
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
        }
        
        [Fact]
        public void Answer()
        {
            string[] input = File.ReadAllLines("./input1.txt");
            var floors = input.Select(InputParser.ParseFloor).ToArray();
            var elevator = new Elevator(floors);
            var count = EnumerableEx.Generate(
                new StateSearch(ImmutableHashSet.Create(elevator), ImmutableHashSet<Elevator>.Empty),
                state => !state.IsFound(),
                state => state.Next(),
                _ => _).Count();
            _output.WriteLine($"Part1: {count}");
        }
    }
}