/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/

using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.api.generics;
using com.programmerare.shortestpaths.core.validation;
using System.Collections.Generic;

namespace com.programmerare.shortestpaths.core.impl.generics
{
public abstract class PathFinderFactoryGenericsBase<F, P, E, V, W> 
    : PathFinderFactoryGenerics<F, P, E, V, W> 
	where F : PathFinderGenerics<P, E, V, W>
    where P : PathGenerics<E, V, W>
    where E : EdgeGenerics<V, W>
    where V : Vertex
    where W : Weight
{
	protected PathFinderFactoryGenericsBase() { }

	/**
	 * @param edges list of edges
	 * @param graphEdgesValidationDesired should be NO (for performance reason) if validation has already been done
	 * @return an instance of a PathFinderGenerics implementation
	 */
	public F createPathFinder(
		IList<E> edges, 
		GraphEdgesValidationDesired graphEdgesValidationDesired
	)
    {
		GraphGenerics<E, V, W> graph = GraphGenericsImpl<E, V, W>.createGraphGenerics<E, V, W>(edges, graphEdgesValidationDesired);
		return createPathFinder(graph); // the overloaded method must be implemented by subclasses
	}

    abstract public F createPathFinder(
		GraphGenerics<E, V, W> graph 
	);
}
}