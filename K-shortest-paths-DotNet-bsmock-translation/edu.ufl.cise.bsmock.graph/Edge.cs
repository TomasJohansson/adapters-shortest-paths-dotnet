using System;
using System.Text;

namespace edu.ufl.cise.bsmock.graph
{
    /**
     * The Edge class implements standard properties and methods for a weighted edge in a directed graph.
     *
     * Created by Brandon Smock on 6/19/15.
     */
    public class Edge { // : Cloneable is used in the forked Java project but does not seem to use the method "Object.clone()"
        private String fromNode;
        private String toNode;
        private double weight;

        public Edge() {
            this.fromNode = null;
            this.toNode = null;
            this.weight = double.MaxValue;
        }

        public Edge(String fromNode, String toNode, double weight) {
            this.fromNode = fromNode;
            this.toNode = toNode;
            this.weight = weight;
        }

        public String getFromNode() {
            return fromNode;
        }

        public void setFromNode(String fromNode) {
            this.fromNode = fromNode;
        }

        public String getToNode() {
            return toNode;
        }

        public void setToNode(String toNode) {
            this.toNode = toNode;
        }

        public double getWeight() {
            return weight;
        }

        public void setWeight(double weight) {
            this.weight = weight;
        }

        public Edge clone() {
            return new Edge(fromNode, toNode, weight);
        }

        public String toString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            sb.Append(fromNode);
            sb.Append(",");
            sb.Append(toNode);
            sb.Append("){");
            sb.Append(weight);
            sb.Append("}");

            return sb.ToString();
        }

        public bool equals(Edge edge2) {
            if (hasSameEndpoints(edge2) && weight == edge2.getWeight())
                return true;

            return false;
        }

        public bool hasSameEndpoints(Edge edge2) {
            if (fromNode.Equals(edge2.getFromNode()) && toNode.Equals(edge2.getToNode()))
                return true;

            return false;
        }
    }
}