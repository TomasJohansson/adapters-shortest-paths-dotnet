/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/

using System.Collections.Generic;

namespace com.programmerare.shortestpaths.core.api.generics
{
    /**
     * A path represents a connection between two Vertices, the start Vertex in the first Edge of the edge List, 
     * and the end Vertex in the last Edge of the edge List. 
     * 
     * @author Tomas Johansson
     */
    public interface PathGenerics<E, V, W> 
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {
	
	    /**
	     * @return a list of Edge instances which together represent the Path.
	     * 	For each Edge in the list, the start Vertex should the same Vertex as the end Vertex for the previous Edge in the List.
	     * (but of course not for the first Edge in the List, since it has not previous Edge)   
	     */
	    IList<E> getEdgesForPath();
	
	    /**
	     * @return the sum of all the weights in the Weight instances aggregated by all the Edge instances in the Path. 
	     * Obviously, the client code could calculate the sum through the method returning a List of Edge instance, but 
	     * it is provided here as a convenience method in the interface.  
	     */
	    W getTotalWeightForPath();	
    }
}