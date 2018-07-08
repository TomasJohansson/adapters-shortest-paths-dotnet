using System;
using System.Collections.Generic;

namespace edu.ufl.cise.bsmock.graph.util
{
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

        public IDictionary<String, DijkstraNode> getNodes() {
            return nodes;
        }

        public void setNodes(IDictionary<String, DijkstraNode> nodes) {
            this.nodes = nodes;
        }

        public String getRoot() {
            return root;
        }

        public void add(DijkstraNode newNode) {
            nodes.Add(newNode.getLabel(),newNode);
        }

        public void setParentOf(String node, String parent) {
    //        if (parent != null && !nodes.containsKey(parent)) {
    //            System.out.println("Warning: parent node not present in tree.");
    //        }
            if (!nodes.ContainsKey(node))
                nodes.Add(node,new DijkstraNode(node));

            nodes[node].setParent(parent);

        }

        public String getParentOf(String node) {
            if (nodes.ContainsKey(node))
                return nodes[node].getParent();
            else
                return null;
        }
    }
}