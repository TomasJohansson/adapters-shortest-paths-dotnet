/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
using com.programmerare.shortestpaths.core.api;

namespace com.programmerare.shortestpaths.core.impl
{
    /**
     * @author Tomas Johansson
     */
    public sealed class VertexImpl : Vertex {

	    private readonly string id;
	
	    public static Vertex CreateVertex(
		    string id		
	    ) {
		    return new VertexImpl(
			    id
		    );
	    }

	    public static Vertex CreateVertex(
		    int id			
	    ) {
		    return CreateVertex(id.ToString());
	    }
	
	    private VertexImpl(string id) {
		    this.id = id;
	    }

        public string VertexId => id;

        public override string ToString() {
		    return "VertexImpl [id=" + id + "]";
	    }

	    public override int GetHashCode() {
		    int prime = 31;
		    int result = 1;
		    result = prime * result + ((id == null) ? 0 : id.GetHashCode());
		    return result;
	    }

	    public override bool Equals(object obj) {
		    if (this == obj)
			    return true;
		    if (obj == null)
			    return false;
		    if (!(obj is VertexImpl))
			    return false;
		    VertexImpl other = (VertexImpl) obj;
		    if (id == null) {
			    if (other.id != null)
				    return false;
		    } else if (!id.Equals(other.id))
			    return false;
		    return true;
	    }

	    public string RenderToString() {
		    return ToString();
	    }
    }
}