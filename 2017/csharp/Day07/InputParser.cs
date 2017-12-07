using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day07
{
    public class InputParser
    {
        private readonly IEnumerable<string> _lines;

        public InputParser(IEnumerable<string> lines)
        {
            _lines = lines;
        }

        public IEnumerable<Program> GetPrograms()
        {
            return _lines.Select(ParseLine);
        }

        private static Program ParseLine(string line)
        {
            var parts = Regex.Split(line, @"(\w+)\s\((\d+)\)(?:\s->\s(.+))?").Skip(1).ToArray();
            return new Program(parts[0], int.Parse(parts[1]), GetChildren(parts[2]));
        }

        private static string[] GetChildren(string children)
        {
            if (!string.IsNullOrEmpty(children))
                return children.Split(',').Select(c => c.Trim()).ToArray();
            return new string[0];
        }
    }
}