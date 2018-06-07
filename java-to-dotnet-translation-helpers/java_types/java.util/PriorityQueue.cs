using Priority_Queue;
using System;
using System.Collections.Generic;

namespace java.util
{
    // https://docs.oracle.com/javase/7/docs/api/java/util/PriorityQueue.html
    // Created by Tomas to simulate the following Java class:
    // import java.util.PriorityQueue;

    // Regarding .NET PriorityQueue:
    // https://github.com/dotnet/corefx/issues/574
    // Add PriorityQueue<T> to Collections
    // https://github.com/dotnet/corefxlab/pull/1850

    public class PriorityQueue<T>
        where T : IComparable<T>
    {
        SimplePriorityQueue<T,T> simplePriorityQueue = new SimplePriorityQueue<T,T>();

        public void clear()
        {
            simplePriorityQueue.Clear();
        }

        public void add(T t)
        {
            simplePriorityQueue.Enqueue(t, t);
        }

        public bool isEmpty()
        {
            return simplePriorityQueue.Count == 0;
        }

        // https://docs.oracle.com/javase/7/docs/api/java/util/PriorityQueue.html#poll()
        // Retrieves and removes the head of this queue, or returns null if this queue is empty.
        public T poll()
        {
            if (this.isEmpty())
            {
                return default(T);
            }
            return simplePriorityQueue.Dequeue();
        }

        // javadoc: 
        // "Returns true if and only if this queue contained the specified element"
        public bool remove(T t)
        {
            if(!simplePriorityQueue.Contains(t))
            {
                return false; 
            }
            simplePriorityQueue.Remove(t);   
            return true; 
        }

        public bool contains(T t)
        {
            return simplePriorityQueue.Contains(t);
        }
    }
}