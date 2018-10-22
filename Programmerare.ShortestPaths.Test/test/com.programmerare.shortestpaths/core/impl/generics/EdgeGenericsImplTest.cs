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
using static Programmerare.ShortestPaths.Core.Impl.WeightImpl; // SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS
using static Programmerare.ShortestPaths.Core.Impl.EdgeImpl; // createEdge
using static Programmerare.ShortestPaths.Core.Impl.VertexImpl; // createVertex
using Programmerare.ShortestPaths.Core.Api;

namespace Programmerare.ShortestPaths.Core.Impl.Generics
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