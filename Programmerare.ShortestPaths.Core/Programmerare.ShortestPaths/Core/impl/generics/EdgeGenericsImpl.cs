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

namespace Programmerare.ShortestPaths.Core.Impl.Generics
{
    public class EdgeGenericsImpl<V, W> : EdgeGenerics<V , W> 
        where V : Vertex
        where W : Weight
    {
	    private readonly string id;
	    private readonly V startVertex;
	    private readonly V endVertex;
	    private readonly W weight;

	    public static EdgeGenerics<V, W> CreateEdgeGenerics<V, W>(
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
	
	    public static EdgeGenerics<V, W> CreateEdgeGenerics<V, W>(
		    V startVertex, 
		    V endVertex, 
		    W weight			
	    ) 
            where V : Vertex
            where W : Weight
        {
		    return CreateEdgeGenerics(
			    CreateEdgeIdValue(startVertex.VertexId, endVertex.VertexId),
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

        public V StartVertex => startVertex;

        public V EndVertex => endVertex;

        public W EdgeWeight => weight;

        public string EdgeId => id;

        private const string SEPARATOR_BETWEEN_START_AND_END_VERTEX_ID = "_";

        /// <summary>
        /// Creates an edge id by a concatenation of the two vertices and with a separator between those.
        /// </summary>
        /// <param name="startVertexId">the id for the vertex at the start of an edge</param>
        /// <param name="endVertexId">the id for the vertex at the end of an edge</param>
        /// <returns>the id to be used for the edge</returns>
	    public static string CreateEdgeIdValue(string startVertexId, string endVertexId) {
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

	    public string RenderToString() {
		    return ToString();
	    }
    }
}