using com.programmerare.shortestpaths.core.api.generics;

/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
namespace com.programmerare.shortestpaths.core.api
{
    /**
     * @see PathFinderGenerics
     * @author Tomas Johansson
     */
    public interface PathFinder 
	    : PathFinderGenerics<
		    Path,
		    Edge,  // Edge<Vertex , Weight>
		    Vertex , 
		    Weight
	    >
    {}
}