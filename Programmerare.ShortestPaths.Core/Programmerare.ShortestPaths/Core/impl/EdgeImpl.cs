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

namespace Programmerare.ShortestPaths.Core.Impl
{
    public sealed class EdgeImpl : EdgeGenericsImpl<Vertex, Weight> , Edge {

	    private EdgeImpl(
		    string edgeId, 
		    Vertex startVertex, 
		    Vertex endVertex, 
		    Weight weight
	    ) : base(edgeId, startVertex, endVertex, weight) {
	    }

	    public static Edge CreateEdge(
		    Vertex startVertex, 
		    Vertex endVertex, 
		    Weight weight
	    ) {
		    string edgeId = CreateEdgeIdValue(startVertex.VertexId, endVertex.VertexId);
		    return CreateEdge(edgeId, startVertex, endVertex, weight);
	    }

	    public static Edge CreateEdge(
		    string edgeId, 
		    Vertex startVertex, 
		    Vertex endVertex, 
		    Weight weight
	    ) {
		    return new EdgeImpl(edgeId, startVertex, endVertex, weight);
	    }	
    }
}