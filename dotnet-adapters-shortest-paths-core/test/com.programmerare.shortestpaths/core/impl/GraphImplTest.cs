/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
using NUnit.Framework;
using static NUnit.Framework.Assert;
using com.programmerare.shortestpaths.core.api;
using static com.programmerare.shortestpaths.core.impl.VertexImpl; // createVertex
using static com.programmerare.shortestpaths.core.impl.WeightImpl; // createWeight
                                                                   // using static com.programmerare.shortestpaths.core.impl.generics.EdgeGenericsImpl<Vertex, Weight>; // createEdgeGenerics
using com.programmerare.shortestpaths.core.api.generics;
using com.programmerare.shortestpaths.core.validation;
using System.Collections.Generic;
using com.programmerare.shortestpaths.core.impl.generics;
using System;

namespace com.programmerare.shortestpaths.core.impl
{
    /**
     * @author Tomas Johansson
     */
    [TestFixture]
    public class GraphImplTest {

	    private IList<EdgeGenerics<Vertex,Weight>> edgesForAcceptableGraph;
	    private IList<EdgeGenerics<Vertex,Weight>> edgesForUnacceptableGraph;

	    private GraphGenerics<EdgeGenerics<Vertex, Weight>, Vertex, Weight> graph;
	
	    [SetUp]
	    public void setUp()  {
		
		    EdgeGenerics<Vertex,Weight> edge_A_B = createEdgeGenerics(createVertex("A"), createVertex("B"), createWeight(123));
		    EdgeGenerics<Vertex,Weight> edge_B_C = createEdgeGenerics(createVertex("B"), createVertex("C"), createWeight(456));
		    edgesForAcceptableGraph = new List<EdgeGenerics<Vertex,Weight>> { edge_A_B, edge_B_C };

		    // the same edge (A to B) defined once again is NOT correct
		    EdgeGenerics<Vertex,Weight> edge_A_B_again = createEdgeGenerics(createVertex("A"), createVertex("B"), createWeight(789));
		    edgesForUnacceptableGraph = new List<EdgeGenerics<Vertex,Weight>> { edge_A_B, edge_A_B_again };
	    }

        internal static EdgeGenerics<Vertex, Weight> createEdgeGenerics(Vertex vertex1, Vertex vertex2, Weight weight)
        {
            return com.programmerare.shortestpaths.core.impl.generics.EdgeGenericsImpl<Vertex, Weight>.createEdgeGenerics(vertex1, vertex2, weight);
        }

        //@Test(expected = GraphValidationException.class)
        [Test]
	    public void testCreateGraph_SHOULD_throw_exception_for_unacceptable_graph_when_validation_REQUIRED() {
            Fail("Fix exception test ...");
		    //graph = GraphGenericsImpl.createGraphGenerics(edgesForUnacceptableGraph, GraphEdgesValidationDesired.YES);
	    }
	
	    [Test]
	    public void testCreateGraph_should_NOT_throw_exception_for_unacceptable_graph_when_validation_NOT_required() {
            Fail("Fix exception test ...");
		    //graph = GraphGenericsImpl<Edge, Vertex, Weight>.createGraphGenerics<Edge, Vertex, Weight>(edgesForUnacceptableGraph, GraphEdgesValidationDesired.NO);
	    }	

	    [Test]
	    public void testCreateGraph_should_NOT_throw_exception_for_acceptable_graph() {
		    // a bit lazy to do two validation below within the same test method, 
		    // but since the graph should be acceptable, no exception should be thrown 
		    // regardless if validation is required
		    
            Fail("Fix this test");
      //      graph = GraphGenericsImpl<Edge, Vertex, Weight>.createGraphGenerics<Edge, Vertex, Weight>(edgesForAcceptableGraph, GraphEdgesValidationDesired.NO);
		    //graph = GraphGenericsImpl<Edge, Vertex, Weight>.createGraphGenerics(edgesForAcceptableGraph, GraphEdgesValidationDesired.YES);
	    }	
    }
}