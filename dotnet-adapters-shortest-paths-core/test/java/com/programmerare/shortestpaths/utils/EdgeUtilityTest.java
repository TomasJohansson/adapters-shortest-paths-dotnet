/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.utils;

import static com.programmerare.shortestpaths.core.impl.VertexImpl.createVertex;
import static com.programmerare.shortestpaths.core.impl.WeightImpl.createWeight;
import static com.programmerare.shortestpaths.core.impl.generics.EdgeGenericsImpl.createEdgeGenerics;
import static org.junit.Assert.assertEquals;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import org.junit.Before;
import org.junit.Test;

import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;
import com.programmerare.shortestpaths.core.api.generics.EdgeGenerics;
import com.programmerare.shortestpaths.utils.EdgeUtility.SelectionStrategyWhenEdgesAreDuplicated;

public class EdgeUtilityTest {

	EdgeUtility<EdgeGenerics<Vertex,Weight>, Vertex, Weight> edgeUtility;
	private final static String A = "A";
	private final static String B = "B";
	private final static String C = "C";
	private final static String D = "D";
	
	private final static double weightBaseValueFor_A_B = 1;
	private final static double weightBaseValueFor_B_C = 2;
	private final static double weightBaseValueFor_C_D = 3;
	
	
	private List<EdgeGenerics<Vertex, Weight>> edges = new ArrayList<EdgeGenerics<Vertex, Weight>>();
	// there will be 12 edges, 3 (A_B , B_C , C_D) for each multiplier value below
	
	private final static List<Integer> multipliers = Arrays.asList(10, 1, 1000, 100);
	
	private final static int numberOfEdgesPerMultiplier = 3;

	// the indeces below refer to the list of edges cnostructed in the setup method by iterating the multiplier
	private final static int startIndexForFirst = 0;
	private final static int startIndexForSmallest = startIndexForFirst + numberOfEdgesPerMultiplier;
	private final static int startIndexForLargest = startIndexForSmallest + numberOfEdgesPerMultiplier;
	private final static int startIndexForLast = startIndexForLargest + numberOfEdgesPerMultiplier;
	
	@Before
	public void setUp() throws Exception {
		edgeUtility = EdgeUtility.create();
		edges = new ArrayList<EdgeGenerics<Vertex, Weight>>();
		for (Integer multiplier : multipliers) {
			edges.add(createEdgeGenerics(createVertex(A), createVertex(B), createWeight(weightBaseValueFor_A_B * multiplier)));
			edges.add(createEdgeGenerics(createVertex(B), createVertex(C), createWeight(weightBaseValueFor_B_C * multiplier)));
			edges.add(createEdgeGenerics(createVertex(C), createVertex(D), createWeight(weightBaseValueFor_C_D * multiplier)));
		}
		assertEquals(3 * multipliers.size(), edges.size());
	}

	@Test
	public void testGetEdgesWithoutDuplicates_Selecting_First() {
		List<EdgeGenerics<Vertex, Weight>> result = edgeUtility.getEdgesWithoutDuplicates(edges, SelectionStrategyWhenEdgesAreDuplicated.FIRST_IN_LIST_OF_EDGES);
		assertResult(result, startIndexForFirst);
	}

	@Test
	public void testGetEdgesWithoutDuplicates_Selecting_Last() {
		List<EdgeGenerics<Vertex, Weight>> result = edgeUtility.getEdgesWithoutDuplicates(edges, SelectionStrategyWhenEdgesAreDuplicated.LAST_IN_LIST_OF_EDGES);
		assertResult(result, startIndexForLast);
	}

	@Test
	public void testGetEdgesWithoutDuplicates_Selecting_Smallest() {
		List<EdgeGenerics<Vertex, Weight>> result = edgeUtility.getEdgesWithoutDuplicates(edges, SelectionStrategyWhenEdgesAreDuplicated.SMALLEST_WEIGHT);
		assertResult(result, startIndexForSmallest);
	}
	
	@Test
	public void testGetEdgesWithoutDuplicates_Selecting_Largest() {
		List<EdgeGenerics<Vertex, Weight>> result = edgeUtility.getEdgesWithoutDuplicates(edges, SelectionStrategyWhenEdgesAreDuplicated.LARGEST_WEIGHT);
		assertResult(result, startIndexForLargest);
	}

	private void assertResult(List<EdgeGenerics<Vertex, Weight>> result, int startIndex) {
		assertEquals(3, result.size());		
		assertEquals(edges.get(startIndex++), result.get(0));
		assertEquals(edges.get(startIndex++), result.get(1));
		assertEquals(edges.get(startIndex), result.get(2));
	}	
}