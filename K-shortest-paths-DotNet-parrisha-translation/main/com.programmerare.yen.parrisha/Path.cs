/// MIT license. For more information see the file "LICENSE"

// A class that contains a list of Nodes representing a path
using System;
using System.Collections.Generic;

namespace com.programmerare.yen.parrisha {

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
                this.AddNode(node);
            }
        }

        public Path(Node node): this() {
            this.AddNode(node);
        }

        public Path(IList<Node> nodes): this() {
            foreach(Node node in nodes) {
                this.AddNode(node);
            }
        }

	    // Override the "+" operator to add paths together
        public static Path operator +(Path path, Path other) {
		    Path temp = new Path(path);
            foreach(Node other_node in other.Nodes) {
                temp.AddNode(other_node);
            }
		    return temp;
        }

	    // Adds a node to the end of the path if a valid connection exists
	    public void AddNode(Node nodeToAdd) {
		    if(Nodes.Count  == 0 || Nodes[Nodes.Count -1].HasEdgeToNode(nodeToAdd.Index)) {
		        Nodes.Add(nodeToAdd);
            }
        }

	    // Find the total cost of the path by iterating through each pair of nodes
	    public double GetPathCost() {
		    double sum = 0;
            for(int i=0; i<Nodes.Count-1; i++) {
                //#print("checking cost between " + str(self.nodes[i].index) + " and " + str(self.nodes[i+1].index))
                sum = sum + Nodes[i].HasEdgeTo(Nodes[i+1].Index);
            }
		    return sum;
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
        public static Path Pop(this IList<Path> paths, int indexForItemToRemove) {
            Path path = paths[indexForItemToRemove];
            paths.RemoveAt(indexForItemToRemove);
            return path;
        }

        public static void SortByPathCost(this List<Path> paths) {
            paths.Sort(new PathComparer());
        }

        public class PathComparer : IComparer<Path>
        {
            public int Compare(Path x, Path y)
            {
                return (int)(x.GetPathCost() - y.GetPathCost());
            }
        }
    }
}