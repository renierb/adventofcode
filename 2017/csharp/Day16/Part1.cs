// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace Day16
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
            string[] moves = {"s1", "x3/4", "pe/b"};
            OrderAfterDanceOf("abcde", moves, "baedc");
        }

        private static void OrderAfterDanceOf(string starting, string[] moves, string expected)
        {
            Assert.Equal(expected, Compute(starting, moves));
        }

        private static string Compute(string starting, string[] moves)
        {
            var programs = starting.ToArray();
            moves.ForEach(ApplyMove(programs));
            return string.Concat(programs);
        }

        private static Action<string> ApplyMove(char[] programs)
        {
            var regex = new Regex(@"\d+", RegexOptions.Compiled | RegexOptions.ECMAScript);
            return move =>
            {
                switch (move[0])
                {
                    case 's':
                        Spin(programs, GetSpinCount(move));
                        break;
                    case 'x':
                        var x = GetPositions(regex, move);
                        ExchangePositions(programs, x[0], x[1]);
                        break;
                    case 'p':
                        ExchangePartners(programs, move[1], move[3]);
                        break;
                    default:
                        throw new Exception("uh oh!");
                }
            };
        }

        private static int GetSpinCount(string move)
        {
            return int.Parse(string.Concat(move.Skip(1)));
        }

        private static int[] GetPositions(Regex regex, string move)
        {
            return regex.Matches(move).Select(_ => int.Parse(_.Value)).ToArray();
        }

        private static void Spin(char[] programs, int spin)
        {
            var count = programs.Length - spin;
            programs.Skip(count).Concat(programs.Take(count)).ToArray().ForEach((c, i) => programs[i] = c);
        }

        private static void ExchangePositions(char[] programs, int posA, int posB)
        {
            char atPosA = programs[posA];
            programs[posA] = programs[posB];
            programs[posB] = atPosA;
        }

        private static void ExchangePartners(char[] programs, char a, char b)
        {
            var indexA = Array.IndexOf(programs, a);
            var indexB = Array.IndexOf(programs, b);
            programs[indexA] = b;
            programs[indexB] = a;
        }

        [Fact]
        public void Answer()
        {
            string[] input = File.ReadAllText("./input1.txt").Split(',');
            _output.WriteLine($"Part1: {Compute("abcdefghijklmnop", input)}");
        }
    }
}