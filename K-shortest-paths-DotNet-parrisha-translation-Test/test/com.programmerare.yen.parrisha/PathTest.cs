/// MIT license. For more information see the file "LICENSE"

using System.Collections.Generic;
using NUnit.Framework;

namespace com.programmerare.yen.parrisha {

    [TestFixture]
    public class PathTest
    {
        [Test]
        public void OperatorPlusTest() {
            var n0 = new Node(0);
            var n1 = new Node(1);
            var n2 = new Node(2);
            var n3 = new Node(3);
            var n4 = new Node(4);
            var n5 = new Node(5);

            n0.AddEdge(1, 9);
            n1.AddEdge(2, 9);
            n2.AddEdge(3, 9);
            n3.AddEdge(4, 9);
            n4.AddEdge(5, 9);

            var path_0_1_2 = new Path(new List<Node>{n0, n1, n2});
            var path_3_4_5 = new Path(new List<Node>{n3, n4, n5});

            var path = path_0_1_2 + path_3_4_5;
            Assert.AreEqual(6, path.Nodes.Count);
            Assert.AreEqual(n0, path.Nodes[0]);
            Assert.AreEqual(n1, path.Nodes[1]);
            Assert.AreEqual(n2, path.Nodes[2]);
            Assert.AreEqual(n3, path.Nodes[3]);
            Assert.AreEqual(n4, path.Nodes[4]);
            Assert.AreEqual(n5, path.Nodes[5]);
        }
    }
}