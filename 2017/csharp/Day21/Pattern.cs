using System;
using System.Collections.Generic;
using System.Linq;

namespace Day21
{
    public class Pattern : IEquatable<Pattern>
    {
        private static readonly (int r, int c)[][][] Patterns2D =
        {
            new[] // identity
            {
                new[] {(0, 0), (0, 1)},
                new[] {(1, 0), (1, 1)},
            },
            new[] // // flip left to right
            {
                new[] {(0, 1), (0, 0)},
                new[] {(1, 1), (1, 0)},
            },
            new[] // flip upside down
            {
                new[] {(1, 0), (1, 1)},
                new[] {(0, 0), (0, 1)},
            },
            new[] // flip upside down, left to right
            {
                new[] {(1, 1), (1, 0)},
                new[] {(0, 1), (0, 0)},
            },
            new[] // rotate clock-wise
            {
                new[] {(1, 0), (0, 0)},
                new[] {(1, 1), (0, 1)},
            },
            new[] // rotate anti clock-wise
            {
                new[] {(0, 1), (1, 1)},
                new[] {(0, 0), (1, 0)},
            },
        };

        private static readonly (int r, int c)[][][] Patterns3D =
        {
            new[] // identity
            {
                new[] {(0, 0), (0, 1), (0, 2)},
                new[] {(1, 0), (1, 1), (1, 2)},
                new[] {(2, 0), (2, 1), (2, 2)},
            },
            new[] // flip left and right
            {
                new[] {(0, 2), (0, 1), (0, 0)},
                new[] {(1, 2), (1, 1), (1, 0)},
                new[] {(2, 2), (2, 1), (2, 0)},
            },
            new[] // flip top and bottom
            {
                new[] {(2, 0), (2, 1), (2, 2)},
                new[] {(1, 0), (1, 1), (1, 2)},
                new[] {(0, 0), (0, 1), (0, 2)},
            },
            new[] // flip top and bottom + left to right
            {
                new[] {(2, 2), (2, 1), (2, 0)},
                new[] {(1, 2), (1, 1), (1, 0)},
                new[] {(0, 2), (0, 1), (0, 0)},
            },
            new[] // rotate clock-wise
            {
                new[] {(2, 0), (1, 0), (0, 0)},
                new[] {(2, 1), (1, 1), (0, 1)},
                new[] {(2, 2), (1, 2), (0, 2)},
            },
            new[] // rotate clock-wise + flip left and right
            {
                new[] {(0, 0), (1, 0), (2, 0)},
                new[] {(0, 1), (1, 1), (2, 1)},
                new[] {(0, 2), (1, 2), (2, 2)},
            },
            new[] // rotate anti clock-wise
            {
                new[] {(0, 2), (1, 2), (2, 2)},
                new[] {(0, 1), (1, 1), (2, 1)},
                new[] {(0, 0), (1, 0), (2, 0)},
            },
            new[] // rotate anti clock-wise + flip left and right
            {
                new[] {(2, 2), (1, 2), (0, 2)},
                new[] {(2, 1), (1, 1), (0, 1)},
                new[] {(2, 0), (1, 0), (0, 0)},
            },
        };

        public Pattern(string[] rows)
        {
            Rows = rows;
        }

        public int DimensionFactor => Rows.Length % 2 == 0 ? 2 : 3;
        public string[] Rows { get; }

        public override int GetHashCode()
        {
            return Rows.Select(r => r.Count(c => c == '#')).Sum();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((Pattern) obj);
        }

        public bool Equals(Pattern other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (DimensionFactor == 2)
                return Is2DMatch(other);
            return Is3DMatch(other);
        }

        private bool Is2DMatch(Pattern other)
        {
            if (other.DimensionFactor != 2)
                return false;
            if (Patterns2D.Any(IsMatch(other)))
                return true;
            return false;
        }

        private bool Is3DMatch(Pattern other)
        {
            if (other.DimensionFactor != 3)
                return false;
            if (Patterns3D.Any(IsMatch(other)))
                return true;
            return false;
        }

        private Func<(int r, int c)[][], bool> IsMatch(Pattern other)
        {
            return pattern =>
                pattern.Select(GetMatchCount(other)).Sum() == DimensionFactor * DimensionFactor;
        }

        private Func<(int r, int c)[], int, int> GetMatchCount(Pattern other)
        {
            return (positions, r) =>
                positions.Select((position, c) => IsMatch(other, position, r, c) ? 1 : 0).Sum();
        }

        private bool IsMatch(Pattern other, (int r, int c) position, int r, int c)
        {
            return Rows[r][c] == other.Rows[position.r][position.c];
        }

        public IEnumerable<Pattern> GetInnerPatterns()
        {
            int skip = 0;
            int take = DimensionFactor;
            foreach (var rows in Rows.Buffer(DimensionFactor))
            {
                for (int i = 0; i < rows[0].Length / DimensionFactor; i++)
                {
                    var patternRows = rows.Select(row => string.Concat(row.Skip(skip).Take(take)));
                    yield return new Pattern(patternRows.ToArray());
                    skip += take;
                }
                skip = 0;
            }
        }
    }
}