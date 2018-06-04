using System;
using System.Collections;
using System.Collections.Generic;

namespace java.util
{
    // TODO: make this Set into an interface like in Java
    // https://docs.oracle.com/javase/7/docs/api/java/util/Set.html
    public class Set<T> : IEnumerator<T> , IEnumerable<T>
    {
        // IEnumerator/IEnumerable are needed to support "foreach" iteration
        
        System.Collections.Generic.HashSet<T> _set = new System.Collections.Generic.HashSet<T>();            

        private IEnumerator<T> _enumerator;

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public T Current
        {
            get
            {
                return _enumerator.Current;
            }
        }

        public void add(T t)
        {
            _set.Add(t);
        }

        public void addAll(Collection<T> remVertexList)
        {
            // TODO: find the methods invoking this method 
            // and see if they are ever used ...???
            throw new Exception("Not implemented because it did not seem to be used");
        }

        public void clear()
        {
            _set.Clear();
        }

        public void remove(T t)
        {
            _set.Remove(t);
        }

        public bool contains(T t)
        {
            return _set.Contains(t);
        }

        public bool MoveNext()
        {
            return _enumerator.MoveNext();
        }

        public void Reset()
        {
            _enumerator = GetEnumerator();
        }

        public void Dispose()
        {
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _set.GetEnumerator();
        }
    }
}