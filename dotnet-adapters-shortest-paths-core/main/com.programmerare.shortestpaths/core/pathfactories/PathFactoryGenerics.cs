/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.api.generics;
using com.programmerare.shortestpaths.core.impl.generics;
using System.Collections.Generic;

namespace com.programmerare.shortestpaths.core.pathfactories
{
    public sealed class PathFactoryGenerics<P, E, V, W> : PathFactory<P, E, V, W>
        where P : PathGenerics<E, V, W>
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {
	    public P CreatePath(W totalWeight, IList<E> edges) {
		    PathGenerics<E, V, W> path = PathGenericsImpl<E, V, W>.CreatePathGenerics<E, V, W>(totalWeight, edges);
		    P p = (P) path;
		    return p;
	    }
    }
}