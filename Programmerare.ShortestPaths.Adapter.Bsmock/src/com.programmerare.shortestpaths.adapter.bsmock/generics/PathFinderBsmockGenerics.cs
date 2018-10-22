/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* Regarding the license (Apache), please find more information 
* in the file "LICENSE_NOTICE.txt" in the project root directory 
* and also license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using edu.ufl.cise.bsmock.graph.ksp;
using Programmerare.ShortestPaths.Core.Api;
using Programmerare.ShortestPaths.Core.Impl.Generics;
using Programmerare.ShortestPaths.Core.PathFactories;
using Programmerare.ShortestPaths.Core.Api.Generics;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Programmerare.ShortestPaths.Adapter.Bsmock.Generics
{
    /// <summary>
    /// "Adapter" implementation of the "Target" interface 
    /// https://en.wikipedia.org/wiki/Adapter_pattern
    /// </summary>
    /// <typeparam name="P">Path</typeparam>
    /// <typeparam name="E">Edge</typeparam>
    /// <typeparam name="V">Vertex</typeparam>
    /// <typeparam name="W">Weight</typeparam>
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
			    this.graphAdaptee.AddEdge(
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
		    IList<edu.ufl.cise.bsmock.graph.util.Path> pathList = yenAlgorithm.Ksp(
			    graphAdaptee, 
			    startVertex.VertexId,
			    endVertex.VertexId,
			    maxNumberOfPaths
		    );
		    foreach(edu.ufl.cise.bsmock.graph.util.Path path in pathList) {
			    java.util.LinkedList<edu.ufl.cise.bsmock.graph.Edge> listOfEdges = path.GetEdges();
			    IList<E> edges = new System.Collections.Generic.List<E>();
			    foreach(edu.ufl.cise.bsmock.graph.Edge edgeAdaptee in listOfEdges) {
				    E edge = GetOriginalEdgeInstance(edgeAdaptee);
				    edges.Add(
					    edge
				    );				
			    }
			    W totalWeight = base.CreateInstanceWithTotalWeight(path.GetTotalCost(), edges);
			    paths.Add(base.CreatePath(totalWeight, edges));
		    }
            return new ReadOnlyCollection<P>(paths);
	    }

	    private E GetOriginalEdgeInstance(edu.ufl.cise.bsmock.graph.Edge edgeAdaptee) {
		    return base.GetOriginalEdgeInstance(edgeAdaptee.GetFromNode(), edgeAdaptee.GetToNode());
	    }
    }
}