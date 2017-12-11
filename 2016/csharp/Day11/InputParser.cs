using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day11
{
    public class InputParser
    {
        private static readonly Dictionary<string, int> FloorNumbers = new Dictionary<string, int>()
        {
            {"first", 1},
            {"second", 2},
            {"third", 3},
            {"fourth", 4},
        };

        // http://regexstorm.net/reference
        public static Floor ParseFloor(string floorArrangement)
        {
            var match = Regex.Match(floorArrangement,
                @"^The\s(?<floor>first|second|third|fourth){1}\sfloor\scontains(?:,?\s(?:and\s)?a\s(?<items>[\w-]+\s(?:generator|microchip))|.+)+\.$");
            var floorNumber = GetFloorNumber(match);
            return new Floor(floorNumber, GetItems(match));
        }

        private static int GetFloorNumber(Match match)
        {
            return FloorNumbers[match.Groups["floor"].Value];
        }

        private static string[] GetItems(Match match)
        {
            return match.Groups["items"].Captures.Select(_ => string.Concat(_.Value.Split(' ').Select(x => x[0]))).ToArray();
        }
    }
}