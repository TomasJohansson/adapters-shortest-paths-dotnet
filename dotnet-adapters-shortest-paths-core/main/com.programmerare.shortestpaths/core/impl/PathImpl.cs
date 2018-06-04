/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.core.impl;

import java.util.List;

import com.programmerare.shortestpaths.core.api.Edge;
import com.programmerare.shortestpaths.core.api.Path;
import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;
import com.programmerare.shortestpaths.core.impl.generics.PathGenericsImpl;

public class PathImpl extends PathGenericsImpl<Edge , Vertex , Weight> implements Path {

	protected PathImpl(final Weight totalWeight, final List<Edge> edges) {
		super(totalWeight, edges);
	}

	public static Path createPath(final Weight totalWeight, final List<Edge> edges) {
		return new PathImpl(totalWeight, edges);
	}

}
