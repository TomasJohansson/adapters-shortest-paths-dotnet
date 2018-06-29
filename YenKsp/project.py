import sys
import argparse

from Node import *

def main():

	#Parse the command line searching for a single string argument of the file to open
	parser = argparse.ArgumentParser(description='ECE 643 KSP Project')
	parser.add_argument('--infile', dest='file_in', default="input.txt", help='File to process network from and find KSP for')
	parser.add_argument('--k', dest='k', type=int, default='3', help='Number of K shortest paths to find')
	parser.add_argument('--source', dest='source', type=int, default='1', help='Index of starting node')
	parser.add_argument('--sink', dest='sink', type=int, default='-1', help='Index of sink node, defaults to last node')
	
	args = parser.parse_args()

	#Project Parts 1 and 2
	#Open the file and build a network of nodes with edges
	print("ECE 643 Project Part 1 and 2")
	nodes = build_table(args.file_in)

	#If sink was not explicitly specified, assign it to the last node
	if args.sink == -1:
		args.sink = len(nodes)
	
	#Project Part 3
	#Run djikstra from each node (except the sink node) to each other node
	print("\nECE 643 Project Part 3")
	for i in range(len(nodes)-1):
		for j in range(len(nodes)):
			if i != j:
				print("Searching for shortest path from " + str(i+1) + " to " + str(j+1))
				shortestPath = dijkstraImpl(nodes, i, j)
				if shortestPath != -1:
					shortestPath.printPath()
				else:
					print("No path found")


	#Project Part 4
	#Run Yens KSP algorithm to find the 3 shortest paths
	print("\nECE 643 Project Part 4")
	KSPs = yensImpl(nodes, args.source-1, args.sink-1, args.k)
	print("\n\nResults of searching for " + str(args.k) + " shortest paths from " + str(args.source) + " to " + str(args.sink))
	for i, ksp in enumerate(KSPs):
		print("Found KSP " + str(i+1))
		ksp.printPath()

	for i in range(len(KSPs), args.k):
		print("Not enough paths to find KSP " + str(i+1))

#Takes an input file as an argument and builds a Node/Edge structure
def build_table(file_in):

	#Open the file as readonly
	with open(file_in, 'r') as f:
		#The first line of the file is a single integer for number of Nodes
		for i in f.readline().split():
			numNodes = int(i)
		#print(numNodes)

		#Create a list of Nodes
		nodes = [Node(i) for i in range(numNodes)]

		#Read the rest of the file and create edges
		# Each line contain 4 integers, comma separated
		# ID,FromNode,ToNode,Weight'
		#But! Nodes are 1-indexed, so subtract 1 internally
		for line in f.readlines():
			ID, FromNode, ToNode, Weight = [int(i) for i in line.split(",")]
			FromNode = FromNode - 1;
			ToNode = ToNode - 1;
			#print (ID, FromNode, ToNode, Weight)
			#The data structure uses unidirectional links, so create an edge in each direction
			nodes[FromNode].addEdge(ToNode, Weight);
			#nodes[ToNode].addEdge(FromNode, Weight);

	#End the "with" statement, which closes the file

	#Print an adjacency/weight matrix
	print("   " + "".join(' {} '.format(i+1) for i in range(numNodes)))
	print("  " + "".join('___' for _ in range(numNodes)))
	for i in range(numNodes):
		adjacency = [];
		for j in range(numNodes):
			if i == j:
				adjacency.append('-')
				continue
			distance = nodes[i].hasEdgeTo(j)
			if distance == -1:
				adjacency.append('X')
			else:
				adjacency.append(distance)
		print(str(i+1) + " | " + "".join('{}  '.format(k) for k in adjacency))

	return nodes;


