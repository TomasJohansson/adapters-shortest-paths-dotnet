// ----------------------
using Priority_Queue;
// https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp
// NuGet: <PackageReference Include="OptimizedPriorityQueue" Version="4.1.1" />
// ----------------------
using System;

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
        //where T : IComparable<T>
    // Reason for using Comparable<Vertex> :
    // Java code: Comparable<Vertex> is used because PriorityQueue is 
    // used with natural ordering in class class DijkstraShortestPathAlg:
    // private PriorityQueue<BaseVertex> vertexCandidateQueue = new PriorityQueue<BaseVertex>();
    // https://docs.oracle.com/javase/7/docs/api/java/util/PriorityQueue.html#PriorityQueue()
    // It is the method compareTo below which determines 
    // what should be returned from the priority queue.
    // HOWEVER, this is currently not needed in the C# translation 
    // of the project since a PriorityQueue is used which instead 
    // takes the weight parameter in the add method,
    // which was how the compareTo method was implemented before in this class 
    // i.e. before the method Comparable.compareTo was removed

    {
        SimplePriorityQueue<T,double> simplePriorityQueue = new SimplePriorityQueue<T,double>();

        public void clear()
        {
            simplePriorityQueue.Clear();
        }

        // add method in Java PriorityQueue:
        //public void add(T t)
        //{
        //    simplePriorityQueue.Enqueue(t, 0);
        //}
        // Note the below deviation from above Java method in this method.
        // In Java the add method only takes one parameter.
        // Here the second parameter below is used instead 
        // of using the Comparable interface as in Java.
        // The reason is I wanted to reuse a priority queue 
        // implementation for .NET but it (https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp) 
        // does not seem to provide a similar API with a comparable
        // interface comparing two items but since both
        // YanQi and Bsmock implementations (Java) implemented 
        // the Java compareTo method with the weights,
        // it could be provided as a parameter instead,
        // and now all the tests succeed for YanQi and Bsmock
        // which both uses this PriorityQueue
        public void add(T t, double weight)
        {
            simplePriorityQueue.Enqueue(t, weight);
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