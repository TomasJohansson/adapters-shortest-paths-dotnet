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
    {
        private Queue<T> queue = new Queue<T>();

        public void clear()
        {
            queue.Clear();
        }

        public void add(T t)
        {
            queue.Enqueue(t);
        }

        public bool isEmpty()
        {
            return queue.Count == 0;
        }

        // https://docs.oracle.com/javase/7/docs/api/java/util/PriorityQueue.html#poll()
        // Retrieves and removes the head of this queue, or returns null if this queue is empty.
        public T poll()
        {
            return queue.Dequeue();
        }
    }
}