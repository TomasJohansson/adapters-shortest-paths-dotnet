using NUnit.Framework;
using com.programmerare.shortestpaths.adapter.yanqi;
using static NUnit.Framework.Assert;
using com.programmerare.shortestpaths.core.validation;
using System;
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
            var path1 = shortestPaths[0];
            var path2 = shortestPaths[1];
            var path3 = shortestPaths[2];

            AreEqual(13, path1.getTotalWeightForPath().getWeightValue(), SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
            AreEqual(15, path2.getTotalWeightForPath().getWeightValue(), SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
            AreEqual(21, path3.getTotalWeightForPath().getWeightValue(), SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);

            AreEqual(2, path1.getEdgesForPath().Count);
            AreEqual(2, path2.getEdgesForPath().Count);
            AreEqual(3, path3.getEdgesForPath().Count);

            // all starts att A and ends at D

            // path1 : A -> B -> D
            AreEqual("A", path1.getEdgesForPath()[0].getStartVertex().getVertexId());
            AreEqual("B", path1.getEdgesForPath()[0].getEndVertex().getVertexId());
            AreEqual("D", path1.getEdgesForPath()[1].getEndVertex().getVertexId());

            // path2 : A -> C -> D
            AreEqual("A", path2.getEdgesForPath()[0].getStartVertex().getVertexId());
            AreEqual("C", path2.getEdgesForPath()[0].getEndVertex().getVertexId());
            AreEqual("D", path2.getEdgesForPath()[1].getEndVertex().getVertexId());

            // path3 : A -> B -> C -> D
            AreEqual("A", path3.getEdgesForPath()[0].getStartVertex().getVertexId());
            AreEqual("B", path3.getEdgesForPath()[0].getEndVertex().getVertexId());
            AreEqual("C", path3.getEdgesForPath()[1].getEndVertex().getVertexId());
            AreEqual("D", path3.getEdgesForPath()[2].getEndVertex().getVertexId());

          //  foreach (Path path in shortestPaths) {
		        //Weight totalWeightForPath = path.getTotalWeightForPath();
		        //Console.WriteLine(totalWeightForPath);
		        //IList<Edge> pathEdges = path.getEdgesForPath();
          //  }
        }
    }
}