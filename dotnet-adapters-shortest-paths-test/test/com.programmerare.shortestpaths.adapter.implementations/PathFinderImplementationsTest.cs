using NUnit.Framework;
using com.programmerare.shortestpaths.adapter.yanqi;
using com.programmerare.shortestpaths.adapter.bsmock;
using com.programmerare.shortestpaths.adapter.quickgraph;
using com.programmerare.shortestpaths.adapter.parrisha;
using static NUnit.Framework.Assert;
using com.programmerare.shortestpaths.core.validation;
using System.Collections.Generic;
using com.programmerare.shortestpaths.core.api;
using static com.programmerare.shortestpaths.core.impl.GraphImpl;
using static com.programmerare.shortestpaths.core.impl.WeightImpl; // SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS
using static com.programmerare.shortestpaths.core.impl.EdgeImpl; // createEdge
using static com.programmerare.shortestpaths.core.impl.VertexImpl; // createVertex

// Regarding QuickGraph there are two test classes:
// 	QuickGraph_3_6_6_Test (NOT using the adapters API, i.e. direct usage of QuickGraph)
// 	PathFinderImplementationsTest (this file, using the adapters API)

namespace dotnet_adapters_shortest_paths_test.test.com.programmerare.shortestpaths.adapter.yanqi
{
    [TestFixture]
    class PathFinderImplementationsTest
    {
        private Graph graph;
        private PathFinder pathFinder;
        Vertex a, b, c, d;

        [SetUp]
        public void SetUp()
        {
	        a = CreateVertex("A");
	        b = CreateVertex("B");
	        c = CreateVertex("C");
	        d = CreateVertex("D");            
	        
            IList<Edge> edges = new List<Edge>{
		        CreateEdge(a, b, CreateWeight(5)),
		        CreateEdge(a, c, CreateWeight(6)),
		        CreateEdge(b, c, CreateWeight(7)),
		        CreateEdge(b, d, CreateWeight(8)),
		        CreateEdge(c, d, CreateWeight(9))
            };
            graph = CreateGraph(edges, GraphEdgesValidationDesired.YES); 
        }

        [Test]
        public void PathFinderTestYanQi()
        {
            PathFinderTest(new PathFinderFactoryYanQi());
        }

        [Test]
        public void PathFinderTestParrisha()
        {
            PathFinderTest(new PathFinderFactoryParrisha());
        }

        [Test]
        public void PathFinderTestBSmock()
        {
            PathFinderTest(new PathFinderFactoryBsmock());
        }

        [Test]
        public void PathFinderTestQuickGraph()
        {
            PathFinderTest(new PathFinderFactoryQuickGraph());
        }
		
        private void PathFinderTest(PathFinderFactory pathFinderFactory)
        {
            pathFinder = pathFinderFactory.CreatePathFinder(graph);

            IList<Path> shortestPaths = pathFinder.FindShortestPaths(a, d, 10);
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
            AreEqual(expectedTotalWeight, path.TotalWeightForPath.WeightValue, SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
            var edges = path.EdgesForPath;
            AreEqual(expectedVertices[0], edges[0].StartVertex.VertexId);
            AreEqual(expectedVertices.Length, edges.Count + 1); // one more since each edge contain two nodes
            for(int i=0; i<edges.Count; i++)
            {
                AreEqual(expectedVertices[i+1], edges[i].EndVertex.VertexId);
            }
        }
    }
}