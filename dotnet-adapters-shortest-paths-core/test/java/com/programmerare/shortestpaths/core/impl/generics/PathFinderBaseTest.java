/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.core.impl.generics;

import static com.programmerare.shortestpaths.core.impl.generics.PathGenericsImpl.createPathGenerics;
import static com.programmerare.shortestpaths.core.impl.WeightImpl.createWeight;
import static com.programmerare.shortestpaths.core.impl.generics.GraphGenericsImpl.createGraphGenerics;
import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNotNull;

import java.util.Arrays;
import java.util.List;

import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;

import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;
import com.programmerare.shortestpaths.core.api.generics.EdgeGenerics;
import com.programmerare.shortestpaths.core.api.generics.GraphGenerics;
import com.programmerare.shortestpaths.core.api.generics.PathGenerics;
import com.programmerare.shortestpaths.core.parsers.EdgeParser;
import com.programmerare.shortestpaths.core.validation.GraphEdgesValidationDesired;
import com.programmerare.shortestpaths.core.validation.GraphValidationException;

public class PathFinderBaseTest {

	private GraphGenerics<EdgeGenerics<Vertex,Weight>,Vertex,Weight> graph;
	private List<PathGenerics<EdgeGenerics<Vertex,Weight>,Vertex,Weight>> pathWithAllEdgesBeingPartOfTheGraph;
	private List<PathGenerics<EdgeGenerics<Vertex,Weight>,Vertex,Weight>> pathWithAllEdgesNOTbeingPartOfTheGraph;
	
	private final static String NEWLINE = " \r\n";
	
	@Test
	public void testCreateWeightInstance() {
		
		PathFinderConcrete<PathGenerics<EdgeGenerics<Vertex,Weight>,Vertex,Weight>, EdgeGenerics<Vertex,Weight>,Vertex,Weight> pathFinderConcrete = new PathFinderConcrete<PathGenerics<EdgeGenerics<Vertex,Weight>,Vertex,Weight> , EdgeGenerics<Vertex,Weight>,Vertex,Weight>(graph, GraphEdgesValidationDesired.YES);
		// TDOO: refactor duplicated creations as above
		
		List<EdgeGenerics<Vertex, Weight>> edges = graph.getEdges();
		Weight weightForFirstEdge = edges.get(0).getEdgeWeight();
		
		Weight createdWeightInstance = pathFinderConcrete.createInstanceWithTotalWeight(12.456, edges);
		assertNotNull(createdWeightInstance);
		assertEquals(weightForFirstEdge.getClass(), createdWeightInstance.getClass());
		assertEquals(12.456, createdWeightInstance.getWeightValue(), 0.0001);
	}

	@Before
	public void setUp() throws Exception {
		final EdgeParser<EdgeGenerics<Vertex, Weight>, Vertex, Weight> edgeParser = EdgeParser.createEdgeParserGenerics();
		final List<EdgeGenerics<Vertex,Weight>> edges = edgeParser.fromMultiLinedStringToListOfEdges(
				"A B 5" + NEWLINE +  
				"B C 6" + NEWLINE +
				"C D 7" + NEWLINE +
				"D E 8" + NEWLINE);
		graph = createGraphGenerics(edges);	
		
		final List<EdgeGenerics<Vertex,Weight>> edgeForPath1 = edgeParser.fromMultiLinedStringToListOfEdges(
				"A B 5" + NEWLINE +  
				"B C 7" + NEWLINE);
		PathGenerics<EdgeGenerics<Vertex,Weight>, Vertex, Weight> path1 = createPathGenerics(createWeight(1234), edgeForPath1);
		
		final List<EdgeGenerics<Vertex,Weight>> edgeForPath2 = edgeParser.fromMultiLinedStringToListOfEdges(
				"B C 5" + NEWLINE +  
				"C D 7" + NEWLINE);
		PathGenerics<EdgeGenerics<Vertex,Weight>, Vertex, Weight>  path2 = createPathGenerics(createWeight(1234), edgeForPath2);

		final List<EdgeGenerics<Vertex,Weight>> edgeForPath3 = edgeParser.fromMultiLinedStringToListOfEdges(
				"A B 5" + NEWLINE +  
				"E F 7" + NEWLINE); // NOT part of the graph
		PathGenerics<EdgeGenerics<Vertex,Weight>, Vertex, Weight>  path3 = createPathGenerics(createWeight(1234), edgeForPath3);
		
		pathWithAllEdgesBeingPartOfTheGraph = Arrays.asList(path1, path2);
		pathWithAllEdgesNOTbeingPartOfTheGraph = Arrays.asList(path2, path3);
	}

	
	@Test
	public void validateThatAllEdgesInAllPathsArePartOfTheGraph_should_NOT_throw_exception() {
		//Path<Edge<Vertex,Weight>, Vertex, Weight> 
		PathFinderConcrete<PathGenerics<EdgeGenerics<Vertex,Weight>,Vertex,Weight>, EdgeGenerics<Vertex,Weight>,Vertex,Weight> pathFinderConcrete = new PathFinderConcrete<PathGenerics<EdgeGenerics<Vertex,Weight>,Vertex,Weight>, EdgeGenerics<Vertex,Weight>,Vertex,Weight>(graph, GraphEdgesValidationDesired.YES);
		pathFinderConcrete.validateThatAllEdgesInAllPathsArePartOfTheGraph(this.pathWithAllEdgesBeingPartOfTheGraph);
	}
	
	@Test(expected = GraphValidationException.class)
	public void validateThatAllEdgesInAllPathsArePartOfTheGraph_SHOULD_throw_exception() {
		PathFinderConcrete<PathGenerics<EdgeGenerics<Vertex,Weight>,Vertex,Weight>, EdgeGenerics<Vertex,Weight>,Vertex,Weight> pathFinderConcrete = new PathFinderConcrete<PathGenerics<EdgeGenerics<Vertex,Weight>,Vertex,Weight>, EdgeGenerics<Vertex,Weight>,Vertex,Weight>(graph, GraphEdgesValidationDesired.YES);
		pathFinderConcrete.validateThatAllEdgesInAllPathsArePartOfTheGraph(this.pathWithAllEdgesNOTbeingPartOfTheGraph);
	}

	// TODO: refactor duplication ... the same etst class as below is duplicated in another test class file
	public final class PathFinderConcrete<P extends PathGenerics<E, V, W> ,  E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> extends PathFinderBase<P, E, V, W> {
		protected PathFinderConcrete(GraphGenerics<E, V, W> graph, GraphEdgesValidationDesired graphEdgesValidationDesired) {
			super(graph);
		}

		@Override
		protected List<P> findShortestPathHook(V startVertex, V endVertex, int maxNumberOfPaths) {
			return null;
		}
	}
}