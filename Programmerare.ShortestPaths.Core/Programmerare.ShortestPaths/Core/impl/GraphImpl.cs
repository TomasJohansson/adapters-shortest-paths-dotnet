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
using Programmerare.ShortestPaths.Core.Impl.Generics;
using Programmerare.ShortestPaths.Core.Validation;
using System.Collections.Generic;

namespace Programmerare.ShortestPaths.Core.Impl
{
    public sealed class GraphImpl : GraphGenericsImpl<Edge, Vertex , Weight> , Graph {

	    private GraphImpl(
		    IList<Edge> edges,
		    GraphEdgesValidationDesired graphEdgesValidationDesired
	    ) : base(edges, graphEdgesValidationDesired) {
	    }

        /// <summary>
        /// Creates a graph instance, but will validate the edges and throw an exception if validation fails.
        /// If validation is not desired, then use the overloaded method.
        /// </summary>
        /// <param name="edges">list of all the edges for the graph</param>
        /// <returns>an instance implementing the Graph interface</returns>
	    public static Graph CreateGraph(
		    IList<Edge> edges
	    ) {
		    return CreateGraph(edges, GraphEdgesValidationDesired.YES);
	    }
	
        /// <param name="edges">list of all the edges for the graph</param>
        /// <param name="graphEdgesValidationDesired">enum specifying whether or not validation is desired</param>
        /// <returns>an instance implementing the Graph interface</returns>
	    public static Graph CreateGraph(
		    IList<Edge> edges,
		    GraphEdgesValidationDesired graphEdgesValidationDesired
	    ) {
		    return new GraphImpl(edges, graphEdgesValidationDesired);
	    }	
    }
}