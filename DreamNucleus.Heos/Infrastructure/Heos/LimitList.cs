using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamNucleus.Heos.Infrastructure.Heos
{
    public class LimitList<T> : IEnumerable<T>
    {
        private LinkedList<T> _list = new LinkedList<T>();

        public LimitList(int maximumCount)
        {
            if (maximumCount <= 0)
                throw new ArgumentException(null, "maximumCount");

            MaximumCount = maximumCount;
        }

        public int MaximumCount { get; private set; }

        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        public void Add(T value)
        {
            if (_list.Count == MaximumCount)
            {
                _list.RemoveFirst();
            }
            _list.AddLast(value);
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException();

                return _list.Skip(index).First();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
