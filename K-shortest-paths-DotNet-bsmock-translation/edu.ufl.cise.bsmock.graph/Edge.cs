using java.lang;
using System;

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
            StringBuffer sb = new StringBuffer();
            sb.append("(");
            sb.append(fromNode);
            sb.append(",");
            sb.append(toNode);
            sb.append("){");
            sb.append(weight);
            sb.append("}");

            return sb.toString();
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