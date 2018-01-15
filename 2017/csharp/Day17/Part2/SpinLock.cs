using System;
using System.Collections;
using System.Collections.Generic;

namespace Day17.Part2
{
    public class SpinLock : IEnumerable<int>, IEnumerator<int>
    {
        private readonly int _steps;
        private readonly CircularLinkedList<int> _spinlock = new CircularLinkedList<int>();
        private Node<int> _current;
        
        public SpinLock(int steps)
        {
            if (steps <= 0)
                throw new ArgumentOutOfRangeException(nameof(steps));
            _steps = steps;
            _spinlock.AddFirst(0);
            _current = _spinlock.Head;
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
            for (int i = 0; i < _steps; i++)
                _current = _current.Next;
            _spinlock.AddAfter(_current, _spinlock.Count);
            _current = _current.Next;
            return true;
        }

        public void Reset()
        {
            _spinlock.Clear();
            _spinlock.AddFirst(0);
            _current = _spinlock.Head;
        }

        public int Current => _spinlock.Head.Next.Value;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}