// ReSharper disable MemberCanBePrivate.Global

using System;
using System.Collections;
using System.Collections.Generic;

namespace Day17.Part2
{
    public sealed class CircularLinkedList<T> : ICollection<T>
    {
        public Node<T> Head { get; private set; }
        public Node<T> Tail { get; private set; }

        public int Count { get; private set; }

        public Node<T> this[int index]
        {
            get
            {
                if (index >= Count || index < 0)
                    throw new ArgumentOutOfRangeException(nameof(index));
                Node<T> node = Head;
                for (int i = 0; i < index; i++)
                    node = node.Next;
                return node;
            }
        }

        private void AddFirstItem(T item)
        {
            Head = new Node<T>(item);
            Tail = Head;
            Head.Next = Tail;
        }

        public void AddFirst(T item)
        {
            if (Head == null)
                AddFirstItem(item);
            else
            {
                var newNode = new Node<T>(item) {Next = Head};
                Tail.Next = newNode;
                Head = newNode;
            }

            ++Count;
        }

        public void AddLast(T item)
        {
            if (Head == null)
                AddFirstItem(item);
            else
            {
                var newNode = new Node<T>(item) {Next = Head};
                Tail.Next = newNode;
                Tail = newNode;
            }

            ++Count;
        }

        public void AddAfter(Node<T> node, T item)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            var newNode = new Node<T>(item) {Next = node.Next};
            node.Next = newNode;

            if (node == Tail)
                Tail = newNode;
            ++Count;
        }

        public bool Remove(T item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = Head;
            if (current != null)
            {
                do
                {
                    yield return current.Value;
                    current = current.Next;
                } while (current != Head);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Contains(T item)
        {
            throw new NotSupportedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0 || arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            Node<T> node = Head;
            do
            {
                array[arrayIndex++] = node.Value;
                node = node.Next;
            } while (node != Head);
        }

        bool ICollection<T>.IsReadOnly => false;

        void ICollection<T>.Add(T item)
        {
            AddLast(item);
        }
    }
}