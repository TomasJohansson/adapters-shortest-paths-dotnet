/*
* Regarding the license (Apache), please find more information 
* in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and also license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using System;
using System.Collections.Generic;

namespace edu.ufl.cise.bsmock.graph.util {
    /**
     * Created by brandonsmock on 6/8/15.
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
    //        if (parent != null && !nodes.containsKey(parent)) {
    //            System.out.println("Warning: parent node not present in tree.");
    //        }
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