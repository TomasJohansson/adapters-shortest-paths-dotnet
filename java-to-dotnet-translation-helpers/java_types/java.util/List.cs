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
    }
}
