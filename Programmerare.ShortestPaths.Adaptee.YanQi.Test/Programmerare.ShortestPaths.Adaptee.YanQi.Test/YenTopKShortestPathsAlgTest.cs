using NUnit.Framework;
using edu.asu.emit.algorithm.graph;
using edu.asu.emit.algorithm.graph.abstraction;
using edu.asu.emit.algorithm.graph.shortestpaths;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Programmerare.ShortestPaths.Test.Utils;

namespace Programmerare.ShortestPaths.Adaptee.YanQi.Test
{
    [TestFixture]
    class YenTopKShortestPathsAlgTest
    {
        private const double SMALL_DELTA_VALUE_FOR_ASSERTIONS = 0.00000001;
        
        [Test]
        public void TestVerySmallGraph() {
            if(!IsAssemblyForAdapteeYanQiSupportingStreamReader()) {
                Assert.Ignore(); // TODO: refactor this to a helper method "IgnoreIfTrue"
            }
            Graph graph = CreateGraph("graph_very_small.txt");
            // Small Graph with three possible paths from vertex 0 to vertex 3. https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-test/src/test/resources/test_graphs/small_graph_1.xml
            var yenAlg = new YenTopKShortestPathsAlg(
                graph, 
                graph.GetVertex(0), // from vertex
                graph.GetVertex(3)  // to vertex
            );
            var first = yenAlg.Next();
            var second = yenAlg.Next();
            var third = yenAlg.Next();
            Assert.IsFalse(yenAlg.HasNext());
            string expectedResult = @"
			    13 0 1 3
			    15 0 2 3
			    21 0 1 2 3
            ";
            // The first path has weight 13, the second path has weight 15 ...
            // The first path is going from vertex 0 to vertex 1 to vertex 3 ...
            var listWithExpectedWeightsAndNodes = GetExpectedWeightAndNodes(expectedResult);
            assertExpectedPath(listWithExpectedWeightsAndNodes[0], first);
            assertExpectedPath(listWithExpectedWeightsAndNodes[1], second);
            assertExpectedPath(listWithExpectedWeightsAndNodes[2], third);
        }

        [Test]
        public void TestSmallGraph() {
            if(!IsAssemblyForAdapteeYanQiSupportingStreamReader()) {
                Assert.Ignore(); // TODO: refactor this to a helper method "IgnoreIfTrue"
            }
            Graph graph = CreateGraph("graph_small.txt");
            // https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-test/src/test/resources/test_graphs/origin_bsmock/tiny_graph_01.xml
            string expectedResult = @"
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
            var listWithExpectedWeightsAndNodes = GetExpectedWeightAndNodes(expectedResult);
            var yenAlg = new YenTopKShortestPathsAlg(graph, graph.GetVertex(1), graph.GetVertex(10));
            int counter = 0;
            const int numberOfExpectedPathsDefined = 25;
            while(yenAlg.HasNext() && counter < numberOfExpectedPathsDefined)
            {
                assertExpectedPath(
                    listWithExpectedWeightsAndNodes[counter], 
                    yenAlg.Next()
                );
                counter++;
            }
        }

        private IList<WeightAndNodes> GetExpectedWeightAndNodes(string expectedResult)
        {
            string[] paths = Regex.Split(expectedResult.Trim(), @"[\n\r]+");
            System.Collections.Generic.IList<WeightAndNodes> wnList = new System.Collections.Generic.List<WeightAndNodes>();
            foreach(string path in paths)
            {
                Console.WriteLine("will split path string " + path);
                string[] weightAndNodes = Regex.Split(path.Trim(), @"\s");
                double weight = double.Parse(weightAndNodes[0]);
                System.Collections.Generic.IList<int> nodess = new System.Collections.Generic.List<int>();
                for(int i=1; i<weightAndNodes.Length; i++)
                {
                    Console.WriteLine("weightAndNodes[i] : " + weightAndNodes[i]);
                    int node = int.Parse(weightAndNodes[i]);
                    nodess.Add(node);
                }
                var ww = new WeightAndNodes(weight, nodess);
                wnList.Add(ww);
                Console.WriteLine(ww);
            }
            return wnList;
        }

        private void assertExpectedPath(WeightAndNodes expectedPath, Path actualPath)
        {
            Assert.AreEqual(expectedPath.Weight, actualPath.GetWeight(), SMALL_DELTA_VALUE_FOR_ASSERTIONS);
            assertVertexList(expectedPath.Nodes, actualPath.GetVertexList());
        }

        private void assertVertexList(IList<int> expectedVertices, java.util.List<BaseVertex> actualVertices)
        {
            Assert.AreEqual(expectedVertices.Count, actualVertices.size());
            int count = expectedVertices.Count;
            for(int i=0; i<count; i++)
            {
                Assert.AreEqual(expectedVertices[i], actualVertices.get(i).GetId());
            }
        }

        private Graph CreateGraph(string fileName)
        {
            return GraphFactory.createVariableGraph("data_programmerare/" + fileName);
        }

        public static bool IsAssemblyForAdapteeYanQiSupportingStreamReader() {
            // file based test can currently not be executed when the target assembly is 
            // .NET Standard 1.0 - 1.6 because of missing constructor 
            // for StreamReader taking a filename parameter which is currently required 
            var targetFramework = TargetFrameworkDetector.GetTargetFrameworkForAssembly(typeof(Graph));
            return targetFramework.IsSupportingFileStreamReader();
        }
    }
    class WeightAndNodes
    {
        private readonly double weight;
        private readonly System.Collections.Generic.IList<int> nodess;

        public WeightAndNodes(double weight, System.Collections.Generic.IList<int> nodess)
        {
            this.weight = weight;
            this.nodess = nodess;
        }

        public double Weight => weight;

        public System.Collections.Generic.IList<int> Nodes => nodess;

        public override string ToString()
        {
            return "Weight " + Weight + " nodes: " + Nodes;
        }

    }
}
