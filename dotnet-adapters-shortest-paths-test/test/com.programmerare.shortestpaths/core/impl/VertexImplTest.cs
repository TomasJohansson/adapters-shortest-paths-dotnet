/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
//import static com.programmerare.shortestpaths.core.impl.VertexImpl.createVertex;
//import static org.junit.Assert.assertEquals;
//import static org.junit.Assert.assertTrue;
//import org.junit.Before;
//import org.junit.Test;
using NUnit.Framework;
using static NUnit.Framework.Assert;
using com.programmerare.shortestpaths.core.api;
using static com.programmerare.shortestpaths.core.impl.VertexImpl; // createVertex
using static com.programmerare.shortestpaths.core.impl.WeightImpl; // createWeight

namespace com.programmerare.shortestpaths.core.impl
{
    /**
     * @author Tomas Johansson
     */
    [TestFixture]
    public class VertexImplTest {

	    private Vertex vertexA;
	    private Vertex vertexB;
	
	    [SetUp]
	    public void setUp() {
		    vertexA = createVertex(357);
		    vertexB = createVertex("357");		
	    }
	
	    [Test]
	    public void testGetVertexId() {
		    Vertex vertexA = createVertex(357);
		    Vertex vertexB = createVertex("357");
		
		    AreEqual(vertexA.getVertexId(), vertexB.getVertexId());
		
		    AreEqual(vertexA, vertexB);
		    AreEqual(vertexA.GetHashCode(), vertexB.GetHashCode());
	    }
	
	    [Test]
	    public void testEquals() {
		    AreEqual(vertexA, vertexB);

		    IsTrue(vertexA.Equals(vertexB));
		    IsTrue(vertexB.Equals(vertexA));
	    }
	
	    [Test]
	    public void testHashCode() {
		    AreEqual(vertexA.GetHashCode(), vertexB.GetHashCode());
	    }	

    }
}