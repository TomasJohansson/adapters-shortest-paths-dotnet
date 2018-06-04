/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.core.api;

import com.programmerare.shortestpaths.core.api.generics.GraphGenerics;

/**
 * @see GraphGenerics 
 * @author Tomas Johansson
 */
public interface Graph extends GraphGenerics<Edge, Vertex , Weight> { }