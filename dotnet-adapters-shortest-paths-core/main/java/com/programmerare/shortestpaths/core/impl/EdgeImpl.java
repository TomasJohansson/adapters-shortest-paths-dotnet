/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.core.impl;

import com.programmerare.shortestpaths.core.api.Edge;
import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;
import com.programmerare.shortestpaths.core.impl.generics.EdgeGenericsImpl;

/**
 * @author Tomas Johansson
 */
public final class EdgeImpl extends EdgeGenericsImpl<Vertex, Weight> implements Edge {

	protected EdgeImpl(
		final String edgeId, 
		final Vertex startVertex, 
		final Vertex endVertex, 
		final Weight weight
	) {
		super(edgeId, startVertex, endVertex, weight);
	}

	public static Edge createEdge(
		final Vertex startVertex, 
		final Vertex endVertex, 
		final Weight weight
	) {
		final String edgeId = createEdgeIdValue(startVertex.getVertexId(), endVertex.getVertexId());
		return createEdge(edgeId, startVertex, endVertex, weight);
	}

	public static Edge createEdge(
		final String edgeId, 
		final Vertex startVertex, 
		final Vertex endVertex, 
		final Weight weight
	) {
		return new EdgeImpl(edgeId, startVertex, endVertex, weight);
	}	
}