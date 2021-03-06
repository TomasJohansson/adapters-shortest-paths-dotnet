/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* Regarding the license (Apache), please find more information 
* in the file "LICENSE_NOTICE.txt" in the project root directory 
* and also license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using Programmerare.ShortestPaths.Adapter.Bsmock.Generics;
using Programmerare.ShortestPaths.Core.Api;
using Programmerare.ShortestPaths.Core.Api.Generics;

namespace Programmerare.ShortestPaths.Adapter.Bsmock
{
    public class PathFinderFactoryBsmock 
	    : PathFinderFactoryBsmockGenerics<
			    PathFinder, // PathFinder< Edge<Vertex , Weight> , Vertex , Weight> ,
			    Path,
			    Edge, // Edge<Vertex , Weight> ,  
			    Vertex,
			    Weight
		    >	
	    , PathFinderFactory 
    {
	    public override PathFinder CreatePathFinder(
		    GraphGenerics<Edge, Vertex, Weight> graph
	    ) {
		    return new PathFinderBsmock(graph);
	    }	
    }
}