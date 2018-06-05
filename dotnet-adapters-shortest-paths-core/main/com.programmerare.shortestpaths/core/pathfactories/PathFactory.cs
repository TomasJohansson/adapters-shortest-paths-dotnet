/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.api.generics;
using System.Collections.Generic;

namespace com.programmerare.shortestpaths.core.pathfactories
{
    /**
     * {@code
     * Used for creating an instance of Path<E, V, W>.
     * 	Example of path types which can be created by an implementation class:
     * 		PathDefault (which is defined as extends Path< EdgeDefault , Vertex , Weight > )
     * 		Path<Edge, Vertex, Weight> (and note that all these types are part of the library)
     * 		Path<Road ,  City , WeightDeterminedByRoadLengthAndQuality>
     * 				(and note that the above three types within brackets are NOT part of the library but 
     * 					examples of types which client code can create as subtypes)
     * }
     * @param <P> Path or subtype
     * @param <E> Edge or subtype
     * @param <V> Vertex or subtype 
     * @param <W> Weight or subtype
     * 
     * @author Tomas Johansson
     */
    public interface PathFactory<P, E, V, W> 
        where P : PathGenerics<E, V, W>
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {
	    P createPath(W totalWeight, IList<E> edges);
    }
}