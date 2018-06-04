/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.core.impl.generics;

import java.util.List;

import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;
import com.programmerare.shortestpaths.core.api.generics.EdgeGenerics;
import com.programmerare.shortestpaths.core.api.generics.GraphGenerics;
import com.programmerare.shortestpaths.core.api.generics.PathFinderGenerics;
import com.programmerare.shortestpaths.core.api.generics.PathGenerics;
import com.programmerare.shortestpaths.core.pathfactories.PathFactory;
import com.programmerare.shortestpaths.core.pathfactories.PathFactoryGenerics;
import com.programmerare.shortestpaths.core.validation.GraphValidationException;

public abstract class PathFinderBase
	<
		P extends PathGenerics<E, V, W> , 
		E extends EdgeGenerics<V, W> , 
		V extends Vertex , 
		W extends Weight
	> 
	implements PathFinderGenerics<P, E, V, W> 
{
	private PathFactory<P, E, V, W> pathFactory; // new PathFactoryGenerics<P, E, V, W> or new PathFactoryDefault()
	
	private final GraphGenerics<E, V, W> graph;
	private final EdgeMapper<E, V, W> edgeMapper;

	private W weightProtoypeFactory = null;

	protected PathFinderBase(
		final GraphGenerics<E, V, W> graph 
	) {
		this(
			graph, 
			null
		);
	}
	
	/**
	 * @param graph an implementation of the interface GraphGenerics
	 * @param pathFactory an implementation of the interface PathFactory, if null then a default instance will be created
	 */
	protected PathFinderBase(
		final GraphGenerics<E, V, W> graph, 
		final PathFactory<P, E, V, W> pathFactory
	) {
		this.graph = graph;		
		this.pathFactory = pathFactory != null ? pathFactory : createStandardInstanceOfPathFactory();
		// Prevondition to method below is that validation is performed i.e. 
		// the method below will NOT try to validate,
		edgeMapper = EdgeMapper.createEdgeMapper(graph.getEdges());
	}

	private PathFactory<P, E, V, W> createStandardInstanceOfPathFactory() {
		return new PathFactoryGenerics<P, E, V, W>();
	}
	
	/**
	 * final method to enforce the validation, and then forward to the hook method for the implementations
	 */
	public final List<P> findShortestPaths(
		final V startVertex, 
		final V endVertex, 
		final int maxNumberOfPaths
	) {
		validateThatBothVerticesArePartOfTheGraph(startVertex, endVertex);
		
		final List<P> shortestPaths = findShortestPathHook(
			startVertex, 
			endVertex, 
			maxNumberOfPaths				
		);

		// TODO: Maybe it should be optional to perform this test (in a parameter)
		validateThatAllEdgesInAllPathsArePartOfTheGraph(shortestPaths);
		
		return shortestPaths;
	}

	void validateThatAllEdgesInAllPathsArePartOfTheGraph(final List<P> paths) {
		for (P path : paths) {
			List<E> edgesForPath = path.getEdgesForPath();
			for (E e : edgesForPath) {
				if(!graph.containsEdge(e)) {
					// potential improvement: Use Notification pattern to collect all (if more than one) errors instead of throwing at the first error
					throw new GraphValidationException("Edge in path is not part of the graph: " + e);
				}
			}
		}
	}

	private void validateThatBothVerticesArePartOfTheGraph(final V startVertex, final V endVertex) {
		// potential improvement: Use Notification pattern to collect all (if more than one) errors instead of throwing at the first error
		if(!graph.containsVertex(startVertex)) {
			throwExceptionBecauseVertexNotIncludedInGraph("start", startVertex);
		}
		if(!graph.containsVertex(endVertex)) {
			throwExceptionBecauseVertexNotIncludedInGraph("end", endVertex);
		}		
	}

	/**
	 * @param startOrEndmessagePrefix intended to be one of the strings "start" or "end"
	 * @param startVertex
	 */
	private void throwExceptionBecauseVertexNotIncludedInGraph(final String startOrEndmessagePrefix, final V vertex) {
		throw new GraphValidationException(startOrEndmessagePrefix + " vertex is not part of the graph: " + vertex);
	}


	protected E getOriginalEdgeInstance(final String startVertexId, final String endVertexId) {
		return edgeMapper.getOriginalEdgeInstance(startVertexId, endVertexId);
	}

	protected GraphGenerics<E, V, W> getGraph() {
		return graph;
	}

	// "Hook" : see the Template Method Design Pattern
	protected abstract List<P> findShortestPathHook(V startVertex, V endVertex, int maxNumberOfPaths);
	
	protected W createInstanceWithTotalWeight(final double totalWeight, final List<E> edgesUsedForDeterminingWeightClass) {
		if(weightProtoypeFactory == null) {
			if(edgesUsedForDeterminingWeightClass.size() > 0) {
				E e = edgesUsedForDeterminingWeightClass.get(0);
				weightProtoypeFactory = e.getEdgeWeight();
			}
			// else throw exception may be a good idea since it does not seem to make any sense with a path witz zero edges 
		}
		return (W)weightProtoypeFactory.create(totalWeight);
	}
	
	protected P createPath(W totalWeight, List<E> edges) {
		return pathFactory.createPath(totalWeight, edges);
	}
	
}