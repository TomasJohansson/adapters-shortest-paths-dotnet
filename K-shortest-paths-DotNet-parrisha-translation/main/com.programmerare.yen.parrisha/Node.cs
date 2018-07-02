/// MIT license. For more information see the file "LICENSE"

// A class that contains a list of Nodes representing a path
using System;
using System.Collections.Generic;

namespace com.programmerare.yen.parrisha {

    public class Node {
        private IList<Edge> Edges { get; }
        public int Index { get; }
    
        //  Default constructor with an argument of the Node's index
        public Node(int index_in) {
            Edges = new List<Edge>();
            Index = index_in;
        }
	    // Adds an edge to the internal list of edges emanating from this node
	    //  Note: Does not protect against duplicate edges at this point
	    //  Arguments:
	    //    toNode: Integer, index of the nodes this edge points to
	    public void AddEdge(int toNode, double weight_in) {
		    Edges.Add(new Edge(Index, toNode, weight_in));
        }
	    // Searches the edge list for a connection to "toNode"
	    //  Arguments:
	    //     toNode: Integer, index of the node to search for
	    public double HasEdgeTo(int toNode) {
            foreach(Edge edge in Edges) {
		        if(edge.ToNode == toNode) {
                    return edge.Weight;
                }
            }
		    return -1;
        }
	    public bool HasEdgeToNode(int toNode) {
            double d = HasEdgeTo(toNode);
            // return false if the value is -1 and otherwise true
            double diffAbsValue = Math.Abs(d + 1);
            return diffAbsValue > 0.0000001;
        }

	    // Returns a shallow copy list of the Edge objects that emanate from this node
	    public IList<Edge> GetEdgesFrom() {
		    var edgeList = new List<Edge>();
		    foreach(Edge edge in Edges) {
			    if(!edge.Broken) {
                    edgeList.Add(edge);
                }
            }
		    return edgeList;
        }
	
        public void BreakEdge(int toNode) {
		    foreach(Edge edge in Edges) {
                if(edge.ToNode == toNode) {
                    // #print("Breaking edge from " + str(i.fromNode+1) + " to " + str(i.toNode+1))
                    edge.Broken = true;
                }
            }
        }

	    public void FixEdges() {
		    foreach(Edge edge in Edges) {
                edge.Broken = false;
            }
        }

        public override bool Equals(object obj)
        {
            var node = obj as Node;
            return node != null && Index == node.Index;
        }
        public override int GetHashCode()
        {
            return Index;
        }
    }


}