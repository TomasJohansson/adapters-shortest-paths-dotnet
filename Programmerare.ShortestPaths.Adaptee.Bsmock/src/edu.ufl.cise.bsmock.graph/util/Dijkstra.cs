/*
* Regarding the license (Apache), please find more information 
* in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and also license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

/**
 * Created by brandonsmock on 6/1/15.
 */
using System;
using System.Collections.Generic;

namespace edu.ufl.cise.bsmock.graph.util {
    public sealed class Dijkstra {

        private Dijkstra() {}

        public static ShortestPathTree ShortestPathTree(Graph graph, String sourceLabel) {
            IDictionary<String,Node> nodes = graph.GetNodes();
            if (!nodes.ContainsKey(sourceLabel))
                throw new Exception("Source node not found in graph.");
            ShortestPathTree predecessorTree = new ShortestPathTree(sourceLabel);
            ISet<DijkstraNode> visited = new HashSet<DijkstraNode>();
            java.util.PriorityQueue<DijkstraNode> pq = new java.util.PriorityQueue<DijkstraNode>();
            foreach (String nodeLabel in nodes.Keys) {
                DijkstraNode newNode = new DijkstraNode(nodeLabel);
                newNode.SetDist(double.MaxValue);
                newNode.SetDepth(int.MaxValue);
                predecessorTree.Add(newNode);
            }
            DijkstraNode sourceNode = predecessorTree.GetNodes()[predecessorTree.GetRoot()];
            sourceNode.SetDist(0);
            sourceNode.SetDepth(0);
            pq.add(sourceNode, sourceNode.GetDist());

            int count = 0;
            while (!pq.isEmpty()) {
                DijkstraNode current = pq.poll();
                String currLabel = current.GetLabel();
                visited.Add(current);
                count++;
                IDictionary<String, Double> neighbors = nodes[currLabel].GetNeighbors();
                foreach (String currNeighborLabel in neighbors.Keys) {
                    DijkstraNode neighborNode = predecessorTree.GetNodes()[currNeighborLabel];
                    Double currDistance = neighborNode.GetDist();
                    Double newDistance = current.GetDist() + nodes[currLabel].GetNeighbors()[currNeighborLabel];
                    if (newDistance < currDistance) {
                        DijkstraNode neighbor = predecessorTree.GetNodes()[currNeighborLabel];

                        pq.remove(neighbor);
                        neighbor.SetDist(newDistance);
                        neighbor.SetDepth(current.GetDepth() + 1);
                        neighbor.SetParent(currLabel);
                        pq.add(neighbor, neighbor.GetDist());
                    }
                }
            }

            return predecessorTree;
        }

        public static Path ShortestPath(Graph graph, String sourceLabel, String targetLabel) {
            //if (!nodes.containsKey(sourceLabel))
            //    throw new Exception("Source node not found in graph.");
            IDictionary<String,Node> nodes = graph.GetNodes();
            ShortestPathTree predecessorTree = new ShortestPathTree(sourceLabel);
            java.util.PriorityQueue<DijkstraNode> pq = new java.util.PriorityQueue<DijkstraNode>();
            foreach (String nodeLabel in nodes.Keys) {
                DijkstraNode newNode = new DijkstraNode(nodeLabel);
                newNode.SetDist(double.MaxValue);
                newNode.SetDepth(int.MaxValue);
                predecessorTree.Add(newNode);
            }
            DijkstraNode sourceNode = predecessorTree.GetNodes()[predecessorTree.GetRoot()];

            sourceNode.SetDist(0);
            sourceNode.SetDepth(0);
            pq.add(sourceNode, sourceNode.GetDist());

            int count = 0;
            while (!pq.isEmpty()) {
                DijkstraNode current = pq.poll();
                String currLabel = current.GetLabel();
                if (currLabel.Equals(targetLabel)) {
                    Path shortestPath = new Path();
                    String currentN = targetLabel;
                    String parentN = predecessorTree.GetParentOf(currentN);
                    while (parentN != null) {
                        shortestPath.AddFirst(new Edge(parentN,currentN,nodes[parentN].GetNeighbors()[currentN]));
                        currentN = parentN;
                        parentN = predecessorTree.GetParentOf(currentN);
                    }
                    return shortestPath;
                }
                count++;
                IDictionary<String, Double> neighbors = nodes[currLabel].GetNeighbors();
                foreach (String currNeighborLabel in neighbors.Keys) {
                    DijkstraNode neighborNode = predecessorTree.GetNodes()[currNeighborLabel];
                    Double currDistance = neighborNode.GetDist();
                    Double newDistance = current.GetDist() + nodes[currLabel].GetNeighbors()[currNeighborLabel];
                    if (newDistance < currDistance) {
                        DijkstraNode neighbor = predecessorTree.GetNodes()[currNeighborLabel];

                        pq.remove(neighbor);
                        neighbor.SetDist(newDistance);
                        neighbor.SetDepth(current.GetDepth() + 1);
                        neighbor.SetParent(currLabel);
                        pq.add(neighbor, neighbor.GetDist());
                    }
                }
            }

            return null;
        }
    }
}