#Implementation of Dijkstra's algorithm
#Arguments:
#   nodes: A list of nodes created by the build_table function above
#   fromNode: integer of the starting Node
#   toNode: integer of the Node to find the shortest path to
def dijkstraImpl(nodes, fromNode, toNode):
	#Create a list of visited nodes initialized with the starting node
	visited = [nodes[fromNode]]
	#cost_list is a list potential nodes to visit and paths to them
	cost_list = []
	#Initialize some variables
	# current_node: The node which edges will be checked from and evaluated for lower costs
	# current_path: The Path (essentially a list of nodes) that was traversed to get to the current node
	# 				Keep track of and store the current_path in the cost_list structure because the lowest
	#				cost edge to traverse next may not originate from the current_node
	current_node = nodes[fromNode];
	current_path = Path(nodes[fromNode])

	#Continue to search nodes until we reach the destination
	while current_node.index != toNode:
		#Find connections from the current Node and assign costs
		for edge in current_node.getEdgesFrom():
			#print("Checking edge from " + str(edge.fromNode) + " to " + str(edge.toNode))
			#If the visited list does not have any instances of the toNode for this edge
			if not any(n_visited.index == edge.toNode for n_visited in visited):
				#Search for a cost entry for this node
				for idx, (n_cost, p_cost) in enumerate(cost_list):
					if n_cost.index == edge.toNode:
						#Compute total cost and update if less than
						if current_path.getPathCost() + edge.weight < p_cost.getPathCost():
							#Create a potential path to this node to update the cost_list entry with
							candidatePath = Path(current_path)
							candidatePath.addNode(nodes[edge.toNode])
							#Use the idx variable from enumerate loop to update the correct cost_list entry
							cost_list[idx] = (n_cost, candidatePath)
						#Having found a matching cost_list entry, exit the search
						break
				else:
					#The else statement will not be executed if a break was triggered
					#If no cost entry was found, add to the cost table
					# Note: This is equivalent to the cost having been infinity
					#Create a potential path to this node to insert into the cost_list
					candidatePath = Path(current_path)
					candidatePath.addNode(nodes[edge.toNode])
					cost_list.append((nodes[edge.toNode], candidatePath))

			#End the if statement checking the visited list
		#End the for statement iterating through edges from current node

		#If cost_list is empty at this point, no valid path exists to the destination
		if (len(cost_list) == 0):
			print("All path options exhausted, no valid path exists")
			break
		#Pick a new node!
		#Sort cost_list by the path cost of the Path component
		cost_list.sort(key = lambda cost: cost[1].getPathCost())
		#Pop the first entry in the now sorted list
		# Update the current_node and current_path variables from that entry
		(current_node, current_path) = cost_list.pop(0)
		#print("Selected a new node to visit: " + str(current_node.index) + ", it had a cost of " + str(current_path.getPathCost()))
		#current_path.printPath();

		#Add this node to the visited list
		visited.append(current_node)

	#Repeat the while statement
	else:
		#When the while statement exits normally, we have found the final path
		#print("Final path picked: ")
		return current_path

	#If the while statement exited from a break, the else will not be executed
	#Need to return a NULL here as no shortest path exists
	return -1


#Implementation of Yen's algorithm for finding K shortest paths in a network
# Uses Dijkstra implemented above for shortest path calculation
# Returns a list of Path variables.
# Implemented based on the pseudo-code from "http://en.wikipedia.org/wiki/Yen%27s_algorithm"
# Arguments:
#   nodes: A list of nodes created by the build_table function above
#   fromNode: integer of the starting Node
#   toNode: integer of the Node to find the shortest path to
#	numPaths: how many shortest paths to find
def yensImpl(nodes, fromNode, toNode, numPaths):
	#Create an empty list of paths that will be returned and a list of potential paths
	Apaths = []
	Bpaths = []
	#First find the 1st shortest path using Dijkstra
	Apaths.append(dijkstraImpl(nodes, fromNode, toNode))

	#Loop to find the remaining k shortest paths
	for k in range(1, numPaths):

		#Loop through all but the last node in the previous lowest-cost path
		for i, spurNode in enumerate(Apaths[k-1][:-1]):

			rootPath = Path(Apaths[k-1][:i+1])
			#rootPath.printPath()

			#Check the previous shortest paths and compare to rootPath
			#Break any edges at the end of the rootPath if it coincides with a
			# previous shortest path
			for testPath in Apaths:
				if rootPath[:] == testPath[:i+1]:
					spurNode.breakEdge(testPath[i+1].index)

			#For each node rootPathNode in rootPath except spurNode:
			#   remove rootPathNode from Graph

			#Calculate the spur path from the spur node to the sink
			spurPath = dijkstraImpl(nodes, spurNode.index, toNode)
			#Fix any edges that were broken
			spurNode.fixEdges()

			if spurPath == -1:
				#No valid path exists, skip to next node
				continue

			totalPath = rootPath + spurPath
            #Need to check if spurPath already exists in B
			if not any(totalPath[:] == bpath[:] for bpath in Bpaths):
				#print("Adding a path to Bpaths:")
				#totalPath.printPath()
				Bpaths.append(totalPath)
			else:
				print("Not adding a path to Bpaths because it already existed:")
				totalPath.printPath()

		#If Bpaths is empty, no more possible paths exist, so exit
		if len(Bpaths) == 0:
			break
		#Sort the list of candidate paths
		Bpaths.sort(key = lambda item: item.getPathCost())
		#Move the lowest path cost from B to A
		Apaths.append(Bpaths.pop(0))
		print("Found shortest path " + str(k+1) + ": ")
		Apaths[k].printPath()

	return Apaths


#Execute the main function when called from command line
# main();
# input("Done with program")

