/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.core.parsers;

import static com.programmerare.shortestpaths.core.impl.WeightImpl.SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS;
import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNotNull;

import java.util.List;

import org.junit.Before;
import org.junit.Test;

import com.programmerare.shortestpaths.core.api.Edge;
import com.programmerare.shortestpaths.core.api.Path;
import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;
import com.programmerare.shortestpaths.core.api.generics.EdgeGenerics;
import com.programmerare.shortestpaths.core.api.generics.PathGenerics;
import com.programmerare.shortestpaths.core.validation.GraphValidationException;

public class PathParserTest {

	private PathParser<PathGenerics<EdgeGenerics<Vertex, Weight>, Vertex, Weight>, EdgeGenerics<Vertex, Weight>, Vertex, Weight> pathParserGenerics;
	
	private PathParser<Path, Edge, Vertex, Weight> pathParserPathDefault;

	@Before
	public void setUp() throws Exception {
		final String stringWithEdges = "A B 5\r\n" + 
				"A C 6\r\n" + 
				"B C 7\r\n" + 
				"B D 8\r\n" + 
				"C D 9";
//	    <graphDefinition>
//	    A B 5
//	    A C 6
//	    B C 7
//	    B D 8
//	    C D 9    
//	 	</graphDefinition>
		
		final EdgeParser<EdgeGenerics<Vertex, Weight>, Vertex, Weight> edgeParser = EdgeParser.createEdgeParserGenerics();
		final List<EdgeGenerics<Vertex, Weight>> edges = edgeParser.fromMultiLinedStringToListOfEdges(stringWithEdges);
		pathParserGenerics = PathParser.createPathParserGenerics(edges);
	
		final EdgeParser<Edge, Vertex, Weight> edgeParserDefault = EdgeParser.createEdgeParserDefault();
		List<Edge> edgesDefault = edgeParserDefault.fromMultiLinedStringToListOfEdges(stringWithEdges);
//		System.out.println("edgesDefault.get(0).getClass() " + edgesDefault.get(0).getClass());
		pathParserPathDefault = PathParser.createPathParserDefault(edgesDefault);
	}

	@Test
	public void testFromStringToListOfPaths() {
//	    <outputExpected>
//			13 A B D
//			15 A C D
//			21 A B C D
//	    </outputExpected>
		
		List<PathGenerics<EdgeGenerics<Vertex, Weight>, Vertex, Weight>> lListOfPaths = pathParserGenerics.fromStringToListOfPaths("13 A B D\r\n" + 
				"15 A C D\r\n" + 
				"21 A B C D");
		assertNotNull(lListOfPaths);
		assertEquals(3,  lListOfPaths.size());
		
		PathGenerics<EdgeGenerics<Vertex, Weight>, Vertex, Weight> path1 = lListOfPaths.get(0); // 13 A B D
		PathGenerics<EdgeGenerics<Vertex, Weight>, Vertex, Weight> path2 = lListOfPaths.get(1); // 15 A C D 
		PathGenerics<EdgeGenerics<Vertex, Weight>, Vertex, Weight> path3 = lListOfPaths.get(2); // 21 A B C D
		assertNotNull(path1);
		assertNotNull(path2);
		assertNotNull(path3);
		assertEquals(13.0, path1.getTotalWeightForPath().getWeightValue(), SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
		assertEquals(15.0, path2.getTotalWeightForPath().getWeightValue(), SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
		assertEquals(21.0, path3.getTotalWeightForPath().getWeightValue(), SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
		
		List<EdgeGenerics<Vertex, Weight>> edgesForPath1 = path1.getEdgesForPath();
		List<EdgeGenerics<Vertex, Weight>> edgesForPath2 = path2.getEdgesForPath();
		List<EdgeGenerics<Vertex, Weight>> edgesForPath3 = path3.getEdgesForPath();
		assertNotNull(edgesForPath1);
		assertNotNull(edgesForPath2);
		assertNotNull(edgesForPath3);
		assertEquals(2, edgesForPath1.size());
		assertEquals(2, edgesForPath2.size());
		assertEquals(3, edgesForPath3.size());

		// edgesForPath1 "13 A B D" means path "A -> B" and "B -> D"
		assertEquals(pathParserGenerics.getEdgeIncludingTheWeight("A", "B"), edgesForPath1.get(0));
		assertEquals(pathParserGenerics.getEdgeIncludingTheWeight("B", "D"), edgesForPath1.get(1));

		// edgesForPath2 // 15 A C D
		assertEquals(pathParserGenerics.getEdgeIncludingTheWeight("A", "C"), edgesForPath2.get(0));
		assertEquals(pathParserGenerics.getEdgeIncludingTheWeight("C", "D"), edgesForPath2.get(1));
		
		// 21 A B C D
		assertEquals(pathParserGenerics.getEdgeIncludingTheWeight("A", "B"), edgesForPath3.get(0));
		assertEquals(pathParserGenerics.getEdgeIncludingTheWeight("B", "C"), edgesForPath3.get(1));
		assertEquals(pathParserGenerics.getEdgeIncludingTheWeight("C", "D"), edgesForPath3.get(2));
	}
	
	@Test(expected = GraphValidationException.class)
	public void testgetEdgeIncludingTheWeight_should_throw_exception_when_edge_does_not_exist() {
		// the edges in the setup method do not contain any edge between vertices A and D
		pathParserGenerics.getEdgeIncludingTheWeight("A", "D");	
	}

	// yes, I am lazy here, with two methods tested, convenient with strings compared to creating Path and Edge objects 
	@Test
	public void test_fromStringToPath_Generic_and_fromPathToString() {
		// the pathParser is constructed in setup method with  two edges: A -> B (weight 5) and B -> D (weight 8) 
		final String inputPathString = "13 A B D";
		
		PathGenerics<EdgeGenerics<Vertex, Weight>, Vertex, Weight> path = pathParserGenerics.fromStringToPath(inputPathString);
		// TODO: test below and above methods from separate test methods
		final String outputPathString = pathParserGenerics.fromPathToString(path);
		
		assertEquals(inputPathString, outputPathString);
	}
	
	@Test
	public void test_fromStringToPath_and_fromPathToString() {
		// the pathParser is constructed in setup method with  two edges: A -> B (weight 5) and B -> D (weight 8) 
		final String inputPathString = "13 A B D";
		
		Path path = pathParserPathDefault.fromStringToPath(inputPathString);
		// TODO: test below and above methods from separate test methods
		final String outputPathString = pathParserPathDefault.fromPathToString(path);
		
		assertEquals(inputPathString, outputPathString);
	}	
}