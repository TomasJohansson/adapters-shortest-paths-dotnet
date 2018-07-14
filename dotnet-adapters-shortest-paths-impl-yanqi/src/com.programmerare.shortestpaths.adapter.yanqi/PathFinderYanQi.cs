using com.programmerare.shortestpaths.adapter.yanqi.generics;
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.api.generics;
using com.programmerare.shortestpaths.core.pathfactories;

namespace com.programmerare.shortestpaths.adapter.yanqi
{
    public class PathFinderYanQi 
	    : PathFinderYanQiGenerics<
		    Path,
		    Edge, // Edge<Vertex, Weight> 
		    Vertex , 
		    Weight
	    >
	    , PathFinder
    {
	    public PathFinderYanQi(
		    GraphGenerics<Edge, Vertex, Weight> graph
	    ): base(graph, new PathFactoryDefault()) {
		    
	    }
    }
}