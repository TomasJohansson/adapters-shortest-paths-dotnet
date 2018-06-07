using java.util;
using java.lang;
using System;

namespace edu.ufl.cise.bsmock.graph
{
    /**
     * The Node class implements a node in a directed graph keyed on a label of type String, with adjacency lists for
     * representing edges.
     *
     * Created by brandonsmock on 5/31/15.
     */
    public class Node {
        protected String label;
        protected HashMapN<String,Double> neighbors; // adjacency list, with HashMap for each edge weight

        public Node() {
            neighbors = new HashMapN<String,Double>();
        }

        public Node(String label) {
            this.label = label;
            neighbors = new HashMapN<String,Double>();
        }

        public String getLabel() {
            return label;
        }

        public void setLabel(String label) {
            this.label = label;
        }


        public new HashMapN<String,Double> getNeighbors() {
            return neighbors;
        }

        public void setNeighbors(HashMapN<String,Double> neighbors) {
            this.neighbors = neighbors;
        }

        public void addEdge(String toNodeLabel,Double weight) {
            neighbors.put(toNodeLabel, weight);
        }

        public double removeEdge(String toNodeLabel) {
            if (neighbors.containsKey(toNodeLabel)) {
                double weight = neighbors.get(toNodeLabel);
                neighbors.remove(toNodeLabel);
                return weight;
            }

            return double.MaxValue;
        }

        public Set<String> getAdjacencyList() {
            return neighbors.keySet();
        }

        public LinkedList<Edge> getEdges() {
            LinkedList<Edge> edges = new LinkedList<Edge>();
            foreach (String toNodeLabel in neighbors.keySet()) {
                edges.add(new Edge(label,toNodeLabel,neighbors.get(toNodeLabel)));
            }

            return edges;
        }
    
        public String toString() {
            StringBuilder nodeStringB = new StringBuilder();
            nodeStringB.append(label);
            nodeStringB.append(": {");
            Set<String> adjacencyList = this.getAdjacencyList();
            Iterator<String> alIt = adjacencyList.iterator();
            HashMapN<String, Double> neighbors = this.getNeighbors();
            while (alIt.hasNext()) {
                String neighborLabel = alIt.next();
                nodeStringB.append(neighborLabel.ToString());
                nodeStringB.append(": ");
                nodeStringB.append(neighbors.get(neighborLabel));
                if (alIt.hasNext())
                    nodeStringB.append(", ");
            }
            nodeStringB.append("}");
            nodeStringB.append("\n");

            return nodeStringB.toString();
        }
    }
}