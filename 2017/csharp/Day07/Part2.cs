using System;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day07
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
            var parser = new InputParser(File.ReadLines("./input0.txt"));
            Assert.Equal(60, GetCorrectWeight(GetUnbalancedProgram(parser)));
        }

        private static int GetCorrectWeight(Program unbalanced)
        {
            var weightOffset = unbalanced.Parent.Children.Select(child => child.GetCumulativeWeight())
                .GroupBy(weight => weight, (weight, _) => weight - unbalanced.GetCumulativeWeight())
                .FirstOrDefault(weight => weight != 0);
            return unbalanced.Weight + weightOffset;
        }

        private static Program GetUnbalancedProgram(InputParser parser)
        {
            var programs = parser.GetPrograms().ToArray();
            foreach (var program in programs)
            {
                program.ExtractChildren(programs.ToDictionary(_ => _.Name));
            }
            return TraverseAndFindUnbalanced(FindBottomProgram(programs));
        }

        private static Program TraverseAndFindUnbalanced(Program program)
        {
            if (program == null)
                throw new ArgumentNullException(nameof(program));
            var children = program.Children;
            if (children.Length > 0)
            {
                var unbalanced = children.GroupBy(CumulativeWeight).FirstOrDefault(HasUnbalancedWeight)?.First();
                if (unbalanced != null)
                    return TraverseAndFindUnbalanced(unbalanced);
            }
            return program;
        }

        private static int CumulativeWeight(Program child)
        {
            return child.GetCumulativeWeight();
        }

        private static bool HasUnbalancedWeight(IGrouping<int, Program> weights)
        {
            return weights.Count() == 1;
        }

        private static Program FindBottomProgram(Program[] programs)
        {
            return programs.FirstOrDefault(NotAChild(programs));
        }

        private static Func<Program, bool> NotAChild(Program[] programs)
        {
            return program => { return !programs.Any(p => p.ChildrenNames.Contains(program.Name, StringComparer.OrdinalIgnoreCase)); };
        }

        [Fact]
        public void Answer()
        {
            var parser = new InputParser(File.ReadLines("./input1.txt"));
            _output.WriteLine($"Part2: {GetCorrectWeight(GetUnbalancedProgram(parser))}");
        }
    }
}