/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.impl.generics;

namespace com.programmerare.shortestpaths.core.impl
{
    /**
     * @author Tomas Johansson
     */
    public sealed class EdgeImpl : EdgeGenericsImpl<Vertex, Weight> , Edge {

	    private EdgeImpl(
		    string edgeId, 
		    Vertex startVertex, 
		    Vertex endVertex, 
		    Weight weight
	    ) : base(edgeId, startVertex, endVertex, weight) {
	    }

	    public static Edge CreateEdge(
		    Vertex startVertex, 
		    Vertex endVertex, 
		    Weight weight
	    ) {
		    string edgeId = CreateEdgeIdValue(startVertex.VertexId, endVertex.VertexId);
		    return CreateEdge(edgeId, startVertex, endVertex, weight);
	    }

	    public static Edge CreateEdge(
		    string edgeId, 
		    Vertex startVertex, 
		    Vertex endVertex, 
		    Weight weight
	    ) {
		    return new EdgeImpl(edgeId, startVertex, endVertex, weight);
	    }	
    }
}