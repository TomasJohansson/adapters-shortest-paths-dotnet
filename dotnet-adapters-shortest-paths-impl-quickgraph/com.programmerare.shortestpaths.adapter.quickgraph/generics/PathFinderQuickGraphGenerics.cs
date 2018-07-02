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

/*
// https://github.com/YaccConstructor/QuickGraph
using QuickGraph;
//using QuickGraph.Algorithms.ShortestPath.Yen;
// When "YC.QuickGraph 3.7.3" was added through NuGet, 
// the following packages were also added:
//Successfully installed 'DotNet.Contracts 1.10.10126.1' to dotnet-adapters-shortest-paths-impl-quickgraph
//Successfully installed 'DotParser 1.0.6' to dotnet-adapters-shortest-paths-impl-quickgraph
//Successfully installed 'FSharp.Core 2.0.0' to dotnet-adapters-shortest-paths-impl-quickgraph
//Successfully installed 'FSharpx.Collections.Experimental 1.7.3' to dotnet-adapters-shortest-paths-impl-quickgraph
//Successfully installed 'FSharpx.Core 1.7.3' to dotnet-adapters-shortest-paths-impl-quickgraph
//Successfully installed 'YC.QuickGraph 3.7.3' to dotnet-adapters-shortest-paths-impl-quickgraph

namespace com.programmerare.shortestpaths.adapter.quickgraph.generics
{
    public class PathFinderQuickGraphGenerics <P, E, V, W>
	    : PathFinderBase<P, E, V, W>
	    , PathFinderGenerics<P, E, V, W>

        where P : PathGenerics<E, V, W>
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    { 
	    private readonly AdjacencyGraph<string, TaggedEquatableEdge<string,double>> graphAdaptee;
	    private readonly MapperForIntegerIdsAndGeneralStringIds idMapper;

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
		    MapperForIntegerIdsAndGeneralStringIds idMapper = MapperForIntegerIdsAndGeneralStringIds.CreateIdMapper(0);
		    
      //      IList<EdgeYanQi> vertices = createListOfVerticesWhileAlsoPopulatingIdMapper(idMapper);
		    //// "Adaptee" https://en.wikipedia.org/wiki/Adapter_pattern
		    //this.graphAdaptee = new GraphPossibleToCreateProgrammatically(
			   // idMapper.getNumberOfVertices(),
			   // vertices
		    //);
            // "Adaptee" https://en.wikipedia.org/wiki/Adapter_pattern
            this.graphAdaptee = new AdjacencyGraph<string, TaggedEquatableEdge<string,double>>(false);
            IList<string> verticesAsListOfStrings = graph.Vertices.Select(v => v.VertexId).ToList();
            this.graphAdaptee.AddVertexRange(verticesAsListOfStrings);
            var edges = graph.Edges;
            foreach(E e in edges)
            {
                this.graphAdaptee.AddEdge(new TaggedEquatableEdge<string, double>(e.StartVertex.VertexId, e.EndVertex.VertexId, e.EdgeWeight.WeightValue));
            }

		    this.idMapper = idMapper;
	    }
	
	    //private IList<EdgeYanQi> createListOfVerticesWhileAlsoPopulatingIdMapper(MapperForIntegerIdsAndGeneralStringIds idMapper) {
		   // IList<E> edges = this.getGraph().getEdges();
		   // IList<EdgeYanQi> vertices = new List<EdgeYanQi>();
		   // foreach (E edge in edges) {
			  //  int integerIdForStartVertex = idMapper.createOrRetrieveIntegerId(edge.getStartVertex().getVertexId());
			  //  int integerIdForEndVertex = idMapper.createOrRetrieveIntegerId(edge.getEndVertex().getVertexId());
			  //  vertices.Add(new EdgeYanQi(integerIdForStartVertex, integerIdForEndVertex, edge.getEdgeWeight().getWeightValue()));
		   // }
		   // return vertices;
	    //}

	    protected override IList<P> FindShortestPathHook(
		    V startVertex, 
		    V endVertex, 
		    int maxNumberOfPaths
	    ) {
            throw new System.Exception();
      //      var yen = new YenShortestPathsAlgorithm<string>(this.graphAdaptee, startVertex.VertexId, endVertex.VertexId, maxNumberOfPaths);

		    //IList<P> paths = new List<P>();
		    ////int startVertexId = idMapper.createOrRetrieveIntegerId(startVertex.getVertexId());
		    ////int endVertexId = idMapper.createOrRetrieveIntegerId(endVertex.getVertexId());
		    
      //      var result = yen.Execute().ToList();

      //      var pathEnumerator = result.GetEnumerator();
		    //while(pathEnumerator.MoveNext()) {
      //          IEnumerable<TaggedEquatableEdge<string, double>> path = pathEnumerator.Current;
      //          var edgesEnumerator = path.GetEnumerator();
      //          IList<E> edges = new List<E>();
      //          double pathWeight = 0;
      //          while(edgesEnumerator.MoveNext()) {
      //              TaggedEquatableEdge<string, double> edgeAdaptee = edgesEnumerator.Current;
      //              E edge = GetOriginalEdgeInstance(edgeAdaptee.Source, edgeAdaptee.Target);
      //              edges.Add(
      //                  edge
      //              );	
      //              pathWeight += edgeAdaptee.Tag;
      //          }
      //          // TODO kanske
      //          W totalWeight = base.CreateInstanceWithTotalWeight(pathWeight, edges);
      //          paths.Add(base.CreatePath(totalWeight, edges));
      //          if (maxNumberOfPaths == paths.Count)
      //          {
      //              break;
      //          }
      //      }
		    //return new ReadOnlyCollection<P>(paths);
	    }

	    //private E getOriginalEdgeInstance(BaseVertex startVertexForEdge, BaseVertex endVertexForEdge) {
		   // string startVertexId = idMapper.getBackThePreviouslyStoredGeneralStringIdForInteger(startVertexForEdge.getId());
		   // string endVertexId = idMapper.getBackThePreviouslyStoredGeneralStringIdForInteger(endVertexForEdge.getId());		
		   // return base.getOriginalEdgeInstance(startVertexId, endVertexId);
	    //}
    }
}
*/