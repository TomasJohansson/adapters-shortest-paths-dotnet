using System;
using System.Text;

namespace edu.ufl.cise.bsmock.graph {
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

        public String GetFromNode() {
            return fromNode;
        }

        public void SetFromNode(String fromNode) {
            this.fromNode = fromNode;
        }

        public String GetToNode() {
            return toNode;
        }

        public void SetToNode(String toNode) {
            this.toNode = toNode;
        }

        public double GetWeight() {
            return weight;
        }

        public void SetWeight(double weight) {
            this.weight = weight;
        }

        public Edge Clone() {
            return new Edge(fromNode, toNode, weight);
        }

        public override String ToString() {
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

        public bool Equals(Edge edge2) {
            if (HasSameEndpoints(edge2) && weight == edge2.GetWeight())
                return true;

            return false;
        }

        public bool HasSameEndpoints(Edge edge2) {
            if (fromNode.Equals(edge2.GetFromNode()) && toNode.Equals(edge2.GetToNode()))
                return true;

            return false;
        }
    }
}