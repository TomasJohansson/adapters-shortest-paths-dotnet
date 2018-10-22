/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in the "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using Programmerare.ShortestPaths.Core.Api;
using NUnit.Framework;
using static NUnit.Framework.Assert;
using static Programmerare.ShortestPaths.Core.Impl.WeightImpl; // SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS
using static Programmerare.ShortestPaths.Core.Impl.EdgeImpl; // createEdge
using static Programmerare.ShortestPaths.Core.Impl.VertexImpl; // createVertex
using System.Collections.Generic;

namespace Programmerare.ShortestPaths.Core.Impl.Generics
{
    /**
     * @author Tomas Johansson
     */
    public class EdgeMapperTest {
	    private Edge edgeX1, edgeX2, edgeY1, edgeY2;
	
	    [SetUp]
	    public void setUp()  {
		    edgeX1 = CreateEdge(CreateVertex("A"), CreateVertex("B"), CreateWeight(7));
		    edgeX2 = CreateEdge(CreateVertex("A"), CreateVertex("B"), CreateWeight(7));
		
		    edgeY1 = CreateEdge(CreateVertex("B"), CreateVertex("C"), CreateWeight(8));
		    edgeY2 = CreateEdge(CreateVertex("B"), CreateVertex("C"), CreateWeight(8));
		
		
	    }

	    [Test]
	    public void testGetOriginalObjectInstancesOfTheEdges() {
		    List<Edge> originalEdges = new List<Edge>{edgeX1, edgeY1};
		    List<Edge> equalEdgesButOtherInstances = new List<Edge>{edgeX2, edgeY2};
		
		    AreEqual(originalEdges[0], equalEdgesButOtherInstances[0]);
		    AreEqual(originalEdges[1], equalEdgesButOtherInstances[1]);
		    // Note that the above were equal but they are NOT the same instances as you can see here:
		    IsFalse(originalEdges[0] == equalEdgesButOtherInstances[0]);
		    IsFalse(originalEdges[1] == equalEdgesButOtherInstances[1]);

		
		    EdgeMapper<Edge, Vertex, Weight> edgeMapper = EdgeMapper<Edge, Vertex, Weight>.CreateEdgeMapper<Edge, Vertex, Weight>(originalEdges);
		    IList<Edge> originalObjectInstancesOfTheEdges = edgeMapper.GetOriginalObjectInstancesOfTheEdges(equalEdgesButOtherInstances);
		    // Note that the input parameter above vas the list which did NOT have the same instances as the original
		    // list (i.e. the list passed into the constructor of EdgeMapper) but the returned list should have the same 
		    // instances, and they should be mapped through the id of the edge
		    AreSame(originalEdges[0], originalObjectInstancesOfTheEdges[0]);
		    AreSame(originalEdges[1], originalObjectInstancesOfTheEdges[1]); 		
	    }
	
	    [Test]
	    public void testGetOriginalEdgeInstance() {
		    IList<Edge> originalEdges = new List<Edge> {edgeX1, edgeY1 };
		
		    EdgeMapper<Edge, Vertex, Weight> edgeMapper = EdgeMapper<Edge, Vertex, Weight>.CreateEdgeMapper<Edge, Vertex, Weight>(originalEdges);

		    // the same edge instance should be retrieve when we below pass in the string ids for the vertices of the edge 
		
		    AreSame(edgeX1, edgeMapper.GetOriginalEdgeInstance(edgeX1.StartVertex.VertexId, edgeX1.EndVertex.VertexId));
		    AreSame(edgeY1, edgeMapper.GetOriginalEdgeInstance(edgeY1.StartVertex.VertexId, edgeY1.EndVertex.VertexId));
	    }
    }
}