using System;
using System.Collections.Generic;
using System.Linq;
using QuickGraph.Algorithms.RankedShortestPath;
using QuickGraph;
using NUnit.Framework;

//QuickGraph 3.6
//https://archive.codeplex.com/?p=quickgraph

//YC.QuickGraph 3.7.3
//https://github.com/YaccConstructor/QuickGraph/releases

//According to download information from NuGet:

//QuickGraph 3.6.61119.7
//November 20, 2011 
//286 k downloads

//YC.QuickGraph 3.7.3
//Aug 23, 2016
//13.9 k downloads

// Regarding above version 3.7.3
// see the folder QuickGraphTest_3_7_3 
// and also the following bug report:
// https://github.com/YaccConstructor/QuickGraph/issues/178

namespace dotnet_adapters_shortest_paths_test.QuickGraphTest_3_6_6
{
    // <package id="QuickGraph" version="3.6.61119.7" targetFramework="net472" />
    [TestFixture]
    public class QuickGraph_3_6_6_Test { // QuickGraph 3.6.6
        [Test]
        public void TestQuickGraph_3_6_6() {
            // TODO: implement actual assertions i.e. real test code
            var vertexA = "A";
            var vertexB = "B";
            var vertexC = "C";
            var vertexD = "D";

            var edgeAB = new Edge<string>(vertexA, vertexB);
            var edgeAC = new Edge<string>(vertexA, vertexC);
            var edgeBC = new Edge<string>(vertexB, vertexC);
            var edgeBD = new Edge<string>(vertexB, vertexD);
            var edgeCD = new Edge<string>(vertexC, vertexD);
            
            var g = new BidirectionalGraph<string, Edge<string>>(); // AdjacencyGraph<TVertex,TEdge> 
            g.AddVertex(vertexA);
            g.AddVertex(vertexB);
            g.AddVertex(vertexC);
            g.AddVertex(vertexD);
            g.AddEdge(edgeAB);
            g.AddEdge(edgeAC);
            g.AddEdge(edgeBC);
            g.AddEdge(edgeBD);
            g.AddEdge(edgeCD);

            var weights = new Dictionary<Edge<string>, double>();
            weights.Add(edgeAB, 5);
            weights.Add(edgeAC, 6);
            weights.Add(edgeBC, 7);
            weights.Add(edgeBD, 8);
            weights.Add(edgeCD, 9);

            HoffmanPavleyRankedShortestPathAlgorithmm(
                g, 
                weights,
                vertexA,
                vertexD,
                g.VertexCount
            );
        }

        // based on code example found here: https://github.com/rhishi/QuickGraph/blob/2f92ed688182f9f0d97b03a8a54dc761ded9b59d/3.0/sources/QuickGraph.Tests/Algorithms/RankedShortestPath/HoffmanPavleyRankedShortestPathAlgorithmTest.cs
        private IEnumerable<IEnumerable<TEdge>> HoffmanPavleyRankedShortestPathAlgorithmm<TVertex,TEdge>(
            IBidirectionalGraph<TVertex, TEdge> g,
            Dictionary<TEdge, double> edgeWeights,
            TVertex rootVertex,
            TVertex goalVertex,
            int pathCount
        )
            where TEdge : IEdge<TVertex>
        {
            var target = new HoffmanPavleyRankedShortestPathAlgorithm<TVertex, TEdge>(g, e => edgeWeights[e]);
            target.ShortestPathCount = pathCount;
            target.Compute(rootVertex, goalVertex);

            double lastWeight = double.MinValue;
            foreach (var path in target.ComputedShortestPaths)
            {
                var edges = path.ToArray();
                foreach(var edge in edges) {
                    Console.WriteLine("edge.Source : " + edge.Source);
                    Console.WriteLine("edge.Target : " + edge.Target);
                }
                Console.WriteLine("path: {0}", Enumerable.Sum(path, e => edgeWeights[e]));
                double weight = Enumerable.Sum(path, e => edgeWeights[e]);
                Assert.IsTrue(lastWeight <= weight, "{0} <= {1}", lastWeight, weight);
                Assert.AreEqual(rootVertex, Enumerable.First(path).Source);
                Assert.AreEqual(goalVertex, Enumerable.Last(path).Target);
                Assert.IsTrue(EdgeExtensions.IsPathWithoutCycles<TVertex, TEdge>(path));

                lastWeight = weight;
            }
            return target.ComputedShortestPaths;
        }
    }
}
//https://github.com/rhishi/QuickGraph/blob/master/3.0/sources/QuickGraph.Tests/Algorithms/RankedShortestPath/HoffmanPavleyRankedShortestPathAlgorithmTest.cs
// https://github.com/rhishi/QuickGraph/blob/2f92ed688182f9f0d97b03a8a54dc761ded9b59d/3.0/sources/QuickGraph/UndirectedGraph.cs
// https://github.com/rhishi/QuickGraph/blob/2f92ed688182f9f0d97b03a8a54dc761ded9b59d/3.0/sources/QuickGraph/AdjacencyGraph.cs
/// A mutable directed graph data structure efficient for sparse
/// graph representation where out-edge need to be enumerated only.
// https://github.com/rhishi/QuickGraph/blob/2f92ed688182f9f0d97b03a8a54dc761ded9b59d/3.0/sources/QuickGraph/BidirectionalGraph.cs
/// mutable directed graph data structure efficient for sparse
/// graph representation where out-edge and in-edges need to be enumerated. Requires
/// twice as much memory as the adjacency graph.
// https://github.com/rhishi/QuickGraph/blob/master/3.0/sources/QuickGraph.Tests/Algorithms/RankedShortestPath/HoffmanPavleyRankedShortestPathAlgorithmTest.cs