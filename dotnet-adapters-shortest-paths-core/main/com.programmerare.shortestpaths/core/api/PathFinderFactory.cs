using com.programmerare.shortestpaths.core.api.generics;

/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
namespace com.programmerare.shortestpaths.core.api
{
    /**
     * @see PathFinderFactoryGenerics 
     * @author Tomas Johansson
     */
    public interface PathFinderFactory 
	    : 
        PathFinderFactoryGenerics<
            PathFinder , // PathFinder< Edge<Vertex , Weight> , Vertex , Weight>
            Path,
            Edge, // Edge<Vertex , Weight>  
            Vertex,
            Weight
        >
    { }
}