package com.programmerare.shortestpaths.adapter.yanqi.generics;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import com.programmerare.edu.asu.emit.algorithm.graph.EdgeYanQi;
import com.programmerare.edu.asu.emit.algorithm.graph.GraphPossibleToCreateProgrammatically;
import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;
import com.programmerare.shortestpaths.core.api.generics.EdgeGenerics;
import com.programmerare.shortestpaths.core.api.generics.GraphGenerics;
import com.programmerare.shortestpaths.core.api.generics.PathFinderGenerics;
import com.programmerare.shortestpaths.core.api.generics.PathGenerics;
import com.programmerare.shortestpaths.core.impl.generics.PathFinderBase;
import com.programmerare.shortestpaths.core.pathfactories.PathFactory;
import com.programmerare.shortestpaths.utils.MapperForIntegerIdsAndGeneralStringIds;

import edu.asu.emit.algorithm.graph.abstraction.BaseVertex;
import edu.asu.emit.algorithm.graph.shortestpaths.YenTopKShortestPathsAlg;


/**
 * "Adapter" implementation of the "Target" interface 
 * @author Tomas Johansson
 * @see https://en.wikipedia.org/wiki/Adapter_pattern
 */
public class PathFinderYanQiGenerics 
	< 
		P extends PathGenerics<E, V, W>,  
		E extends EdgeGenerics<V, W>, 
		V extends Vertex, 
		W extends Weight
	>
	extends PathFinderBase<P,  E, V, W> 
	implements PathFinderGenerics<P, E, V, W>
{ 
	private final edu.asu.emit.algorithm.graph.Graph graphAdaptee;
	private final MapperForIntegerIdsAndGeneralStringIds idMapper;

	protected PathFinderYanQiGenerics(
		final GraphGenerics<E, V, W> graph 
	) {
		this(
			graph, 
			null				
		);
	}
	protected PathFinderYanQiGenerics(
		final GraphGenerics<E, V, W> graph, 
		final PathFactory<P, E, V, W> pathFactory
	) {
		super(graph, pathFactory);

		final MapperForIntegerIdsAndGeneralStringIds idMapper = MapperForIntegerIdsAndGeneralStringIds.createIdMapper(0);
		final List<EdgeYanQi> vertices = createListOfVerticesWhileAlsoPopulatingIdMapper(idMapper);
		
		// "Adaptee" https://en.wikipedia.org/wiki/Adapter_pattern		
		this.graphAdaptee = new GraphPossibleToCreateProgrammatically(
			idMapper.getNumberOfVertices(),
			vertices
		);
		this.idMapper = idMapper;
	}
	
	private List<EdgeYanQi> createListOfVerticesWhileAlsoPopulatingIdMapper(final MapperForIntegerIdsAndGeneralStringIds idMapper) {
		final List<E> edges = this.getGraph().getEdges();
		final List<EdgeYanQi> vertices = new ArrayList<EdgeYanQi>();
		for (final E edge : edges) {
			final int integerIdForStartVertex = idMapper.createOrRetrieveIntegerId(edge.getStartVertex().getVertexId());
			final int integerIdForEndVertex = idMapper.createOrRetrieveIntegerId(edge.getEndVertex().getVertexId());
			vertices.add(new EdgeYanQi(integerIdForStartVertex, integerIdForEndVertex, edge.getEdgeWeight().getWeightValue()));
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
	@Override
	protected List<P> findShortestPathHook(
		final V startVertex, 
		final V endVertex, 
		final int maxNumberOfPaths
	) {
		final List<P> paths = new ArrayList<P>();
		final int startVertexId = idMapper.createOrRetrieveIntegerId(startVertex.getVertexId());
		final int endVertexId = idMapper.createOrRetrieveIntegerId(endVertex.getVertexId());
		final YenTopKShortestPathsAlg yenAlg = new YenTopKShortestPathsAlg(graphAdaptee, graphAdaptee.getVertex(startVertexId), graphAdaptee.getVertex(endVertexId));
		while(yenAlg.hasNext()) {
			final edu.asu.emit.algorithm.graph.Path path = yenAlg.next();
			final List<E> edges = new ArrayList<E>();
			final List<BaseVertex> vertexList = path.getVertexList();
			for (int i = 1; i < vertexList.size(); i++) {
				final BaseVertex startVertexForEdge = vertexList.get(i-1);
				final BaseVertex endVertexForEdge = vertexList.get(i);
				final E edge = getOriginalEdgeInstance(startVertexForEdge, endVertexForEdge); 
				edges.add(
					edge
				);				
			}
			final W totalWeight = super.createInstanceWithTotalWeight(path.getWeight(), edges);			
			paths.add(super.createPath(totalWeight, edges));
			if(maxNumberOfPaths == paths.size()) {
				break;
			}
		}
		return Collections.unmodifiableList(paths);
	}



	private E getOriginalEdgeInstance(final BaseVertex startVertexForEdge, final BaseVertex endVertexForEdge) {
		final String startVertexId = idMapper.getBackThePreviouslyStoredGeneralStringIdForInteger(startVertexForEdge.getId());
		final String endVertexId = idMapper.getBackThePreviouslyStoredGeneralStringIdForInteger(endVertexForEdge.getId());		
		return super.getOriginalEdgeInstance(startVertexId, endVertexId);
	}
}