// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Day25
{
    public class Part1
    {
        private readonly ITestOutputHelper _output;

        public Part1(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        private void Tests()
        {
            var states = new Dictionary<string, State>(2)
            {
                {
                    "A", new State(new[]
                    {
                        new Action(1, Move.Right, "B"),
                        new Action(0, Move.Left, "B"),
                    })
                },
                {
                    "B", new State(new[]
                    {
                        new Action(1, Move.Left, "A"),
                        new Action(1, Move.Right, "A"),
                    })
                }
            };
            CheckSumOf(states, "A", 6, 3);
        }

        private static void CheckSumOf(Dictionary<string, State> states, string startState, int steps, int expected)
        {
            Assert.Equal(expected, Compute(states, startState, steps));
        }

        private static long Compute(Dictionary<string, State> states, string startState, int steps)
        {
            var tape = new HashSet<int>();
            int cursor = 0;

            var state = states[startState];
            for (int i = 0; i < steps; i++)
            {
                var value = tape.Contains(cursor) ? 1 : 0;
                var action = state.Actions[value];
                if (action.Write == 0)
                    tape.Remove(cursor);
                else
                    tape.Add(cursor);
                cursor += (int) action.Move;
                state = states[action.State];
            }
            return tape.Count;
        }

        [Fact]
        public void Answer()
        {
            //string[] input = File.ReadAllLines("./input1.txt");
            var states = new Dictionary<string, State>(6)
            {
                {
                    "A", new State(new[]
                    {
                        new Action(1, Move.Right, "B"),
                        new Action(0, Move.Left, "C"),
                    })
                },
                {
                    "B", new State(new[]
                    {
                        new Action(1, Move.Left, "A"),
                        new Action(1, Move.Right, "D"),
                    })
                },
                {
                    "C", new State(new[]
                    {
                        new Action(0, Move.Left, "B"),
                        new Action(0, Move.Left, "E"),
                    })
                },
                {
                    "D", new State(new[]
                    {
                        new Action(1, Move.Right, "A"),
                        new Action(0, Move.Right, "B"),
                    })
                },
                {
                    "E", new State(new[]
                    {
                        new Action(1, Move.Left, "F"),
                        new Action(1, Move.Left, "C"),
                    })
                },
                {
                    "F", new State(new[]
                    {
                        new Action(1, Move.Right, "D"),
                        new Action(1, Move.Right, "A"),
                    })
                },
            };
            _output.WriteLine($"Part1: {Compute(states, "A", 12667664)}");
        }
    }
}