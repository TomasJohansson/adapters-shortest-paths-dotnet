using System;
using System.Collections;
using System.Collections.Generic;

namespace java.util
{
    // https://docs.oracle.com/javase/7/docs/api/java/util/LinkedList.html
    public class LinkedList<T> : List<T>
    {
        System.Collections.Generic.LinkedList<T> _linkedList = new System.Collections.Generic.LinkedList<T>();

        public T Current => default(T);

        object IEnumerator.Current {
            get
            {
                return default(T);
            }
        }

        public T getLast()
        {
            return _linkedList.Last.Value;
        }

        public T getFirst()
        {
            // Returns the first element in this list.
            // exception if empty
            return _linkedList.First.Value;
        }

        public void addFirst(T t)
        {
            _linkedList.AddFirst(t);
        }

        public void addLast(T t)
        {
            _linkedList.AddLast(t);
        }

        public Iterator<T> iterator()
        {
            return new Iterator<T>(_linkedList);
        }

        // javadoc: 
        // "This method is equivalent to addLast(E)."
        // https://docs.oracle.com/javase/7/docs/api/java/util/LinkedList.html#add(E)
        public void add(T t)
        {
            addLast(t);
        }

        public void add(int index, T element)
        {
            throw new NotImplementedException();
        }

        public T remove(int i)
        {
            throw new NotImplementedException();
        }

        public T get(int i) {
            // java linked list has this method get(i)
            // but .NET linked list does not have _linkedList[i]
            // so therefore has to implement in some otehr way ...
            // TODO something better than slow iteration below ...
            var enumerator = this.GetEnumerator();
            int counter = -1;
            while(enumerator.MoveNext())
            {
                counter++;
                if(counter == i) break;
            }
            return enumerator.Current;
        }

        public void clear()
        {
            throw new NotImplementedException();
        }

        public void addAll(List<T> list)
        {
            IEnumerator<T> enumerator = list.GetEnumerator();
            while(enumerator.MoveNext())
            {
                this.add(enumerator.Current);
            }
        }

        public int indexOf(T t)
        {
            throw new NotImplementedException();
        }

        public List<T> subList(int fromIndex, int toIndex)
        {
            throw new NotImplementedException();
        }

        public int size()
        {
            return _linkedList.Count;
        }

        public bool isEmpty()
        {
            return size() == 0;
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _linkedList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _linkedList.GetEnumerator();
            //return new LinkedListEnumerator<T>(_linkedList);
        }

        public void __Reverse()
        {
            throw new NotImplementedException();
        }
    }
}