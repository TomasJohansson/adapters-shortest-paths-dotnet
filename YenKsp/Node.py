#A class that contains a list of Nodes representing a path
class Path:
	#Default constructor
	# If passed a Path object, will copy that path's node list
	# If passed a Node object, will create a new path starting at that node
	# If passed a list of Nodes, will create a new path with those nodes
	def __init__(self, data):
		if isinstance(data, Node):
			self.nodes = [data]
		elif isinstance(data, Path):
			self.nodes = data[:]
		elif isinstance(data, list) and isinstance(data[0], Node):
			self.nodes = [data[0]]
			for newNode in data[1:]:
				self.addNode(newNode)
		else:
			raise ArgumentError('Tried to create a Path with invalid argument')

	#Override the "+" operator to add paths together
	def __add__(self, other):
		temp = Path(self)
		for other_node in other[:]:
			temp.addNode(other_node)
		return temp

	#Override the getItem() function which allows for Path[i] to be called
	# Note: does not return a Path object, but rather a list of nodes
	#       The list can be passed to the Path constructor if a new object is desired
	def __getitem__(self, key):
		#Since nodes is already a list, can just forward the key argument to it
		return self.nodes[key]

	#Adds a node to the end of the path if a valid connection exists
	def addNode(self, nodeToAdd):
		if (self.nodes[len(self.nodes)-1].hasEdgeTo(nodeToAdd.index) != -1):
			self.nodes.append(nodeToAdd)
		else:
			print("Error adding node to path, no edge exists")

	#Find the total cost of the path by iterating through each pair of nodes
	def getPathCost(self):
		sum = 0
		for i in range(len(self.nodes) - 1):
			#print("checking cost between " + str(self.nodes[i].index) + " and " + str(self.nodes[i+1].index))
			sum = sum + self.nodes[i].hasEdgeTo(self.nodes[i+1].index)
		return sum;

	#Prints a human readable version of the path
	def printPath(self):
		print(" -> ".join(str(i.index + 1) for i in self.nodes) + " (Cost: " + str(self.getPathCost()) + "): ")

	#Used for debug only
	def debugPrintPath(self):
		for i in self.nodes:
			print(id(i))
			print(i.edges)

class Node:
	#Default constructor with an argument of the Node's index
	def __init__(self, index_in):
		self.edges = []
		self.index = index_in;

	#Adds an edge to the internal list of edges emanating from this node
	# Note: Does not protect against duplicate edges at this point
	# Arguments:
	#   toNode: Integer, index of the nodes this edge points to
	def addEdge(self, toNode, weight_in):
		self.edges.append(Edge(self.index, toNode, weight_in))

	#Searches the edge list for a connection to "toNode"
	# Arguments:
	#    toNode: Integer, index of the node to search for
	def hasEdgeTo(self, toNode):
		for edge in self.edges:
			if edge.toNode == toNode:
				return edge.weight
		return -1;

	#Returns a shallow copy list of the Edge objects that emanate from this node
	def getEdgesFrom(self):
		edgeList = []
		for i in self.edges:
			if i.broken == False:
				edgeList.append(i)
		return edgeList

	def breakEdge(self, toNode):
		for i in self.edges:
			if i.toNode == toNode:
				#print("Breaking edge from " + str(i.fromNode+1) + " to " + str(i.toNode+1))
				i.broken = True

	def fixEdges(self):
		for i in self.edges:
			i.broken = False


#Simple class that represents an edge between two nodes
#   Class Variables:
#       weight: Cost of this edge
#       fromNode: integer of the node index this edge originates from
#       toNode: integer of the node index this edge goes to
#       broken: boolean that can temporarily allow an edge to be ignored for path calculation
class Edge:

	def __init__(self, node1, node2, weight_in):
		self.weight = weight_in
		self.fromNode = node1
		self.toNode = node2
		self.broken = False