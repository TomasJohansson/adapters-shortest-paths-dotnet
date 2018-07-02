using System;
using System.Collections.Generic;
using System.Linq;
using QuickGraph.Algorithms.RankedShortestPath;
using QuickGraph;
using NUnit.Framework;

// QuickGraph 3.6
// https://archive.codeplex.com/?p=quickgraph

// YC.QuickGraph 3.7.3
// https://github.com/YaccConstructor/QuickGraph/releases

// According to download information from NuGet:

// QuickGraph 3.6.61119.7
// November 20, 2011 
// 286 k downloads

// YC.QuickGraph 3.7.3
// Aug 23, 2016
// 13.9 k downloads

// Regarding above version 3.7.3
// see the folder QuickGraphTest_3_7_3 
// and also the following bug report:
// https://github.com/YaccConstructor/QuickGraph/issues/178

namespace dotnet_adapters_shortest_paths_test.QuickGraphTest_3_6_6
{
    // <package id="QuickGraph" version="3.6.61119.7" targetFramework="net472" />
    [TestFixture]
    public class QuickGraph_3_6_6_Test { // QuickGraph 3.6.6
        private const double DELTA = 0.000001;

        private String vertexA, vertexB, vertexC, vertexD;
        private Edge<string> edgeAB, edgeAC, edgeBC, edgeBD, edgeCD;
        private double weightAB, weightAC, weightBC, weightBD, weightCD;
        private BidirectionalGraph<string, Edge<string>> graph;
        private Dictionary<Edge<string>, double> edgeWeights;

        [SetUp]
        public void SetUp() {
            vertexA = "A";
            vertexB = "B";
            vertexC = "C";
            vertexD = "D";

            edgeAB = new Edge<string>(vertexA, vertexB);
            edgeAC = new Edge<string>(vertexA, vertexC);
            edgeBC = new Edge<string>(vertexB, vertexC);
            edgeBD = new Edge<string>(vertexB, vertexD);
            edgeCD = new Edge<string>(vertexC, vertexD);
            
            graph = new BidirectionalGraph<string, Edge<string>>();
            graph.AddVertex(vertexA);
            graph.AddVertex(vertexB);
            graph.AddVertex(vertexC);
            graph.AddVertex(vertexD);
            graph.AddEdge(edgeAB);
            graph.AddEdge(edgeAC);
            graph.AddEdge(edgeBC);
            graph.AddEdge(edgeBD);
            graph.AddEdge(edgeCD);

            weightAB = 5;
            weightAC = 6;
            weightBC = 7;
            weightBD = 8;
            weightCD = 9;

            edgeWeights = new Dictionary<Edge<string>, double>();
            edgeWeights.Add(edgeAB, weightAB);
            edgeWeights.Add(edgeAC, weightAC);
            edgeWeights.Add(edgeBC, weightBC);
            edgeWeights.Add(edgeBD, weightBD);
            edgeWeights.Add(edgeCD, weightCD);
        }
            

        [Test]
        public void TestQuickGraph_3_6_6() {
            var target = new HoffmanPavleyRankedShortestPathAlgorithm<string, Edge<string>>(graph, e => edgeWeights[e]);
            target.ShortestPathCount = graph.VertexCount;
            target.Compute(vertexA, vertexD);
            var paths = target.ComputedShortestPaths.ToList();

            Assert.AreEqual(3, paths.Count);

            var path1 = paths[0];
            double weight1 = Enumerable.Sum(path1, e => edgeWeights[e]);
            Assert.AreEqual(weightAB + weightBD, weight1, DELTA);
            var edges1 = path1.ToList();
            Assert.AreEqual(2, edges1.Count);
            Assert.AreEqual(edgeAB, edges1[0]);
            Assert.AreEqual(edgeBD, edges1[1]);

            var path2 = paths[1];
            double weight2 = Enumerable.Sum(path2, e => edgeWeights[e]);
            Assert.AreEqual(weightAC + weightCD, weight2, DELTA);
            var edges2 = path2.ToList();
            Assert.AreEqual(2, edges2.Count);
            Assert.AreEqual(edgeAC, edges2[0]);
            Assert.AreEqual(edgeCD, edges2[1]);

            var path3 = paths[2];
            double weight3 = Enumerable.Sum(path3, e => edgeWeights[e]);
            Assert.AreEqual(weightAB + weightBC + weightCD, weight3, DELTA);
            var edges3 = path3.ToList();
            Assert.AreEqual(3, edges3.Count);
            Assert.AreEqual(edgeAB, edges3[0]);
            Assert.AreEqual(edgeBC, edges3[1]);            
            Assert.AreEqual(edgeCD, edges3[2]);

            // the below code is based on code example found here: https://github.com/rhishi/QuickGraph/blob/master/3.0/sources/QuickGraph.Tests/Algorithms/RankedShortestPath/HoffmanPavleyRankedShortestPathAlgorithmTest.cs
            //HoffmanPavleyRankedShortestPathAlgorithmm(
            //    graph, 
            //    edgeWeights,
            //    vertexA,
            //    vertexD,
            //    graph.VertexCount
            //);
        }

