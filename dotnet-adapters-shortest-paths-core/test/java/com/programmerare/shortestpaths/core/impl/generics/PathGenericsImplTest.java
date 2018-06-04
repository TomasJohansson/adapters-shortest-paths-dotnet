/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.core.impl.generics;

import static com.programmerare.shortestpaths.core.impl.PathImpl.createPath;
import static com.programmerare.shortestpaths.core.impl.EdgeImpl.createEdge;
import static com.programmerare.shortestpaths.core.impl.VertexImpl.createVertex;
import static com.programmerare.shortestpaths.core.impl.WeightImpl.SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS;
import static com.programmerare.shortestpaths.core.impl.WeightImpl.createWeight;
import static com.programmerare.shortestpaths.core.impl.generics.PathGenericsImpl.createPathGenerics;
import static org.junit.Assert.assertEquals;

import java.util.Arrays;
import java.util.List;

import org.junit.Before;
import org.junit.Test;

import com.programmerare.shortestpaths.core.api.Edge;
import com.programmerare.shortestpaths.core.api.Path;
import com.programmerare.shortestpaths.core.api.generics.EdgeGenerics;

/**
 * @author Tomas Johansson
 */
public class PathGenericsImplTest {

	private Edge edgeAB3;
	private Edge edgeBC5;
	private Edge edgeCD7;
	private String firstVertex, secondVertex, thirdVertex, fourthVertex;
	private double weightFirstEdge, weightSecondEdge, weightThirdEdge, totalWeight;
	private Path path; 
			
	@Before
	public void setUp() throws Exception {
		firstVertex = "A";
		secondVertex = "B";
		thirdVertex = "C";
		fourthVertex = "D";
	
		weightFirstEdge = 3;
		weightSecondEdge = 5;
		weightThirdEdge = 7;
		totalWeight = weightFirstEdge + weightSecondEdge + weightThirdEdge;
		
		edgeAB3 = createEdge(createVertex(firstVertex), createVertex(secondVertex), createWeight(weightFirstEdge));
		edgeBC5 = createEdge(createVertex(secondVertex), createVertex(thirdVertex), createWeight(weightSecondEdge));
		edgeCD7 = createEdge(createVertex(thirdVertex), createVertex(fourthVertex), createWeight(weightThirdEdge));
		
		path = createPath(createWeight(totalWeight), Arrays.asList(edgeAB3, edgeBC5, edgeCD7));
	}

	@Test
	public void testGetTotalWeightForPath() {
		assertEquals(totalWeight, path.getTotalWeightForPath().getWeightValue(), SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
	}

	@Test
	public void testGetEdgesForPath() {
		List<? extends EdgeGenerics> edgesForPath = path.getEdgesForPath();
		assertEquals(3, edgesForPath.size());
		assertEquals(edgeAB3, edgesForPath.get(0));
		assertEquals(edgeBC5, edgesForPath.get(1));
		assertEquals(edgeCD7, edgesForPath.get(2));
	}

	@Test(expected = RuntimeException.class) 
	public void testExceptionIsThrownIfVerticesIsNotMatching() { 
		createPathGenerics(
			createWeight(15d),  
			Arrays.asList(
				createEdge(createVertex("A"), createVertex("B"), createWeight(3d)),
				createEdge(createVertex("B"), createVertex("C"), createWeight(5d)),
				 // Note that "X" should be "C" below, which is the reason for expected exceotion
				createEdge(createVertex("X"), createVertex("D"), createWeight(7d))
			),
			false,
			true // tell creation method to throw exception if not all vertices are matching 				
		);
	}
	
	@Test(expected = RuntimeException.class) 
	public void testExceptionIsTotalWeightIsNotMatching() {
		createPathGenerics(
			createWeight(16), // SHOULD be 15 ( 3 + 5 + 7 ) and therefore an exception should be thrown 
			Arrays.asList(
				createEdge(createVertex("A"), createVertex("B"), createWeight(3d)),
				createEdge(createVertex("B"), createVertex("C"), createWeight(5d)),
				createEdge(createVertex("C"), createVertex("D"), createWeight(7d))
			),
			true,  // tell creation method to throw exception if sum is not matching
			false 				
		);
	}	
}