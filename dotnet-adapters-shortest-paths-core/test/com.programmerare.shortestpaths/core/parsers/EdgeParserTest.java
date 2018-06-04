/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.core.parsers;

import static com.programmerare.shortestpaths.core.impl.EdgeImpl.createEdge;
import static com.programmerare.shortestpaths.core.impl.VertexImpl.createVertex;
import static com.programmerare.shortestpaths.core.impl.WeightImpl.SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS;
import static com.programmerare.shortestpaths.core.impl.WeightImpl.createWeight;
import static com.programmerare.shortestpaths.core.impl.generics.EdgeGenericsImpl.createEdgeGenerics;
import static com.programmerare.shortestpaths.core.parsers.EdgeParser.createEdgeParser;
import static org.junit.Assert.*;

import java.util.List;

import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;

import com.programmerare.shortestpaths.core.api.Edge;
import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;
import com.programmerare.shortestpaths.core.api.generics.EdgeGenerics;
import com.programmerare.shortestpaths.core.parsers.EdgeParser;

/**
 * @author Tomas Johansson
 */
public class EdgeParserTest {

	private EdgeParser<EdgeGenerics<Vertex, Weight>, Vertex, Weight> edgeParserGenerics;
	private EdgeParser<Edge, Vertex, Weight> edgeParserDefault;
	
	@Before
	public void setUp() throws Exception {
		edgeParserGenerics = EdgeParser.createEdgeParser(new EdgeParser.EdgeFactoryGenerics<EdgeGenerics<Vertex, Weight>, Vertex, Weight>());
		edgeParserDefault = EdgeParser.createEdgeParser(new EdgeParser.EdgeFactoryDefault());
	}

	@Test
	public void testFromStringToEdge() {
		Edge edge = edgeParserDefault.fromStringToEdge("A B 3.7");
		assertNotNull(edge);
		assertNotNull(edge.getStartVertex());
		assertNotNull(edge.getEndVertex());
		assertNotNull(edge.getEdgeWeight());
		assertEquals("A", edge.getStartVertex().getVertexId());
		assertEquals("B", edge.getEndVertex().getVertexId());
		assertEquals(3.7, edge.getEdgeWeight().getWeightValue(), SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
	}
	// TODO: refactor away duplication from above and below methods
	@Test
	public void testFromStringToEdgeGenerics() {
		EdgeGenerics<Vertex, Weight> edge = edgeParserGenerics.fromStringToEdge("A B 3.7");
		assertNotNull(edge);
		assertNotNull(edge.getStartVertex());
		assertNotNull(edge.getEndVertex());
		assertNotNull(edge.getEdgeWeight());
		assertEquals("A", edge.getStartVertex().getVertexId());
		assertEquals("B", edge.getEndVertex().getVertexId());
		assertEquals(3.7, edge.getEdgeWeight().getWeightValue(), SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
	}

	@Test
	public void testFromEdgeParserGenericsToString() {
		Vertex startVertex = createVertex("A");
		Vertex endVertex = createVertex("B");
		Weight weight = createWeight(3.7);		
		EdgeGenerics<Vertex, Weight> edge = createEdgeGenerics(startVertex, endVertex, weight);
		assertEquals("A B 3.7", edgeParserGenerics.fromEdgeToString(edge));
	}
	// TODO: refactor away duplication from above and below methods	
	@Test
	public void testFromEdgeToString() {
		Vertex startVertex = createVertex("A");
		Vertex endVertex = createVertex("B");
		Weight weight = createWeight(3.7);
		Edge edge = createEdge(startVertex, endVertex, weight);
		assertEquals("A B 3.7", edgeParserDefault.fromEdgeToString(edge));
	}
	
	@Test
	public void testFromMultiLineStringToListOfEdgesGenerics() {
//	    <graphDefinition>
//	    A B 5
//	    A C 6
//	    B C 7
//	    B D 8
//	    C D 9    
//	    </graphDefinition>
		final String multiLinedString = "A B 5\r\n" + 
				"A C 6\r\n" + 
				"B C 7\r\n" + 
				"B D 8\r\n" + 
				"C D 9";
		final List<EdgeGenerics<Vertex, Weight>> edges = edgeParserGenerics.fromMultiLinedStringToListOfEdges(multiLinedString);
		assertNotNull(edges);
		assertEquals(5,  edges.size());
		final EdgeGenerics<Vertex, Weight> firstEdge = edges.get(0);
		final EdgeGenerics<Vertex, Weight> lastEdge = edges.get(4);		
		assertNotNulls(firstEdge);
		assertNotNulls(lastEdge);
		
		assertEquals("A", firstEdge.getStartVertex().getVertexId());
		assertEquals("B", firstEdge.getEndVertex().getVertexId());
		assertEquals(5, firstEdge.getEdgeWeight().getWeightValue(), SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
		
		assertEquals("C", lastEdge.getStartVertex().getVertexId());
		assertEquals("D", lastEdge.getEndVertex().getVertexId());
		assertEquals(9, lastEdge.getEdgeWeight().getWeightValue(), SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
	}
	// TODO: refactor away duplication from above and below methods	
	@Test
	public void testFromMultiLineStringToListOfEdges() {
//	    <graphDefinition>
//	    A B 5
//	    A C 6
//	    B C 7
//	    B D 8
//	    C D 9    
//	    </graphDefinition>
		final String multiLinedString = "A B 5\r\n" + 
				"A C 6\r\n" + 
				"B C 7\r\n" + 
				"B D 8\r\n" + 
				"C D 9";
		final List<Edge> edges = edgeParserDefault.fromMultiLinedStringToListOfEdges(multiLinedString);
		assertNotNull(edges);
		assertEquals(5,  edges.size());
		final EdgeGenerics<Vertex, Weight> firstEdge = edges.get(0);
		final EdgeGenerics<Vertex, Weight> lastEdge = edges.get(4);		
		assertNotNulls(firstEdge);
		assertNotNulls(lastEdge);
		
		assertEquals("A", firstEdge.getStartVertex().getVertexId());
		assertEquals("B", firstEdge.getEndVertex().getVertexId());
		assertEquals(5, firstEdge.getEdgeWeight().getWeightValue(), SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
		
		assertEquals("C", lastEdge.getStartVertex().getVertexId());
		assertEquals("D", lastEdge.getEndVertex().getVertexId());
		assertEquals(9, lastEdge.getEdgeWeight().getWeightValue(), SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
	}
	
	private void assertNotNulls(EdgeGenerics<Vertex, Weight> edge) {
		assertNotNull(edge);
		assertNotNull(edge.getStartVertex());
		assertNotNull(edge.getEndVertex());
		assertNotNull(edge.getEdgeWeight());
	}
	
}