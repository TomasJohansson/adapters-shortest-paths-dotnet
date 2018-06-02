using System;
using System.Collections;
using System.Collections.Generic;
using edu.asu.emit.algorithm.graph.abstraction;

namespace java.util
{
    // TODO: make this List into an interface like in Java
    // https://docs.oracle.com/javase/7/docs/api/java/util/List.html
    public class List<T> : IEnumerator<T> , IEnumerable<T>
    {
        // IEnumerator/IEnumerable are needed to support "foreach" iteration

        public T Current => throw new NotImplementedException();

        object IEnumerator.Current => throw new NotImplementedException();

        public void addAll(List<T> list)
        {
            throw new NotImplementedException();
        }

        public void clear()
        {
            throw new NotImplementedException();
        }

        public void add(T t)
        {
            throw new NotImplementedException();
        }

        public bool isEmpty()
        {
            throw new NotImplementedException();
        }

        public int size()
        {
            throw new NotImplementedException();
        }

        public List<T> subList(int v, int p)
        {
            throw new NotImplementedException();
        }

        public int indexOf(T t)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            // TODO maybe need to implement this ?
            // used like this:
            // int curPathHash = curPath.getVertexList().subList(0, curPath.getVertexList().indexOf(curDerivation)).GetHashCode();
            return base.GetHashCode();
        }

        public T get(int i)
        {
            throw new NotImplementedException();
        }

        internal BaseVertex remove(int v)
        {
            throw new NotImplementedException();
        }

        public void add<E>(int v, E element) where E : BaseElementWithWeight
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
