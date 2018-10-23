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

namespace edu.ufl.cise.bsmock.graph.util {
    /**
     * Created by brandonsmock on 6/8/15.
     * The above statement applies to the original Java code found here:
     * https://github.com/bsmock/k-shortest-paths
     * Regarding the translation of that Java code to this .NET code, see the top of this source file for more information.
     */
    public class ShortestPathTree {
        private IDictionary<String,DijkstraNode> nodes;
        private readonly String root;

        public ShortestPathTree() {
            this.nodes = new Dictionary<String, DijkstraNode>();
            this.root = "";
        }

        public ShortestPathTree(String root) {
            this.nodes = new Dictionary<String, DijkstraNode>();
            this.root = root;
        }

        public IDictionary<String, DijkstraNode> GetNodes() {
            return nodes;
        }

        public void SetNodes(IDictionary<String, DijkstraNode> nodes) {
            this.nodes = nodes;
        }

        public String GetRoot() {
            return root;
        }

        public void Add(DijkstraNode newNode) {
            nodes.Add(newNode.GetLabel(),newNode);
        }

        public void SetParentOf(String node, String parent) {
            // if (parent != null && !nodes.containsKey(parent)) {
            //      System.out.println("Warning: parent node not present in tree.");
            // }
            if (!nodes.ContainsKey(node))
                nodes.Add(node,new DijkstraNode(node));

            nodes[node].SetParent(parent);
        }

        public String GetParentOf(String node) {
            if (nodes.ContainsKey(node))
                return nodes[node].GetParent();
            else
                return null;
        }
    }
}