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
	
	    public static GraphEdgesValidator<P, E, V, W> createGraphEdgesValidator<P, E, V, W>()
            where P : PathGenerics<E,V,W>
            where E : EdgeGenerics<V, W>
            where V : Vertex
            where W : Weight
        {
		    return new GraphEdgesValidator<P, E, V, W>();
	    }	

	    public void validateEdgesAsAcceptableInputForGraphConstruction(IList<E> edges) {

		    IDictionary<string, bool> mapForValidatingUniqueEdgeId = new Dictionary<string, bool>(); 
		    IDictionary<string, bool> mapForValidatingUniqueVerticesIds = new Dictionary<string, bool>();
		
		    // * all three aggregated objects (weight and both vertices) must be non-null 
		    // * all edge ids and vertex ids (start and end vertices in each edge) must be non-null and non-blank
		    // * all edges must be unique in two ways:
		    // 		unique ids
		    //  	unique combination of start vertex and end vertex (i.e. not multiple weights for an edge between two vertices)
		    //  	(but it can be noted that often the above two are same i.e. the vertex id is a concatenation if not set explicitly)
		    foreach (E edge in edges) {
			    validateNonNullObjects(edge);
			    validateNonBlankIds(edge);

			    validateUniqueEdgeId(edge, mapForValidatingUniqueEdgeId);
			    validateUniqueVerticesIds(edge, mapForValidatingUniqueVerticesIds);
		    }		
	    }

	    public void validateNonNullObjects(E edge) {
		    throwExceptionIfConditionTrue(edge == null, "edge == null", edge);
		    throwExceptionIfConditionTrue(edge.getStartVertex() == null, "start vertex is null for edge", edge);
		    throwExceptionIfConditionTrue(edge.getEndVertex() == null, "end vertex is null for edge", edge);
		    throwExceptionIfConditionTrue(edge.getEdgeWeight() == null, "weight is null for edge", edge);
	    }

	    /**
	     * Precondition: the "outer" objects (e.g. "edge" and "edge.getStartVertex()" should already have been checked for not being null 
	     * @param edge
	     */
	    public void validateNonBlankIds(E edge) {
		    validateNonBlankId(edge.getEdgeId(), edge);
		    validateNonBlankId(edge.getStartVertex().getVertexId(), edge.getStartVertex());
		    validateNonBlankId(edge.getEndVertex().getVertexId(), edge.getEndVertex());
	    }
	
	    private void throwExceptionIfConditionTrue(bool conditionFoExceptionToBeThrown, string exceptionMessagePrefix, StringRenderable edgeOrVertexOrWeight) {
		    if(conditionFoExceptionToBeThrown) {
			    string exceptionMessageSuffix = edgeOrVertexOrWeight == null ? "null" : edgeOrVertexOrWeight.renderToString();
			    throw new GraphValidationException(exceptionMessagePrefix + " " + exceptionMessageSuffix);
		    }
	    }

	    private void validateNonBlankId(string id, StringRenderable edgeOrVertex) {
		    throwExceptionIfConditionTrue(id == null || id.Trim().Equals(""), "id value must not be empty", edgeOrVertex);
	    }
	
	    void validateUniqueEdgeId(E edge, IDictionary<string, bool> mapForValidatingUniqueEdgeId) {
		    throwExceptionIfConditionTrue(mapForValidatingUniqueEdgeId.ContainsKey(edge.getEdgeId()), "Edge id must be unique wich it was not. To remove duplicated edges, you can use a method in the class " + nameOfClassForRemovingDuplicateEdges, edge);
		    mapForValidatingUniqueEdgeId.Add(edge.getEdgeId(), true);
	    }

	    /**
	     * Precondition: the edge should already have been checked for nulls and blank values, i.e.
	     * the method can assume that 'edge.getStartVertex().getVertexId()' will work without throwing NullPointerException
	     * @param edge
	     * @param mapForValidatingUniqueVerticesIds
	     */
	    void validateUniqueVerticesIds(E edge, IDictionary<string, bool> mapForValidatingUniqueVerticesIds) {
		    string concatenationOdVerticesIds = edge.getStartVertex().getVertexId() + "_" + edge.getEndVertex().getVertexId();
		    throwExceptionIfConditionTrue(mapForValidatingUniqueVerticesIds.ContainsKey(concatenationOdVerticesIds), "edge id must be unique wich it was not " + concatenationOdVerticesIds, edge);
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
	    public void validateAllPathsOnlyContainEdgesDefinedInGraph(
		    IList<P> paths, 
		    IList<E> allEdgesForGraph
	    ) {
		    IDictionary<string, E> mapWithAllEdgesInGraph = createMapWithAllEdgesInGraph(allEdgesForGraph);
		
		    foreach (P path in paths) {
			    IList<E> edgesForPath = path.getEdgesForPath();
			    foreach (E edgeInPath in edgesForPath) {
				    validateNonNullObjects(edgeInPath);
				    validateNonBlankIds(edgeInPath);
				    string key = createMapKeyUsedInMapWithEdges(edgeInPath);
				    throwExceptionIfConditionTrue(!mapWithAllEdgesInGraph.ContainsKey(key), "The edge in path is not part of the graph", edgeInPath);
			    }
		    }
	    }

	    /**
	     * @param edgesForGraph
	     * @return a map with edges as values, and the key is a string created with a private helper method in this same class   
	     */
	    private IDictionary<string, E> createMapWithAllEdgesInGraph(IList<E> edgesForGraph) {
		    IDictionary<string, E> mapWithAllEdgesInGraph = new Dictionary<string, E>();
		    foreach (E edgeInGraph in edgesForGraph) {
			    // the method used below should never cause a NullPointerException if the above documented precondition is fulfilled
			    string key = createMapKeyUsedInMapWithEdges(edgeInGraph);
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
	    private string createMapKeyUsedInMapWithEdges(E edge) {
		    string key = edge.getEdgeId() + "_" + edge.getStartVertex().getVertexId() + "_" +  edge.getEndVertex().getVertexId();
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
	    public static void validateEdgesForGraphCreation<P, E, V, W>(IList<E> edges) 
            where P : PathGenerics<E,V,W>
            where E : EdgeGenerics<V, W>
            where V : Vertex
            where W : Weight
        {
		    GraphEdgesValidator<P, E, V, W> graphEdgesValidator = createGraphEdgesValidator<P, E, V, W>();
		    graphEdgesValidator.validateEdgesAsAcceptableInputForGraphConstruction(edges);
	    }
    }
}