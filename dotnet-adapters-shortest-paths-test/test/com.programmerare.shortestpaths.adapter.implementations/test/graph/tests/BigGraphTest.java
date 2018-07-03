package com.programmerare.shortestpaths.graph.tests;

import static com.programmerare.shortestpaths.core.impl.EdgeImpl.createEdge;
import static com.programmerare.shortestpaths.core.impl.VertexImpl.createVertex;
import static com.programmerare.shortestpaths.core.impl.WeightImpl.createWeight;
import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertTrue;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import org.junit.Test;

import com.programmerare.shortestpaths.core.api.Edge;
import com.programmerare.shortestpaths.core.api.PathFinderFactory;
import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.graph.utils.GraphShortestPathAssertionHelper;
import com.programmerare.shortestpaths.graph.utils.PathFinderFactories;


/**
 * This test class can generate a big graph, for example 1000 nodes (and then some more edges)
 * and how many of the best paths that should be find, and it searches for paths 
 * with all implementations (currently three) and verifies them against each other,
 * i.e. if all different independent implementations in the same result, you should be able to 
 * feel pretty sure that the result is correct.
 * 
 * It can be run either as a junit test or through the main method.
 * The difference is that when running as "main method program" then there 
 * will be output printed to the console window to display the time it takes for the different implementations.
 * 
 * @author Tomas Johansson
 */
public class BigGraphTest {

	private boolean isExecutingThroughTheMainMethod = false;
	
	public static void main(String[] args) throws IOException {
		BigGraphTest implementationComparisonTest = new BigGraphTest();
		implementationComparisonTest.isExecutingThroughTheMainMethod = true;

		final int numberOfVertices = 500;
		final int numberOfPathsToFind = 50;
		implementationComparisonTest.testProgrammaticallyGeneratedGraph(
			numberOfVertices,
			numberOfPathsToFind				
		);
		// resulting oputput with 500 and 50 on my machine:
		// seconds : 0 for implementation com.programmerare.shortestpaths.adapter.impl.yanqi.GraphYanQi
		// seconds : 1 for implementation com.programmerare.shortestpaths.adapter.impl.bsmock.GraphBsmock
		// seconds : 50 for implementation com.programmerare.shortestpaths.adapter.impl.jgrapht.GraphJgrapht		
	}


	@Test
	public void testProgrammaticallyGeneratedGraph() throws IOException {	
		final int numberOfVertices = 200;
		final int numberOfPathsToFind = 20;
		testProgrammaticallyGeneratedGraph(
			numberOfVertices,
			numberOfPathsToFind				
		);
	}
	
	private void testProgrammaticallyGeneratedGraph(
		final int numberOfVertices, 
		final int numberOfPathsToFind
	) throws IOException {
		final List<Edge> edgesForBigGraph = createEdgesForBigGraph(numberOfVertices);
		final Vertex startVertex = edgesForBigGraph.get(0).getStartVertex();
		final Vertex endVertex = edgesForBigGraph.get(edgesForBigGraph.size()-1).getEndVertex();
		final List<PathFinderFactory> pathFinderFactories = PathFinderFactories.createPathFinderFactories();
		
		GraphShortestPathAssertionHelper graphShortestPathAssertionHelper = new GraphShortestPathAssertionHelper(isExecutingThroughTheMainMethod);
		graphShortestPathAssertionHelper.testResultsWithImplementationsAgainstEachOther(edgesForBigGraph, startVertex, endVertex, numberOfPathsToFind, pathFinderFactories);
	}

	private List<Edge> createEdgesForBigGraph(final int numberOfVertices) {
		final List<Vertex> vertices = createVertices(numberOfVertices);
		assertEquals(numberOfVertices, vertices.size());
		
		double decreasingWeightValue = 10 * numberOfVertices;
		
		final List<Edge> edges = new ArrayList<Edge>();
		
		for(int i=0; i<numberOfVertices-1; i++) {
			edges.add(createEdge(vertices.get(i), vertices.get(i+1), createWeight(--decreasingWeightValue)));
		}
		assertEquals(numberOfVertices-1, edges.size());
				
		for(int i=0; i<numberOfVertices-10; i+=10) {
			edges.add(createEdge(vertices.get(i), vertices.get(i+10), createWeight(--decreasingWeightValue)));
		}
		
		for(int i=0; i<numberOfVertices-100; i+=100) {
			decreasingWeightValue--;
			edges.add(createEdge(vertices.get(i), vertices.get(i+100), createWeight(--decreasingWeightValue)));
		}		

		// now construct a really short path with the smallest value as each weight and only a few edges from start vertex to end vertex
		final int numberOfVerticesToSkip = numberOfVertices / 4;
		// totally five vertices including start and end i.e. three between 
		final Vertex secondVertex = vertices.get(numberOfVerticesToSkip);
		final Vertex thirdVertex =  vertices.get(numberOfVerticesToSkip * 2);
		final Vertex fourthVertex =  vertices.get(numberOfVerticesToSkip * 3);
		
		final Vertex startVertex = vertices.get(0);
		final Vertex endVertex = vertices.get(numberOfVertices-1);
		
		edges.add(createEdge(startVertex, secondVertex, createWeight(--decreasingWeightValue)));
		edges.add(createEdge(secondVertex, thirdVertex, createWeight(--decreasingWeightValue)));
		edges.add(createEdge(thirdVertex, fourthVertex, createWeight(--decreasingWeightValue)));
		edges.add(createEdge(fourthVertex, endVertex, createWeight(--decreasingWeightValue)));
		
		assertTrue(decreasingWeightValue > 0); 
		
		return edges;
	}

	private List<Vertex> createVertices(int numberOfVerticesToCreate) {
		final List<Vertex> vertices = new ArrayList<Vertex>();
		for(int i=1; i<=numberOfVerticesToCreate; i++) {
			vertices.add(createVertex(i));
		}
		return vertices;
	}

}