using System;
using System.Linq;
using dotnet_adapters_shortest_paths_test.test.QuickGraphTest;
using NUnit.Framework;
using QuickGraph;
using QuickGraph.Algorithms.ShortestPath.Yen;

// Test classes for QuickGraph:
// PathFinderYanQiTest (using the adapters API)
// QuickGraphTest/QuickGraphTest2 (NOT using the adapters API, i.e. direct usage of QuickGraph)

namespace dotnet_adapters_shortest_paths_impl_quickgraph.QuickGraphTest
{
    [TestFixture]
    public class QuickGraphTest2
    {

        [Test]
        public void Test()
        {
            //     const string graphDefinition = @"
            //         A B 5
            //         A C 6
            //         B C 7
            //         B D 8
            //         C D 9    
            //     ";
            //     const string expectedPaths = @"
            //13 A B D
            //15 A C D
            //21 A B C D
            //     ";
            //     var expectedTestResult = new ExpectedTestResult(expectedPathsString: @"
            //13 A B D
            //15 A C D
            //21 A B C D
            //     ");
            //     const string graphDefinition = @"
            //         F G 13
            //         F H 14
            //         F I 15
            //         F J 16
            //         G H 17
            //         G I 18
            //         G J 19
            //         H I 20
            //         H J 21
            //         J K 22    
            //     ";
            //     const string expectedPaths = @"
            //38 F J K
            //54 F G J K
            //57 F H J K
            //73 F G H J K
            //     ";
            const string graphDefinition = @"
                0 1 5.0
                0 2 5.5
                0 3 4.0
                0 4 7.5
                0 8 3.0
                0 9 2.5
                1 4 1.5
                2 3 5.0
                4 2 4.6
                1 3 2.5
                4 1 3.0
                3 4 7.5
                3 1 5.5
                2 0 3.5
                5 0 2.5
                4 5 4.0
                2 5 3.0
                5 3 1.5
                1 5 5.0
                5 2 6.5
                6 3 7.0
                6 10 5.2
                2 6 3.4
                6 0 2.5
                1 6 2.5
                5 6 3.5
                7 6 2.5
                7 2 5.5
                7 10 8.0
                1 7 1.8
                5 7 3.5
                4 7 6.5
                2 8 4.0
                7 8 3.0
                8 3 1.5
                8 5 2.0
                8 1 1.5
                9 2 3.5
                9 5 3.0
                10 1 3.5
                10 6 4.0    
            ";
            // the above and below strings are from the java project
            // ...\adapters-shortest-paths-test\src\test\resources\test_graphs\origin_bsmock\tiny_graph_01.xml
            const string expectedPaths = @"
			    7.7 1 6 10
			    9.5 1 7 6 10
			    9.8 1 7 10
			    13.7 1 5 6 10
			    14.2 1 4 5 6 10
			    14.7 1 4 2 6 10
			    15.5 1 7 8 5 6 10
			    15.7 1 4 7 6 10
			    15.9 1 7 2 6 10
			    16 1 4 7 10
			    16.2 1 5 7 6 10
			    16.5 1 5 7 10
			    16.7 1 4 5 7 6 10
			    17 1 4 5 7 10
			    17.8 1 4 2 5 6 10
			    19 1 7 2 5 6 10
			    20.1 1 5 2 6 10
			    20.3 1 4 2 5 7 6 10
			    20.6 1 4 5 2 6 10
			    20.6 1 4 2 5 7 10
			    20.8 1 4 2 8 5 6 10
			    21.5 1 6 0 8 5 7 10
			    21.6 1 5 0 2 6 10
			    21.7 1 4 7 8 5 6 10
			    21.9 1 7 8 5 2 6 10
            ";

            var expectedTestResult = new ExpectedTestResult(expectedPathsString: expectedPaths);

            var graphTestData = new GraphTestData(
                graphDefinition: graphDefinition, 
                startVertex: "1", // "F", 
                endVertex: "10", // "K", 
                maxNumberOfPaths : 250, 
                expectedPaths : expectedPaths
            );



            //var graph = new AdjacencyGraph<string, TaggedEquatableEdge<string,double>>(false);
            //graph.AddVertexRange(graphTestData.vert);

            test(graphTestData, expectedTestResult);
        }

        private void test(GraphTestData graphTestData, ExpectedTestResult expectedTestResult)
        {
            var graph = new AdjacencyGraph<string, TaggedEquatableEdge<string, double>>(false);
            
            //graph.AddVertexRange(new List<string>{"A", "B", "C", "D"});
            //graph.AddEdge(new TaggedEquatableEdge<string, double>("A", "B", 5));
            //graph.AddEdge(new TaggedEquatableEdge<string, double>("A", "C", 6));
            //graph.AddEdge(new TaggedEquatableEdge<string, double>("B", "C", 7));
            //graph.AddEdge(new TaggedEquatableEdge<string, double>("B", "D", 8));
            //graph.AddEdge(new TaggedEquatableEdge<string, double>("C", "D", 9));

            graph.AddVertexRange(graphTestData.Vertices);
            foreach(TestEdge e in graphTestData.Edges)
            {
                graph.AddEdge(new TaggedEquatableEdge<string, double>(e.StartVertex, e.EndVertex, e.Weight));
            }

            
            var yen = new YenShortestPathsAlgorithm<string>(graph, graphTestData.StartVertex, graphTestData.EndVertex, graphTestData.MaxNumberOfPaths);
            testYen(yen, expectedTestResult);
        }

        private void testYen(YenShortestPathsAlgorithm<string> yen, ExpectedTestResult expectedTestResult)
        {
            var expectedPaths = expectedTestResult.ExpectedPaths;
            var actualPaths = yen.Execute().ToList();
            //Assert.AreEqual(expectedPaths.Count, actualPaths.Count);
            foreach(var p in actualPaths)
            {
                var es = p.ToList();
                Console.WriteLine("path edges below");
                foreach(var ee in es)
                {
                    Console.WriteLine(ee.Source + " , " + ee.Target + " , " + ee.Tag);
                }
            }

        }
    }
}
