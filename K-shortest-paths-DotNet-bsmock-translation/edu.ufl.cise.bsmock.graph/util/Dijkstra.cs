/**
 * Created by brandonsmock on 6/1/15.
 */
using System;
using System.Collections.Generic;

namespace edu.ufl.cise.bsmock.graph.util
{
    public sealed class Dijkstra {

        private Dijkstra() {}

        public static ShortestPathTree shortestPathTree(Graph graph, String sourceLabel) {
            IDictionary<String,Node> nodes = graph.getNodes();
            if (!nodes.ContainsKey(sourceLabel))
                throw new Exception("Source node not found in graph.");
            ShortestPathTree predecessorTree = new ShortestPathTree(sourceLabel);
            ISet<DijkstraNode> visited = new HashSet<DijkstraNode>();
            java.util.PriorityQueue<DijkstraNode> pq = new java.util.PriorityQueue<DijkstraNode>();
            foreach (String nodeLabel in nodes.Keys) {
                DijkstraNode newNode = new DijkstraNode(nodeLabel);
                newNode.setDist(double.MaxValue);
                newNode.setDepth(int.MaxValue);
                predecessorTree.add(newNode);
            }
            DijkstraNode sourceNode = predecessorTree.getNodes()[predecessorTree.getRoot()];
            sourceNode.setDist(0);
            sourceNode.setDepth(0);
            pq.add(sourceNode, sourceNode.getDist());

            int count = 0;
            while (!pq.isEmpty()) {
                DijkstraNode current = pq.poll();
                String currLabel = current.getLabel();
                visited.Add(current);
                count++;
                IDictionary<String, Double> neighbors = nodes[currLabel].getNeighbors();
                foreach (String currNeighborLabel in neighbors.Keys) {
                    DijkstraNode neighborNode = predecessorTree.getNodes()[currNeighborLabel];
                    Double currDistance = neighborNode.getDist();
                    Double newDistance = current.getDist() + nodes[currLabel].getNeighbors()[currNeighborLabel];
                    if (newDistance < currDistance) {
                        DijkstraNode neighbor = predecessorTree.getNodes()[currNeighborLabel];

                        pq.remove(neighbor);
                        neighbor.setDist(newDistance);
                        neighbor.setDepth(current.getDepth() + 1);
                        neighbor.setParent(currLabel);
                        pq.add(neighbor, neighbor.getDist());
                    }
                }
            }

            return predecessorTree;
        }

        public static Path shortestPath(Graph graph, String sourceLabel, String targetLabel) {
            //if (!nodes.containsKey(sourceLabel))
            //    throw new Exception("Source node not found in graph.");
            IDictionary<String,Node> nodes = graph.getNodes();
            ShortestPathTree predecessorTree = new ShortestPathTree(sourceLabel);
            java.util.PriorityQueue<DijkstraNode> pq = new java.util.PriorityQueue<DijkstraNode>();
            foreach (String nodeLabel in nodes.Keys) {
                DijkstraNode newNode = new DijkstraNode(nodeLabel);
                newNode.setDist(double.MaxValue);
                newNode.setDepth(int.MaxValue);
                predecessorTree.add(newNode);
            }
            DijkstraNode sourceNode = predecessorTree.getNodes()[predecessorTree.getRoot()];

            sourceNode.setDist(0);
            sourceNode.setDepth(0);
            pq.add(sourceNode, sourceNode.getDist());

            int count = 0;
            while (!pq.isEmpty()) {
                DijkstraNode current = pq.poll();
                String currLabel = current.getLabel();
                if (currLabel.Equals(targetLabel)) {
                    Path shortestPath = new Path();
                    String currentN = targetLabel;
                    String parentN = predecessorTree.getParentOf(currentN);
                    while (parentN != null) {
                        shortestPath.addFirst(new Edge(parentN,currentN,nodes[parentN].getNeighbors()[currentN]));
                        currentN = parentN;
                        parentN = predecessorTree.getParentOf(currentN);
                    }
                    return shortestPath;
                }
                count++;
                IDictionary<String, Double> neighbors = nodes[currLabel].getNeighbors();
                foreach (String currNeighborLabel in neighbors.Keys) {
                    DijkstraNode neighborNode = predecessorTree.getNodes()[currNeighborLabel];
                    Double currDistance = neighborNode.getDist();
                    Double newDistance = current.getDist() + nodes[currLabel].getNeighbors()[currNeighborLabel];
                    if (newDistance < currDistance) {
                        DijkstraNode neighbor = predecessorTree.getNodes()[currNeighborLabel];

                        pq.remove(neighbor);
                        neighbor.setDist(newDistance);
                        neighbor.setDepth(current.getDepth() + 1);
                        neighbor.setParent(currLabel);
                        pq.add(neighbor, neighbor.getDist());
                    }
                }
            }

            return null;
        }
    }
}