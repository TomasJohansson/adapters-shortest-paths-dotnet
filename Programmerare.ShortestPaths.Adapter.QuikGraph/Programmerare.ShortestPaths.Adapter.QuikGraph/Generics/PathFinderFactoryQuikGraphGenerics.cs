/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* Regarding the license (Apache), please find more information 
* in the file "LICENSE_NOTICE.txt" in the project root directory 
* and also license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using Programmerare.ShortestPaths.Core.Api;
using Programmerare.ShortestPaths.Core.Api.Generics;
using Programmerare.ShortestPaths.Core.Impl.Generics;

namespace Programmerare.ShortestPaths.Adapter.QuikGraph.Generics
{
    public class PathFinderFactoryQuikGraphGenerics<F, P, E, V, W>
        : PathFinderFactoryGenericsBase<F, P, E, V, W>
        , PathFinderFactoryGenerics<F, P, E, V, W>
        where F : PathFinderGenerics<P, E, V, W>
        where P : PathGenerics<E, V, W>
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {
        public override F CreatePathFinder(GraphGenerics<E, V, W> graph)
        {
            PathFinderGenerics<P, E, V, W> pathFinder = new PathFinderQuikGraphGenerics<P, E, V, W>(graph);
            return (F)pathFinder;
        }
    }
}