using System;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day07
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
            var parser = new InputParser(File.ReadLines("./input0.txt"));
            Assert.Equal("tknk", FindBottomProgram(parser));
        }

        private static string FindBottomProgram(InputParser input)
        {
            var programs = input.GetPrograms().ToArray();
            return programs.Where(NotChild(programs)).Select(p => p.Name).FirstOrDefault();
        }

        private static Func<Program, bool> NotChild(Program[] programs)
        {
            return program => { return !programs.Any(p => p.ChildrenNames.Contains(program.Name, StringComparer.OrdinalIgnoreCase)); };
        }

        [Fact]
        public void Answer()
        {
            var parser = new InputParser(File.ReadLines("./input1.txt"));
            _output.WriteLine($"Part1: {FindBottomProgram(parser)}");
        }
    }
}