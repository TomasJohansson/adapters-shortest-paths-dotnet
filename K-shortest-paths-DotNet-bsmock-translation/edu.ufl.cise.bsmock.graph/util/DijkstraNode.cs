using java.lang;
using java.util;
using System;

namespace edu.ufl.cise.bsmock.graph.util
{
    /**
     * Created by brandonsmock on 6/6/15.
     */
    public class DijkstraNode : Node , Comparable<DijkstraNode> { // ,  {
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

        public int compareTo(DijkstraNode comparedNode) {
            double distance1 = this.dist;
            double distance2 = comparedNode.getDist();
            if (distance1 == distance2)
                return 0;
            if (distance1 > distance2)
                return 1;
            return -1;
        }
        public int CompareTo(DijkstraNode other)
        {
            return compareTo(other);
        }

        public bool equals(DijkstraNode comparedNode) {
            return this.getLabel().Equals(comparedNode.getLabel());
        }


    }
}