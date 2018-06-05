/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
//import static com.programmerare.shortestpaths.core.impl.VertexImpl.createVertex;
//import static com.programmerare.shortestpaths.core.impl.WeightImpl.createWeight;
//import static com.programmerare.shortestpaths.core.impl.generics.EdgeGenericsImpl.createEdgeGenerics;
//import static org.junit.Assert.assertEquals;
//import java.util.ArrayList;
//import java.util.Arrays;
//import java.util.List;
//import org.junit.Before;
//import org.junit.Test;
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.api.generics;
//using com.programmerare.shortestpaths.utils.EdgeUtility.SelectionStrategyWhenEdgesAreDuplicated;
using com.programmerare.shortestpaths.core.api;
using NUnit.Framework;
using static NUnit.Framework.Assert;
using static com.programmerare.shortestpaths.core.impl.GraphImplTest; // createEdgeGenerics
using static com.programmerare.shortestpaths.core.impl.WeightImpl; // SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS
using static com.programmerare.shortestpaths.core.impl.EdgeImpl; // createEdge
using static com.programmerare.shortestpaths.core.impl.VertexImpl; // createVertex
using System.Collections.Generic;
using com.programmerare.shortestpaths.core.api.generics;

namespace com.programmerare.shortestpaths.utils
{
    //[TestFixture]
    //public class EdgeUtilityTest {

	   // EdgeUtility<EdgeGenerics<Vertex,Weight>, Vertex, Weight> edgeUtility;
	   // private const string A = "A";
	   // private const string B = "B";
	   // private const string C = "C";
	   // private const string D = "D";
	
	   // private static double weightBaseValueFor_A_B = 1;
	   // private static double weightBaseValueFor_B_C = 2;
	   // private static double weightBaseValueFor_C_D = 3;
	
	
	   // private List<EdgeGenerics<Vertex, Weight>> edges = new ArrayList<EdgeGenerics<Vertex, Weight>>();
	   // // there will be 12 edges, 3 (A_B , B_C , C_D) for each multiplier value below
	
	   // private readonly static List<int> multipliers = new List<int> {10, 1, 1000, 100 };
	
	   // private const int numberOfEdgesPerMultiplier = 3;

	   // // the indeces below refer to the list of edges cnostructed in the setup method by iterating the multiplier
	   // private const int startIndexForFirst = 0;
	   // private const int startIndexForSmallest = startIndexForFirst + numberOfEdgesPerMultiplier;
	   // private const int startIndexForLargest = startIndexForSmallest + numberOfEdgesPerMultiplier;
	   // private const int startIndexForLast = startIndexForLargest + numberOfEdgesPerMultiplier;
	
	   // [SetUp]
	   // public void setUp()  {
		  //  edgeUtility = EdgeUtility.create();
		  //  edges = new List<EdgeGenerics<Vertex, Weight>>();
		  //  foreach (int multiplier in multipliers) {
			 //   edges.Add(createEdgeGenerics(createVertex(A), createVertex(B), createWeight(weightBaseValueFor_A_B * multiplier)));
			 //   edges.Add(createEdgeGenerics(createVertex(B), createVertex(C), createWeight(weightBaseValueFor_B_C * multiplier)));
			 //   edges.Add(createEdgeGenerics(createVertex(C), createVertex(D), createWeight(weightBaseValueFor_C_D * multiplier)));
		  //  }
		  //  AreEqual(3 * multipliers.Count, edges.Count);
	   // }

	   // [Test]
	   // public void testGetEdgesWithoutDuplicates_Selecting_First() {
		  //  List<EdgeGenerics<Vertex, Weight>> result = edgeUtility.getEdgesWithoutDuplicates(edges, SelectionStrategyWhenEdgesAreDuplicated.FIRST_IN_LIST_OF_EDGES);
		  //  assertResult(result, startIndexForFirst);
	   // }

	   // [Test]
	   // public void testGetEdgesWithoutDuplicates_Selecting_Last() {
		  //  List<EdgeGenerics<Vertex, Weight>> result = edgeUtility.getEdgesWithoutDuplicates(edges, SelectionStrategyWhenEdgesAreDuplicated.LAST_IN_LIST_OF_EDGES);
		  //  assertResult(result, startIndexForLast);
	   // }

	   // [Test]
	   // public void testGetEdgesWithoutDuplicates_Selecting_Smallest() {
		  //  List<EdgeGenerics<Vertex, Weight>> result = edgeUtility.getEdgesWithoutDuplicates(edges, SelectionStrategyWhenEdgesAreDuplicated.SMALLEST_WEIGHT);
		  //  assertResult(result, startIndexForSmallest);
	   // }
	
	   // [Test]
	   // public void testGetEdgesWithoutDuplicates_Selecting_Largest() {
		  //  List<EdgeGenerics<Vertex, Weight>> result = edgeUtility.getEdgesWithoutDuplicates(edges, SelectionStrategyWhenEdgesAreDuplicated.LARGEST_WEIGHT);
		  //  assertResult(result, startIndexForLargest);
	   // }

	   // private void assertResult(List<EdgeGenerics<Vertex, Weight>> result, int startIndex) {
		  //  AreEqual(3, result.Count);
		  //  AreEqual(edges[startIndex++], result[0]);
		  //  AreEqual(edges[startIndex++], result[1]);
		  //  AreEqual(edges[startIndex], result[2]);
	   // }	
    //}
}