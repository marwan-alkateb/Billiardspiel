using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tracking.Core.Maths
{
    /// <summary>
    /// Implements a buffer of fixed size, where the write index
    /// loops around once the end is reached.
    /// </summary>
    /// <typeparam name="T">Buffer item type</typeparam>
    internal class RingBuffer<T> : IEnumerable<T>
    {
        private readonly T[] data;
        private int filled = 0;
        private int idx = 0;

        public RingBuffer(int length)
        {
            if (length <= 0)
                throw new ArgumentException("Length must be > 0");

            data = new T[length];
        }

        public void Push(T item)
        {
            if (filled < data.Length) filled++;
            data[idx] = item;
            idx++;
            if (idx >= data.Length)
                idx = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return data.Take(filled).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.Take(filled).GetEnumerator();
        }
    }
}
