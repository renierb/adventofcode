using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Day11
{
    public class Elevator
    {
        public ImmutableArray<Floor> Floors { get; }
        private int Floor { get; set; } = 0;

        public Elevator(Floor[] floors, int floor = 0)
        {
            Floors = ImmutableArray.Create(floors);
            Floor = floor;
        }

        public Elevator(ImmutableArray<Floor> floors, int floor = 0)
        {
            Floors = floors;
            Floor = floor;
        }

        public bool IsGoal()
        {
            var topFloor = Floors.Length - 1;
            if (topFloor == Floor)
            {
                return Floors.Take(topFloor).All(floor => floor.Items.Count == 0);
            }
            return false;
        }

        public bool IsLegal()
        {
            return !Floors[Floor].Items.IsEmpty &&
                   Floors.All(floor => floor.Items.GroupBy(item => item[0]).All(g => g.Count() == 2 || g.All(_ => _.EndsWith("g"))));
        }

        public IEnumerable<Elevator> Next()
        {
            foreach (var elevator in Next(Floor + 1))
                yield return elevator;
            foreach (var elevator in Next(Floor - 1))
                yield return elevator;
        }

        public IEnumerable<Elevator> Next(int toFloor)
        {
            if (toFloor < 0 || toFloor >= Floors.Length)
                yield break;

            ImmutableArray<Floor> MoveCargo(params string[] cargo)
            {
                return Floors
                    .SetItem(toFloor, new Floor(toFloor, Floors[toFloor].Items.Union(cargo)))
                    .SetItem(Floor, new Floor(Floor, Floors[toFloor].Items.Except(cargo)));
            }

            var items = Floors[Floor].Items;
            if (items.IsEmpty)
                yield break;

            foreach (var x in items)
            {
                foreach (var y in items.Remove(x))
                {
                    var next = new Elevator(MoveCargo(x, y), toFloor);
                    if (next.IsLegal())
                    {
                        yield return next;
                    }
                }
            }
        }
    }
}