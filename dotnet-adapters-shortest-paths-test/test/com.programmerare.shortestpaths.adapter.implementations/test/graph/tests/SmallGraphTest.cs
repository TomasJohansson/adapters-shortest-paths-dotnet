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
using System.Collections.Generic;
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.validation;
using com.programmerare.shortestpaths.adapter.bsmock;
using com.programmerare.shortestpaths.adapter.yanqi;
using static com.programmerare.shortestpaths.core.impl.WeightImpl; // SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS
using static com.programmerare.shortestpaths.core.impl.EdgeImpl; // createEdge
using static com.programmerare.shortestpaths.core.impl.VertexImpl; // createVertex

namespace com.programmerare.shortestpaths.graph.tests
{
    /**
     * Tests for a small graph. It is small for the purpose that it should be easy to understand.
     * The tested graph have four vertices A,B,C,D with edges as below 
     * (from start vertex to start vertex and then the weight within parenthesis)
     * A --> B (3)
     * B --> C (5)
     * C --> D (7) 
     * B --> D (13)
     *
     */
    [TestFixture]
    public class SmallGraphTest {

	    [Test]
	    public void TestFindShortestPaths_Bsmock() {
		    TestFindShortestPaths(
			    new PathFinderFactoryBsmock()
		    );		
	    }	
	
	    //[Test]
	    //public void TestFindShortestPaths_QuickGraph() {
		   // TestFindShortestPaths(
			  //  new PathFinderFactoryQuickGraph()
		   // );		
	    //}
	
	    [Test]
	    public void testFindShortestPaths_YanQi() {
		    TestFindShortestPaths(
			    new PathFinderFactoryYanQi()
		    );
	    }

	    //[Test]
	    //public void TestFindShortestPaths_Parrisha() {
		   // TestFindShortestPaths(
			  //  new PathFinderFactoryParrisha()
		   // );		
	    //}
        
	    public void TestFindShortestPaths(
			    PathFinderFactory pathFinderFactory
	    ) {
		    Edge edgeAB3 = CreateEdge(CreateVertex("A"), CreateVertex("B"), CreateWeight(3));
		    Edge edgeBC5 = CreateEdge(CreateVertex("B"), CreateVertex("C"), CreateWeight(5));
		    Edge edgeCD7 = CreateEdge(CreateVertex("C"), CreateVertex("D"), CreateWeight(7));
		    Edge edgeBD13= CreateEdge(CreateVertex("B"), CreateVertex("D"), CreateWeight(13));
		
		    IList<Edge> edges = new List<Edge>();
		    edges.Add(edgeAB3);
		    edges.Add(edgeBC5);
		    edges.Add(edgeCD7);
		    edges.Add(edgeBD13);
		    // There are two ways from A to C in a Graph with the above edges:
		    // A - B - C- D  	, with weight 15 ( 3 + 5 + 7 )
		    // A - B - D  		, with weight 16 ( 3 + 13 )
	
		    PathFinder pathFinder = pathFinderFactory.CreatePathFinder(
			    edges,
			    GraphEdgesValidationDesired.YES // TODO: refactor the construction of edges to able to do the validation only once instead of doing it for each factory
		    );
		    IList<Path> shortestPaths = pathFinder.FindShortestPaths(CreateVertex("A"), CreateVertex("D"), 5); // max 5 but actually we should only find 2
		    Assert.AreEqual(2,  shortestPaths.Count);

		    Path path = shortestPaths[0]; // the shortest mentioned above with total weight 15
		    Assert.AreEqual(15,  path.TotalWeightForPath.WeightValue, SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
		    IList<Edge> edgesForPath = path.EdgesForPath;
		    Assert.AreEqual(3,  edgesForPath.Count);
		    assertEqualsAndTheSameInstance(edgeAB3,  edgesForPath[0]);
		    assertEqualsAndTheSameInstance(edgeBC5,  edgesForPath[1]);
		    assertEqualsAndTheSameInstance(edgeCD7,  edgesForPath[2]);
    //		
    //
		    Path path2 = shortestPaths[1];
		    Assert.AreEqual(16,  path2.TotalWeightForPath.WeightValue, SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
		    IList<Edge> edgesForPath2 = path2.EdgesForPath;
		    Assert.AreEqual(2,  edgesForPath2.Count);
		    assertEqualsAndTheSameInstance(edgeAB3,  edgesForPath2[0]);
		    assertEqualsAndTheSameInstance(edgeBD13,  edgesForPath2[1]);
	    }

	    private void assertEqualsAndTheSameInstance(Edge edgeFromOriginalInput, Edge edgeFromResultingPath) {
		    Assert.AreEqual(edgeFromOriginalInput,  edgeFromResultingPath);
		    // Note that the below assertion works thanks to the class EdgeMappe
		    Assert.AreSame(edgeFromOriginalInput, edgeFromResultingPath);
	    }
    }

}