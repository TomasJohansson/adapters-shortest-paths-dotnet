using edu.ufl.cise.bsmock.graph.ksp;
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.impl.generics;
using com.programmerare.shortestpaths.core.pathfactories;
using com.programmerare.shortestpaths.core.api.generics;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace com.programmerare.shortestpaths.adapter.bsmock.generics
{
    /**
     * "Adapter" implementation of the "Target" interface 
     * @author Tomas Johansson
     * @see https://en.wikipedia.org/wiki/Adapter_pattern
     */
    public class PathFinderBsmockGenerics <P, E, V, W>
	    : PathFinderBase<P, E, V, W>
	    , PathFinderGenerics<P, E, V, W>

        where P : PathGenerics<E, V, W>
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    { 
	    private readonly edu.ufl.cise.bsmock.graph.Graph graphAdaptee;
	    private readonly Yen yenAlgorithm;	

	    public PathFinderBsmockGenerics(
		    GraphGenerics<E, V, W> graph 
	    ): 	this(
			    graph, 
			    null				
		    ) 
        {
	    }
	    protected PathFinderBsmockGenerics(
		    GraphGenerics<E, V, W> graph, 
		    PathFactory<P, E, V, W> pathFactory
	    ): base(graph, pathFactory) {
		    this.yenAlgorithm = new Yen();
		    this.graphAdaptee = new edu.ufl.cise.bsmock.graph.Graph();
		    PopulateGraphAdapteeWithEdges();
	    }
	
	    private void PopulateGraphAdapteeWithEdges() {
		    IList<E> edges = this.GetGraph().Edges;
		    foreach(E edge in edges) {
			    this.graphAdaptee.addEdge(
				    edge.StartVertex.VertexId, 
				    edge.EndVertex.VertexId, 
				    edge.EdgeWeight.WeightValue
			    );	
		    }
	    }

	    protected override IList<P> FindShortestPathHook(
		    V startVertex, 
		    V endVertex, 
		    int maxNumberOfPaths
	    ) {
            IList<P> paths = new System.Collections.Generic.List<P>();
		    IList<edu.ufl.cise.bsmock.graph.util.Path> pathList = yenAlgorithm.ksp(
			    graphAdaptee, 
			    startVertex.VertexId,
			    endVertex.VertexId,
			    maxNumberOfPaths
		    );
		    foreach(edu.ufl.cise.bsmock.graph.util.Path path in pathList) {
			    java.util.LinkedList<edu.ufl.cise.bsmock.graph.Edge> listOfEdges = path.getEdges();
			    IList<E> edges = new System.Collections.Generic.List<E>();
			    foreach(edu.ufl.cise.bsmock.graph.Edge edgeAdaptee in listOfEdges) {
				    E edge = GetOriginalEdgeInstance(edgeAdaptee);
				    edges.Add(
					    edge
				    );				
			    }
			    W totalWeight = base.CreateInstanceWithTotalWeight(path.getTotalCost(), edges);
			    paths.Add(base.CreatePath(totalWeight, edges));
		    }
            return new ReadOnlyCollection<P>(paths);
	    }

	    private E GetOriginalEdgeInstance(edu.ufl.cise.bsmock.graph.Edge edgeAdaptee) {
		    return base.GetOriginalEdgeInstance(edgeAdaptee.getFromNode(), edgeAdaptee.getToNode());
	    }
    }
}