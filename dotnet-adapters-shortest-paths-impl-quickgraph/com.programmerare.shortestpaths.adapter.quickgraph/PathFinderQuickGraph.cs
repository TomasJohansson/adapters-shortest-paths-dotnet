using com.programmerare.shortestpaths.adapter.quickgraph.generics;
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.api.generics;
using com.programmerare.shortestpaths.core.pathfactories;

namespace com.programmerare.shortestpaths.adapter.quickgraph
{
    public class PathFinderQuickGraph 
	    : PathFinderQuickGraphGenerics<
		    Path,
		    Edge, // Edge<Vertex, Weight> 
		    Vertex , 
		    Weight
	    >
	    , PathFinder
    {
	    public PathFinderQuickGraph(
		    GraphGenerics<Edge, Vertex, Weight> graph
	    ): base(graph, new PathFactoryDefault()) {
		    
	    }
    }
}