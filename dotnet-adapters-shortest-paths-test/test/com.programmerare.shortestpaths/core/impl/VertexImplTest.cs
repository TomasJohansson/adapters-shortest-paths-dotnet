/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
using NUnit.Framework;
using static NUnit.Framework.Assert;
using com.programmerare.shortestpaths.core.api;
using static com.programmerare.shortestpaths.core.impl.VertexImpl; // createVertex

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
		    vertexA = CreateVertex(357);
		    vertexB = CreateVertex("357");		
	    }
	
	    [Test]
	    public void testGetVertexId() {
		
		    AreEqual(vertexA.VertexId, vertexB.VertexId);
		
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