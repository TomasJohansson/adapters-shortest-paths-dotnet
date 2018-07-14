/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.api.generics;
using System.Collections.Generic;

namespace com.programmerare.shortestpaths.core.validation
{
    public sealed class GraphEdgesValidator<P, E, V, W> 
        where P : PathGenerics<E,V,W>
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {

	    private readonly static string nameOfClassForRemovingDuplicateEdges;
	    static GraphEdgesValidator() {
		    // refactoring friendly reference to the class name
		    nameOfClassForRemovingDuplicateEdges = "TODO....";//typeof(EdgeUtility).FullName;
	    }
	
	    private GraphEdgesValidator() {	}
	
	    public static GraphEdgesValidator<P, E, V, W> CreateGraphEdgesValidator<P, E, V, W>()
            where P : PathGenerics<E,V,W>
            where E : EdgeGenerics<V, W>
            where V : Vertex
            where W : Weight
        {
		    return new GraphEdgesValidator<P, E, V, W>();
	    }	

	    public void ValidateEdgesAsAcceptableInputForGraphConstruction(IList<E> edges) {

		    IDictionary<string, bool> mapForValidatingUniqueEdgeId = new Dictionary<string, bool>(); 
		    IDictionary<string, bool> mapForValidatingUniqueVerticesIds = new Dictionary<string, bool>();
		
		    // * all three aggregated objects (weight and both vertices) must be non-null 
		    // * all edge ids and vertex ids (start and end vertices in each edge) must be non-null and non-blank
		    // * all edges must be unique in two ways:
		    // 		unique ids
		    //  	unique combination of start vertex and end vertex (i.e. not multiple weights for an edge between two vertices)
		    //  	(but it can be noted that often the above two are same i.e. the vertex id is a concatenation if not set explicitly)
		    foreach (E edge in edges) {
			    ValidateNonNullObjects(edge);
			    ValidateNonBlankIds(edge);

			    ValidateUniqueEdgeId(edge, mapForValidatingUniqueEdgeId);
			    ValidateUniqueVerticesIds(edge, mapForValidatingUniqueVerticesIds);
		    }		
	    }

	    public void ValidateNonNullObjects(E edge) {
		    ThrowExceptionIfConditionTrue(edge == null, "edge == null", edge);
		    ThrowExceptionIfConditionTrue(edge.StartVertex== null, "start vertex is null for edge", edge);
		    ThrowExceptionIfConditionTrue(edge.EndVertex== null, "end vertex is null for edge", edge);
		    ThrowExceptionIfConditionTrue(edge.EdgeWeight== null, "weight is null for edge", edge);
	    }

	    /**
	     * Precondition: the "outer" objects (e.g. "edge" and "edge.getStartVertex()" should already have been checked for not being null 
	     * @param edge
	     */
	    public void ValidateNonBlankIds(E edge) {
		    ValidateNonBlankId(edge.EdgeId, edge);
		    ValidateNonBlankId(edge.StartVertex.VertexId, edge.StartVertex);
		    ValidateNonBlankId(edge.EndVertex.VertexId, edge.EndVertex);
	    }
	
	    private void ThrowExceptionIfConditionTrue(bool conditionFoExceptionToBeThrown, string exceptionMessagePrefix, StringRenderable edgeOrVertexOrWeight) {
		    if(conditionFoExceptionToBeThrown) {
			    string exceptionMessageSuffix = edgeOrVertexOrWeight == null ? "null" : edgeOrVertexOrWeight.RenderToString();
			    throw new GraphValidationException(exceptionMessagePrefix + " " + exceptionMessageSuffix);
		    }
	    }

	    private void ValidateNonBlankId(string id, StringRenderable edgeOrVertex) {
		    ThrowExceptionIfConditionTrue(id == null || id.Trim().Equals(""), "id value must not be empty", edgeOrVertex);
	    }
	
	    public void ValidateUniqueEdgeId(E edge, IDictionary<string, bool> mapForValidatingUniqueEdgeId) {
		    ThrowExceptionIfConditionTrue(mapForValidatingUniqueEdgeId.ContainsKey(edge.EdgeId), "Edge id must be unique wich it was not. To remove duplicated edges, you can use a method in the class " + nameOfClassForRemovingDuplicateEdges, edge);
		    mapForValidatingUniqueEdgeId.Add(edge.EdgeId, true);
	    }

	    /**
	     * Precondition: the edge should already have been checked for nulls and blank values, i.e.
	     * the method can assume that 'edge.getStartVertex().getVertexId()' will work without throwing NullPointerException
	     * @param edge
	     * @param mapForValidatingUniqueVerticesIds
	     */
	    public void ValidateUniqueVerticesIds(E edge, IDictionary<string, bool> mapForValidatingUniqueVerticesIds) {
		    string concatenationOdVerticesIds = edge.StartVertex.VertexId+ "_" + edge.EndVertex.VertexId;
		    ThrowExceptionIfConditionTrue(mapForValidatingUniqueVerticesIds.ContainsKey(concatenationOdVerticesIds), "edge id must be unique wich it was not " + concatenationOdVerticesIds, edge);
		    mapForValidatingUniqueVerticesIds.Add(concatenationOdVerticesIds, true);		
	    }

	    /**
	     * An example of usage for this method is that both parameters (expected list of paths, and a list of edges) 
	     * may be defined in an xml file, but they might be defined incorrectly, and then it is desirable
	     * to fail early to easier realize that the problem is how the test is defined rather than the behaviour 
	     * of the code under test.
	     * Precondition: The list of all the edges should be valid, i.e. it is NOT tested again in this method that
	     * 	the vertices and weights are not null.    
	     * @param paths list of paths
	     * @param allEdgesForGraph all edges for the graph
	     */
	    public void ValidateAllPathsOnlyContainEdgesDefinedInGraph(
		    IList<P> paths, 
		    IList<E> allEdgesForGraph
	    ) {
		    IDictionary<string, E> mapWithAllEdgesInGraph = CreateMapWithAllEdgesInGraph(allEdgesForGraph);
		
		    foreach (P path in paths) {
			    IList<E> edgesForPath = path.EdgesForPath;
			    foreach (E edgeInPath in edgesForPath) {
				    ValidateNonNullObjects(edgeInPath);
				    ValidateNonBlankIds(edgeInPath);
				    string key = CreateMapKeyUsedInMapWithEdges(edgeInPath);
				    ThrowExceptionIfConditionTrue(!mapWithAllEdgesInGraph.ContainsKey(key), "The edge in path is not part of the graph", edgeInPath);
			    }
		    }
	    }

	    /**
	     * @param edgesForGraph
	     * @return a map with edges as values, and the key is a string created with a private helper method in this same class   
	     */
	    private IDictionary<string, E> CreateMapWithAllEdgesInGraph(IList<E> edgesForGraph) {
		    IDictionary<string, E> mapWithAllEdgesInGraph = new Dictionary<string, E>();
		    foreach (E edgeInGraph in edgesForGraph) {
			    // the method used below should never cause a NullPointerException if the above documented precondition is fulfilled
			    string key = CreateMapKeyUsedInMapWithEdges(edgeInGraph);
			    mapWithAllEdgesInGraph.Add(key, edgeInGraph);
		    }
		    return mapWithAllEdgesInGraph;
	    }

	    /**
	     * Precondition: the input edge and all its aggregated parts must be non-null, i.e. this method should 
	     * never throw an NullPointerException if the precondition is respected  
	     * @param edge
	     * @return
	     */
	    private string CreateMapKeyUsedInMapWithEdges(E edge) {
		    string key = edge.EdgeId+ "_" + edge.StartVertex.VertexId+ "_" +  edge.EndVertex.VertexId;
		    return key;
	    }

	    /**
	     * {@code 
	     * Static convenience method. Can be invoked like this:
	     * GraphEdgesValidator.<Path, Edge, Vertex, Weight>validateEdgesForGraphCreation(edges);
	     * }
	     * @param <P> path
	     * @param <E> edge
	     * @param <V> vertex
	     * @param <W> weight
	     * @param edges list of edges
	     */
	    //public static <E extends Edge> void validateEdgesForGraphCreation(final List<E> edges) {
	    public static void ValidateEdgesForGraphCreation<P, E, V, W>(IList<E> edges) 
            where P : PathGenerics<E,V,W>
            where E : EdgeGenerics<V, W>
            where V : Vertex
            where W : Weight
        {
		    GraphEdgesValidator<P, E, V, W> graphEdgesValidator = CreateGraphEdgesValidator<P, E, V, W>();
		    graphEdgesValidator.ValidateEdgesAsAcceptableInputForGraphConstruction(edges);
	    }
    }
}