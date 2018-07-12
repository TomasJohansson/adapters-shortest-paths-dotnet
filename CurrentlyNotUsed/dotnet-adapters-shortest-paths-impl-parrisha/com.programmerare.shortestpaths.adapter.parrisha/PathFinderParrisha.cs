using com.programmerare.shortestpaths.adapter.parrisha.generics;
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.api.generics;
using com.programmerare.shortestpaths.core.pathfactories;

namespace com.programmerare.shortestpaths.adapter.parrisha
{
    public class PathFinderParrisha 
	    : PathFinderParrishaGenerics<
		    Path,
		    Edge, // Edge<Vertex, Weight> 
		    Vertex , 
		    Weight
	    >
	    , PathFinder
    {
	    public PathFinderParrisha(
		    GraphGenerics<Edge, Vertex, Weight> graph
	    ): base(graph, new PathFactoryDefault()) {
		    
	    }
    }
}