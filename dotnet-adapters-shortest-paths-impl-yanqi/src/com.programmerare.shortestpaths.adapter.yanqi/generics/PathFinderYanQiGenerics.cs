/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* Regarding the license (Apache), please find more information 
* in the file "LICENSE_NOTICE.txt" in the project root directory 
* and also license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using edu.asu.emit.algorithm.graph.abstraction;
using edu.asu.emit.algorithm.graph.shortestpaths;
using com.programmerare.edu.asu.emit.algorithm.graph;
using Programmerare.ShortestPaths.Core.Api;
using Programmerare.ShortestPaths.Core.Impl.Generics;
using Programmerare.ShortestPaths.Core.PathFactories;
using Programmerare.ShortestPaths.Utils;
using Programmerare.ShortestPaths.Core.Api.Generics;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Programmerare.ShortestPaths.Adapter.YanQi.Generics
{
    /// <summary>
    /// "Adapter" implementation of the "Target" interface 
    /// https://en.wikipedia.org/wiki/Adapter_pattern
    /// </summary>
    /// <typeparam name="P">Path</typeparam>
    /// <typeparam name="E">Edge</typeparam>
    /// <typeparam name="V">Vertex</typeparam>
    /// <typeparam name="W">Weight</typeparam>
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

        /// <summary>
        /// Note that currently there seem to be no way for the "yanqi" implementation to actually specify the number 
	    /// of shortest paths to return, but it seems to find all paths.
	    /// However, the implementation of this method instead takes care of reducing the result.
	    /// Otherwise, if the semantic of the method is not respected it can not for example be tested 
	    /// against results from other implementations since then they would return a different number of paths.
        /// </summary>
        /// <param name="startVertex"></param>
        /// <param name="endVertex"></param>
        /// <param name="maxNumberOfPaths"></param>
        /// <returns></returns>
	    protected override IList<P> FindShortestPathHook(
		    V startVertex, 
		    V endVertex, 
		    int maxNumberOfPaths
	    ) {
		    IList<P> paths = new List<P>();
		    int startVertexId = idMapper.CreateOrRetrieveIntegerId(startVertex.VertexId);
		    int endVertexId = idMapper.CreateOrRetrieveIntegerId(endVertex.VertexId);
		    YenTopKShortestPathsAlg yenAlg = new YenTopKShortestPathsAlg(graphAdaptee, graphAdaptee.GetVertex(startVertexId), graphAdaptee.GetVertex(endVertexId));
		    while(yenAlg.HasNext()) {
			    global::edu.asu.emit.algorithm.graph.Path path = yenAlg.Next();
			    IList<E> edges = new List<E>();
			    java.util.List<BaseVertex> vertexList = path.GetVertexList();
			    for (int i = 1; i < vertexList.size(); i++) {
				    BaseVertex startVertexForEdge = vertexList.get(i-1);
				    BaseVertex endVertexForEdge = vertexList.get(i);
				    E edge = GetOriginalEdgeInstance(startVertexForEdge, endVertexForEdge); 
				    edges.Add(
					    edge
				    );				
			    }
			    W totalWeight = base.CreateInstanceWithTotalWeight(path.GetWeight(), edges);
			    paths.Add(base.CreatePath(totalWeight, edges));
			    if(maxNumberOfPaths == paths.Count) {
				    break;
			    }
		    }
		    return new ReadOnlyCollection<P>(paths);
	    }

	    private E GetOriginalEdgeInstance(BaseVertex startVertexForEdge, BaseVertex endVertexForEdge) {
		    string startVertexId = idMapper.GetBackThePreviouslyStoredGeneralStringIdForInteger(startVertexForEdge.GetId());
		    string endVertexId = idMapper.GetBackThePreviouslyStoredGeneralStringIdForInteger(endVertexForEdge.GetId());		
		    return base.GetOriginalEdgeInstance(startVertexId, endVertexId);
	    }
    }
}