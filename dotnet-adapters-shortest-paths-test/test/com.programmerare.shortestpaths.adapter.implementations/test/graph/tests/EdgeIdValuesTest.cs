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
using static Programmerare.ShortestPaths.Core.Impl.WeightImpl; // SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS
using static Programmerare.ShortestPaths.Core.Impl.EdgeImpl; // createEdge
using static Programmerare.ShortestPaths.Core.Impl.VertexImpl; // createVertex
using Programmerare.ShortestPaths.Graphs.Utils;
using System.Collections.Generic;
using Programmerare.ShortestPaths.Core.Validation;
using Programmerare.ShortestPaths.Core.Impl.Generics;

namespace Programmerare.ShortestPaths.Graphs.Tests
{
    /**
     * The focus of this test class is the id value for edges.
     * 
     * These can be either set explicitly, but if not, then they are set to a concatenation
     * of the ids for the two vertices of the edge.
     * Either way, the ids of edges are used for being able to return the same instances as was sent as parameters. 
     * In other words, the client code sends Edge instances as parameters to the method:
     * "Graph PathFinderFactory.createGraph(List<T> edges)"
     * Then the client will get back a Graph instance and will invoke the method:
     * "List<Path<T>> Graph.findShortestPaths(Vertex startVertex, Vertex endVertex, int maxNumberOfPaths);"
     * The returned list of Path instances will aggregate instances of Edge, and those instances will be retrieved 
     * from the edges sent to the createGraph method. 
     * 
     * The id values of the edge values play a significant role, and this test class try to make sure it works
     * for both scenarios, i.e. either explicitly set ids or implicitly set (with concatenation of vertices ids).
     * 
     * See also javadoc comments at the test methods.
     *
     * The tests are performed for all adapter implementations.
     * (the implementation is determined by the instance of PathFinderFactory) 
     * 
     */
    public class EdgeIdValuesTest {

	    // Name of the vertices
	    private const string A = "A";
	    private const string B = "B";
	    private const string C = "C";
	    private const string D = "D";

	    // Name of the edegs
	    private const string EDGE_A_to_B = "EDGE_A_to_B";
	    private const string EDGE_A_to_C = "EDGE_A_to_C";
	    private const string EDGE_B_to_C = "EDGE_B_to_C";
	    private const string EDGE_B_to_D = "EDGE_B_to_D";
	    private const string EDGE_C_to_D = "EDGE_C_to_D";
	
	    // Weights for the edges
	    private const double WEIGHT_A_to_B = 5;
	    private const double WEIGHT_A_to_C = 6;
	    private const double WEIGHT_B_to_C = 7;
	    private const double WEIGHT_B_to_D = 8;
	    private const double WEIGHT_C_to_D = 9;

	    private EdgeIdValueStrategy edgeIdValueStrategy;
	
	    private Vertex a;
	    private Vertex b;
	    private Vertex c;
	    private Vertex d;

	    [SetUp]
	    public void SetUp() {
		    a = CreateVertex(A);
		    b = CreateVertex(B);
		    c = CreateVertex(C);
		    d = CreateVertex(D);
	    }


	    /**
	     * Tests that when there are no explicit Id value for Edge when it is constructed, 
	     * then the Id will be created as a concatenation of the Id's of the two vertices
	     */
	    [Test]
	    public void testDefaultEdgeIdValuesWhenNotExplicitlyDefined() {
		    IList<Edge> edges = new List<Edge>();
		    //edges.add(e)
			    // Note there are not explicit names of the edges when they are created  
		    edges.Add(CreateEdge(a, b, CreateWeight(WEIGHT_A_to_B))); // the edge Id will be a concatenation i.e. "A_B"
		    edges.Add(CreateEdge(a, c, CreateWeight(WEIGHT_A_to_C)));
		    edges.Add(CreateEdge(b, c, CreateWeight(WEIGHT_B_to_C)));
		    edges.Add(CreateEdge(b, d, CreateWeight(WEIGHT_B_to_D)));
		    edges.Add(CreateEdge(c, d, CreateWeight(WEIGHT_C_to_D)));

		    // There are three possible paths, below sorted in the best order regarding shortest total weight:
		    //    A to B to D (total cost: 13 = 5 + 8)
		    //    A to C to D (total cost: 15 = 6 + 9)
		    //    A to B to C to D (total cost: 21 = 5 + 7 + 9)
		    edgeIdValueStrategy = new EdgeIdValuesNotExplicitlySpecified(); // when not specified, they should be a concatenation
		    VerifyExpectedResults(edges);
	    }
	