        // based on code example found here: https://github.com/rhishi/QuickGraph/blob/master/3.0/sources/QuickGraph.Tests/Algorithms/RankedShortestPath/HoffmanPavleyRankedShortestPathAlgorithmTest.cs
        //private IEnumerable<IEnumerable<TEdge>> HoffmanPavleyRankedShortestPathAlgorithmm<TVertex,TEdge>(
        //    IBidirectionalGraph<TVertex, TEdge> g,
        //    Dictionary<TEdge, double> edgeWeights,
        //    TVertex rootVertex,
        //    TVertex goalVertex,
        //    int pathCount
        //)
        //    where TEdge : IEdge<TVertex>
        //{
        //    var target = new HoffmanPavleyRankedShortestPathAlgorithm<TVertex, TEdge>(g, e => edgeWeights[e]);
        //    target.ShortestPathCount = pathCount;
        //    target.Compute(rootVertex, goalVertex);

        //    double lastWeight = double.MinValue;
        //    foreach (var path in target.ComputedShortestPaths)
        //    {
        //        var edges = path.ToArray();
        //        foreach(var edge in edges) {
        //            Console.WriteLine("edge.Source : " + edge.Source);
        //            Console.WriteLine("edge.Target : " + edge.Target);
        //        }
        //        Console.WriteLine("path: {0}", Enumerable.Sum(path, e => edgeWeights[e]));
        //        double weight = Enumerable.Sum(path, e => edgeWeights[e]);
        //        Assert.IsTrue(lastWeight <= weight, "{0} <= {1}", lastWeight, weight);
        //        Assert.AreEqual(rootVertex, Enumerable.First(path).Source);
        //        Assert.AreEqual(goalVertex, Enumerable.Last(path).Target);
        //        Assert.IsTrue(EdgeExtensions.IsPathWithoutCycles<TVertex, TEdge>(path));

        //        lastWeight = weight;
        //    }
        //    return target.ComputedShortestPaths;
        //}
    }
}
// https://github.com/rhishi/QuickGraph/blob/master/3.0/sources/QuickGraph.Tests/Algorithms/RankedShortestPath/HoffmanPavleyRankedShortestPathAlgorithmTest.cs
// https://github.com/rhishi/QuickGraph/blob/2f92ed688182f9f0d97b03a8a54dc761ded9b59d/3.0/sources/QuickGraph/UndirectedGraph.cs
// https://github.com/rhishi/QuickGraph/blob/2f92ed688182f9f0d97b03a8a54dc761ded9b59d/3.0/sources/QuickGraph/AdjacencyGraph.cs
/// A mutable directed graph data structure efficient for sparse
/// graph representation where out-edge need to be enumerated only.
// https://github.com/rhishi/QuickGraph/blob/2f92ed688182f9f0d97b03a8a54dc761ded9b59d/3.0/sources/QuickGraph/BidirectionalGraph.cs
/// mutable directed graph data structure efficient for sparse
/// graph representation where out-edge and in-edges need to be enumerated. Requires
/// twice as much memory as the adjacency graph.