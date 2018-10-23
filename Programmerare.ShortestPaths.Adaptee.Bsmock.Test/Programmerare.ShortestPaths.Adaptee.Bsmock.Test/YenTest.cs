using NUnit.Framework;
using edu.ufl.cise.bsmock.graph;
using edu.ufl.cise.bsmock.graph.ksp;
using edu.ufl.cise.bsmock.graph.util;
using System;
using System.Collections.Generic;

namespace Programmerare.ShortestPaths.Adaptee.Bsmock.Test
{
    /// <summary>
    /// Tomas Johansson is the author of this test class.
    /// </summary>
    public class YenTest {

        [Test]
        public void YenKShortestPathsTest()
        {
            const double deltaValue = 0.0000001;

            var graph = new Graph();
            graph.AddEdge("A", "B", 5);
            graph.AddEdge("A", "C", 6);
            graph.AddEdge("B", "C", 7);
            graph.AddEdge("B", "D", 8);
            graph.AddEdge("C", "D", 9);

            Yen yenAlgorithm = new Yen();
            IList<Path> paths = yenAlgorithm.Ksp(graph, "A", "D", 5);
            Assert.AreEqual(3, paths.Count);

            Path path1 = paths[0];
            Path path2 = paths[1];
            Path path3 = paths[2];
            Assert.AreEqual(13, path1.GetTotalCost(), deltaValue);
            Assert.AreEqual(15, path2.GetTotalCost(), deltaValue);
            Assert.AreEqual(21, path3.GetTotalCost(), deltaValue);

            java.util.LinkedList<String> nodes1 = path1.GetNodes();
            Assert.AreEqual(3, nodes1.size());
            Assert.AreEqual("A", nodes1.get(0));
            Assert.AreEqual("B", nodes1.get(1));
            Assert.AreEqual("D", nodes1.get(2));
        
            java.util.LinkedList<String> nodes2 = path2.GetNodes();
            Assert.AreEqual(3, nodes2.size());
            Assert.AreEqual("A", nodes2.get(0));
            Assert.AreEqual("C", nodes2.get(1));
            Assert.AreEqual("D", nodes2.get(2));

            java.util.LinkedList<String> nodes3 = path3.GetNodes();
            Assert.AreEqual(4, nodes3.size());
            Assert.AreEqual("A", nodes3.get(0));
            Assert.AreEqual("B", nodes3.get(1));
            Assert.AreEqual("C", nodes3.get(2));
            Assert.AreEqual("D", nodes3.get(3));
        }
    }
}