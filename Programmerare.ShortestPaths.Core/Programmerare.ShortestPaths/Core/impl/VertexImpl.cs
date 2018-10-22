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

namespace Programmerare.ShortestPaths.Core.Impl
{
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