/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.impl.generics;
using com.programmerare.shortestpaths.core.validation;
using System.Collections.Generic;

namespace com.programmerare.shortestpaths.core.impl
{
    public sealed class GraphImpl : GraphGenericsImpl<Edge, Vertex , Weight> , Graph {

	    private GraphImpl(
		    IList<Edge> edges,
		    GraphEdgesValidationDesired graphEdgesValidationDesired
	    ) : base(edges, graphEdgesValidationDesired) {
	    }

	    /**
	     * Creates a graph instance, but will validate the edges and throw an exception if validation fails.
	     * If validation is not desired, then use the overloaded method. 
	     * @param edges list of all the edges for the graph
	     * @return an instance implementing the Graph interface
	     */	
	    public static Graph createGraph(
		    List<Edge> edges
	    ) {
		    return createGraph(edges, GraphEdgesValidationDesired.YES);
	    }
	
	    /**
	     * @param edges list of all the edges for the graph
	     * @param graphEdgesValidationDesired enum specifying whether or not validation is desired
	     * @return an instance implementing the Graph interface
	     */
	    public static Graph createGraph(
		    List<Edge> edges,
		    GraphEdgesValidationDesired graphEdgesValidationDesired
	    ) {
		    return new GraphImpl(edges, graphEdgesValidationDesired);
	    }	
    }
}