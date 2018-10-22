using System;
using System.Collections.Generic;
using System.Text;

namespace java.util
{
    // https://docs.oracle.com/javase/7/docs/api/java/util/List.html
    public interface  List<T> 
        // .NET interfaces below are implemented to support C# foreach statement
        : IEnumerator<T> , IEnumerable<T>
    {
        void add(T t);
        void add(int index, T element);
        T remove(int i);
        T get(int i);
        void clear();
        void addAll(java.util.List<T> list);
        int indexOf(T t);
        List<T> subList(int fromIndex, int toIndex);
        int size();
        bool isEmpty();
        
        void __Reverse(); // .NET

        //void Dispose();
        //void Reset();
        //IEnumerator<T> GetEnumerator()
        //bool MoveNext();

        // https://docs.oracle.com/javase/7/docs/api/java/util/List.html#equals(java.lang.Object)
        // Compares the specified object with this list for equality. Returns true if and only if the specified object is also a list, both lists have the same size, and all corresponding pairs of elements in the two lists are equal. (Two elements e1 and e2 are equal if (e1==null ? e2==null : e1.equals(e2)).) In other words, two lists are defined to be equal if they contain the same elements in the same order. This definition ensures that the equals method works properly across different implementations of the List interface.
        bool equals(Object o);
        bool Equals(object o); // .NET signature
        
        // https://docs.oracle.com/javase/7/docs/api/java/util/List.html#hashCode()
        // Returns the hash code value for this list. The hash code of a list is defined to be the result of the following calculation: 
        int hashCode();
        int GetHashCode(); // .NET signature
    }
}
