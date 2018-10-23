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

namespace edu.ufl.cise.bsmock.graph {
    /**
     * The Edge class implements standard properties and methods for a weighted edge in a directed graph.
     *
     * Created by Brandon Smock on 6/19/15.
     * The above statement applies to the original Java code found here:
     * https://github.com/bsmock/k-shortest-paths
     * Regarding the translation of that Java code to this .NET code, see the top of this source file for more information.
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