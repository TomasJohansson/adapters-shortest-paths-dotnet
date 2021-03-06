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

using System.Text.RegularExpressions;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Programmerare.ShortestPaths.Adaptees.Common.DotNetTypes;

namespace edu.ufl.cise.bsmock.graph {
    /**
     * The Graph class implements a weighted, directed graph using an adjacency list representation.
     *
     * Created by brandonsmock on 6/1/15.
     * The above statement applies to the original Java code found here:
     * https://github.com/bsmock/k-shortest-paths
     * Regarding the translation of that Java code to this .NET code, see the top of this source file for more information.
     */
    public class Graph {
        private IDictionary<String,Node> nodes;

        public Graph() {
            nodes = new Dictionary<String,Node>();
        }

        public Graph(String filename): this() {
            ReadFromFile(filename);
        }

        public Graph(IDictionary<String,Node> nodes) {
            this.nodes = nodes;
        }

        public int NumNodes() {
            return nodes.Count;
        }

        public int NumEdges() {
            int edgeCount = 0;
            foreach (Node node in nodes.Values) {
                edgeCount += node.GetEdges().size();
            }
            return edgeCount;
        }

        public void AddNode(String label) {
            if (!nodes.ContainsKey(label))
                nodes.Add(label,new Node(label));
        }

        public void AddNode(Node node) {
            String label = node.GetLabel();
            if (!nodes.ContainsKey(label))
                nodes.Add(label,node);
        }

        public void AddEdge(String label1, String label2, Double weight) {
            if (!nodes.ContainsKey(label1))
                AddNode(label1);
            if (!nodes.ContainsKey(label2))
                AddNode(label2);
            nodes[label1].AddEdge(label2,weight);
        }

        public void AddEdge(Edge edge) {
            AddEdge(edge.GetFromNode(),edge.GetToNode(),edge.GetWeight());
        }

        public void AddEdges(java.util.LinkedList<Edge> edges) {
            foreach (Edge edge in edges) {
                AddEdge(edge);
            }
        }

        public Edge RemoveEdge(String label1, String label2) {
            if (nodes.ContainsKey(label1)) {
                double weight = nodes[label1].RemoveEdge(label2);
                if (weight != double.MaxValue) {
                    return new Edge(label1, label2, weight);
                }
            }

            return null;
        }

        public double GetEdgeWeight(String label1, String label2) {
            if (nodes.ContainsKey(label1)) {
                Node node1 = nodes[label1];
                if (node1.GetNeighbors().ContainsKey(label2)) {
                    return node1.GetNeighbors()[label2];
                }
            }

            return double.MaxValue;
        }

        public IDictionary<String,Node> GetNodes() {
            return nodes;
        }

        public java.util.LinkedList<Edge> GetEdgeList() {
            java.util.LinkedList<Edge> edgeList = new java.util.LinkedList<Edge>();

            foreach (Node node in nodes.Values) {
                edgeList.addAll(node.GetEdges());
            }

            return edgeList;
        }

        public ICollection<String> GetNodeLabels() {
            return nodes.Keys;
        }

        public Node GetNode(String label) {
            return nodes[label];
        }

        public java.util.LinkedList<Edge> RemoveNode(String label) {
            java.util.LinkedList<Edge> edges = new java.util.LinkedList<Edge>();
            if (nodes.ContainsKey(label)) {
                Node node = nodes[label];
                nodes.Remove(label);
                edges.addAll(node.GetEdges());
                edges.addAll(RemoveEdgesToNode(label));
            }
            return edges;
        }

        public java.util.LinkedList<Edge> RemoveEdgesToNode(String label) {
            java.util.LinkedList<Edge> edges = new java.util.LinkedList<Edge>();
            foreach (Node node in nodes.Values) {
                if (node.GetAdjacencyList().Contains(label)) { // TODO: perfomance ... Contains in collection ...
                    double weight = node.RemoveEdge(label);
                    edges.add(new Edge(node.GetLabel(),label,weight));
                }
            }
            return edges;
        }



        public Graph Transpose() {
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
                ICollection<String> adjacencyList = node.GetAdjacencyList();
                var alIt = adjacencyList.GetEnumerator();
                IDictionary<String, Double> neighbors = node.GetNeighbors();
                while (alIt.MoveNext()) {
                    String neighborLabel = alIt.Current;
                    newNodes[neighborLabel].AddEdge(nodeLabel,neighbors[neighborLabel]);
                }
            }

            return new Graph(newNodes);
        }

        public void Clear() {
            nodes = new Dictionary<String,Node>();
        }

#if (NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
    // StreamReader constructor with file path is missing for the above targets
    // but please also note that this method 
    // (and constructor "Graph(String filename)" using it, is not used
    //   except from test code which is not using .NET Standard 1.0-1.6 )
    // is not actually used within this project
    // which is a port of a Java project that provided this file reading method.

    /// <summary>
    /// The method is not supported for .NET Standard 1.0 - 1.6
    /// </summary>
    public void ReadFromFile(String fileName) {
        throw new NotImplementedException(TargetFrameworkNotSupportedMessage.NET_STANDARD_1_0_to_1_6_NOT_SUPPORTED);
    }
#else
        public void ReadFromFile(String fileName) {
            //try {
                StreamReader br = new StreamReader(fileName);

                String line = br.ReadLine();

                while (line != null) {
                    String[] edgeDescription = Regex.Split(line, "\\s");
                    if (edgeDescription.Length == 3) {
                        AddEdge(edgeDescription[0],edgeDescription[1], double.Parse(edgeDescription[2]));
                        //addEdge(edgeDescription[1],edgeDescription[0],Double.parseDouble(edgeDescription[2]));
                    }
                    line = br.ReadLine();
                }
            //} catch (Exception e) {
            //    e.printStackTrace();
            //}
        }
#endif
        public override String ToString() {
            StringBuilder graphStringB = new StringBuilder();
            var it = nodes.Keys.GetEnumerator();
            while (it.MoveNext()) {
                String nodeLabel = it.Current;
                graphStringB.Append(nodeLabel.ToString());
                graphStringB.Append(": {");
                Node node = nodes[nodeLabel];
                ICollection<String> adjacencyList = node.GetAdjacencyList();
                var alIt = adjacencyList.GetEnumerator();
                IDictionary<String, Double> neighbors = node.GetNeighbors();
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

        public void GraphToFile(String filename) {
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
