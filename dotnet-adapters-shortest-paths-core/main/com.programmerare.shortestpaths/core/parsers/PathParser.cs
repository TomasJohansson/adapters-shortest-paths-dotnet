/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.api.generics;
using com.programmerare.shortestpaths.core.impl;
using com.programmerare.shortestpaths.core.impl.generics;
using com.programmerare.shortestpaths.core.pathfactories;
using com.programmerare.shortestpaths.core.validation;
using com.programmerare.shortestpaths.utils;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace com.programmerare.shortestpaths.core.parsers
{
    /**
    *  TODO: write more/better documentation ...
    * 
    * {@code 
    * String representation of the "List<Path<Edge>>" i.e. the same type returned from the following method: 
    * List<Path<Edge>> shortestPaths = pathFinder.findShortestPaths(startVertex, endVertex, numberOfPathsToFind);
    * The intended purpose is to define strings within xml files with the expected result
    * 
    * Each line ns a string is first the total weight and then the sequence of vertices.
    * Example:  "13 A B D"
    * The simple representation (without weight informatin) is the reason why the list of edges is also needed,
    * i.e. to find the weights.
    * }
    * 
    * @param <P> path
    * @param <E> edge
    * @param <V> vertex
    * @param <W> weight

    * @author Tomas Johansson
    *
    */
    public sealed class PathParser<P, E, V, W>
        where P : PathGenerics<E, V, W>
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {

	    //<P extends Path<E,V,W> , E extends Edge<V, W> , V extends Vertex , W extends Weight> implements PathFinder<P, E, V, W>
	
	    private readonly IDictionary<string, E> mapWithEdgesAndVertexConcatenationAsKey;

	    private PathFactory<P, E, V, W> pathFactory;// = new PathFactoryGenerics<P, E, V, W>();

	    /**
	     * @param pathFactory used for creating an instance of Path<E, V, W> 
	     * @param edgesUsedForFindingTheWeightsBetweenVerticesInPath
	     * @see PathFactory
	     */
	    private PathParser(
		    PathFactory<P, E, V, W> pathFactory,
		    IList<E> edgesUsedForFindingTheWeightsBetweenVerticesInPath
	    ) {
		    this.pathFactory = pathFactory;
		    // TOOD: use input validator here when that branch has been merged into the same code base
    //		this.edgesUsedForFindingTheWeightsBetweenVerticesInPath = edgesUsedForFindingTheWeightsBetweenVerticesInPath;
		
		    mapWithEdgesAndVertexConcatenationAsKey = new Dictionary<string, E>();
		    foreach (E edge in edgesUsedForFindingTheWeightsBetweenVerticesInPath) {
			    string key = EdgeGenericsImpl<V, W>.createEdgeIdValue(edge.getStartVertex().getVertexId(), edge.getEndVertex().getVertexId());
			    mapWithEdgesAndVertexConcatenationAsKey.Add(key, edge);
		    }
	    }
	
	    public static PathParser<P, E, V, W> createPathParser<P, E, V, W>(
		    PathFactory<P, E, V, W> pathFactory,
		    IList<E> edgesUsedForFindingTheWeightsBetweenVerticesInPath
	    )
            where P : PathGenerics<E, V, W>
            where E : EdgeGenerics<V, W>
            where V : Vertex
            where W : Weight
        {
		    return new PathParser<P, E, V, W>(pathFactory, edgesUsedForFindingTheWeightsBetweenVerticesInPath);
	    }
	
	    public static PathParser<P, E, V, W> createPathParserGenerics<P, E, V, W>(
		    IList<E> edgesUsedForFindingTheWeightsBetweenVerticesInPath
	    )
            where P : PathGenerics<E, V, W>
            where E : EdgeGenerics<V, W>
            where V : Vertex
            where W : Weight

        {
		    return createPathParser(new PathFactoryGenerics<P, E, V, W>(), edgesUsedForFindingTheWeightsBetweenVerticesInPath);
	    }	

	    public static PathParser<Path , Edge , Vertex , Weight> createPathParserDefault(
		    IList<Edge> edgesUsedForFindingTheWeightsBetweenVerticesInPath
	    ) {
		    return createPathParser(new PathFactoryDefault(), edgesUsedForFindingTheWeightsBetweenVerticesInPath);
	    }	
	
	
	    public IList<P> fromStringToListOfPaths(string multiLinedString) {
		    IList<string> listOfLines = StringUtility.getMultilineStringAsListOfTrimmedStringsIgnoringLinesWithOnlyWhiteSpace(multiLinedString);
		    return fromListOfStringsToListOfPaths(listOfLines);
	    }
	
	    public IList<P> fromListOfStringsToListOfPaths(IList<string> listOfStrings) {
		    IList<P> listOfPaths = new List<P>();
		    foreach (string aString in listOfStrings) {
			    listOfPaths.Add(fromStringToPath(aString));
		    }
		    return listOfPaths;
	    }

	    /**
	     * @param pathString first the total weight and then the sequence of vertices.
	     * 		Example:  "13 A B D"
	     * @return
	     */
	    public P fromStringToPath(string pathString) {
		    string[] array = Regex.Split(pathString, "\\s+");

		    // TODO check "array.length" and throw exception ...
		    double totalWeight = double.Parse(array[0]);
		
		    IList<E> edges = new List<E>(); 
		
		    for (int i = 2; i < array.Length; i++) {
			    string startVertexId = array[i-1];
			    string endVertexId = array[i];
			    E edge = getEdgeIncludingTheWeight(startVertexId, endVertexId);
			    edges.Add(edge);
		    }
		    W weight = (W) WeightImpl.createWeight(totalWeight);
		    return this.createPath(weight, edges);
	    }
	
	    public string fromPathToString(P path) {
		    StringBuilder sb = new StringBuilder();
		    double d = path.getTotalWeightForPath().getWeightValue();
		    string s = StringUtility.getDoubleAsStringWithoutZeroesAndDotIfNotRelevant(d);
		    sb.Append(s);
		    IList<E> edgesForPath = path.getEdgesForPath();
		    foreach (E edge in edgesForPath) {
			    sb.Append(" ");			
			    sb.Append(edge.getStartVertex().getVertexId());
		    }
		    sb.Append(" ");		
		    sb.Append(edgesForPath[edgesForPath.Count-1].getEndVertex().getVertexId());
		    return sb.ToString();
	    }

	    public E getEdgeIncludingTheWeight(string startVertexId, string endVertexId) {
		    string key = EdgeGenericsImpl<V, W>.createEdgeIdValue(startVertexId, endVertexId);
		    if(!mapWithEdgesAndVertexConcatenationAsKey.ContainsKey(key)) {
			    throw new GraphValidationException("No edge with these vertices: from " + startVertexId + " to " + endVertexId);
		    }
		    return mapWithEdgesAndVertexConcatenationAsKey[key];
	    }

	    private P createPath(W totalWeight, IList<E> edges) {
		    P path = this.pathFactory.createPath(totalWeight, edges);
		    return path;
	    }	
    }
}