/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in this "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using Programmerare.ShortestPaths.Core.Api;
using Programmerare.ShortestPaths.Core.Api.Generics;
using Programmerare.ShortestPaths.Core.Impl.Generics;
using System.Collections.Generic;

namespace Programmerare.ShortestPaths.Core.PathFactories
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