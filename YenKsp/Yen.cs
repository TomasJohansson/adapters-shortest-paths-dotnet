/// MIT license. For more information see the file "LICENSE"

using System;
using System.Collections.Generic;

namespace YenKsp {

    public class Yen {

        //#Implementation of Dijkstra's algorithm
        //#Arguments:
        //#   nodes: A list of nodes created by the build_table function above
        //#   fromNode: integer of the starting Node
        //#   toNode: integer of the Node to find the shortest path to
        public Path DijkstraImpl(IList<Node> nodes, int fromNode, int toNode) {
	        // #Create a list of visited nodes initialized with the starting node
	        var visited = new List<Node> { nodes[fromNode] };
	        //#cost_list is a list potential nodes to visit and paths to them
	        var cost_list = new CostList();
	        //#Initialize some variables
	        //# current_node: The node which edges will be checked from and evaluated for lower costs
	        //# current_path: The Path (essentially a list of nodes) that was traversed to get to the current node
	        //# 				Keep track of and store the current_path in the cost_list structure because the lowest
	        //#				cost edge to traverse next may not originate from the current_node
	        var current_node = nodes[fromNode];
	        var current_path = new Path(nodes[fromNode]);

            Path candidatePath;
	        //#Continue to search nodes until we reach the destination
            bool whileLoopExitedWithBreak = false;
	        while(current_node.Index != toNode) {
		        //#Find connections from the current Node and assign costs
                var edgesToIterate = current_node.GetEdgesFrom();
		        foreach(Edge edge in edgesToIterate) {
			        //#print("Checking edge from " + str(edge.fromNode) + " to " + str(edge.toNode))
			        //#If the visited list does not have any instances of the toNode for this edge
                    if(!visited.Exists( n_visited => n_visited.Index == edge.ToNode)) {
				        //#Search for a cost entry for this node
                        IList<(Node n_cost, Path p_cost)> costList = cost_list.GetCostList();
                        bool forLoopExitedWithBreak = false;
				        for(int idx=0; idx < costList.Count; idx++) {
                            var (n_cost, p_cost) = costList[idx];
					        if(n_cost.Index == edge.ToNode) {
						        //#Compute total cost and update if less than
						        if(current_path.GetPathCost() + edge.Weight < p_cost.GetPathCost()) {
							        //#Create a potential path to this node to update the cost_list entry with
							        candidatePath = new Path(current_path);
							        candidatePath.AddNode(nodes[edge.ToNode]);
							        //#Use the idx variable from enumerate loop to update the correct cost_list entry
							        //cost_list[idx] = (n_cost, candidatePath)
                                    cost_list.SetCostListTuple(idx, (n_cost, candidatePath));
                                }
						        //#Having found a matching cost_list entry, exit the search
                                forLoopExitedWithBreak = true;
						        break;
                            }
                        } // for
                        if(!forLoopExitedWithBreak) {
    				        //else: // Python syntax belong to the for-loop
					        //#The else statement will not be executed if a break was triggered
					        //#If no cost entry was found, add to the cost table
					        //# Note: This is equivalent to the cost having been infinity
					        //#Create a potential path to this node to insert into the cost_list
					        candidatePath = new Path(current_path);
					        candidatePath.AddNode(nodes[edge.ToNode]);
					        cost_list.Append((nodes[edge.ToNode], candidatePath));
                        }
                    } // if-statement before the above for-loop
                    //	#End the if statement checking the visited list
		        } //#End the for statement iterating through edges from current node
		        //#If cost_list is empty at this point, no valid path exists to the destination
		        if (cost_list.GetCostList().Count == 0) {
                    whileLoopExitedWithBreak = true;
			        break;
                }
		        //#Pick a new node!
		        //#Sort cost_list by the path cost of the Path component
		        //cost_list.sort(key = lambda cost: cost[1].getPathCost())
                cost_list.SortByPathCost();
		        //#Pop the first entry in the now sorted list
		        //# Update the current_node and current_path variables from that entry
		        (current_node, current_path) = cost_list.Pop(0);
		        //#print("Selected a new node to visit: " + str(current_node.index) + ", it had a cost of " + str(current_path.getPathCost()))
		        //#current_path.printPath();
		        //#Add this node to the visited list
		        visited.Add(current_node);

	        }//#Repeat the while statement
	        //else: // Pyhon syntax , belongs to the while statement
	        //	//#When the while statement exits normally, we have found the final path
	        //	//#print("Final path picked: ")
	        //	return current_path

	        ////#If the while statement exited from a break, the else will not be executed
	        ////#Need to return a NULL here as no shortest path exists
            bool whileLoopExitedNormally = !whileLoopExitedWithBreak;
            if(whileLoopExitedNormally)
            {
		        //#When the while statement exits normally, we have found the final path
		        //#print("Final path picked: ")
		        return current_path;
            }
            else {
	            //#If the while statement exited from a break, the else will not be executed
	            //#Need to return a NULL here as no shortest path exists
                return null;
            }
        }

