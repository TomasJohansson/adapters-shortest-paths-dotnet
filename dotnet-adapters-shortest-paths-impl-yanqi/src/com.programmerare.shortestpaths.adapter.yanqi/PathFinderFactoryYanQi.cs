using com.programmerare.shortestpaths.adapter.yanqi.generics;
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.api.generics;

namespace com.programmerare.shortestpaths.adapter.yanqi
{
    public class PathFinderFactoryYanQi 
	    : PathFinderFactoryYanQiGenerics<
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
		    return new PathFinderYanQi(graph);
	    }	
    }
}