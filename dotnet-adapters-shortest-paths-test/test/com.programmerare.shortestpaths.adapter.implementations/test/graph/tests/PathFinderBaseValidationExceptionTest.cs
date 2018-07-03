package com.programmerare.shortestpaths.graph.tests;

import static com.programmerare.shortestpaths.core.impl.EdgeImpl.createEdge;
import static com.programmerare.shortestpaths.core.impl.VertexImpl.createVertex;
import static com.programmerare.shortestpaths.core.impl.WeightImpl.createWeight;

import java.util.ArrayList;
import java.util.List;

import org.junit.Before;
import org.junit.Test;

import com.programmerare.shortestpaths.adapter.bsmock.PathFinderFactoryBsmock;
import com.programmerare.shortestpaths.adapter.jgrapht.PathFinderFactoryJgrapht;
import com.programmerare.shortestpaths.adapter.yanqi.PathFinderFactoryYanQi;
import com.programmerare.shortestpaths.core.api.Edge;
import com.programmerare.shortestpaths.core.api.Path;
import com.programmerare.shortestpaths.core.api.PathFinder;
import com.programmerare.shortestpaths.core.api.PathFinderFactory;
import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.validation.GraphEdgesValidationDesired;
import com.programmerare.shortestpaths.core.validation.GraphValidationException;

/**
 * PathFinderBase is an abstract base class, and this test class verifies that the subclasses throw validation exceptions implemented in the base class. 
 * @author Tomas Johansson
 */
public class PathFinderBaseValidationExceptionTest {

	private Edge edgeAB, edgeBC;
	private List<Edge> edges_A_B_and_B_C;
	private Vertex vertexA, vertexB, vertexC, vertexX_notPartOfGraph;
	
	@Before
	public void setUp() throws Exception {
		vertexA = createVertex("A");
		vertexB = createVertex("B");
		vertexC = createVertex("C");
		
		vertexX_notPartOfGraph = createVertex("X");
		
		edgeAB = createEdge(vertexA, vertexB, createWeight(123));
		edgeBC = createEdge(vertexB, vertexC, createWeight(456));
		
		edges_A_B_and_B_C = new ArrayList<Edge>();
		edges_A_B_and_B_C.add(edgeAB);
		edges_A_B_and_B_C.add(edgeBC);
	}

	
	// -------------------------------------------------------------
	// Three tests (for three implementations) with start vertex not part of the graph
	@Test(expected = GraphValidationException.class)
	public void incorrect_startVertex_shouldThrowException_Bsmock() {
		shouldThrowExceptionIfAnyOfTheVerticesIsNotPartOfTheGraph(new PathFinderFactoryBsmock(), vertexX_notPartOfGraph, vertexC);
	}	
	@Test(expected = GraphValidationException.class)
	public void incorrect_startVertex_shouldThrowException_Jgrapht() {
		shouldThrowExceptionIfAnyOfTheVerticesIsNotPartOfTheGraph(new PathFinderFactoryJgrapht(), vertexX_notPartOfGraph, vertexC);
	}
	@Test(expected = GraphValidationException.class)
	public void incorrect_startVertex_shouldThrowException_YanQi() {
		shouldThrowExceptionIfAnyOfTheVerticesIsNotPartOfTheGraph(new PathFinderFactoryYanQi(), vertexX_notPartOfGraph, vertexC);
	}
	
	
	// -------------------------------------------------------------
	// Three tests (for three implementations) with end vertex not part of the graph 
	@Test(expected = GraphValidationException.class)
	public void incorrect_endVertex_shouldThrowException_Bsmock() {
		shouldThrowExceptionIfAnyOfTheVerticesIsNotPartOfTheGraph(new PathFinderFactoryBsmock(), vertexA, vertexX_notPartOfGraph);
	}	
	@Test(expected = GraphValidationException.class)
	public void incorrect_endVertex_shouldThrowException_Jgrapht() {
		shouldThrowExceptionIfAnyOfTheVerticesIsNotPartOfTheGraph(new PathFinderFactoryJgrapht(), vertexA, vertexX_notPartOfGraph);
	}
	@Test(expected = GraphValidationException.class)
	public void incorrect_endVertex_shouldThrowException_YanQi() {
		shouldThrowExceptionIfAnyOfTheVerticesIsNotPartOfTheGraph(new PathFinderFactoryYanQi(), vertexA, vertexX_notPartOfGraph);
	}

	
	// -------------------------------------------------------------
	// Three tests (for three implementations) with start and vertex both part of the graph
	// The purpose of the test is simply to show that these do not throw an exception (as the other tests do) and thus no assertions are done about the found paths
	@Test
	public void correct_startAndEndVertex_should_NOT_ThrowException_Bsmock() {
		shouldThrowExceptionIfAnyOfTheVerticesIsNotPartOfTheGraph(new PathFinderFactoryBsmock(), vertexA, vertexC);
	}	
	@Test
	public void correct_startAndEndVertex_should_NOT_ThrowException_Jgrapht() {
		shouldThrowExceptionIfAnyOfTheVerticesIsNotPartOfTheGraph(new PathFinderFactoryJgrapht(), vertexA, vertexC);
	}
	@Test
	public void correct_startAndEndVertex_should_NOT_ThrowException__YanQi() {
		shouldThrowExceptionIfAnyOfTheVerticesIsNotPartOfTheGraph(new PathFinderFactoryYanQi(), vertexA, vertexC);
	}	
	// -------------------------------------------------------------
	private void shouldThrowExceptionIfAnyOfTheVerticesIsNotPartOfTheGraph(
		PathFinderFactory pathFinderFactory, 
		Vertex startVertex, 
		Vertex endVertex
	) {
		PathFinder pathFinder = pathFinderFactory.createPathFinder(edges_A_B_and_B_C, GraphEdgesValidationDesired.YES);
		List<Path> shortestPaths = pathFinder.findShortestPaths(startVertex, endVertex, maxNumberOfPaths);
	}
	
	private final static int maxNumberOfPaths = 1;
}