/*
* The code in this project is based on the following Java project created by Brandon Smock:
* https://github.com/bsmock/k-shortest-paths/
* Tomas Johansson later forked the above Java project into this location:
* https://github.com/TomasJohansson/k-shortest-paths/
* Tomas Johansson later translated the above Java code to C#.NET .
* That C# code is currently a part of the Visual Studio solution located here:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
* The current name of the subproject (within the VS solution) with the translated C# code:
* Programmerare.ShortestPaths.Adaptee.Bsmock
* 
* Regarding the latest license, Brandon Smock has released (13th of November 2017) the code with Apache License 2.0
* https://github.com/bsmock/k-shortest-paths/commit/b0af3f4a66ab5e4e741a5c9faffeb88def752882
* https://github.com/bsmock/k-shortest-paths/pull/4
* https://github.com/bsmock/k-shortest-paths/blob/master/LICENSE
* 
* You can also find license information in the files "License.txt" and "NOTICE.txt" in the project root directory.
*/

using System;
using System.Collections.Generic;

namespace edu.ufl.cise.bsmock.graph.util {
    /**
     * Created by brandonsmock on 6/6/15.
     * The above statement applies to the original Java code found here:
     * https://github.com/bsmock/k-shortest-paths
     * Regarding the translation of that Java code to this .NET code, see the top of this source file for more information.
     */
    public class DijkstraNode : Node 
        //, Comparable<DijkstraNode> 
    {
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

        private double dist = double.MaxValue;
        private int depth;

        public DijkstraNode(double dist): base() {
            this.dist = dist;
        }

        public DijkstraNode(String label): base(label) {
            this.dist = 0.0;
        }

        public DijkstraNode(String label, double dist): base(label) {
            this.dist = dist;
        }

        public DijkstraNode(String label, double dist, int depth, String parent): base(label) {
            this.dist = dist;
            this.depth = depth;
            base.AddEdge(parent,0.0);
        }

        public double GetDist() {
            return dist;
        }

        public void SetDist(double dist) {
            this.dist = dist;
        }

        public int GetDepth() {
            return depth;
        }

        public void SetDepth(int depth) {
            this.depth = depth;
        }

        public void SetParent(String parent) {
            base.neighbors = new Dictionary<String, Double>();
            base.neighbors.Add(parent,0.0);
        }

        public String GetParent() {
            var neighborLabels = base.neighbors.Keys;
            if (neighborLabels.Count > 1) {
                return null;
            }
            if (neighborLabels.Count < 1) {
                return null;
            }
            var enumerator = neighborLabels.GetEnumerator();
            enumerator.MoveNext();
            return enumerator.Current;
        }

        // see comment at the top of the file regarding why the below Comparable<Vertex> methods has been removed
        //public int compareTo(DijkstraNode comparedNode) {
        //    double distance1 = this.dist;
        //    double distance2 = comparedNode.getDist();
        //    if (distance1 == distance2)
        //        return 0;
        //    if (distance1 > distance2)
        //        return 1;
        //    return -1;
        //}
        //public int CompareTo(DijkstraNode other)
        //{
        //    return compareTo(other);
        //}

        public bool Equals(DijkstraNode comparedNode) {
            return this.GetLabel().Equals(comparedNode.GetLabel());
        }
    }
}