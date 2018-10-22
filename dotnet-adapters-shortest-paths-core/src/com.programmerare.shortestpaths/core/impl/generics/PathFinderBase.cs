/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in this "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using System.Collections.Generic;
using Programmerare.ShortestPaths.Core.Api;
using Programmerare.ShortestPaths.Core.Api.Generics;
using Programmerare.ShortestPaths.Core.PathFactories;
using Programmerare.ShortestPaths.Core.Validation;

namespace Programmerare.ShortestPaths.Core.Impl.Generics
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
	
        /// <summary>
        /// 
        /// </summary>
        /// <param name="graph">an implementation of the interface GraphGenerics</param>
        /// <param name="pathFactory">an implementation of the interface PathFactory, if null then a default instance will be created</param>
	    protected PathFinderBase(
		    GraphGenerics<E, V, W> graph, 
		    PathFactory<P, E, V, W> pathFactory
	    ) {
		    this.graph = graph;		
		    this.pathFactory = pathFactory != null ? pathFactory : CreateStandardInstanceOfPathFactory();
		    // Precondition to method below is that validation is performed i.e. 
		    // the method below will NOT try to validate,
		    edgeMapper = EdgeMapper<E, V, W>.CreateEdgeMapper<E, V, W>(graph.Edges);
	    }

	    private PathFactory<P, E, V, W> CreateStandardInstanceOfPathFactory() {
		    return new PathFactoryGenerics<P, E, V, W>();
	    }
	
        /// <summary>
        /// non-virtual method to enforce the validation, 
        /// and then forward to the hook method for the implementations
        /// </summary>
        /// <param name="startVertex"></param>
        /// <param name="endVertex"></param>
        /// <param name="maxNumberOfPaths"></param>
        /// <returns></returns>
	    public IList<P> FindShortestPaths(
		    V startVertex, 
		    V endVertex, 
		    int maxNumberOfPaths
	    ) {
		    ValidateThatBothVerticesArePartOfTheGraph(startVertex, endVertex);
		
		    IList<P> shortestPaths = FindShortestPathHook(
			    startVertex, 
			    endVertex, 
			    maxNumberOfPaths				
		    );

		    // TODO: Maybe it should be optional to perform this test (in a parameter)
		    ValidateThatAllEdgesInAllPathsArePartOfTheGraph(shortestPaths);
		
		    return shortestPaths;
	    }

	    void ValidateThatAllEdgesInAllPathsArePartOfTheGraph(IList<P> paths) {
		    foreach (P path in paths) {
			    IList<E> edgesForPath = path.EdgesForPath;
			    foreach (E e in edgesForPath) {
				    if(!graph.ContainsEdge(e)) {
					    // potential improvement: Use Notification pattern to collect all (if more than one) errors instead of throwing at the first error
					    throw new GraphValidationException("Edge in path is not part of the graph: " + e);
				    }
			    }
		    }
	    }

	    private void ValidateThatBothVerticesArePartOfTheGraph(V startVertex, V endVertex) {
		    // potential improvement: Use Notification pattern to collect all (if more than one) errors instead of throwing at the first error
		    if(!graph.ContainsVertex(startVertex)) {
			    ThrowExceptionBecauseVertexNotIncludedInGraph("start", startVertex);
		    }
		    if(!graph.ContainsVertex(endVertex)) {
			    ThrowExceptionBecauseVertexNotIncludedInGraph("end", endVertex);
		    }		
	    }

        /// <param name="startOrEndmessagePrefix">intended to be one of the strings "start" or "end"</param>
        /// <param name="vertex">Vertex</param>
	    private void ThrowExceptionBecauseVertexNotIncludedInGraph(string startOrEndmessagePrefix, V vertex) {
		    throw new GraphValidationException(startOrEndmessagePrefix + " vertex is not part of the graph: " + vertex);
	    }

	    protected E GetOriginalEdgeInstance(string startVertexId, string endVertexId) {
		    return edgeMapper.GetOriginalEdgeInstance(startVertexId, endVertexId);
	    }

	    protected GraphGenerics<E, V, W> GetGraph() {
		    return graph;
	    }

	    // "Hook" : see the Template Method Design Pattern
	    protected abstract IList<P> FindShortestPathHook(V startVertex, V endVertex, int maxNumberOfPaths);
	
	    protected W CreateInstanceWithTotalWeight(double totalWeight, IList<E> edgesUsedForDeterminingWeightClass) {
		    if(weightProtoypeFactory == null) {
			    if(edgesUsedForDeterminingWeightClass.Count > 0) {
				    E e = edgesUsedForDeterminingWeightClass[0];
				    weightProtoypeFactory = e.EdgeWeight;
			    }
			    // else throw exception may be a good idea since it does not seem to make any sense with a path witz zero edges 
		    }
		    return (W)weightProtoypeFactory.Create(totalWeight);
	    }
	
	    protected P CreatePath(W totalWeight, IList<E> edges) {
		    return pathFactory.CreatePath(totalWeight, edges);
	    }
    }
}