/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in this "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using Programmerare.ShortestPaths.Core.Api;
using Programmerare.ShortestPaths.Core.Api.Generics;
using Programmerare.ShortestPaths.Core.Impl;
using Programmerare.ShortestPaths.Core.Impl.Generics;
using Programmerare.ShortestPaths.Core.PathFactories;
using Programmerare.ShortestPaths.Core.Validation;
using Programmerare.ShortestPaths.Utils;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Programmerare.ShortestPaths.Core.Parsers
{
    /**
    *  TODO: write more/better documentation ...
    */

    /// <summary>
    /// String representation of the "List{Path{Edge}}" i.e. the same type returned from the following method: 
    /// List{Path{Edge}} shortestPaths = pathFinder.findShortestPaths(startVertex, endVertex, numberOfPathsToFind);
    /// The intended purpose is to define strings within xml files with the expected result
    /// Each line in a string is first the total weight and then the sequence of vertices.
    /// Example:  "13 A B D"
    /// The simple representation (without weight informatin) is the reason why the list of edges is also needed,
    /// i.e. to find the weights.
    /// </summary>
    /// <typeparam name="P">Path</typeparam>
    /// <typeparam name="E">Edge</typeparam>
    /// <typeparam name="V">Vertex</typeparam>
    /// <typeparam name="W">Weight</typeparam>
    public sealed class PathParser<P, E, V, W>
        where P : PathGenerics<E, V, W>
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {

	    private readonly IDictionary<string, E> mapWithEdgesAndVertexConcatenationAsKey;

	    private PathFactory<P, E, V, W> pathFactory;

        /// <param name="pathFactory">
        /// used for creating an instance of Path{E, V, W}.
        /// See <see cref="PathFactory"/>
        /// </param>
        /// <param name="edgesUsedForFindingTheWeightsBetweenVerticesInPath"></param>
	    private PathParser(
		    PathFactory<P, E, V, W> pathFactory,
		    IList<E> edgesUsedForFindingTheWeightsBetweenVerticesInPath
	    ) {
		    this.pathFactory = pathFactory;
		    // TODO: use input validator here when that branch has been merged into the same code base
            //this.edgesUsedForFindingTheWeightsBetweenVerticesInPath = edgesUsedForFindingTheWeightsBetweenVerticesInPath;
		    mapWithEdgesAndVertexConcatenationAsKey = new Dictionary<string, E>();
		    foreach (E edge in edgesUsedForFindingTheWeightsBetweenVerticesInPath) {
			    string key = EdgeGenericsImpl<V, W>.CreateEdgeIdValue(edge.StartVertex.VertexId, edge.EndVertex.VertexId);
			    mapWithEdgesAndVertexConcatenationAsKey.Add(key, edge);
		    }
	    }
	
	    public static PathParser<P, E, V, W> CreatePathParser<P, E, V, W>(
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
	
	    public static PathParser<P, E, V, W> CreatePathParserGenerics<P, E, V, W>(
		    IList<E> edgesUsedForFindingTheWeightsBetweenVerticesInPath
	    )
            where P : PathGenerics<E, V, W>
            where E : EdgeGenerics<V, W>
            where V : Vertex
            where W : Weight

        {
		    return CreatePathParser(new PathFactoryGenerics<P, E, V, W>(), edgesUsedForFindingTheWeightsBetweenVerticesInPath);
	    }	

	    public static PathParser<Path , Edge , Vertex , Weight> CreatePathParserDefault(
		    IList<Edge> edgesUsedForFindingTheWeightsBetweenVerticesInPath
	    ) {
		    return CreatePathParser(new PathFactoryDefault(), edgesUsedForFindingTheWeightsBetweenVerticesInPath);
	    }	
	
	
	    public IList<P> FromStringToListOfPaths(string multiLinedString) {
		    IList<string> listOfLines = StringUtility.GetMultilineStringAsListOfTrimmedStringsIgnoringLinesWithOnlyWhiteSpace(multiLinedString);
		    return FromListOfStringsToListOfPaths(listOfLines);
	    }
	
	    public IList<P> FromListOfStringsToListOfPaths(IList<string> listOfStrings) {
		    IList<P> listOfPaths = new List<P>();
		    foreach (string aString in listOfStrings) {
			    listOfPaths.Add(FromStringToPath(aString));
		    }
		    return listOfPaths;
	    }

        /// <param name="pathString">
        /// First the total weight and then the sequence of vertices for the path.
	    /// Example:  "13 A B D"
        /// </param>
	    public P FromStringToPath(string pathString) {
		    string[] array = Regex.Split(pathString, "\\s+");
		    // TODO check "array.length" and throw exception ...
		    double totalWeight = double.Parse(array[0]);
		
		    IList<E> edges = new List<E>(); 
		
		    for (int i = 2; i < array.Length; i++) {
			    string startVertexId = array[i-1];
			    string endVertexId = array[i];
			    E edge = GetEdgeIncludingTheWeight(startVertexId, endVertexId);
			    edges.Add(edge);
		    }
		    W weight = (W) WeightImpl.CreateWeight(totalWeight);
		    return this.CreatePath(weight, edges);
	    }
	
	    public string FromPathToString(P path) {
		    StringBuilder sb = new StringBuilder();
		    double d = path.TotalWeightForPath.WeightValue;
		    string s = StringUtility.GetDoubleAsStringWithoutZeroesAndDotIfNotRelevant(d);
		    sb.Append(s);
		    IList<E> edgesForPath = path.EdgesForPath;
		    foreach (E edge in edgesForPath) {
			    sb.Append(" ");			
			    sb.Append(edge.StartVertex.VertexId);
		    }
		    sb.Append(" ");		
		    sb.Append(edgesForPath[edgesForPath.Count-1].EndVertex.VertexId);
		    return sb.ToString();
	    }

	    public E GetEdgeIncludingTheWeight(string startVertexId, string endVertexId) {
		    string key = EdgeGenericsImpl<V, W>.CreateEdgeIdValue(startVertexId, endVertexId);
		    if(!mapWithEdgesAndVertexConcatenationAsKey.ContainsKey(key)) {
			    throw new GraphValidationException("No edge with these vertices: from " + startVertexId + " to " + endVertexId);
		    }
		    return mapWithEdgesAndVertexConcatenationAsKey[key];
	    }

	    private P CreatePath(W totalWeight, IList<E> edges) {
		    P path = this.pathFactory.CreatePath(totalWeight, edges);
		    return path;
	    }	
    }
}