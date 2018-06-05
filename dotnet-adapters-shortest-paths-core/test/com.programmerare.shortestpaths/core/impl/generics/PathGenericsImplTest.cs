/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
using com.programmerare.shortestpaths.core.api;
using NUnit.Framework;
using static NUnit.Framework.Assert;
using static com.programmerare.shortestpaths.core.impl.WeightImpl; // SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS
using static com.programmerare.shortestpaths.core.impl.EdgeImpl; // createEdge
using static com.programmerare.shortestpaths.core.impl.VertexImpl; // createVertex
using System.Collections.Generic;
using static com.programmerare.shortestpaths.core.impl.PathImpl; // createPath
using static com.programmerare.shortestpaths.core.impl.EdgeImpl; // createEdge
using static com.programmerare.shortestpaths.core.impl.VertexImpl; // createVertex
//import static com.programmerare.shortestpaths.core.impl.WeightImpl.SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS;
//import static com.programmerare.shortestpaths.core.impl.WeightImpl.createWeight;
//import static com.programmerare.shortestpaths.core.impl.generics.PathGenericsImpl.createPathGenerics;
//import static org.junit.Assert.assertEquals;
//import java.util.Arrays;
//import java.util.List;
//import org.junit.Before;
//import org.junit.Test;
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.api.generics;

namespace com.programmerare.shortestpaths.core.impl.generics
{
    /**
     * @author Tomas Johansson
     */
    [TestFixture]
    public class PathGenericsImplTest {

	    private Edge edgeAB3;
	    private Edge edgeBC5;
	    private Edge edgeCD7;
	    private string firstVertex, secondVertex, thirdVertex, fourthVertex;
	    private double weightFirstEdge, weightSecondEdge, weightThirdEdge, totalWeight;
	    private Path path; 
			
	    [SetUp]
	    public void setUp()  {
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
		
		    path = createPath(createWeight(totalWeight), new List<Edge> {edgeAB3, edgeBC5, edgeCD7 });
	    }

	    [Test]
	    public void testGetTotalWeightForPath() {
		    AreEqual(totalWeight, path.getTotalWeightForPath().getWeightValue(), SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
	    }

	    [Test]
	    public void testGetEdgesForPath() {
		    var edgesForPath = path.getEdgesForPath();
		    AreEqual(3, edgesForPath.Count);
		    AreEqual(edgeAB3, edgesForPath[0]);
		    AreEqual(edgeBC5, edgesForPath[1]);
		    AreEqual(edgeCD7, edgesForPath[2]);
	    }

	    [Test]//@Test(expected = RuntimeException.class) 
	    public void testExceptionIsThrownIfVerticesIsNotMatching() { 
            Fail("TODO fix this method");
		    //createPathGenerics(
		    //	createWeight(15d),  
		    //	Arrays.asList(
		    //		createEdge(createVertex("A"), createVertex("B"), createWeight(3d)),
		    //		createEdge(createVertex("B"), createVertex("C"), createWeight(5d)),
		    //		 // Note that "X" should be "C" below, which is the reason for expected exceotion
		    //		createEdge(createVertex("X"), createVertex("D"), createWeight(7d))
		    //	),
		    //	false,
		    //	true // tell creation method to throw exception if not all vertices are matching 				
		    //);
	    }
	
    
	    //@Test(expected = RuntimeException.class) 
        [Test]
	    public void testExceptionIsTotalWeightIsNotMatching() {
            Fail("fix this method");
		    //createPathGenerics(
		    //	createWeight(16), // SHOULD be 15 ( 3 + 5 + 7 ) and therefore an exception should be thrown 
		    //	Arrays.asList(
		    //		createEdge(createVertex("A"), createVertex("B"), createWeight(3d)),
		    //		createEdge(createVertex("B"), createVertex("C"), createWeight(5d)),
		    //		createEdge(createVertex("C"), createVertex("D"), createWeight(7d))
		    //	),
		    //	true,  // tell creation method to throw exception if sum is not matching
		    //	false 				
		    //);
	    }	
    }
}