using System;

namespace Day20
{
    public class Coordinate
    {
        public Coordinate(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; }
        public int Y { get; }
        public int Z { get; }
    }

    public class Particle : IEquatable<Particle>
    {
        public Particle(int id, Coordinate position, Coordinate velocity, Coordinate acceleration)
        {
            Id = id;
            Position = position;
            Velocity = velocity;
            Acceleration = acceleration;
        }

        public int Id { get; }
        public Coordinate Position { get; }
        public Coordinate Velocity { get; }
        public Coordinate Acceleration { get; }

        public long AbsAcceleration => Math.Abs(Acceleration.X) + Math.Abs(Acceleration.Y) + Math.Abs(Acceleration.Z);

        public double MinDistance { get; private set; } = double.MaxValue;

        private double GetDistanceTo(Particle other)
        {
            var dx = Position.X - other.Position.X;
            var dy = Position.Y - other.Position.Y;
            var dz = Position.Z - other.Position.Z;
            return Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2) + Math.Pow(dz, 2));
        }

        public Particle Move()
        {
            var vx = Velocity.X + Acceleration.X;
            var vy = Velocity.Y + Acceleration.Y;
            var vz = Velocity.Z + Acceleration.Z;
            var px = Position.X + vx;
            var py = Position.Y + vy;
            var pz = Position.Z + vz;
            return new Particle(Id, new Coordinate(px, py, pz), new Coordinate(vx, vy, vz), Acceleration);
        }

        public override int GetHashCode()
        {
            return -1;
        }

        // TODO: Fix Command-Query violation
        public bool Equals(Particle other)
        {
            var distance = GetDistanceTo(other);
            if (distance < MinDistance)
                MinDistance = distance;
            if (distance < other.MinDistance)
                other.MinDistance = distance;
            if (distance > 0.0)
                return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals(obj as Particle);
        }
    }
}