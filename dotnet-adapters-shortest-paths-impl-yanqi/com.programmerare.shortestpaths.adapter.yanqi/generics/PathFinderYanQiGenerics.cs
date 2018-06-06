using edu.asu.emit.algorithm.graph.abstraction;
using edu.asu.emit.algorithm.graph.shortestpaths;
using com.programmerare.edu.asu.emit.algorithm.graph;
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.impl.generics;
using com.programmerare.shortestpaths.core.pathfactories;
using com.programmerare.shortestpaths.utils;
using com.programmerare.shortestpaths.core.api.generics;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace com.programmerare.shortestpaths.adapter.yanqi.generics
{
    /**
     * "Adapter" implementation of the "Target" interface 
     * @author Tomas Johansson
     * @see https://en.wikipedia.org/wiki/Adapter_pattern
     */
    public class PathFinderYanQiGenerics <P, E, V, W>
	    : PathFinderBase<P, E, V, W>
	    , PathFinderGenerics<P, E, V, W>

        where P : PathGenerics<E, V, W>
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    { 
	    private readonly global::edu.asu.emit.algorithm.graph.Graph graphAdaptee;
	    private readonly MapperForIntegerIdsAndGeneralStringIds idMapper;

	    public PathFinderYanQiGenerics(
		    GraphGenerics<E, V, W> graph 
	    ): 	this(
			    graph, 
			    null				
		    ) 
        {
	    }
	    protected PathFinderYanQiGenerics(
		    GraphGenerics<E, V, W> graph, 
		    PathFactory<P, E, V, W> pathFactory
	    ): base(graph, pathFactory) {
		    MapperForIntegerIdsAndGeneralStringIds idMapper = MapperForIntegerIdsAndGeneralStringIds.CreateIdMapper(0);
		    IList<EdgeYanQi> vertices = CreateListOfVerticesWhileAlsoPopulatingIdMapper(idMapper);
		
		    // "Adaptee" https://en.wikipedia.org/wiki/Adapter_pattern		
		    this.graphAdaptee = new GraphPossibleToCreateProgrammatically(
			    idMapper.GetNumberOfVertices(),
			    vertices
		    );
		    this.idMapper = idMapper;
	    }
	
	    private IList<EdgeYanQi> CreateListOfVerticesWhileAlsoPopulatingIdMapper(MapperForIntegerIdsAndGeneralStringIds idMapper) {
		    IList<E> edges = this.GetGraph().Edges;
		    IList<EdgeYanQi> vertices = new List<EdgeYanQi>();
		    foreach (E edge in edges) {
			    int integerIdForStartVertex = idMapper.CreateOrRetrieveIntegerId(edge.StartVertex.VertexId);
			    int integerIdForEndVertex = idMapper.CreateOrRetrieveIntegerId(edge.EndVertex.VertexId);
			    vertices.Add(new EdgeYanQi(integerIdForStartVertex, integerIdForEndVertex, edge.EdgeWeight.WeightValue));
		    }
		    return vertices;
	    }

	    /**
	     * Note that currently there seem to be no way for the "yanqi" implementation to actually specify the number 
	     * of shortest paths to return, but it seems to find all paths.
	     * However, the implementation of this method instead takes care of reducing the result.
	     * Otherwise, if the semantic of the method is not respected it can not for example be tested 
	     * against results from other implementations since then they would return a different number of paths.      
	     */
	    protected override IList<P> FindShortestPathHook(
		    V startVertex, 
		    V endVertex, 
		    int maxNumberOfPaths
	    ) {
		    IList<P> paths = new List<P>();
		    int startVertexId = idMapper.CreateOrRetrieveIntegerId(startVertex.VertexId);
		    int endVertexId = idMapper.CreateOrRetrieveIntegerId(endVertex.VertexId);
		    YenTopKShortestPathsAlg yenAlg = new YenTopKShortestPathsAlg(graphAdaptee, graphAdaptee.getVertex(startVertexId), graphAdaptee.getVertex(endVertexId));
		    while(yenAlg.hasNext()) {
			    global::edu.asu.emit.algorithm.graph.Path path = yenAlg.next();
			    IList<E> edges = new List<E>();
			    java.util.List<BaseVertex> vertexList = path.getVertexList();
			    for (int i = 1; i < vertexList.size(); i++) {
				    BaseVertex startVertexForEdge = vertexList.get(i-1);
				    BaseVertex endVertexForEdge = vertexList.get(i);
				    E edge = GetOriginalEdgeInstance(startVertexForEdge, endVertexForEdge); 
				    edges.Add(
					    edge
				    );				
			    }
			    W totalWeight = base.CreateInstanceWithTotalWeight(path.getWeight(), edges);
			    paths.Add(base.CreatePath(totalWeight, edges));
			    if(maxNumberOfPaths == paths.Count) {
				    break;
			    }
		    }
		    return new ReadOnlyCollection<P>(paths);
	    }

	    private E GetOriginalEdgeInstance(BaseVertex startVertexForEdge, BaseVertex endVertexForEdge) {
		    string startVertexId = idMapper.GetBackThePreviouslyStoredGeneralStringIdForInteger(startVertexForEdge.getId());
		    string endVertexId = idMapper.GetBackThePreviouslyStoredGeneralStringIdForInteger(endVertexForEdge.getId());		
		    return base.GetOriginalEdgeInstance(startVertexId, endVertexId);
	    }
    }
}