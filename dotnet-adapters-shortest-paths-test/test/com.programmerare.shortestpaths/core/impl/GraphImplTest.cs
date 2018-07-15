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
using com.programmerare.shortestpaths.core.api;
using static com.programmerare.shortestpaths.core.impl.GraphImpl;
using static com.programmerare.shortestpaths.core.impl.VertexImpl; // createVertex
using static com.programmerare.shortestpaths.core.impl.EdgeImpl;
using static com.programmerare.shortestpaths.core.impl.WeightImpl; // createWeight
                                                                   // using static com.programmerare.shortestpaths.core.impl.generics.EdgeGenericsImpl<Vertex, Weight>; // createEdgeGenerics
using com.programmerare.shortestpaths.core.api.generics;
using com.programmerare.shortestpaths.core.validation;
using System.Collections.Generic;

namespace com.programmerare.shortestpaths.core.impl
{
    /**
     * @author Tomas Johansson
     */
    [TestFixture]
    public class GraphImplTest {

	    private IList<Edge> edgesForAcceptableGraph;
	    private IList<Edge> edgesForUnacceptableGraph;

	    private Graph graph;
	
	    [SetUp]
	    public void setUp()  {
		
		    Edge edge_A_B = CreateEdge(CreateVertex("A"), CreateVertex("B"), CreateWeight(123));
		    Edge edge_B_C = CreateEdge(CreateVertex("B"), CreateVertex("C"), CreateWeight(456));
		    edgesForAcceptableGraph = new List<Edge> { edge_A_B, edge_B_C };

		    // the same edge (A to B) defined once again is NOT correct
		    Edge edge_A_B_again = CreateEdge(CreateVertex("A"), CreateVertex("B"), CreateWeight(789));
		    edgesForUnacceptableGraph = new List<Edge> { edge_A_B, edge_A_B_again };
	    }

        internal static EdgeGenerics<Vertex, Weight> createEdgeGenerics(Vertex vertex1, Vertex vertex2, Weight weight)
        {
            return com.programmerare.shortestpaths.core.impl.generics.EdgeGenericsImpl<Vertex, Weight>.CreateEdgeGenerics(vertex1, vertex2, weight);
        }

        [Test]
	    public void testCreateGraph_SHOULD_throw_exception_for_unacceptable_graph_when_validation_REQUIRED() {
            var exceptionThrown = Assert.Throws<GraphValidationException>(() => {
    		    graph = CreateGraph(edgesForUnacceptableGraph, GraphEdgesValidationDesired.YES);
            });
            IsNotNull(exceptionThrown);
	    }
	
	    [Test]
	    public void testCreateGraph_should_NOT_throw_exception_for_unacceptable_graph_when_validation_NOT_required() {
		    graph = CreateGraph(edgesForUnacceptableGraph, GraphEdgesValidationDesired.NO);
	    }	

	    [Test]
	    public void testCreateGraph_should_NOT_throw_exception_for_acceptable_graph() {
		    // a bit lazy to do two validation below within the same test method, 
		    // but since the graph should be acceptable, no exception should be thrown 
		    // regardless if validation is required
		    
            graph = CreateGraph(edgesForAcceptableGraph, GraphEdgesValidationDesired.NO);
		    graph = CreateGraph(edgesForAcceptableGraph, GraphEdgesValidationDesired.YES);
	    }	
    }
}