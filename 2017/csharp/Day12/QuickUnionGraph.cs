using System.Collections.Generic;
using System.Linq;

namespace Day12
{
    public class QuickUnionGraph
    {
        private int[] _connections;
        private readonly Dictionary<int, int> _values = new Dictionary<int, int>();

        public QuickUnionGraph(int size)
        {
            _connections = Enumerable.Range(0, size).ToArray();
        }

        private void AddWithGrow(int i)
        {
            _values.TryAdd(i, 0);
            var count = i + 1;
            if (count > _connections.Length)
                _connections = _connections.Concat(Enumerable.Range(_connections.Length, count)).ToArray();
        }

        public void Union(int p, int q)
        {
            AddWithGrow(p);
            AddWithGrow(q);
            int i = GetRoot(p);
            int j = GetRoot(q);
            int iSize = _values[i];
            int jSize = _values[j];
            if (iSize < jSize)
            {
                _connections[i] = j;
                _values[j] += iSize;
            }
            else
            {
                _connections[j] = i;
                _values[i] += jSize;
            }
        }

        public int TotalGroups()
        {
            return _values.Keys.Select(GetRoot).Distinct().Count();
        }

        public int TotalConnected(int to)
        {
            return _connections.Where((_, i) => IsConnected(to, i)).Count();
        }

        public bool IsConnected(int p, int q)
        {
            return GetRoot(p) == GetRoot(q);
        }

        private int GetRoot(int i)
        {
            while (i != _connections[i])
            {
                _connections[i] = _connections[_connections[i]]; // flatten tree optimization
                i = _connections[i];
            }
            return i;
        }
    }
}