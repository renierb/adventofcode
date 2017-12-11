using System.Collections.Immutable;
using System.Linq;

namespace Day11
{
    public class StateSearch
    {
        private readonly ImmutableHashSet<Elevator> _active;
        private readonly ImmutableHashSet<Elevator> _explored;

        public StateSearch(ImmutableHashSet<Elevator> active, ImmutableHashSet<Elevator> explored)
        {
            _active = active;
            _explored = explored;
        }

        public StateSearch Next()
        {
            var active = ImmutableHashSet.Create(_active.SelectMany(e => e.Next()).Except(_explored).ToArray());
            return new StateSearch(active, _active);
        }

        public bool IsFound() => _active.Any(_ => _.IsGoal());
    }
}