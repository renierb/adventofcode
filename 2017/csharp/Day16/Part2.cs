// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace Day16
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
            string[] moves = {"s1", "x3/4", "pe/b"};
            OrderAfterDanceOf("abcde", moves, "abcde");
        }

        private static void OrderAfterDanceOf(string starting, string[] moves, string expected)
        {
            Assert.Equal(expected, Compute(starting, moves));
        }

        private static string Compute(string starting, string[] moves)
        {
            Dictionary<string, int> permutations = new Dictionary<string, int> {{starting, 0}};

            var programs = starting.ToArray();
            var iterations = 1000000000;

            for (int i = 1; i < iterations; i++)
            {
                moves.ForEach(ApplyMove(programs));
                if (!permutations.TryAdd(string.Concat(programs), i))
                    break;
            }

            int pos = iterations % permutations.Count;
            return permutations.First(kv => kv.Value == pos).Key;
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

            var start = Stopwatch.StartNew();
            _output.WriteLine($"Part2: {Compute("abcdefghijklmnop", input)} in {start.ElapsedMilliseconds}ms");
        }
    }
}