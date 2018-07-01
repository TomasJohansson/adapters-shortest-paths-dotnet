/// MIT license. For more information see the file "LICENSE"

// A class that contains a list of Nodes representing a path

namespace YenKsp {

    // Simple class that represents an edge between two nodes
    //    Class Variables:
    //        weight: Cost of this edge
    //        fromNode: integer of the node index this edge originates from
    //        toNode: integer of the node index this edge goes to
    //        broken: boolean that can temporarily allow an edge to be ignored for path calculation
    public class Edge {
        int FromNode { get; }
        public int ToNode { get; }
        public double Weight { get; }
        public bool Broken { get; set; }

	    public Edge(int node1, int node2, double weight_in) {
            Weight = weight_in;
            FromNode = node1;
            ToNode = node2;
            Broken = false;
        }

    }
}