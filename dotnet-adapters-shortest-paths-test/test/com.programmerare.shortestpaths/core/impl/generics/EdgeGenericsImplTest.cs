/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
using NUnit.Framework;
using static NUnit.Framework.Assert;
using static com.programmerare.shortestpaths.core.impl.WeightImpl; // SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS
using static com.programmerare.shortestpaths.core.impl.EdgeImpl; // createEdge
using static com.programmerare.shortestpaths.core.impl.VertexImpl; // createVertex
//using static com.programmerare.shortestpaths.core.impl.WeightImpl.SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS;
//using static com.programmerare.shortestpaths.core.impl.WeightImpl.createWeight;
//using static org.junit.Assert.assertEquals;
//using static org.junit.Assert.assertTrue;
//import org.junit.Before;
//import org.junit.Test;
using com.programmerare.shortestpaths.core.api;

namespace com.programmerare.shortestpaths.core.impl.generics
{
    /**
     * @author Tomas Johansson
     */
    public class EdgeGenericsImplTest {
	    private Vertex vertexA, vertexB;
	    private Weight weight;
	    private double weightValue;
	    private Edge edgeX, edgeY;
	
	    [SetUp]
	    public void setUp()  {
		    vertexA = CreateVertex("A");
		    vertexB = CreateVertex("B");
		    weightValue = 123.45;
		    weight = CreateWeight(weightValue);
		    edgeX = CreateEdge(vertexA, vertexB, weight);
		
		    edgeY = CreateEdge(CreateVertex("A"), CreateVertex("B"), CreateWeight(weightValue));
	    }


	    [Test]
	    public void testGetStartVertex() {
		    AreEqual(vertexA.VertexId, edgeX.StartVertex.VertexId);
		    AreEqual(vertexA, edgeX.StartVertex);
	    }
	
	    [Test]
	    public void testGetEndVertex() {
		    AreEqual(vertexB.VertexId, edgeX.EndVertex.VertexId);
		    AreEqual(vertexB, edgeX.EndVertex);
	    }	
	
	    [Test]
	    public void testgetEdgeWeight() {
		    AreEqual(weightValue, edgeX.EdgeWeight.WeightValue, SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
		    AreEqual(weight, edgeX.EdgeWeight);		
	    }
	
	    [Test]
	    public void testEquals() {
		    AreEqual(edgeX, edgeY);
		    IsTrue(edgeX.Equals(edgeY));
		    IsTrue(edgeY.Equals(edgeX));
	    }
	
	    [Test]
	    public void testHashCode() {
		    AreEqual(edgeX.GetHashCode(), edgeY.GetHashCode());
	    }	
    }
}