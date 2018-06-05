/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/

using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.api.generics;

namespace com.programmerare.shortestpaths.core.impl.generics
{
    /**
     * @author Tomas Johansson
     */
    public class EdgeGenericsImpl<V, W> : EdgeGenerics<V , W> 
        where V : Vertex
        where W : Weight
    {
	    private readonly string id;
	    private readonly V startVertex;
	    private readonly V endVertex;
	    private readonly W weight;

	    public static EdgeGenerics<V, W> createEdgeGenerics<V, W>(
		    string edgeId,
		    V startVertex, 
		    V endVertex, 
		    W weight			
	    ) 
            where V : Vertex
            where W : Weight
        {
		    EdgeGenerics<V, W> e = new EdgeGenericsImpl<V, W>(
			    edgeId,
			    startVertex, 
			    endVertex, 
			    weight				
		    );
		    return e;
	    }
	
	    public static EdgeGenerics<V, W> createEdgeGenerics<V, W>(
		    V startVertex, 
		    V endVertex, 
		    W weight			
	    ) 
            where V : Vertex
            where W : Weight
        {
		    return createEdgeGenerics(
			    createEdgeIdValue(startVertex.getVertexId(), endVertex.getVertexId()),
			    startVertex, 
			    endVertex, 
			    weight					
		    );
	    }

	    protected EdgeGenericsImpl(
		    string edgeId,
		    V startVertex, 
		    V endVertex, 
		    W weight
	    ) {
		    this.startVertex = startVertex;
		    this.endVertex = endVertex;
		    this.weight = weight;
		    this.id = edgeId;
	    }
	
	    public V getStartVertex() {
		    return startVertex;
	    }

	    public V getEndVertex() {
		    return endVertex;
	    }

	    public W getEdgeWeight() {
		    return weight;
	    }
	
	    public string getEdgeId() {
		    return id;
	    }

	    private const string SEPARATOR_BETWEEN_START_AND_END_VERTEX_ID = "_";


	    /**
	     * @param startVertexId the id for the vertex at the start of an edge
	     * @param endVertexId the id for the vertex at the end of an edge
	     * @return the id to be used for the edge, as documented by {@link Vertex#getVertexId()}
	     */
	    public static string createEdgeIdValue(string startVertexId, string endVertexId) {
		    // It could be nicer to place this method somewhere else but the important thing is 
		    // to avoid the duplication, i.e. avoid implementing the concatenation in different places.		
		    return startVertexId + SEPARATOR_BETWEEN_START_AND_END_VERTEX_ID + endVertexId;
	    }

	    public override string ToString() {
		    return "EdgeImpl [id=" + id + ", startVertex=" + startVertex + ", endVertex=" + endVertex + ", weight=" + weight
				    + "]";
	    }

	    public override int GetHashCode() {
		    int prime = 31;
		    int result = 1;
		    result = prime * result + ((endVertex == null) ? 0 : endVertex.GetHashCode());
		    result = prime * result + ((id == null) ? 0 : id.GetHashCode());
		    result = prime * result + ((startVertex == null) ? 0 : startVertex.GetHashCode());
		    result = prime * result + ((weight == null) ? 0 : weight.GetHashCode());
		    return result;
	    }

	    public override bool Equals(object obj) {
		    if (this == obj)
			    return true;
		    if (obj == null)
			    return false;
		    if (!(obj is EdgeGenericsImpl<V, W>))
			    return false;
		    EdgeGenericsImpl<V, W> other = (EdgeGenericsImpl<V, W>) obj;
		    if (endVertex == null) {
			    if (other.endVertex != null)
				    return false;
		    } else if (!endVertex.Equals(other.endVertex))
			    return false;
		    if (id == null) {
			    if (other.id != null)
				    return false;
		    } else if (!id.Equals(other.id))
			    return false;
		    if (startVertex == null) {
			    if (other.startVertex != null)
				    return false;
		    } else if (!startVertex.Equals(other.startVertex))
			    return false;
		    if (weight == null) {
			    if (other.weight != null)
				    return false;
		    } else if (!weight.Equals(other.weight))
			    return false;
		    return true;
	    }

	    public string renderToString() {
		    return ToString();
	    }
    }
}