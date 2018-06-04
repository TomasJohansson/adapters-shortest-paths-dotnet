/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.core.impl.generics;

import static com.programmerare.shortestpaths.core.impl.VertexImpl.createVertex;
import static com.programmerare.shortestpaths.core.impl.WeightImpl.createWeight;
import static com.programmerare.shortestpaths.core.impl.generics.EdgeGenericsImpl.createEdgeGenerics;
import static com.programmerare.shortestpaths.core.impl.generics.GraphGenericsImpl.createGraphGenerics;
import static org.hamcrest.CoreMatchers.hasItem;
import static org.hamcrest.MatcherAssert.assertThat;
import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertSame;
import static org.junit.Assert.assertTrue;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import org.junit.Before;
import org.junit.Test;

import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;
import com.programmerare.shortestpaths.core.api.generics.EdgeGenerics;
import com.programmerare.shortestpaths.core.api.generics.GraphGenerics;

public class GraphGenericsImplTest {

	private EdgeGenerics<Vertex,Weight> edge1, edge2;
	
	@Before
	public void setUp() throws Exception {
		edge1 = createEdgeGenerics(createVertex("A"), createVertex("B"), createWeight(123));
		edge2 = createEdgeGenerics(createVertex("B"), createVertex("C"), createWeight(456));		
	}

	@Test
	public void testGetAllEdges() {
		List<EdgeGenerics<Vertex,Weight>> edges = new ArrayList<EdgeGenerics<Vertex,Weight>>();
		edges.add(edge1);
		edges.add(edge2);
		// refactor the above three rows (duplicated)

		GraphGenerics<EdgeGenerics<Vertex,Weight>, Vertex,Weight> graph = createGraphGenerics(edges);
		
		List<EdgeGenerics<Vertex, Weight>> allEdges = graph.getEdges();
		
		assertEquals(2,  allEdges.size());
		assertSame(edge1, allEdges.get(0));
		assertSame(edge2, allEdges.get(1));
	}
	
	@Test
	public void testGetVertices() {
		List<EdgeGenerics<Vertex,Weight>> edges = new ArrayList<EdgeGenerics<Vertex,Weight>>();
		edges.add(createEdgeGenerics(createVertex("A"), createVertex("B"), createWeight(1)));
		edges.add(createEdgeGenerics(createVertex("A"), createVertex("C"), createWeight(2)));
		edges.add(createEdgeGenerics(createVertex("A"), createVertex("D"), createWeight(3)));
		edges.add(createEdgeGenerics(createVertex("B"), createVertex("C"), createWeight(4)));
		edges.add(createEdgeGenerics(createVertex("B"), createVertex("D"), createWeight(5)));
		edges.add(createEdgeGenerics(createVertex("C"), createVertex("D"), createWeight(6)));
		
		GraphGenerics<EdgeGenerics<Vertex,Weight>, Vertex,Weight> graph = createGraphGenerics(edges);
		
		List<Vertex> vertices = graph.getVertices();
		
		List<String> expectedVerticesIds = Arrays.asList("A", "B", "C", "D");
		
		assertEquals(expectedVerticesIds.size(), vertices.size());
		
		// verify that all vertices in all edges is one of the four above
		for (EdgeGenerics<Vertex,Weight> edge : edges) {
			assertThat(expectedVerticesIds, hasItem(edge.getStartVertex().getVertexId()));
			assertThat(expectedVerticesIds, hasItem(edge.getEndVertex().getVertexId()));
		}		
	}
	
	@Test
	public void testContainsVertex() {
		List<EdgeGenerics<Vertex,Weight>> edges = new ArrayList<EdgeGenerics<Vertex,Weight>>();
		edges.add(edge1);
		edges.add(edge2);
		// refactor the above three rows (duplicated)
		
		GraphGenerics<EdgeGenerics<Vertex,Weight>, Vertex,Weight> graph = createGraphGenerics(edges);

		assertTrue(graph.containsVertex(edge1.getStartVertex()));
		assertTrue(graph.containsVertex(edge1.getEndVertex()));
		assertTrue(graph.containsVertex(edge2.getStartVertex()));
		assertTrue(graph.containsVertex(edge2.getEndVertex()));
		
		Vertex vertex = createVertex("QWERTY");
		assertFalse(graph.containsVertex(vertex));
	}
	// TODO: refactor some code duplicated above and below i.e. put some code in setup method
	@Test
	public void testContainsEdge() {
		List<EdgeGenerics<Vertex,Weight>> edges = new ArrayList<EdgeGenerics<Vertex,Weight>>();
		edges.add(edge1);
		edges.add(edge2);
		// refactor the above three rows (duplicated)
		
		GraphGenerics<EdgeGenerics<Vertex,Weight>, Vertex,Weight> graph = createGraphGenerics(edges);		

		assertTrue(graph.containsEdge(edge1));
		assertTrue(graph.containsEdge(edge2));
		
		EdgeGenerics<Vertex,Weight> edgeNotInTheGraph = createEdgeGenerics(createVertex("XYZ"), createVertex("QWERTY"), createWeight(987));
		assertFalse(graph.containsEdge(edgeNotInTheGraph));
	}	
}