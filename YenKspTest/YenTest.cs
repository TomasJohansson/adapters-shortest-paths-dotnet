using NUnit.Framework;
using System.Collections.Generic;

namespace YenKsp {

    [TestFixture]
    public class YenTest {
	    private Yen yen;
        private Node n0, n1, n2, n3, n4;
        private IList<Node> graphNodes;
    
        private const double DELTA_FOR_COMPARISONS_WITH_DOUBLE_VALUES = 0.000001;

        [SetUp]
        public void SetUp() {
            yen = new Yen();

            n0 = new Node(0);
            n1 = new Node(1);
            n2 = new Node(2);
            n3 = new Node(3);
        
            n0.addEdge(1, 5);
            n0.addEdge(2, 6);
            n1.addEdge(2, 7);
            n1.addEdge(3, 8);
            n2.addEdge(3, 9);

            graphNodes = new List<Node>(){n0, n1, n2, n3 };
        }

        [Test]
        public void yensImplTest() {
            IList<Path> shortestPaths = yen.yensImpl(graphNodes, 0, 3, 3); // # yensImpl(nodes, fromNode, toNode, numPaths):
        
            // Expected shortest paths:
            // 0 -> 1 -> 3 (Cost: 13)
            // 0 -> 2 -> 3 (Cost: 15)
            // 0 -> 1 -> 2 -> 3 (Cost: 21)

            Assert.AreEqual(shortestPaths.Count, 3);
            Assert.AreEqual(shortestPaths[0].getPathCost(), 13, DELTA_FOR_COMPARISONS_WITH_DOUBLE_VALUES);
            Assert.AreEqual(shortestPaths[1].getPathCost(), 15, DELTA_FOR_COMPARISONS_WITH_DOUBLE_VALUES);
            Assert.AreEqual(shortestPaths[2].getPathCost(), 21, DELTA_FOR_COMPARISONS_WITH_DOUBLE_VALUES);

            Assert.AreEqual(shortestPaths[0].Nodes[0], n0);
            Assert.AreEqual(shortestPaths[0].Nodes[1], n1);
            Assert.AreEqual(shortestPaths[0].Nodes[2], n3);

            Assert.AreEqual(shortestPaths[1].Nodes[0], n0);
            Assert.AreEqual(shortestPaths[1].Nodes[1], n2);
            Assert.AreEqual(shortestPaths[1].Nodes[2], n3);

            Assert.AreEqual(shortestPaths[2].Nodes[0], n0);
            Assert.AreEqual(shortestPaths[2].Nodes[1], n1);
            Assert.AreEqual(shortestPaths[2].Nodes[2], n2);
            Assert.AreEqual(shortestPaths[2].Nodes[3], n3);
        }

        [Test]
        public void dijkstraImplTest() {
            Path shortestPath = yen.dijkstraImpl(graphNodes, 0, 3);
            // Expected shortest path: 0 -> 1 -> 3 (Cost: 13)
            Assert.AreEqual(13, shortestPath.getPathCost(), DELTA_FOR_COMPARISONS_WITH_DOUBLE_VALUES);
            Assert.AreEqual(shortestPath.Nodes[0], n0);
            Assert.AreEqual(shortestPath.Nodes[1], n1);
            Assert.AreEqual(shortestPath.Nodes[2], n3);
        }
    }
}