/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.core.impl.generics;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;
import com.programmerare.shortestpaths.core.api.generics.EdgeGenerics;

/**
 * Edge is an interface which the implementations will not know of.
 * Instances are passed as parameter into a construction of an implementation specific graph
 * which will return its own kind of edges.
 * Then those edges will be converted back to the common Edge interface, but in such case 
 * it will be a new instance which may not be desirable if the instances are not 
 * the default implementations provided but this project, but they may be classes with more data methods,
 * and therefore it is desirable to map them back to the original instances, which is the purpose of this class.
 * @author Tomas Johansson
 */
public final class EdgeMapper<E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> {

	private final Map<String, E> edgeMapWithVertexIdsAsKey = new HashMap<String, E>();

	
	/**
	 * Precondition: the edges must already be validated. Use GraphEdgesValidator before createEdgeMapper.
	 * It has package level access to reduce the risk of misusing it with precondition violation.   
	 * @param edges a list of edges to be used for constructing a graph. Note that they are assumed to be validated as a precondition.
	 * @return
	 */
	static <E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> EdgeMapper<E, V, W> createEdgeMapper(final List<E> edges) {
		return new EdgeMapper<E, V, W>(edges);
	}
	
	private EdgeMapper(final List<E> edges) {
		for (E edge : edges) {
			final String idForMapping = getIdForMapping(edge);
			edgeMapWithVertexIdsAsKey.put(idForMapping, edge);
		}
	}

	public List<E> getOriginalObjectInstancesOfTheEdges(final List<E> edges) {
		final List<E> originalObjectInstancesOfTheEdges = new ArrayList<E>();
		for (E edge : edges) {
			originalObjectInstancesOfTheEdges.add(edgeMapWithVertexIdsAsKey.get(getIdForMapping(edge)));
		}		
		return originalObjectInstancesOfTheEdges;
	}

	public E getOriginalEdgeInstance(final String startVertexId, final String endVertexId) {
		return edgeMapWithVertexIdsAsKey.get(getIdForMapping(startVertexId, endVertexId));
	}
	
	private String getIdForMapping(final E edge) {
		return getIdForMapping(edge.getStartVertex(), edge.getEndVertex());
	}
	
	private String getIdForMapping(final V startVertex, final V endVertex) {
		return getIdForMapping(startVertex.getVertexId(), endVertex.getVertexId());
	}
	
	private String getIdForMapping(final String startVertexId, final String endVertexId) {
		return EdgeGenericsImpl.createEdgeIdValue(startVertexId, endVertexId);
	}	
}