/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/

using System.Collections.Generic;
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.api.generics;
using com.programmerare.shortestpaths.core.pathfactories;
using com.programmerare.shortestpaths.core.validation;

namespace com.programmerare.shortestpaths.core.impl.generics
{
    public abstract class PathFinderBase<P, E, V, W> 
	    : PathFinderGenerics<P, E, V, W> 
        where P : PathGenerics<E, V, W>
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {
	    private readonly PathFactory<P, E, V, W> pathFactory; // new PathFactoryGenerics<P, E, V, W> or new PathFactoryDefault()
	
	    private readonly GraphGenerics<E, V, W> graph;
	    private readonly EdgeMapper<E, V, W> edgeMapper;

	    private W weightProtoypeFactory = default(W); // null in Java version

	    protected PathFinderBase(
		    GraphGenerics<E, V, W> graph 
	    ) :
		    this(
			    graph, 
			    null
		    )
        {
	    }
	
	    /**
	     * @param graph an implementation of the interface GraphGenerics
	     * @param pathFactory an implementation of the interface PathFactory, if null then a default instance will be created
	     */
	    protected PathFinderBase(
		    GraphGenerics<E, V, W> graph, 
		    PathFactory<P, E, V, W> pathFactory
	    ) {
		    this.graph = graph;		
		    this.pathFactory = pathFactory != null ? pathFactory : createStandardInstanceOfPathFactory();
		    // Precondition to method below is that validation is performed i.e. 
		    // the method below will NOT try to validate,
		    edgeMapper = EdgeMapper<E, V, W>.createEdgeMapper<E, V, W>(graph.getEdges());
	    }

	    private PathFactory<P, E, V, W> createStandardInstanceOfPathFactory() {
		    return new PathFactoryGenerics<P, E, V, W>();
	    }
	
	    /**
	     * final method to enforce the validation, and then forward to the hook method for the implementations
	     */
	    public IList<P> findShortestPaths(
		    V startVertex, 
		    V endVertex, 
		    int maxNumberOfPaths
	    ) {
		    validateThatBothVerticesArePartOfTheGraph(startVertex, endVertex);
		
		    IList<P> shortestPaths = findShortestPathHook(
			    startVertex, 
			    endVertex, 
			    maxNumberOfPaths				
		    );

		    // TODO: Maybe it should be optional to perform this test (in a parameter)
		    validateThatAllEdgesInAllPathsArePartOfTheGraph(shortestPaths);
		
		    return shortestPaths;
	    }

	    void validateThatAllEdgesInAllPathsArePartOfTheGraph(IList<P> paths) {
		    foreach (P path in paths) {
			    IList<E> edgesForPath = path.getEdgesForPath();
			    foreach (E e in edgesForPath) {
				    if(!graph.containsEdge(e)) {
					    // potential improvement: Use Notification pattern to collect all (if more than one) errors instead of throwing at the first error
					    throw new GraphValidationException("Edge in path is not part of the graph: " + e);
				    }
			    }
		    }
	    }

	    private void validateThatBothVerticesArePartOfTheGraph(V startVertex, V endVertex) {
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
	    private void throwExceptionBecauseVertexNotIncludedInGraph(string startOrEndmessagePrefix, V vertex) {
		    throw new GraphValidationException(startOrEndmessagePrefix + " vertex is not part of the graph: " + vertex);
	    }

	    protected E getOriginalEdgeInstance(string startVertexId, string endVertexId) {
		    return edgeMapper.getOriginalEdgeInstance(startVertexId, endVertexId);
	    }

	    protected GraphGenerics<E, V, W> getGraph() {
		    return graph;
	    }

	    // "Hook" : see the Template Method Design Pattern
	    protected abstract IList<P> findShortestPathHook(V startVertex, V endVertex, int maxNumberOfPaths);
	
	    protected W createInstanceWithTotalWeight(double totalWeight, IList<E> edgesUsedForDeterminingWeightClass) {
		    if(weightProtoypeFactory == null) {
			    if(edgesUsedForDeterminingWeightClass.Count > 0) {
				    E e = edgesUsedForDeterminingWeightClass[0];
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
}