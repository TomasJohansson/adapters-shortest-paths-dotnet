// This version is based on 3.6.6
// and regarding QuickGraph versions, see comments in "QuickGraph_3_6_6_Test.cs"

using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.impl.generics;
using com.programmerare.shortestpaths.core.pathfactories;
using com.programmerare.shortestpaths.core.api.generics;
using System.Collections.Generic;
using System.Linq;
using QuickGraph.Algorithms.RankedShortestPath;
using QuickGraph;
using System.Collections.ObjectModel;

namespace com.programmerare.shortestpaths.adapter.quickgraph.generics
{
    /**
     * "Adapter" implementation of the "Target" interface 
     * @author Tomas Johansson
     * @see https://en.wikipedia.org/wiki/Adapter_pattern
     */
    public class PathFinderQuickGraphGenerics <P, E, V, W>
	    : PathFinderBase<P, E, V, W>
	    , PathFinderGenerics<P, E, V, W>

        where P : PathGenerics<E, V, W>
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    { 
	    private BidirectionalGraph<string, Edge<string>> graphAdaptee;
        private Dictionary<Edge<string>, double> edgeWeightsForGraphAdaptee;

	    public PathFinderQuickGraphGenerics(
		    GraphGenerics<E, V, W> graph 
	    ): 	this(
			    graph, 
			    null				
		    ) 
        {
	    }
	    protected PathFinderQuickGraphGenerics(
		    GraphGenerics<E, V, W> graph, 
		    PathFactory<P, E, V, W> pathFactory
	    ): base(graph, pathFactory) {
            // "Adaptee" https://en.wikipedia.org/wiki/Adapter_pattern
            this.graphAdaptee = new BidirectionalGraph<string, Edge<string>>();
            this.edgeWeightsForGraphAdaptee = new Dictionary<Edge<string>, double>();

            IList<string> verticesAsListOfStrings = graph.Vertices.Select(v => v.VertexId).ToList();
            foreach(string vertex in verticesAsListOfStrings)
            {
                graphAdaptee.AddVertex(vertex);
            }
            var edges = graph.Edges;
            foreach(E e in edges)
            {
                var edge = new Edge<string>(e.StartVertex.VertexId, e.EndVertex.VertexId);
                graphAdaptee.AddEdge(edge);
                edgeWeightsForGraphAdaptee.Add(edge, e.EdgeWeight.WeightValue);
            }
	    }

	    protected override IList<P> FindShortestPathHook(
		    V startVertex, 
		    V endVertex, 
		    int maxNumberOfPaths
	    ) {
            var target = new HoffmanPavleyRankedShortestPathAlgorithm<string, Edge<string>>(this.graphAdaptee, e => this.edgeWeightsForGraphAdaptee[e]);
            target.ShortestPathCount = this.graphAdaptee.VertexCount;
            target.Compute(startVertex.VertexId, endVertex.VertexId);
            var quickGraphPaths = target.ComputedShortestPaths.ToList();
		    IList<P> paths = new List<P>();
		    foreach (var quickGraphPath in quickGraphPaths) {
                IList<E> edges = new List<E>();
                double pathWeight = Enumerable.Sum(quickGraphPath, e => this.edgeWeightsForGraphAdaptee[e]);
                var quickGraphEdges = quickGraphPath.ToList();
                foreach (var quickGraphEdge in quickGraphEdges) {
                    E edge = GetOriginalEdgeInstance(quickGraphEdge.Source, quickGraphEdge.Target);
                    edges.Add(
                        edge
                    );	
                }
                W totalWeight = base.CreateInstanceWithTotalWeight(pathWeight, edges);
                paths.Add(base.CreatePath(totalWeight, edges));
            }
		    return new ReadOnlyCollection<P>(paths);
	    }
    }
}