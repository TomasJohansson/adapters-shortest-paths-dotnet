/**
 * Created by brandonsmock on 6/1/15.
 */
 using System;
 using java.util;

namespace edu.ufl.cise.bsmock.graph.util
{
    public sealed class Dijkstra {

        private Dijkstra() {}

        public static ShortestPathTree shortestPathTree(Graph graph, String sourceLabel) {
            HashMap<String,Node> nodes = graph.getNodes();
            if (!nodes.containsKey(sourceLabel))
                throw new Exception("Source node not found in graph.");
            ShortestPathTree predecessorTree = new ShortestPathTree(sourceLabel);
            Set<DijkstraNode> visited = new HashSet<DijkstraNode>();
            PriorityQueue<DijkstraNode> pq = new PriorityQueue<DijkstraNode>();
            foreach (String nodeLabel in nodes.keySet()) {
                DijkstraNode newNode = new DijkstraNode(nodeLabel);
                newNode.setDist(double.MaxValue);
                newNode.setDepth(int.MaxValue);
                predecessorTree.add(newNode);
            }
            DijkstraNode sourceNode = predecessorTree.getNodes().get(predecessorTree.getRoot());
            sourceNode.setDist(0);
            sourceNode.setDepth(0);
            pq.add(sourceNode, sourceNode.getDist());

            int count = 0;
            while (!pq.isEmpty()) {
                DijkstraNode current = pq.poll();
                String currLabel = current.getLabel();
                visited.add(current);
                count++;
                HashMapN<String, Double> neighbors = nodes.get(currLabel).getNeighbors();
                foreach (String currNeighborLabel in neighbors.keySet()) {
                    DijkstraNode neighborNode = predecessorTree.getNodes().get(currNeighborLabel);
                    Double currDistance = neighborNode.getDist();
                    Double newDistance = current.getDist() + nodes.get(currLabel).getNeighbors().get(currNeighborLabel);
                    if (newDistance < currDistance) {
                        DijkstraNode neighbor = predecessorTree.getNodes().get(currNeighborLabel);

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
            HashMap<String,Node> nodes = graph.getNodes();
            ShortestPathTree predecessorTree = new ShortestPathTree(sourceLabel);
            PriorityQueue<DijkstraNode> pq = new PriorityQueue<DijkstraNode>();
            foreach (String nodeLabel in nodes.keySet()) {
                DijkstraNode newNode = new DijkstraNode(nodeLabel);
                newNode.setDist(double.MaxValue);
                newNode.setDepth(int.MaxValue);
                predecessorTree.add(newNode);
            }
            DijkstraNode sourceNode = predecessorTree.getNodes().get(predecessorTree.getRoot());

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
                        shortestPath.addFirst(new Edge(parentN,currentN,nodes.get(parentN).getNeighbors().get(currentN)));
                        currentN = parentN;
                        parentN = predecessorTree.getParentOf(currentN);
                    }
                    return shortestPath;
                }
                count++;
                HashMapN<String, Double> neighbors = nodes.get(currLabel).getNeighbors();
                foreach (String currNeighborLabel in neighbors.keySet()) {
                    DijkstraNode neighborNode = predecessorTree.getNodes().get(currNeighborLabel);
                    Double currDistance = neighborNode.getDist();
                    Double newDistance = current.getDist() + nodes.get(currLabel).getNeighbors().get(currNeighborLabel);
                    if (newDistance < currDistance) {
                        DijkstraNode neighbor = predecessorTree.getNodes().get(currNeighborLabel);

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