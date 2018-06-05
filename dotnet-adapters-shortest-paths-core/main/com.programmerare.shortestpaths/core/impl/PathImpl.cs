/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.impl.generics;
using System.Collections.Generic;

namespace com.programmerare.shortestpaths.core.impl
{
    public class PathImpl : PathGenericsImpl<Edge, Vertex, Weight> , Path 
    {

	    protected PathImpl(Weight totalWeight, IList<Edge> edges): base(totalWeight, edges) {
		
	    }

	    public static Path createPath(Weight totalWeight, IList<Edge> edges) {
		    return new PathImpl(totalWeight, edges);
	    }

    }
}