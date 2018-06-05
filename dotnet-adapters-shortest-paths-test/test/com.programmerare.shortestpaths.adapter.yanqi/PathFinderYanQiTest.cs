using NUnit.Framework;
using com.programmerare.shortestpaths.adapter.yanqi;
using static NUnit.Framework.Assert;
using com.programmerare.shortestpaths.core.validation;
using System.Collections.Generic;
using com.programmerare.shortestpaths.core.api;
using static com.programmerare.shortestpaths.core.impl.GraphImpl;
using static com.programmerare.shortestpaths.core.impl.WeightImpl; // SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS
using static com.programmerare.shortestpaths.core.impl.EdgeImpl; // createEdge
using static com.programmerare.shortestpaths.core.impl.VertexImpl; // createVertex

namespace dotnet_adapters_shortest_paths_test.test.com.programmerare.shortestpaths.adapter.yanqi
{
    [TestFixture]
    class PathFinderYanQiTest
    {
        private PathFinder pathFinder;
        Vertex a, b, c, d;

        [SetUp]
        public void SetUp()
        {
	        a = createVertex("A");
	        b = createVertex("B");
	        c = createVertex("C");
	        d = createVertex("D");            
	        
            IList<Edge> edges = new List<Edge>{
		        createEdge(a, b, createWeight(5)),
		        createEdge(a, c, createWeight(6)),
		        createEdge(b, c, createWeight(7)),
		        createEdge(b, d, createWeight(8)),
		        createEdge(c, d, createWeight(9))
            };
            Graph graph = createGraph(edges, GraphEdgesValidationDesired.YES); 
            PathFinderFactory pathFinderFactory = new PathFinderFactoryYanQi();
            pathFinder = pathFinderFactory.createPathFinder(graph);
        }
        
        [Test]
        public void PathFinderTest()
        {
            IList<Path> shortestPaths = pathFinder.findShortestPaths(a, d, 10);
            AreEqual(3, shortestPaths.Count);

            // path1 : A -> B -> D (with total weight 13)
            assertPath(shortestPaths[0], 13, "A", "B", "D");

            // path2 : A -> C -> D (with total weight 15)
            assertPath(shortestPaths[1], 15, "A", "C", "D");

            // path3 : A -> B -> C -> D (with total weight 21)
            assertPath(shortestPaths[2], 21, "A", "B", "C", "D");
        }

        private void assertPath(Path path, double expectedTotalWeight, params string[] expectedVertices)
        {
            AreEqual(expectedTotalWeight, path.getTotalWeightForPath().getWeightValue(), SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
            var edges = path.getEdgesForPath();
            AreEqual(expectedVertices[0], edges[0].getStartVertex().getVertexId());
            AreEqual(expectedVertices.Length, edges.Count + 1); // one more since each edge contain two nodes
            for(int i=0; i<edges.Count; i++)
            {
                AreEqual(expectedVertices[i+1], edges[i].getEndVertex().getVertexId());
            }
        }
    }
}