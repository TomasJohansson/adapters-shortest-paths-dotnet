using System;
using System.Collections;
using System.Collections.Generic;

namespace java.util
{
    public abstract class ListImplBase<T> : AbstractList<T> , List<T> 
    {
        // IEnumerator/IEnumerable are needed to support "foreach" iteration

        private System.Collections.Generic.List<T> _list = new System.Collections.Generic.List<T>();

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        private int position = -1;

        public T Current
        {
            get
            {
                try
                {
                    return _list[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public void addAll(java.util.List<T> list)
        {
            foreach(var item in list)
            {
                _list.Add(item);
            }
        }

        public void clear()
        {
            _list.Clear();
        }

        public void add(T t)
        {
            _list.Add(t);
        }

        public bool isEmpty()
        {
            return size() == 0;
        }

        public override int size()
        {
            return _list.Count;
        }

        // https://docs.oracle.com/javase/7/docs/api/java/util/List.html#subList(int,%20int)
        public List<T> subList(int fromIndex, int toIndex)
        {
            var newList = CreateList();
            for (int i = fromIndex; i < toIndex; i++)
            {
                newList.add(this.get(i));
            }
            return newList;
        }

        protected abstract List<T> CreateList();

        public int indexOf(T t)
        {
            return _list.IndexOf(t);
        }

        public override T get(int i)
        {
            return _list[i];
        }

        public T remove(int i)
        {
            var element = get(i);
            _list.RemoveAt(i);
            return element;
        }

        // https://docs.oracle.com/javase/7/docs/api/java/util/List.html#add(int,%20E)
        // Inserts the specified element at the specified position in this list.
        // Shifts the element currently at that position (if any) and any subsequent 
        // elements to the right (adds one to their indices).
        public void add(int index, T element)
        {
            _list.Insert(index, element);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public bool MoveNext()
        {
            // https://msdn.microsoft.com/en-us/library/system.collections.ienumerator.current(v=vs.110).aspx
            position++;
            return (position < _list.Count);
        }

        public void Reset()
        {
            position = -1;
        }

        public void Dispose()
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        internal void __Reverse()
        {
            _list.Reverse();
        }

	    public override String ToString() {
		    return _list.ToString();
	    }

        void List<T>.__Reverse()
        {
            _list.Reverse();
        }

   }
}