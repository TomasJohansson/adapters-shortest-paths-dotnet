/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/

using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.api.generics;
using com.programmerare.shortestpaths.core.validation;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace com.programmerare.shortestpaths.core.impl.generics
{
    public class GraphGenericsImpl<E, V, W> : GraphGenerics<E, V, W> 
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {
	    private readonly IList<E> edges;
	
	    private IList<V> vertices; // lazy loaded when it is needed

	    // vertex id is the key
	    private IDictionary<string, V> mapWithVertices; // lazy loaded at the same time some the list is pppulated
	
	    // edge id is the key
	    private IDictionary<string, E> mapWithEdges; // lazy loaded
	
	    protected GraphGenericsImpl(
		    IList<E> edges,
		    GraphEdgesValidationDesired graphEdgesValidationDesired
	    ) {
		    this.edges = new ReadOnlyCollection<E>(edges);
		    if(graphEdgesValidationDesired == GraphEdgesValidationDesired.YES) {
			    GraphEdgesValidator<PathGenerics<E,V,W>, E, V, W>.ValidateEdgesForGraphCreation<PathGenerics<E,V,W>, E, V, W>(this.edges);
		    }		
	    }
	
	    /**
	     * Creates a graph instance, but will validate the edges and throw an exception if validation fails.
	     * If validation is not desired, then use the overloaded method.
	     * @param <E> edge
	     * @param <V> vertex
	     * @param <W> weight
	     * @param edges list of edges
	     * @return an instance implementing the interface GraphGenerics
	     */
	    public static GraphGenerics<E, V, W> CreateGraphGenerics<E, V, W>(
		    IList<E> edges
	    )
            where E : EdgeGenerics<V, W>
            where V : Vertex
            where W : Weight
        {
		    return CreateGraphGenerics<E, V, W>(edges, GraphEdgesValidationDesired.YES);
	    }
	
	    public static GraphGenerics<E, V, W> CreateGraphGenerics<E, V, W>(
		    IList<E> edges,
		    GraphEdgesValidationDesired graphEdgesValidationDesired
	    )
            where E : EdgeGenerics<V, W>
            where V : Vertex
            where W : Weight
        {
		    var g = new GraphGenericsImpl<E, V, W>(
			    edges,
			    graphEdgesValidationDesired
		    );
		    return g;
	    }

        public IList<E> Edges => edges;

        public IList<V> Vertices
        {
            get
            {
                if (vertices == null)
                { // lazy loading
                    IList<V> vertices = new List<V>();
                    IDictionary<string, V> map = new Dictionary<string, V>();
                    foreach (E edge in edges)
                    {
                        V startVertex = edge.StartVertex;
                        V endVertex = edge.EndVertex;

                        if (!map.ContainsKey(startVertex.VertexId))
                        {
                            map.Add(startVertex.VertexId, startVertex);
                            vertices.Add(startVertex);
                        }

                        if (!map.ContainsKey(endVertex.VertexId))
                        {
                            map.Add(endVertex.VertexId, endVertex);
                            vertices.Add(endVertex);
                        }
                    }
                    this.vertices = vertices;
                    this.mapWithVertices = map;
                }
                return vertices;
            }
        }

        public bool ContainsVertex(V vertex) {
		    var v = Vertices; // triggers the lazy loading if needed, TODO refactor instead of using a getter for this purpose
		    return mapWithVertices.ContainsKey(vertex.VertexId);
	    }

	    public bool ContainsEdge(E edge) {
		    if(mapWithEdges == null) {
			    mapWithEdges = new Dictionary<string, E>();
			    foreach (E e in edges) {
				    mapWithEdges.Add(e.EdgeId, e);
			    }			
		    }
		    return mapWithEdges.ContainsKey(edge.EdgeId);
	    }
    }
}