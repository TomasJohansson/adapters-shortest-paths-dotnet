using System;

namespace java.lang
{
    // https://docs.oracle.com/javase/7/docs/api/java/lang/Comparable.html
    public interface Comparable<T> : IComparable<T>
    {
        // int CompareTo(T other); // inherited by IComparable

        // TODO: use the Comparable ... (class Vertex)
        // so far it has just been added to be able to compile 
        // the source code as a C# library while being ported from Java

        // Java
        int compareTo(T o);
    }
}