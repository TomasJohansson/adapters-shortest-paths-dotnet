/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* Regarding the license (Apache), please find more information 
* in the file "LICENSE_NOTICE.txt" in the project root directory 
* and also license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using Programmerare.ShortestPaths.Adapter.QuikGraph.Generics;
using Programmerare.ShortestPaths.Core.Api;
using Programmerare.ShortestPaths.Core.Api.Generics;
using Programmerare.ShortestPaths.Core.PathFactories;

namespace Programmerare.ShortestPaths.Adapter.QuikGraph
{
    public class PathFinderQuikGraph
        : PathFinderQuikGraphGenerics<
            Path,
            Edge, // Edge<Vertex, Weight> 
            Vertex,
            Weight
        >
        , PathFinder
    {
        public PathFinderQuikGraph(
            GraphGenerics<Edge, Vertex, Weight> graph
        ) : base(graph, new PathFactoryDefault())
        {

        }
    }
}