using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.impl.generics;
using com.programmerare.shortestpaths.core.pathfactories;
using com.programmerare.shortestpaths.utils;
using com.programmerare.shortestpaths.core.api.generics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

// https://github.com/YaccConstructor/QuickGraph
using QuickGraph;
using QuickGraph.Algorithms.ShortestPath.Yen;
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
		    MapperForIntegerIdsAndGeneralStringIds idMapper = MapperForIntegerIdsAndGeneralStringIds.createIdMapper(0);
		    
      //      IList<EdgeYanQi> vertices = createListOfVerticesWhileAlsoPopulatingIdMapper(idMapper);
		    //// "Adaptee" https://en.wikipedia.org/wiki/Adapter_pattern
		    //this.graphAdaptee = new GraphPossibleToCreateProgrammatically(
			   // idMapper.getNumberOfVertices(),
			   // vertices
		    //);
            // "Adaptee" https://en.wikipedia.org/wiki/Adapter_pattern
            this.graphAdaptee = new AdjacencyGraph<string, TaggedEquatableEdge<string,double>>(false);
            IList<string> verticesAsListOfStrings = graph.getVertices().Select(v => v.getVertexId()).ToList();
            this.graphAdaptee.AddVertexRange(verticesAsListOfStrings);
            var edges = graph.getEdges();
            foreach(E e in edges)
            {
                this.graphAdaptee.AddEdge(new TaggedEquatableEdge<string, double>(e.getStartVertex().getVertexId(), e.getEndVertex().getVertexId(), e.getEdgeWeight().getWeightValue()));
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

	    /**
	     * Note that currently there seem to be no way for the "yanqi" implementation to actually specify the number 
	     * of shortest paths to return, but it seems to find all paths.
	     * However, the implementation of this method instead takes care of reducing the result.
	     * Otherwise, if the semantic of the method is not respected it can not for example be tested 
	     * against results from other implementations since then they would return a different number of paths.      
	     */
	    protected override IList<P> findShortestPathHook(
		    V startVertex, 
		    V endVertex, 
		    int maxNumberOfPaths
	    ) {
            var yen = new YenShortestPathsAlgorithm<string>(this.graphAdaptee, startVertex.getVertexId(), endVertex.getVertexId(), maxNumberOfPaths);

		    IList<P> paths = new List<P>();
		    //int startVertexId = idMapper.createOrRetrieveIntegerId(startVertex.getVertexId());
		    //int endVertexId = idMapper.createOrRetrieveIntegerId(endVertex.getVertexId());
		    
            var result = yen.Execute().ToList();

            var pathEnumerator = result.GetEnumerator();
		    while(pathEnumerator.MoveNext()) {
                IEnumerable<TaggedEquatableEdge<string, double>> path = pathEnumerator.Current;
                var edgesEnumerator = path.GetEnumerator();
                IList<E> edges = new List<E>();
                double pathWeight = 0;
                while(edgesEnumerator.MoveNext()) {
                    TaggedEquatableEdge<string, double> edgeAdaptee = edgesEnumerator.Current;
                    E edge = getOriginalEdgeInstance(edgeAdaptee.Source, edgeAdaptee.Target);
                    edges.Add(
                        edge
                    );	
                    pathWeight += edgeAdaptee.Tag;
                }
                // TODO kanske
                W totalWeight = base.createInstanceWithTotalWeight(pathWeight, edges);
                paths.Add(base.createPath(totalWeight, edges));
                if (maxNumberOfPaths == paths.Count)
                {
                    break;
                }
            }
		    return new ReadOnlyCollection<P>(paths);
	    }

	    //private E getOriginalEdgeInstance(BaseVertex startVertexForEdge, BaseVertex endVertexForEdge) {
		   // string startVertexId = idMapper.getBackThePreviouslyStoredGeneralStringIdForInteger(startVertexForEdge.getId());
		   // string endVertexId = idMapper.getBackThePreviouslyStoredGeneralStringIdForInteger(endVertexForEdge.getId());		
		   // return base.getOriginalEdgeInstance(startVertexId, endVertexId);
	    //}
    }
}