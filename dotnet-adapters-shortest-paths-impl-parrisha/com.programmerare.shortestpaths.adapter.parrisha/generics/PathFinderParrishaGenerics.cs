using com.programmerare.yen.parrisha;
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.impl.generics;
using com.programmerare.shortestpaths.core.pathfactories;
using com.programmerare.shortestpaths.utils;
using com.programmerare.shortestpaths.core.api.generics;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace com.programmerare.shortestpaths.adapter.parrisha.generics
{
    /**
     * "Adapter" implementation of the "Target" interface 
     * @author Tomas Johansson
     * @see https://en.wikipedia.org/wiki/Adapter_pattern
     */
    public class PathFinderParrishaGenerics <P, E, V, W>
	    : PathFinderBase<P, E, V, W>
	    , PathFinderGenerics<P, E, V, W>

        where P : PathGenerics<E, V, W>
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    { 
        private readonly IList<Node> graphAdaptee;
	    private readonly MapperForIntegerIdsAndGeneralStringIds idMapper;
        private readonly IDictionary<int, Node> nodesDictionary = new Dictionary<int, Node>();

	    public PathFinderParrishaGenerics(
		    GraphGenerics<E, V, W> graph 
	    ): 	this(
			    graph, 
			    null				
		    ) 
        {
	    }
	    protected PathFinderParrishaGenerics(
		    GraphGenerics<E, V, W> graph, 
		    PathFactory<P, E, V, W> pathFactory
	    ): base(graph, pathFactory) {
		    this.idMapper = MapperForIntegerIdsAndGeneralStringIds.CreateIdMapper(0);
		    
            // "Adaptee" https://en.wikipedia.org/wiki/Adapter_pattern		
		    this.graphAdaptee = CreateListOfVerticesWhileAlsoPopulatingIdMapper(idMapper);
	    }
	
	    private IList<Node> CreateListOfVerticesWhileAlsoPopulatingIdMapper(MapperForIntegerIdsAndGeneralStringIds idMapper) {
            var edges = this.GetGraph().Edges;
		    IList<Node> nodes = new List<Node>();
		    foreach (E edge in edges) {
			    int integerIdForStartVertex = idMapper.CreateOrRetrieveIntegerId(edge.StartVertex.VertexId);
			    int integerIdForEndVertex = idMapper.CreateOrRetrieveIntegerId(edge.EndVertex.VertexId);
                Node startNode = GetOrCreateNode(integerIdForStartVertex, nodes);
                Node endNode = GetOrCreateNode(integerIdForEndVertex, nodes);
                startNode.AddEdge(integerIdForEndVertex, edge.EdgeWeight.WeightValue);
		    }
		    return nodes;
	    }

        private Node GetOrCreateNode(int nodeInteger, IList<Node> nodes)
        {
            if(nodesDictionary.ContainsKey(nodeInteger))
            {
                return nodesDictionary[nodeInteger];
            }
            else
            {
                Node node = new Node(nodeInteger);
                nodesDictionary.Add(nodeInteger, node);
                nodes.Add(node);
                return node;
            }
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
            Yen yen = new Yen();
		    IList<P> paths = new List<P>();
		    int startVertexId = idMapper.CreateOrRetrieveIntegerId(startVertex.VertexId);
		    int endVertexId = idMapper.CreateOrRetrieveIntegerId(endVertex.VertexId);
            IList<yen.parrisha.Path> kPaths = yen.YensImpl(graphAdaptee, startVertexId, endVertexId, maxNumberOfPaths);
            foreach(var pa in kPaths) {
			    IList<E> edges = new List<E>();
			    for (int i = 1; i < pa.Nodes.Count; i++) {
				    var startVertexForEdge = pa.Nodes[i-1];
				    var endVertexForEdge = pa.Nodes[i];
				    E edge = GetOriginalEdgeInstance(startVertexForEdge, endVertexForEdge); 
				    edges.Add(
					    edge
				    );				
			    }
			    W totalWeight = base.CreateInstanceWithTotalWeight(pa.GetPathCost(), edges);
			    paths.Add(base.CreatePath(totalWeight, edges));
		    }
		    return new ReadOnlyCollection<P>(paths);
	    }

        private E GetOriginalEdgeInstance(Node startVertexForEdge, Node endVertexForEdge) {
		    string startVertexId = idMapper.GetBackThePreviouslyStoredGeneralStringIdForInteger(startVertexForEdge.Index);
		    string endVertexId = idMapper.GetBackThePreviouslyStoredGeneralStringIdForInteger(endVertexForEdge.Index);
		    return base.GetOriginalEdgeInstance(startVertexId, endVertexId);
	    }
    }
}