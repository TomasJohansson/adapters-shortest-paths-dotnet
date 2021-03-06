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
using System.Collections.Generic;
using static Programmerare.ShortestPaths.Core.Impl.WeightImpl; // SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS
using static Programmerare.ShortestPaths.Core.Impl.EdgeImpl; // createEdge
using static Programmerare.ShortestPaths.Core.Impl.VertexImpl; // createVertex
using Programmerare.ShortestPaths.Graphs.Utils;
using System;

namespace Programmerare.ShortestPaths.Graphs.Tests
{
    /**
     * This test class can generate a big graph, for example 1000 nodes (and then some more edges)
     * and how many of the best paths that should be find, and it searches for paths 
     * with all implementations (currently three) and verifies them against each other,
     * i.e. if all different independent implementations in the same result, you should be able to 
     * feel pretty sure that the result is correct.
     * 
     * It can be run either as a junit test or through the main method.
     * The difference is that when running as "main method program" then there 
     * will be output printed to the console window to display the time it takes for the different implementations.
     * 
     */
    //[TestFixture]
    [Obsolete] // the test data constructed by this class is now instead in the file "adapters-shortest-paths-dotnet\Programmerare.ShortestPaths.Test\test\Programmerare.ShortestPaths.Adapter.Implementations\resources\test_graphs\generated_graph.xml"
    // please also see the comments within that xml file
    public class BigGraphTest {

	    private bool isExecutingThroughTheMainMethod = false;
	
	    public static void Main(string[] args) {
		    BigGraphTest implementationComparisonTest = new BigGraphTest();
		    implementationComparisonTest.isExecutingThroughTheMainMethod = true;

		    const int numberOfVertices = 500;
		    const int numberOfPathsToFind = 50;
		    implementationComparisonTest.TestProgrammaticallyGeneratedGraph(
			    numberOfVertices,
			    numberOfPathsToFind				
		    );
            // TODO: adjust the below java comments to the C# implementation ...
		    // resulting oputput with 500 and 50 on my machine:
		    // seconds : 0 for implementation com.programmerare.shortestpaths.adapter.impl.yanqi.GraphYanQi
		    // seconds : 1 for implementation com.programmerare.shortestpaths.adapter.impl.bsmock.GraphBsmock
		    // seconds : 50 for implementation com.programmerare.shortestpaths.adapter.impl.jgrapht.GraphJgrapht		
	    }


	    //[Test] // see comment above at the class level where the class is labeled Obsolete
	    public void TestProgrammaticallyGeneratedGraph() {	
		    const int numberOfVertices = 200;
		    const int numberOfPathsToFind = 20;
		    TestProgrammaticallyGeneratedGraph(
			    numberOfVertices,
			    numberOfPathsToFind				
		    );
	    }
	
	    private void TestProgrammaticallyGeneratedGraph(
		    int numberOfVertices, 
		    int numberOfPathsToFind
	    ) {
		    IList<Edge> edgesForBigGraph = CreateEdgesForBigGraph(numberOfVertices);
		    Vertex startVertex = edgesForBigGraph[0].StartVertex;
		    Vertex endVertex = edgesForBigGraph[edgesForBigGraph.Count-1].EndVertex;
		    IList<PathFinderFactory> pathFinderFactories = PathFinderFactories.CreatePathFinderFactories();
		
		    GraphShortestPathAssertionHelper graphShortestPathAssertionHelper = new GraphShortestPathAssertionHelper(isExecutingThroughTheMainMethod);
		    graphShortestPathAssertionHelper.TestResultsWithImplementationsAgainstEachOther(edgesForBigGraph, startVertex, endVertex, numberOfPathsToFind, pathFinderFactories);
	    }

	    private IList<Edge> CreateEdgesForBigGraph(int numberOfVertices) {
		    IList<Vertex> vertices = CreateVertices(numberOfVertices);
		    Assert.AreEqual(numberOfVertices, vertices.Count);
		
		    double decreasingWeightValue = 10 * numberOfVertices;
		
		    IList<Edge> edges = new List<Edge>();
		
		    for(int i=0; i<numberOfVertices-1; i++) {
			    edges.Add(CreateEdge(vertices[i], vertices[i+1], CreateWeight(--decreasingWeightValue)));
		    }
		    Assert.AreEqual(numberOfVertices-1, edges.Count);
				
		    for(int i=0; i<numberOfVertices-10; i+=10) {
			    edges.Add(CreateEdge(vertices[i], vertices[i+10], CreateWeight(--decreasingWeightValue)));
		    }
		
		    for(int i=0; i<numberOfVertices-100; i+=100) {
			    decreasingWeightValue--;
			    edges.Add(CreateEdge(vertices[i], vertices[i+100], CreateWeight(--decreasingWeightValue)));
		    }		

		    // now construct a really short path with the smallest value as each weight and only a few edges from start vertex to end vertex
		    int numberOfVerticesToSkip = numberOfVertices / 4;
		    // totally five vertices including start and end i.e. three between 
		    Vertex secondVertex = vertices[numberOfVerticesToSkip];
		    Vertex thirdVertex =  vertices[numberOfVerticesToSkip * 2];
		    Vertex fourthVertex =  vertices[numberOfVerticesToSkip * 3];
		
		    Vertex startVertex = vertices[0];
		    Vertex endVertex = vertices[numberOfVertices-1];
		
		    edges.Add(CreateEdge(startVertex, secondVertex, CreateWeight(--decreasingWeightValue)));
		    edges.Add(CreateEdge(secondVertex, thirdVertex, CreateWeight(--decreasingWeightValue)));
		    edges.Add(CreateEdge(thirdVertex, fourthVertex, CreateWeight(--decreasingWeightValue)));
		    edges.Add(CreateEdge(fourthVertex, endVertex, CreateWeight(--decreasingWeightValue)));
		
		    Assert.IsTrue(decreasingWeightValue > 0); 
		
		    return edges;
	    }

	    private IList<Vertex> CreateVertices(int numberOfVerticesToCreate) {
		    IList<Vertex> vertices = new List<Vertex>();
		    for(int i=1; i<=numberOfVerticesToCreate; i++) {
			    vertices.Add(CreateVertex(i));
		    }
		    return vertices;
	    }

    }
}