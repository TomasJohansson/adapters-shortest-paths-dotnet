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
using System.Collections.Generic;

namespace Programmerare.ShortestPaths.Core.Impl.Generics
{
    /// <summary>
    /// Edge is an interface which the implementations will not know of.
    /// Instances are passed as parameter into a construction of an implementation specific graph
    /// which will return its own kind of edges.
    /// Then those edges will be converted back to the common Edge interface, but in such case 
    /// it will be a new instance which may not be desirable if the instances are not
    /// the default implementations provided but this project, but they may be classes with more data methods,
    /// and therefore it is desirable to map them back to the original instances, which is the purpose of this class.
    /// </summary>
    /// <typeparam name="E">Edge</typeparam>
    /// <typeparam name="V">Vertex</typeparam>
    /// <typeparam name="W">Weight</typeparam>
    public sealed class EdgeMapper<E, V, W> 
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {
	    private readonly IDictionary<string, E> edgeMapWithVertexIdsAsKey = new Dictionary<string, E>();

        /// <summary>
        /// Precondition: the edges must already be validated. Use GraphEdgesValidator before createEdgeMapper.
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <typeparam name="W"></typeparam>
        /// <param name="edges">a list of edges to be used for constructing a graph. Note that they are assumed to be validated as a precondition</param>
        /// <returns></returns>
	    public static EdgeMapper<E, V, W> CreateEdgeMapper<E, V, W>(IList<E> edges)
            where E : EdgeGenerics<V, W>
            where V : Vertex
            where W : Weight
        {
		    return new EdgeMapper<E, V, W>(edges);
	    }
	
	    private EdgeMapper(IList<E> edges) {
		    foreach (E edge in edges) {
			    string idForMapping = GetIdForMapping(edge);
			    edgeMapWithVertexIdsAsKey.Add(idForMapping, edge);
		    }
	    }

	    public IList<E> GetOriginalObjectInstancesOfTheEdges(IList<E> edges) {
		    IList<E> originalObjectInstancesOfTheEdges = new List<E>();
		    foreach (E edge in edges) {
			    originalObjectInstancesOfTheEdges.Add(edgeMapWithVertexIdsAsKey[GetIdForMapping(edge)]);
		    }		
		    return originalObjectInstancesOfTheEdges;
	    }

	    public E GetOriginalEdgeInstance(string startVertexId, string endVertexId) {
		    return edgeMapWithVertexIdsAsKey[GetIdForMapping(startVertexId, endVertexId)];
	    }
	
	    private string GetIdForMapping(E edge) {
		    return GetIdForMapping(edge.StartVertex, edge.EndVertex);
	    }
	
	    private string GetIdForMapping(V startVertex, V endVertex) {
		    return GetIdForMapping(startVertex.VertexId, endVertex.VertexId);
	    }
	
	    private string GetIdForMapping(string startVertexId, string endVertexId) {
		    return EdgeGenericsImpl<V, W>.CreateEdgeIdValue(startVertexId, endVertexId);
	    }	
    }
}