package com.programmerare.shortestpaths.adapter.bsmock.generics;

import java.util.ArrayList;
import java.util.Collections;
import java.util.LinkedList;
import java.util.List;

import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;
import com.programmerare.shortestpaths.core.api.generics.EdgeGenerics;
import com.programmerare.shortestpaths.core.api.generics.GraphGenerics;
import com.programmerare.shortestpaths.core.api.generics.PathFinderGenerics;
import com.programmerare.shortestpaths.core.api.generics.PathGenerics;
import com.programmerare.shortestpaths.core.impl.generics.PathFinderBase;
import com.programmerare.shortestpaths.core.pathfactories.PathFactory;

import edu.ufl.cise.bsmock.graph.ksp.Yen;

/**
 * "Adapter" implementation of the "Target" interface 
 * @author Tomas Johansson
 * @see https://en.wikipedia.org/wiki/Adapter_pattern
 */
public class PathFinderBsmockGenerics 
	< 
		P extends PathGenerics<E, V, W>,  
		E extends EdgeGenerics<V, W>, 
		V extends Vertex, 
		W extends Weight
	>
	extends PathFinderBase<P,  E, V, W> 
	implements PathFinderGenerics<P, E, V, W>
{
	private final edu.ufl.cise.bsmock.graph.Graph graphAdaptee;
	private final Yen yenAlgorithm;	
	
	protected PathFinderBsmockGenerics(
		final GraphGenerics<E, V, W> graph 
	) {
		this(
			graph, 
			null				
		);
	}
	protected PathFinderBsmockGenerics(
		final GraphGenerics<E, V, W> graph, 
		final PathFactory<P, E, V, W> pathFactory
	) {
		super(graph, pathFactory);
		this.yenAlgorithm = new Yen();
		this.graphAdaptee = new edu.ufl.cise.bsmock.graph.Graph();
		populateGraphAdapteeWithEdges();
	}
	
	private void populateGraphAdapteeWithEdges() {
		final List<E> edges = this.getGraph().getEdges();
		for (final E edge : edges) {
			this.graphAdaptee.addEdge(
				edge.getStartVertex().getVertexId(), 
				edge.getEndVertex().getVertexId(), 
				edge.getEdgeWeight().getWeightValue()
			);	
		}
	}

	@Override
	protected List<P> findShortestPathHook(
		final V startVertex, 
		final V endVertex, 
		final int maxNumberOfPaths
	) {
		final List<P> paths = new ArrayList<P>();
		
		final List<edu.ufl.cise.bsmock.graph.util.Path> pathList = yenAlgorithm.ksp(
			graphAdaptee, 
			startVertex.getVertexId(), 
			endVertex.getVertexId(), 
			maxNumberOfPaths
		);
		for (edu.ufl.cise.bsmock.graph.util.Path path : pathList) {
			final LinkedList<edu.ufl.cise.bsmock.graph.Edge> listOfEdges = path.getEdges();
			final List<E> edges = new ArrayList<E>();
			for (edu.ufl.cise.bsmock.graph.Edge edgeAdaptee : listOfEdges) {
				final E edge = getOriginalEdgeInstance(edgeAdaptee);
				edges.add(
					edge
				);				
			}
			final W totalWeight = super.createInstanceWithTotalWeight(path.getTotalCost(), edges);
			paths.add(super.createPath(totalWeight, edges));
		}
		return Collections.unmodifiableList(paths);
	}

	private E getOriginalEdgeInstance(final edu.ufl.cise.bsmock.graph.Edge edgeAdaptee) {
		return super.getOriginalEdgeInstance(edgeAdaptee.getFromNode(), edgeAdaptee.getToNode());
	}

}