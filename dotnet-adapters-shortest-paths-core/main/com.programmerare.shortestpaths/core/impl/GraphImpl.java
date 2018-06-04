/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.core.impl;

import java.util.List;

import com.programmerare.shortestpaths.core.api.Edge;
import com.programmerare.shortestpaths.core.api.Graph;
import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;
import com.programmerare.shortestpaths.core.impl.generics.GraphGenericsImpl;
import com.programmerare.shortestpaths.core.validation.GraphEdgesValidationDesired;

public final class GraphImpl extends GraphGenericsImpl<Edge, Vertex , Weight> implements Graph {

	private GraphImpl(
		final List<Edge> edges,
		final GraphEdgesValidationDesired graphEdgesValidationDesired
	) {
		super(edges, graphEdgesValidationDesired);
	}

	/**
	 * Creates a graph instance, but will validate the edges and throw an exception if validation fails.
	 * If validation is not desired, then use the overloaded method. 
	 * @param edges list of all the edges for the graph
	 * @return an instance implementing the Graph interface
	 */	
	public static Graph createGraph(
		final List<Edge> edges
	) {
		return createGraph(edges, GraphEdgesValidationDesired.YES);
	}
	
	/**
	 * @param edges list of all the edges for the graph
	 * @param graphEdgesValidationDesired enum specifying whether or not validation is desired
	 * @return an instance implementing the Graph interface
	 */
	public static Graph createGraph(
		final List<Edge> edges,
		final GraphEdgesValidationDesired graphEdgesValidationDesired
	) {
		return new GraphImpl(edges, graphEdgesValidationDesired);
	}	
}