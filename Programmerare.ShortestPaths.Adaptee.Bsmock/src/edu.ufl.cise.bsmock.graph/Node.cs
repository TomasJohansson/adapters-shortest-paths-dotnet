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
using System.Text;

namespace edu.ufl.cise.bsmock.graph {
    /**
     * The Node class implements a node in a directed graph keyed on a label of type String, with adjacency lists for
     * representing edges.
     *
     * Created by brandonsmock on 5/31/15.
     * The above statement applies to the original Java code found here:
     * https://github.com/bsmock/k-shortest-paths
     * Regarding the translation of that Java code to this .NET code, see the top of this source file for more information.
     */
    public class Node {
        protected String label;
        protected IDictionary<String,Double> neighbors; // adjacency list, with HashMap for each edge weight

        public Node() {
            neighbors = new Dictionary<String,Double>();
        }

        public Node(String label) {
            this.label = label;
            neighbors = new Dictionary<String,Double>();
        }

        public String GetLabel() {
            return label;
        }

        public void SetLabel(String label) {
            this.label = label;
        }


        public new IDictionary<String,Double> GetNeighbors() {
            return neighbors;
        }

        public void SetNeighbors(IDictionary<String,Double> neighbors) {
            this.neighbors = neighbors;
        }

        public void AddEdge(String toNodeLabel,Double weight) {
            // todo: different java vs C# ?
            // java replaces existing while C# throws 
            // exceotion so therefore added code below for C# ...
            // but verify the difference ... i.e check with the API 
            // or do testings...
            if(neighbors.ContainsKey(toNodeLabel))
            {
                neighbors.Remove(toNodeLabel);
            }
            neighbors.Add(toNodeLabel, weight);
        }

        public double RemoveEdge(String toNodeLabel) {
            if (neighbors.ContainsKey(toNodeLabel)) {
                double weight = neighbors[toNodeLabel];
                neighbors.Remove(toNodeLabel);
                return weight;
            }

            return double.MaxValue;
        }

        public ICollection<String> GetAdjacencyList() {
            return neighbors.Keys;
        }

        public java.util.LinkedList<Edge> GetEdges() {
            java.util.LinkedList<Edge> edges = new java.util.LinkedList<Edge>();
            foreach (String toNodeLabel in neighbors.Keys) {
                edges.add(new Edge(label,toNodeLabel,neighbors[toNodeLabel]));
            }

            return edges;
        }
    
        public override String ToString() {
            StringBuilder nodeStringB = new StringBuilder();
            nodeStringB.Append(label);
            nodeStringB.Append(": {");
            ICollection<String> adjacencyList = this.GetAdjacencyList();
            var alIt = adjacencyList.GetEnumerator();
            IDictionary<String, Double> neighbors = this.GetNeighbors();
            bool isFirst = true;
            while (alIt.MoveNext()) {
                    if(!isFirst) {
                        nodeStringB.Append(", ");    
                    }
                String neighborLabel = alIt.Current;
                nodeStringB.Append(neighborLabel.ToString());
                nodeStringB.Append(": ");
                nodeStringB.Append(neighbors[neighborLabel]);
                isFirst = false;
            }
            nodeStringB.Append("}");
            nodeStringB.Append("\n");

            return nodeStringB.ToString();
        }
    }
}