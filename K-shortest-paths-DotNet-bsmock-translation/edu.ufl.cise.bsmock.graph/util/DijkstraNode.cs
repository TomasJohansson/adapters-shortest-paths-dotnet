using java.util;
using System;

namespace edu.ufl.cise.bsmock.graph.util
{
    /**
     * Created by brandonsmock on 6/6/15.
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
            base.addEdge(parent,0.0);
        }

        public double getDist() {
            return dist;
        }

        public void setDist(double dist) {
            this.dist = dist;
        }

        public int getDepth() {
            return depth;
        }

        public void setDepth(int depth) {
            this.depth = depth;
        }

        public void setParent(String parent) {
            base.neighbors = new HashMapN<String, Double>();
            base.neighbors.put(parent,0.0);
        }

        public String getParent() {
            Set<String> neighborLabels = base.neighbors.keySet();
            if (neighborLabels.size() > 1) {
                return null;
            }
            if (neighborLabels.size() < 1) {
                return null;
            }
            return base.neighbors.keySet().iterator().next();
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

        public bool equals(DijkstraNode comparedNode) {
            return this.getLabel().Equals(comparedNode.getLabel());
        }


    }
}