/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
using System.Collections.Generic;

namespace com.programmerare.shortestpaths.core.api.generics
{
    /**
     * A Graph represents a collection of Edge instances.
     * 
     * @author Tomas Johansson
     */
    public interface GraphGenerics<E , V , W>
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {

        /**
	     * @return all edges in the graph
	     */
        IList<E> Edges { get; }

        /**
	     * @return all vertices in the graph. 
	     * 	The vertices can be derived from the edges, i.e. the method might be implemented as iterating the edges 
	    *  	as a way to create a list of all vertices.
	    * 	However, for performance reasons should it not be done every time the method is invoked, but preferably implemented with lazy loading.  
	     */
        IList<V> Vertices { get; }

        /**
	     * @param vertex an instance of a Vertex. When the method is implemented, it should use the Vertex id for the comparison,  
	     * i.e. the implementation should NOT use object equality when determining whether the Vertex is part of the Graph or not.  
	     * @return true if there is a Vertex in the graph with the same id as the Vertex parameter.
	     */
        bool ContainsVertex(V vertex);
	
	    /**
	     * @param edge an instance of an Edge. When the method is implemented, it should use the Edge id for the comparison,  
	     * i.e. the implementation should NOT use object equality when determining whether the Edge is part of the Graph or not.  
	     * @return true if there is an Edge in the graph with the same id as the Edge parameter.
	     */
	    bool ContainsEdge(E edge);
    }
}