/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.core.pathfactories;

import java.util.List;

import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;
import com.programmerare.shortestpaths.core.api.generics.EdgeGenerics;
import com.programmerare.shortestpaths.core.api.generics.PathGenerics;

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
public interface PathFactory<P extends PathGenerics<E, V, W> , E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> {
	P createPath(W totalWeight, List<E> edges);
}