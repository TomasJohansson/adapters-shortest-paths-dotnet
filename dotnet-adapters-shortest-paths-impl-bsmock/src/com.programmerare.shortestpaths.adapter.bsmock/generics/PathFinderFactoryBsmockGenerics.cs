/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* Regarding the license (Apache), please find more information 
* in the file "LICENSE_NOTICE.txt" in the project root directory 
* and also license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.api.generics;
using com.programmerare.shortestpaths.core.impl.generics;

namespace com.programmerare.shortestpaths.adapter.bsmock.generics
{
    public class PathFinderFactoryBsmockGenerics<F, P, E, V, W>
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
            PathFinderGenerics<P, E, V, W> pathFinder =  new PathFinderBsmockGenerics<P, E, V, W>(graph);
            // TODO: try to get rid of the casting below ( warning: "Type safety: Unchecked cast from PathFinderYanQi<P,E,V,W> to F" )
            return (F) pathFinder;
        }
    }
}