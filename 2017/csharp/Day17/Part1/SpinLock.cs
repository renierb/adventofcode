using System;
using System.Collections;
using System.Collections.Generic;

namespace Day17.Part1
{
    public class SpinLock : IEnumerable<int>, IEnumerator<int>
    {
        private readonly int _steps;
        private readonly List<int> _spinlock = new List<int>(2018) {0};
        private int _index;
        
        public SpinLock(int steps)
        {
            if (steps <= 0)
                throw new ArgumentOutOfRangeException(nameof(steps));
            _steps = steps;
        }

        public IEnumerator<int> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool MoveNext()
        {
            _index = (_index + _steps) % _spinlock.Count + 1;
            _spinlock.Insert(_index, _spinlock.Count);
            return true;
        }

        public void Reset()
        {
            _spinlock.Clear();
            _spinlock.Add(0);
            _index = 0;
        }

        public int Current => _spinlock[(_index + 1) % _spinlock.Count];

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}