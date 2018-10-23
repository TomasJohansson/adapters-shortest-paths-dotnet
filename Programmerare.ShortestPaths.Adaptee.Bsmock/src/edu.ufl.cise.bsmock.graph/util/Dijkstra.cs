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

/**
 * Created by brandonsmock on 6/1/15.
 * The above statement applies to the original Java code found here:
 * https://github.com/bsmock/k-shortest-paths
 * Regarding the translation of that Java code to this .NET code, see the top of this source file for more information.
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