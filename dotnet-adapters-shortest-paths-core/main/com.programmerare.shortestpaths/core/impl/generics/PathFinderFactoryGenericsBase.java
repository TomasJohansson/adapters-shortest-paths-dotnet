/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.core.impl.generics;

import static com.programmerare.shortestpaths.core.impl.generics.GraphGenericsImpl.createGraphGenerics;

import java.util.List;

import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;
import com.programmerare.shortestpaths.core.api.generics.EdgeGenerics;
import com.programmerare.shortestpaths.core.api.generics.GraphGenerics;
import com.programmerare.shortestpaths.core.api.generics.PathFinderFactoryGenerics;
import com.programmerare.shortestpaths.core.api.generics.PathFinderGenerics;
import com.programmerare.shortestpaths.core.api.generics.PathGenerics;
import com.programmerare.shortestpaths.core.validation.GraphEdgesValidationDesired;

public abstract class PathFinderFactoryGenericsBase
	<F extends PathFinderGenerics<P, E,V,W> , P extends PathGenerics<E, V, W> ,  E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> 
	implements PathFinderFactoryGenerics<F , P, E , V , W> 
{
	protected PathFinderFactoryGenericsBase() { }

	/**
	 * @param edges list of edges
	 * @param graphEdgesValidationDesired should be NO (for performance reason) if validation has already been done
	 * @return an instance of a PathFinderGenerics implementation
	 */
	public final F createPathFinder(
		final List<E> edges, 
		final GraphEdgesValidationDesired graphEdgesValidationDesired
	) {
		final GraphGenerics<E, V, W> graph = createGraphGenerics(edges, graphEdgesValidationDesired);
		return createPathFinder(graph); // the overloaded method must be implemented by subclasses
	}
}