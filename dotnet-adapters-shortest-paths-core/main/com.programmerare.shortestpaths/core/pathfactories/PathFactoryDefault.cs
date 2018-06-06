/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.impl;
using System.Collections.Generic;

namespace com.programmerare.shortestpaths.core.pathfactories
{
    public sealed class PathFactoryDefault
	    : PathFactory<Path , Edge , Vertex , Weight>
    {
	    public Path CreatePath(Weight totalWeight, IList<Edge> edges) {
		    Path pathDefault = PathImpl.CreatePath((Weight)totalWeight, edges);
		    return pathDefault;
	    }
    }
}