        // Implementation of Yen's algorithm for finding K shortest paths in a network
        //  Uses Dijkstra implemented above for shortest path calculation
        //  Returns a list of Path variables.
        //  Implemented based on the pseudo-code from "http://en.wikipedia.org/wiki/Yen%27s_algorithm"
        //  Arguments:
        //    nodes: A list of nodes created by the build_table function above
        //    fromNode: integer of the starting Node
        //    toNode: integer of the Node to find the shortest path to
        // 	numPaths: how many shortest paths to find
        public IList<Path> YensImpl(IList<Node> nodes, int fromNode, int toNode, int numPaths) {
	        // Create an empty list of paths that will be returned and a list of potential paths
	        var Apaths = new List<Path>();
	        var Bpaths = new List<Path>();
	        // First find the 1st shortest path using Dijkstra

            var firstPath = DijkstraImpl(nodes, fromNode, toNode);
	        Apaths.Add(firstPath);
            Path totalPath, rootPath, spurPath;
            Node spurNode;
            // Loop to find the remaining k shortest paths
            for (int k = 1; k < numPaths; k++){
                // Loop through all but the last node in the previous lowest-cost path
                Path previousLowestCostPath = Apaths[k - 1];
                IList<Node> allButTheLastNodeInPreviousLowestCostPath = previousLowestCostPath.GetSubsetOfNodes(0, previousLowestCostPath.Nodes.Count-2);
                for(int i=0; i < allButTheLastNodeInPreviousLowestCostPath.Count; i++) {
                    spurNode = allButTheLastNodeInPreviousLowestCostPath[i];
                    rootPath = new Path(previousLowestCostPath.GetSubsetOfNodes(0, i));
                    // Check the previous shortest paths and compare to rootPath
                    // Break any edges at the end of the rootPath if it coincides with a
                    //  previous shortest path
                    foreach (Path testPath in Apaths) {
                        if(AreNodesEqual(rootPath.Nodes, testPath.GetSubsetOfNodes(0, i))) {
                            spurNode.BreakEdge(testPath.Nodes[i + 1].Index);
                        }
                    }
                    // For each node rootPathNode in rootPath except spurNode:
                    //    remove rootPathNode from Graph

                    // Calculate the spur path from the spur node to the sink
                    spurPath = DijkstraImpl(nodes, spurNode.Index, toNode);
                    // Fix any edges that were broken
                    spurNode.FixEdges();

                    if (spurPath == null) {
                        //#No valid path exists, skip to next node
                        continue;
                    }
                    totalPath = rootPath + spurPath;
                    //#Need to check if spurPath already exists in B
                    if(!Bpaths.Exists(bpath => AreNodesEqual(totalPath.Nodes, bpath.Nodes))) {
                        //#print("Adding a path to Bpaths:")
                        //#totalPath.printPath()
                        Bpaths.Add(totalPath);
                    }
                } // "for i"-loop

                //#If Bpaths is empty, no more possible paths exist, so exit
                if (Bpaths.Count == 0) {
                    break;
                }
                //#Sort the list of candidate paths
                Bpaths.SortByPathCost(); // Bpaths.sort(key = lambda item: item.getPathCost())
                //#Move the lowest path cost from B to A
                Apaths.Add(Bpaths.Pop(0));
                //print("Found shortest path " + str(k+1) + ": ");
            } // "for k"-loop ends
            return Apaths;
        }

        private bool AreNodesEqual(
            IList<Node> rootPathNodes, 
            IList<Node> testPathNodes
        ) {
            //Python:  if (rootPath[:] == testPath[:i + 1]) {
            if(rootPathNodes.Count != testPathNodes.Count) {
                return false;
            }
            for(int i=0; i<rootPathNodes.Count; i++)
            {
                if(!rootPathNodes[i].Equals(testPathNodes[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}