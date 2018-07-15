/*
* Regarding the license (Apache), please find more information 
* in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and also license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
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