	    /**
	     * Tests that the explicitly named edges will be used rather than just concatenation of vertex Ids.
	     * 
	     * This might look like an 'of course' for you, but it is not obvious that it should be like that 
	     * when you consider how the API works.
	     * One thing you should note is that the object INSTANCES of edges you send as a parameter to the PathFinderFactory 
	     * will be returned by the Graph as being aggregated parts of returned Path objects.
	     * The implementation will of course create its own objects and are not aware of your classes.
	     * To make this work, there has to be some kind of mapping.
	     * Previously, the mapping was done internally with the method Edge.getEdgeId but then it was a required part of the contract 
	     * for the Edge interface method that it had to be implemented as a concatenation of the Id's of the vertices.
	     * Now it is instead enough that the implementation of the adapters use the class EdgeMapper. 
	     * (which is changed now in the same commit as this comment was added, as a note in case that class would become renamed, you can trace it in git commit history) 
	     * The EdgeMappe takes care of the needed concatenation of the Ids for the vertices to retrieve abck the original instances. 
	     */
	    [Test]
	    public void TestCreateEdgesWithoutExplicitNames2() {
		    IList<Edge> edges = new List<Edge>();
		    edges.Add(CreateEdge(EDGE_A_to_B, a, b, CreateWeight(WEIGHT_A_to_B)));
		    edges.Add(CreateEdge(EDGE_A_to_C, a, c, CreateWeight(WEIGHT_A_to_C)));
		    edges.Add(CreateEdge(EDGE_B_to_C, b, c, CreateWeight(WEIGHT_B_to_C)));
		    edges.Add(CreateEdge(EDGE_B_to_D, b, d, CreateWeight(WEIGHT_B_to_D)));
		    edges.Add(CreateEdge(EDGE_C_to_D, c, d, CreateWeight(WEIGHT_C_to_D)));
		    edgeIdValueStrategy = new EdgeIdValueExplicitlySpecified();
		    VerifyExpectedResults(edges);
	    }
	

	    private void VerifyExpectedResults(IList<Edge> edges) {
		    // the parameter GraphEdgesValidationDesired.NO will be used so therefore do the validation once externally here first		
		    GraphEdgesValidator<Path, Edge, Vertex, Weight>.ValidateEdgesForGraphCreation<Path, Edge, Vertex, Weight>(edges);
		
		    ExpectedPath[] expectedPaths = GetExpectedPaths();

		    IList<PathFinderFactory> pathFinderFactories = PathFinderFactories.CreatePathFinderFactories();
		    foreach (PathFinderFactory pathFinderFactory in pathFinderFactories) {
			    VerifyExpectedPaths(a, d, edges, pathFinderFactory, expectedPaths);
		    }
	    }
	
	    private ExpectedPath[] GetExpectedPaths() {
		    ExpectedPath[] expectedPaths = new ExpectedPath[] {
				    new ExpectedPath(13, new ExpectedEdge[] {
						new ExpectedEdge(GetEdgeId(A, B), A, B, WEIGHT_A_to_B),
						new ExpectedEdge(GetEdgeId(B, D), B, D, WEIGHT_B_to_D)
				    }),
				    new ExpectedPath(15, new ExpectedEdge[] {
					    new ExpectedEdge(GetEdgeId(A, C), A, C, WEIGHT_A_to_C),
					    new ExpectedEdge(GetEdgeId(C, D), C, D, WEIGHT_C_to_D)
				    }),
				    new ExpectedPath(21, new ExpectedEdge[] {
					    new ExpectedEdge(GetEdgeId(A, B), A, B, WEIGHT_A_to_B),
					    new ExpectedEdge(GetEdgeId(B, C), B, C, WEIGHT_B_to_C),
					    new ExpectedEdge(GetEdgeId(C, D), C, D, WEIGHT_C_to_D)
				    })
		    };
		    return expectedPaths;
	    }

	    private string GetEdgeId(string startVertexId, string endVertexId) {
		    return edgeIdValueStrategy.GetEdgeId(startVertexId, endVertexId);
	    }

