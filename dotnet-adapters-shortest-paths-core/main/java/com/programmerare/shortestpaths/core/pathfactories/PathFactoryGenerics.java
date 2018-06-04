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
import com.programmerare.shortestpaths.core.impl.generics.PathGenericsImpl;

public final class PathFactoryGenerics<P extends PathGenerics<E, V, W> , E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> 
	implements PathFactory<P, E, V, W>
{
	public P createPath(final W totalWeight, final List<E> edges) {
		final PathGenerics<E, V, W> path = PathGenericsImpl.createPathGenerics(totalWeight, edges);
		final P p = (P) path;
		// System.out.println("PathFactory created PathFactoryGenerics " + path.getClass());
		return p;
	}
}