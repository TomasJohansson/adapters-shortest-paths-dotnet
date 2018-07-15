/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* Regarding the license (Apache), please find more information 
* in the file "LICENSE_NOTICE.txt" in the project root directory 
* and also license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using com.programmerare.shortestpaths.adapter.bsmock.generics;
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.api.generics;

namespace com.programmerare.shortestpaths.adapter.bsmock
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