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
using System.Text;

namespace edu.ufl.cise.bsmock.graph.util {
    /**
     * The Path class implements a path in a weighted, directed graph as a sequence of Edges.
     *
     * Created by Brandon Smock on 6/18/15.
     * The above statement applies to the original Java code found here:
     * https://github.com/bsmock/k-shortest-paths
     * Regarding the translation of that Java code to this .NET code, see the top of this source file for more information.
     */
    public class Path 
      //  : Comparable<Path> 
    { // : Cloneable is used in the forked Java project but does not seem to use the method "Object.clone()"
        private java.util.LinkedList<Edge> edges;
        private double totalCost;

        public Path() {
            edges = new java.util.LinkedList<Edge>();
            totalCost = 0;
        }

        public Path(double totalCost) {
            edges = new java.util.LinkedList<Edge>();
            this.totalCost = totalCost;
        }

        public Path(java.util.LinkedList<Edge> edges) {
            this.edges = edges;
            totalCost = 0;
            foreach (Edge edge in edges) {
                totalCost += edge.GetWeight();
            }
        }

        public Path(java.util.LinkedList<Edge> edges, double totalCost) {
            this.edges = edges;
            this.totalCost = totalCost;
        }

        public java.util.LinkedList<Edge> GetEdges() {
            return edges;
        }

        public void SetEdges(java.util.LinkedList<Edge> edges) {
            this.edges = edges;
        }

        public java.util.LinkedList<String> GetNodes() {
            java.util.LinkedList<String> nodes = new java.util.LinkedList<String>();

            foreach (Edge edge in edges) {
                nodes.add(edge.GetFromNode());
            }

            Edge lastEdge = edges.getLast();
            if (lastEdge != null) {
                nodes.add(lastEdge.GetToNode());
            }

            return nodes;
        }

        public double GetTotalCost() {
            return totalCost;
        }

        public void SetTotalCost(double totalCost) {
            this.totalCost = totalCost;
        }

        public void AddFirstNode(String nodeLabel) {
            String firstNode = edges.getFirst().GetFromNode();
            edges.addFirst(new Edge(nodeLabel, firstNode,0));
        }

        public void AddFirst(Edge edge) {
            edges.addFirst(edge);
            totalCost += edge.GetWeight();
        }

        public void Add(Edge edge) {
            edges.add(edge);
            totalCost += edge.GetWeight();
        }

        // Disabled method because never used
        //public void addLastNode(String nodeLabel) {
        //    String lastNode = edges.getLast().getToNode();
        //    edges.addLast(new Edge(lastNode, nodeLabel,0));
        //}

        public int Size() {
            return edges.size();
        }

        public override String ToString() {
            StringBuilder sb = new StringBuilder();
            int numEdges = edges.size();
            sb.Append(totalCost);
            sb.Append(": [");
            if (numEdges > 0) {
                for (int i = 0; i < edges.size(); i++) {
                    sb.Append(edges.get(i).GetFromNode().ToString());
                    sb.Append("-");
                }

                sb.Append(edges.getLast().GetToNode().ToString());
            }
            sb.Append("]");
            return sb.ToString();
        }

    /*  
        @Override
        public boolean equals(Object o) {
            if (this == o) return true;
            if (o == null || getClass() != o.getClass()) return false;

            Path path = (Path) o;

            if (Double.compare(path.totalCost, totalCost) != 0) return false;
            if (!edges.equals(path.edges)) return false;

            return true;
        }

        @Override
        public int hashCode() {
            int result;
            long temp;
            result = edges.hashCode();
            temp = Double.doubleToLongBits(totalCost);
            result = 31 * result + (int) (temp ^ (temp >>> 32));
            return result;
        }
        */
        // The above methods were disabled in the Java code but the below .NET method ("Equals") is needed
        public override bool Equals(object path2)
        {
            var path = path2 as Path;
            return this.Equals(path);
        }
        public override int GetHashCode()
        {
            return (int)BitConverter.DoubleToInt64Bits(totalCost);
        }

        public bool Equals(Path path2) {
            if (path2 == null)
                return false;

            java.util.LinkedList<Edge> edges2 = path2.GetEdges();

            int numEdges1 = edges.size();
            int numEdges2 = edges2.size();

            if (numEdges1 != numEdges2) {
                return false;
            }

            for (int i = 0; i < numEdges1; i++) {
                Edge edge1 = edges.get(i);
                Edge edge2 = edges2.get(i);
                if (!edge1.GetFromNode().Equals(edge2.GetFromNode()))
                    return false;
                if (!edge1.GetToNode().Equals(edge2.GetToNode()))
                    return false;
            }

            return true;
        }

        // Java Comparable interface
        public int compareTo(Path path2) {
            double path2Cost = path2.GetTotalCost();
            if (totalCost == path2Cost)
                return 0;
            if (totalCost > path2Cost)
                return 1;
            return -1;
        }

        // .NET IComparable interface
        public int CompareTo(Path other) {
            return compareTo(other);
        }

        public Path Clone() {
            java.util.LinkedList<Edge> edges = new java.util.LinkedList<Edge>();

            foreach (Edge edge in this.edges) {
                edges.add(edge.Clone());
            }

            return new Path(edges);
        }

        public Path ShallowClone() {
            java.util.LinkedList<Edge> edges = new java.util.LinkedList<Edge>();

            foreach (Edge edge in this.edges) {
                edges.add(edge);
            }

            return new Path(edges,this.totalCost);
        }

        public Path CloneTo(int i) {
            java.util.LinkedList<Edge> edges = new java.util.LinkedList<Edge>();
            int l = this.edges.size();
            if (i > l)
                i = l;

            //for (Edge edge : this.edges.subList(0,i)) {
            for (int j = 0; j < i; j++) {
                edges.add(this.edges.get(j).Clone());
            }

            return new Path(edges);
        }

        public Path CloneFrom(int i) {
            java.util.LinkedList<Edge> edges = new java.util.LinkedList<Edge>();

            foreach (Edge edge in this.edges.subList(i,this.edges.size())) {
                edges.add(edge.Clone());
            }

            return new Path(edges);
        }

        public void AddPath(Path p2) {
            // ADD CHECK TO SEE THAT PATH P2'S FIRST NODE IS SAME AS THIS PATH'S LAST NODE

            this.edges.addAll(p2.GetEdges());
            this.totalCost += p2.GetTotalCost();
        }
    }
}