	    private void VerifyExpectedPaths(
		    Vertex startVertex, 
		    Vertex endVertex, 
		    IList<Edge> edges, 
		    PathFinderFactory pathFinderFactory,
		    ExpectedPath[] expectedShortestPaths
	    ) {
		    PathFinder pathFinder = pathFinderFactory.CreatePathFinder(
			    edges, 
			    GraphEdgesValidationDesired.NO // do the validation one time instead of doing it for each pathFinderFactory
		    );
		    IList<Path> actualShortestPaths = pathFinder.FindShortestPaths(startVertex, endVertex, 10);
		
		    Assert.AreEqual(expectedShortestPaths.Length, actualShortestPaths.Count);

		    string errorContext = pathFinderFactory.GetType().Name; 
		    for (int i = 0; i < expectedShortestPaths.Length; i++) {
			    errorContext += " , i: "+ i;
			    ExpectedPath expectedPath = expectedShortestPaths[i];
			    Path actualPath = actualShortestPaths[i];
			    Assert.AreEqual(expectedPath.totalWeight, actualPath.TotalWeightForPath.WeightValue, SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS, errorContext);
			    ExpectedEdge[] expectedEdgesForPath = expectedPath.expectedEdges;
			    IList<Edge> actualEdgesForPath = actualPath.EdgesForPath;
			    Assert.AreEqual(expectedEdgesForPath.Length, actualEdgesForPath.Count, errorContext);
			    for (int j = 0; j < expectedEdgesForPath.Length; j++) {
				    errorContext += " , j=" + j;
				    ExpectedEdge expectedEdge = expectedEdgesForPath[j];
				    Edge actualEdge = actualEdgesForPath[j];
				    Assert.NotNull(actualEdge, errorContext);
				    Assert.NotNull(actualEdge.EdgeWeight, errorContext);
				    Assert.NotNull(actualEdge.StartVertex, errorContext);
				    Assert.NotNull(actualEdge.EndVertex, errorContext);
				    Assert.AreEqual(expectedEdge.weight, actualEdge.EdgeWeight.WeightValue, SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS, errorContext);
				    Assert.AreEqual(expectedEdge.startVertexId, actualEdge.StartVertex.VertexId, errorContext);
				    Assert.AreEqual(expectedEdge.endVertexId, actualEdge.EndVertex.VertexId, errorContext);
				    Assert.AreEqual(expectedEdge.edgeId, actualEdge.EdgeId, errorContext);
			    }			
		    }
	    }

	    public class ExpectedPath {
		    public double totalWeight;
		    public ExpectedEdge[] expectedEdges;
		    public ExpectedPath(double totalWeight, ExpectedEdge[] expectedEdges) {
			    this.totalWeight = totalWeight;
			    this.expectedEdges = expectedEdges;
		    }
	    }
	    public class ExpectedEdge {
		    public string edgeId;
		    public string startVertexId;
		    public string endVertexId;
		    public double weight;
		    public ExpectedEdge(string edgeId, string startVertexId, string endVertexId, double weight) {
			    this.edgeId = edgeId;
			    this.startVertexId = startVertexId;
			    this.endVertexId = endVertexId;
			    this.weight = weight;
		    }
	    }
	
	    public interface EdgeIdValueStrategy {
		    string GetEdgeId(string startVertexId, string endVertexId);
	    }
	
	    public class EdgeIdValuesNotExplicitlySpecified : EdgeIdValueStrategy {
		    public string GetEdgeId(string startVertexId, string endVertexId) {
			    return EdgeGenericsImpl<Vertex, Weight>.CreateEdgeIdValue(startVertexId, endVertexId);
		    }
	    }

	    public class EdgeIdValueExplicitlySpecified : EdgeIdValueStrategy {
		    private IDictionary<string, string> edgeNameMappedFromIdsOfVertices;
		    public EdgeIdValueExplicitlySpecified() {
			    edgeNameMappedFromIdsOfVertices = new Dictionary<string, string>();
			    edgeNameMappedFromIdsOfVertices.Add(A + B, EDGE_A_to_B);
			    edgeNameMappedFromIdsOfVertices.Add(A + C, EDGE_A_to_C);
			    edgeNameMappedFromIdsOfVertices.Add(B + C, EDGE_B_to_C);
			    edgeNameMappedFromIdsOfVertices.Add(B + D, EDGE_B_to_D);
			    edgeNameMappedFromIdsOfVertices.Add(C + D, EDGE_C_to_D);			
		    }
		    public string GetEdgeId(string startVertexId, string endVertexId) {
			    return edgeNameMappedFromIdsOfVertices[startVertexId + endVertexId];
		    }
	    }	
    }
}