/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.impl.generics;
using com.programmerare.shortestpaths.core.impl;

namespace com.programmerare.shortestpaths.core.impl
{
    /**
     * @author Tomas Johansson
     */
    public sealed class EdgeImpl : EdgeGenericsImpl<Vertex, Weight> , Edge {

	    protected EdgeImpl(
		    string edgeId, 
		    Vertex startVertex, 
		    Vertex endVertex, 
		    Weight weight
	    ) : base(edgeId, startVertex, endVertex, weight) {
		
	    }

	    public static Edge createEdge(
		    Vertex startVertex, 
		    Vertex endVertex, 
		    Weight weight
	    ) {
		    string edgeId = createEdgeIdValue(startVertex.getVertexId(), endVertex.getVertexId());
		    return createEdge(edgeId, startVertex, endVertex, weight);
	    }

	    public static Edge createEdge(
		    string edgeId, 
		    Vertex startVertex, 
		    Vertex endVertex, 
		    Weight weight
	    ) {
		    return new EdgeImpl(edgeId, startVertex, endVertex, weight);
	    }	
    }
}