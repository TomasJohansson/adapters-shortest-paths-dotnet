/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.core.api;

import com.programmerare.shortestpaths.core.api.generics.PathFinderGenerics;

/**
 * @see PathFinderGenerics
 * @author Tomas Johansson
 */
public interface PathFinder 
	extends PathFinderGenerics<
		Path,
		Edge,  // Edge<Vertex , Weight>
		Vertex , 
		Weight
	>
{}