/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.core.impl;

import static com.programmerare.shortestpaths.core.impl.VertexImpl.createVertex;
import static com.programmerare.shortestpaths.core.impl.WeightImpl.createWeight;
import static com.programmerare.shortestpaths.core.impl.generics.EdgeGenericsImpl.createEdgeGenerics;

import java.util.Arrays;
import java.util.List;

import org.junit.Before;
import org.junit.Test;

import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;
import com.programmerare.shortestpaths.core.api.generics.EdgeGenerics;
import com.programmerare.shortestpaths.core.api.generics.GraphGenerics;
import com.programmerare.shortestpaths.core.impl.generics.GraphGenericsImpl;
import com.programmerare.shortestpaths.core.validation.GraphEdgesValidationDesired;
import com.programmerare.shortestpaths.core.validation.GraphValidationException;

public class GraphImplTest {

	private List<EdgeGenerics<Vertex,Weight>> edgesForAcceptableGraph;
	private List<EdgeGenerics<Vertex,Weight>> edgesForUnacceptableGraph;

	private GraphGenerics<EdgeGenerics<Vertex, Weight>, Vertex, Weight> graph;
	
	@Before
	public void setUp() throws Exception {
		
		final EdgeGenerics<Vertex,Weight> edge_A_B = createEdgeGenerics(createVertex("A"), createVertex("B"), createWeight(123));
		final EdgeGenerics<Vertex,Weight> edge_B_C = createEdgeGenerics(createVertex("B"), createVertex("C"), createWeight(456));
		edgesForAcceptableGraph = Arrays.asList(edge_A_B, edge_B_C);

		// the same edge (A to B) defined once again is NOT correct
		final EdgeGenerics<Vertex,Weight> edge_A_B_again = createEdgeGenerics(createVertex("A"), createVertex("B"), createWeight(789));
		edgesForUnacceptableGraph = Arrays.asList(edge_A_B, edge_A_B_again);
	}

	@Test(expected = GraphValidationException.class)
	public void testCreateGraph_SHOULD_throw_exception_for_unacceptable_graph_when_validation_REQUIRED() {
		graph = GraphGenericsImpl.createGraphGenerics(edgesForUnacceptableGraph, GraphEdgesValidationDesired.YES);
	}
	
	@Test
	public void testCreateGraph_should_NOT_throw_exception_for_unacceptable_graph_when_validation_NOT_required() {
		graph = GraphGenericsImpl.createGraphGenerics(edgesForUnacceptableGraph, GraphEdgesValidationDesired.NO);
	}	

	@Test
	public void testCreateGraph_should_NOT_throw_exception_for_acceptable_graph() {
		// a bit lazy to do two validation below within the same test method, 
		// but since the graph should be acceptable, no exception should be thrown 
		// regardless if validation is required
		graph = GraphGenericsImpl.createGraphGenerics(edgesForAcceptableGraph, GraphEdgesValidationDesired.NO);
		graph = GraphGenericsImpl.createGraphGenerics(edgesForAcceptableGraph, GraphEdgesValidationDesired.YES);
	}	
}