// A class that contains a list of Nodes representing a path
using System;
using System.Collections.Generic;

namespace YenKsp {

    public class Path {
        public IList<Node> Nodes { get; }

	    // Default constructors
	    // If passed a Path object, will copy that path's node list
	    // If passed a Node object, will create a new path starting at that node
	    // If passed a list of Nodes, will create a new path with those nodes
        private Path() {
            Nodes = new List<Node>();
        }

        public Path(Path path): this() {
            foreach(Node node in path.Nodes) {
                this.addNode(node);
            }
        }

        public Path(Node node): this() {
            this.addNode(node);
        }

        public Path(IList<Node> nodes): this() {
            foreach(Node node in nodes) {
                this.addNode(node);
            }
        }

	    // Override the "+" operator to add paths together
        public static Path operator +(Path path, Path other) {
		    Path temp = new Path(path);
            foreach(Node other_node in other.Nodes) {
                temp.addNode(other_node);
            }
		    return temp;
        }

	    // Adds a node to the end of the path if a valid connection exists
	    public void addNode(Node nodeToAdd) {
		    if(Nodes.Count  == 0 || Nodes[Nodes.Count -1].hasEdgeToNode(nodeToAdd.Index)) {
		        Nodes.Add(nodeToAdd);
            }
		    else  {
                print("Error adding node to path, no edge exists");
            }
        }

	    // Find the total cost of the path by iterating through each pair of nodes
	    public double getPathCost() {
		    double sum = 0;
            for(int i=0; i<Nodes.Count-1; i++) {
                //#print("checking cost between " + str(self.nodes[i].index) + " and " + str(self.nodes[i+1].index))
                sum = sum + Nodes[i].hasEdgeTo(Nodes[i+1].Index);
            }
		    return sum;
        }
	    // Prints a human readable version of the path
	    public void printPath() {
            //print(" -> ".join(str(i.index + 1) for i in self.nodes) + " (Cost: " + str(self.getPathCost()) + "): ")
            debugPrintPath();
        }

	    // Used for debug only
	    public void debugPrintPath() {
            foreach(Node node in this.Nodes) {
                print(node.Index + " ");
            }
        }
        private void print(string s) {
            Console.Write(s);
        }
    
        public IList<Node> GetSubsetOfNodes(int startIndexInclusive, int endIndexInclusive) {
            var nodes = new List<Node>();
            for(int i=startIndexInclusive; i <= endIndexInclusive; i++) {
                nodes.Add(this.Nodes[i]);
            }
            return nodes;
        }
    }

    public static class ExtensionMethods {
        public static Path pop(this IList<Path> paths, int indexForItemToRemove) {
            Path path = paths[indexForItemToRemove];
            paths.RemoveAt(indexForItemToRemove);
            return path;
        }

        public static void sortByPathCost(this List<Path> paths) {
            paths.Sort(new PathComparer());
        }

        public class PathComparer : IComparer<Path>
        {
            public int Compare(Path x, Path y)
            {
                return (int)(x.getPathCost() - y.getPathCost());
            }
        }
    }
}