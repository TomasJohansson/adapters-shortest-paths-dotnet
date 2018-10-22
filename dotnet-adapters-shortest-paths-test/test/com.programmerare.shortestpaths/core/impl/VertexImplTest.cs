/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in the "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using NUnit.Framework;
using static NUnit.Framework.Assert;
using Programmerare.ShortestPaths.Core.Api;
using static Programmerare.ShortestPaths.Core.Impl.VertexImpl; // createVertex

namespace Programmerare.ShortestPaths.Core.Impl
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