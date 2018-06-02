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

        public T Current => throw new NotImplementedException();

        object IEnumerator.Current => throw new NotImplementedException();

        public void add(T t)
        {
            throw new NotImplementedException();
        }

        public void addAll(Collection<T> remVertexList)
        {
            throw new NotImplementedException();
        }

        public void clear()
        {
            throw new NotImplementedException();
        }

        public void remove(T t)
        {
            throw new NotImplementedException();
        }

        public bool contains(T t)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}