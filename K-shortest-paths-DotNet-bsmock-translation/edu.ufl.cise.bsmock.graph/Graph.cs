using System.Text.RegularExpressions;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace edu.ufl.cise.bsmock.graph
{
    /**
     * The Graph class implements a weighted, directed graph using an adjacency list representation.
     *
     * Created by brandonsmock on 6/1/15.
     */
    public class Graph {
        private IDictionary<String,Node> nodes;

        public Graph() {
            nodes = new Dictionary<String,Node>();
        }

        public Graph(String filename): this() {
            readFromFile(filename);
        }

        public Graph(IDictionary<String,Node> nodes) {
            this.nodes = nodes;
        }

        public int numNodes() {
            return nodes.Count;
        }

        public int numEdges() {
            int edgeCount = 0;
            foreach (Node node in nodes.Values) {
                edgeCount += node.getEdges().size();
            }
            return edgeCount;
        }

        public void addNode(String label) {
            if (!nodes.ContainsKey(label))
                nodes.Add(label,new Node(label));
        }

        public void addNode(Node node) {
            String label = node.getLabel();
            if (!nodes.ContainsKey(label))
                nodes.Add(label,node);
        }

        public void addEdge(String label1, String label2, Double weight) {
            if (!nodes.ContainsKey(label1))
                addNode(label1);
            if (!nodes.ContainsKey(label2))
                addNode(label2);
            nodes[label1].addEdge(label2,weight);
        }

        public void addEdge(Edge edge) {
            addEdge(edge.getFromNode(),edge.getToNode(),edge.getWeight());
        }

        public void addEdges(java.util.LinkedList<Edge> edges) {
            foreach (Edge edge in edges) {
                addEdge(edge);
            }
        }

        public Edge removeEdge(String label1, String label2) {
            if (nodes.ContainsKey(label1)) {
                double weight = nodes[label1].removeEdge(label2);
                if (weight != double.MaxValue) {
                    return new Edge(label1, label2, weight);
                }
            }

            return null;
        }

        public double getEdgeWeight(String label1, String label2) {
            if (nodes.ContainsKey(label1)) {
                Node node1 = nodes[label1];
                if (node1.getNeighbors().ContainsKey(label2)) {
                    return node1.getNeighbors()[label2];
                }
            }

            return double.MaxValue;
        }

        public IDictionary<String,Node> getNodes() {
            return nodes;
        }

        public java.util.LinkedList<Edge> getEdgeList() {
            java.util.LinkedList<Edge> edgeList = new java.util.LinkedList<Edge>();

            foreach (Node node in nodes.Values) {
                edgeList.addAll(node.getEdges());
            }

            return edgeList;
        }

        public ICollection<String> getNodeLabels() {
            return nodes.Keys;
        }

        public Node getNode(String label) {
            return nodes[label];
        }

        public java.util.LinkedList<Edge> removeNode(String label) {
            java.util.LinkedList<Edge> edges = new java.util.LinkedList<Edge>();
            if (nodes.ContainsKey(label)) {
                Node node = nodes[label];
                nodes.Remove(label);
                edges.addAll(node.getEdges());
                edges.addAll(removeEdgesToNode(label));
            }
            return edges;
        }

        public java.util.LinkedList<Edge> removeEdgesToNode(String label) {
            java.util.LinkedList<Edge> edges = new java.util.LinkedList<Edge>();
            foreach (Node node in nodes.Values) {
                if (node.getAdjacencyList().Contains(label)) { // TODO: perfomance ... Contains in collection ...
                    double weight = node.removeEdge(label);
                    edges.add(new Edge(node.getLabel(),label,weight));
                }
            }
            return edges;
        }



        public Graph transpose() {
            IDictionary<String,Node> newNodes = new Dictionary<String, Node>();

            var it = nodes.Keys.GetEnumerator();
            while (it.MoveNext()) {
                String nodeLabel = it.Current;
                newNodes.Add(nodeLabel,new Node(nodeLabel));
            }

            it = nodes.Keys.GetEnumerator();
            while (it.MoveNext()) {
                String nodeLabel = it.Current;
                Node node = nodes[nodeLabel];
                ICollection<String> adjacencyList = node.getAdjacencyList();
                var alIt = adjacencyList.GetEnumerator();
                IDictionary<String, Double> neighbors = node.getNeighbors();
                while (alIt.MoveNext()) {
                    String neighborLabel = alIt.Current;
                    newNodes[neighborLabel].addEdge(nodeLabel,neighbors[neighborLabel]);
                }
            }

            return new Graph(newNodes);
        }

        public void clear() {
            nodes = new Dictionary<String,Node>();
        }

        public void readFromFile(String fileName) {
            //try {
                StreamReader br = new StreamReader(fileName);

                String line = br.ReadLine();

                while (line != null) {
                    String[] edgeDescription = Regex.Split(line, "\\s");
                    if (edgeDescription.Length == 3) {
                        addEdge(edgeDescription[0],edgeDescription[1], double.Parse(edgeDescription[2]));
                        //addEdge(edgeDescription[1],edgeDescription[0],Double.parseDouble(edgeDescription[2]));
                    }
                    line = br.ReadLine();
                }
            //} catch (Exception e) {
            //    e.printStackTrace();
            //}
        }

        public String toString() {
            StringBuilder graphStringB = new StringBuilder();
            var it = nodes.Keys.GetEnumerator();
            while (it.MoveNext()) {
                String nodeLabel = it.Current;
                graphStringB.Append(nodeLabel.ToString());
                graphStringB.Append(": {");
                Node node = nodes[nodeLabel];
                ICollection<String> adjacencyList = node.getAdjacencyList();
                var alIt = adjacencyList.GetEnumerator();
                IDictionary<String, Double> neighbors = node.getNeighbors();
                bool isFirst = true;
                while (alIt.MoveNext()) {
                    if(!isFirst) {
                        graphStringB.Append(", ");
                    }
                    String neighborLabel = alIt.Current;
                    graphStringB.Append(neighborLabel.ToString());
                    graphStringB.Append(": ");
                    graphStringB.Append(neighbors[neighborLabel]);
                    isFirst = false;
                }
                graphStringB.Append("}");
                graphStringB.Append("\n");
            }
            return graphStringB.ToString();
        }

        public void graphToFile(String filename) {
            throw new NotImplementedException();
            //BufferedWriter writer = null;
            ////try {
            //    File subgraphFile = new File(filename);

            //    // This will output the full path where the file will be written to...
            //    Console.WriteLine(subgraphFile.getCanonicalPath());

            //    writer = new BufferedWriter(new FileWriter(subgraphFile));
            //    writer.write(nodes.Count + "\n\n");

            //    var it = nodes.Values.GetEnumerator();
            //    while (it.MoveNext()) {
            //        Node node = it.Current;
            //        String nodeLabel = node.getLabel();
            //        if (nodes.ContainsKey(nodeLabel)) {
            //            IDictionary<String,Double> neighbors = node.getNeighbors();
            //            var it2 = neighbors.Keys.GetEnumerator();
            //            while (it2.MoveNext()) {
            //                String nodeLabel2 = it2.Current;
            //                if (nodes.ContainsKey(nodeLabel2)) {
            //                    writer.write(nodeLabel + " " + nodeLabel2 + " " + neighbors[nodeLabel2] + "\n");
            //                }
            //            }
            //        }
            //    }
            ////} catch (Exception e) {
            ////    e.printStackTrace();
            ////} finally {
            ////    try {
            ////        // Close the writer regardless of what happens...
            ////        writer.close();
            ////    } catch (Exception e) {
            ////    }
            ////}
        }
    }
}
