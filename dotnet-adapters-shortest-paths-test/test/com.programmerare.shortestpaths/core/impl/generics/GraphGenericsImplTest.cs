/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
//import static com.programmerare.shortestpaths.core.impl.VertexImpl.createVertex;
//import static com.programmerare.shortestpaths.core.impl.WeightImpl.createWeight;
//import static com.programmerare.shortestpaths.core.impl.generics.EdgeGenericsImpl.createEdgeGenerics;
//using static com.programmerare.shortestpaths.core.impl.generics.GraphGenericsImpl; // createGraphGenerics
//import static org.hamcrest.CoreMatchers.hasItem;
//import static org.hamcrest.MatcherAssert.assertThat;
//import static org.junit.Assert.assertEquals;
//import static org.junit.Assert.assertFalse;
//import static org.junit.Assert.assertSame;
//import static org.junit.Assert.assertTrue;
//import org.junit.Before;
//import org.junit.Test;
using com.programmerare.shortestpaths.core.api;
using NUnit.Framework;
using static NUnit.Framework.Assert;
using static com.programmerare.shortestpaths.core.impl.GraphImplTest; // createEdgeGenerics
using static com.programmerare.shortestpaths.core.impl.WeightImpl; // SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS
using static com.programmerare.shortestpaths.core.impl.EdgeImpl; // createEdge
using static com.programmerare.shortestpaths.core.impl.VertexImpl; // createVertex
using System.Collections.Generic;
using com.programmerare.shortestpaths.core.api.generics;

namespace com.programmerare.shortestpaths.core.impl.generics
{
    public class GraphGenericsImplTest {

	    private EdgeGenerics<Vertex,Weight> edge1, edge2;
	
	    [SetUp]
	    public void setUp() {
		    edge1 = createEdgeGenerics(createVertex("A"), createVertex("B"), createWeight(123));
		    edge2 = createEdgeGenerics(createVertex("B"), createVertex("C"), createWeight(456));		
	    }

	    [Test]
	    public void testGetAllEdges() {
		    IList<EdgeGenerics<Vertex,Weight>> edges = new List<EdgeGenerics<Vertex,Weight>>();
		    edges.Add(edge1);
		    edges.Add(edge2);
		    // refactor the above three rows (duplicated)

            Fail("Fix test ...createGraphGenerics below ");
		    GraphGenerics<EdgeGenerics<Vertex,Weight>, Vertex,Weight> graph = null;//createGraphGenerics(edges);
		
		    IList<EdgeGenerics<Vertex, Weight>> allEdges = graph.getEdges();
		
		    AreEqual(2,  allEdges.Count);
		    AreSame(edge1, allEdges[0]);
		    AreSame(edge2, allEdges[1]);
	    }
	
	    [Test]
	    public void testGetVertices() {
		    IList<EdgeGenerics<Vertex,Weight>> edges = new List<EdgeGenerics<Vertex,Weight>>();
		    edges.Add(createEdgeGenerics(createVertex("A"), createVertex("B"), createWeight(1)));
		    edges.Add(createEdgeGenerics(createVertex("A"), createVertex("C"), createWeight(2)));
		    edges.Add(createEdgeGenerics(createVertex("A"), createVertex("D"), createWeight(3)));
		    edges.Add(createEdgeGenerics(createVertex("B"), createVertex("C"), createWeight(4)));
		    edges.Add(createEdgeGenerics(createVertex("B"), createVertex("D"), createWeight(5)));
		    edges.Add(createEdgeGenerics(createVertex("C"), createVertex("D"), createWeight(6)));
		
            Fail("Fix test ...createGraphGenerics below ");
		    GraphGenerics<EdgeGenerics<Vertex,Weight>, Vertex,Weight> graph = null;//createGraphGenerics(edges);
		
		    IList<Vertex> vertices = graph.getVertices();
		
		    IList<string> expectedVerticesIds = new List<string>{"A", "B", "C", "D" };
		
		    AreEqual(expectedVerticesIds.Count, vertices.Count);
		
		    // verify that all vertices in all edges is one of the four above
		    foreach (EdgeGenerics<Vertex,Weight> edge in edges) {
                // Java version used hamcres as below:
			    //assertThat(expectedVerticesIds, hasItem(edge.getStartVertex().getVertexId()));
			    //assertThat(expectedVerticesIds, hasItem(edge.getEndVertex().getVertexId()));
                Fail("fix the test");
		    }		
	    }
	
	    [Test]
	    public void testContainsVertex() {
		    List<EdgeGenerics<Vertex,Weight>> edges = new List<EdgeGenerics<Vertex,Weight>>();
		    edges.Add(edge1);
		    edges.Add(edge2);
		    // refactor the above three rows (duplicated)
		
            Fail("Fix test ...createGraphGenerics below ");
		    GraphGenerics<EdgeGenerics<Vertex,Weight>, Vertex,Weight> graph = null;//createGraphGenerics(edges);

		    IsTrue(graph.containsVertex(edge1.getStartVertex()));
		    IsTrue(graph.containsVertex(edge1.getEndVertex()));
		    IsTrue(graph.containsVertex(edge2.getStartVertex()));
		    IsTrue(graph.containsVertex(edge2.getEndVertex()));
		
		    Vertex vertex = createVertex("QWERTY");
		    IsFalse(graph.containsVertex(vertex));
	    }
	    // TODO: refactor some code duplicated above and below i.e. put some code in setup method
	    [Test]
	    public void testContainsEdge() {
		    List<EdgeGenerics<Vertex,Weight>> edges = new List<EdgeGenerics<Vertex,Weight>>();
		    edges.Add(edge1);
		    edges.Add(edge2);
		    // refactor the above three rows (duplicated)
		
            Fail("Fix test ...createGraphGenerics below ");
		    GraphGenerics<EdgeGenerics<Vertex,Weight>, Vertex,Weight> graph = null;//createGraphGenerics(edges);		

		    IsTrue(graph.containsEdge(edge1));
		    IsTrue(graph.containsEdge(edge2));
		
		    EdgeGenerics<Vertex,Weight> edgeNotInTheGraph = createEdgeGenerics(createVertex("XYZ"), createVertex("QWERTY"), createWeight(987));
		    IsFalse(graph.containsEdge(edgeNotInTheGraph));
	    }	
    }

}