// Test classes for QuickGraph:
// PathFinderYanQiTest (using the adapters API)
// QuickGraphTest/QuickGraphTest2 (NOT using the adapters API, i.e. direct usage of QuickGraph)

using NUnit.Framework;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dotnet_adapters_shortest_paths_impl_quickgraph.QuickGraphTest
{

    [TestFixture]
    public class QuickGraphTest
    {
        [Test]
        public void Test()
        {
            // Version (retrieved with NuGet) used for this test:
            //  <package id="YC.QuickGraph" version="3.7.3" targetFramework="net472" />
            var graph = new AdjacencyGraph<string, TaggedEquatableEdge<string, double>>(false);
            graph.AddVertexRange(new List<string> { "A", "B", "C", "D" });
            graph.AddEdge(new TaggedEquatableEdge<string, double>("A", "B", 5));
            graph.AddEdge(new TaggedEquatableEdge<string, double>("A", "C", 6));
            graph.AddEdge(new TaggedEquatableEdge<string, double>("B", "C", 7));
            graph.AddEdge(new TaggedEquatableEdge<string, double>("B", "D", 8));
            graph.AddEdge(new TaggedEquatableEdge<string, double>("C", "D", 9));
            var yen = new YenShortestPathsAlgorithm<string>(graph, "A", "D", 5);
            // The three paths *should* be:
            // A -> B -> D (weight: 13 = 5 + 8)
            // A -> C -> D (weight: 15 = 6 + 9)
            // A -> B -> C -> D (weight: 21 = 5 + 7 + 9)
            var actualPaths = yen.Execute().ToList();
            foreach(var path in actualPaths)
            {
                var edges = path.ToList();
                Console.WriteLine();
                Console.Write(edges[0].Source);
                for(int i=0; i<edges.Count; i++) {
                    Console.Write(" -> " +edges[i].Target);
                }
            }
            // Output from above loops:
            //  A -> B -> D
            //  A -> C -> D
            // The last path is missing (i.e. A -> B -> C -> D)
            Assert.AreEqual(3, actualPaths.Count); // Failure: Expected: 3 But was:  2
        }
    }
}
// The above code example is reported as an issue at QuickGraph github:
// https://github.com/YaccConstructor/QuickGraph/issues/178