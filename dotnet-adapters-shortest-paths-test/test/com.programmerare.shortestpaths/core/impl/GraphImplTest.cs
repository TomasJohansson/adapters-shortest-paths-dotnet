/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
using NUnit.Framework;
using static NUnit.Framework.Assert;
using com.programmerare.shortestpaths.core.api;
using static com.programmerare.shortestpaths.core.impl.GraphImpl;
using static com.programmerare.shortestpaths.core.impl.VertexImpl; // createVertex
using static com.programmerare.shortestpaths.core.impl.VertexImpl;
using static com.programmerare.shortestpaths.core.impl.EdgeImpl;
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

	    private IList<Edge> edgesForAcceptableGraph;
	    private IList<Edge> edgesForUnacceptableGraph;

	    private Graph graph;
	
	    [SetUp]
	    public void setUp()  {
		
		    Edge edge_A_B = createEdge(createVertex("A"), createVertex("B"), createWeight(123));
		    Edge edge_B_C = createEdge(createVertex("B"), createVertex("C"), createWeight(456));
		    edgesForAcceptableGraph = new List<Edge> { edge_A_B, edge_B_C };

		    // the same edge (A to B) defined once again is NOT correct
		    Edge edge_A_B_again = createEdge(createVertex("A"), createVertex("B"), createWeight(789));
		    edgesForUnacceptableGraph = new List<Edge> { edge_A_B, edge_A_B_again };
	    }

        internal static EdgeGenerics<Vertex, Weight> createEdgeGenerics(Vertex vertex1, Vertex vertex2, Weight weight)
        {
            return com.programmerare.shortestpaths.core.impl.generics.EdgeGenericsImpl<Vertex, Weight>.createEdgeGenerics(vertex1, vertex2, weight);
        }

        [Test]
	    public void testCreateGraph_SHOULD_throw_exception_for_unacceptable_graph_when_validation_REQUIRED() {
            var exceptionThrown = Assert.Throws<GraphValidationException>(() => {
    		    graph = createGraph(edgesForUnacceptableGraph, GraphEdgesValidationDesired.YES);
            });
            IsNotNull(exceptionThrown);
	    }
	
	    [Test]
	    public void testCreateGraph_should_NOT_throw_exception_for_unacceptable_graph_when_validation_NOT_required() {
		    graph = createGraph(edgesForUnacceptableGraph, GraphEdgesValidationDesired.NO);
	    }	

	    [Test]
	    public void testCreateGraph_should_NOT_throw_exception_for_acceptable_graph() {
		    // a bit lazy to do two validation below within the same test method, 
		    // but since the graph should be acceptable, no exception should be thrown 
		    // regardless if validation is required
		    
            graph = createGraph(edgesForAcceptableGraph, GraphEdgesValidationDesired.NO);
		    graph = createGraph(edgesForAcceptableGraph, GraphEdgesValidationDesired.YES);
	    }	
    }
}