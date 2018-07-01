using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class TestYensImpl {
	// unit test for the same graph as specified in the file "yens_tomas.txt"

    [Test]
    public void yensImplTest() {
        var n0 = new Node(0);
        var n1 = new Node(1);
        var n2 = new Node(2);
        var n3 = new Node(3);
        
        n0.addEdge(1, 5);
        n0.addEdge(2, 6);
        n1.addEdge(2, 7);
        n1.addEdge(3, 8);
        n2.addEdge(3, 9);
        
        var nodes = new List<Node>(){n0, n1, n2, n3 };
        var yen = new Yen();
        var paths = yen.yensImpl(nodes, 0, 3, 3); // # yensImpl(nodes, fromNode, toNode, numPaths):
        //# print(paths[0].printPath()) # 1 -> 2 -> 4 (Cost: 13)
        //# print(paths[1].printPath()) # 1 -> 3 -> 4 (Cost: 15)
        //# print(paths[2].printPath()) # 1 -> 2 -> 3 -> 4 (Cost: 21)

        Assert.AreEqual(paths.Count, 3);
        //Assert.AreEqual(13, paths.getPathCost());
        double delta = 0.000001;
        Assert.AreEqual(paths[0].getPathCost(), 13, delta);
        Assert.AreEqual(paths[1].getPathCost(), 15, delta);
        Assert.AreEqual(paths[2].getPathCost(), 21, delta);

        Assert.AreEqual(paths[0].Nodes[0], n0);
        Assert.AreEqual(paths[0].Nodes[1], n1);
        Assert.AreEqual(paths[0].Nodes[2], n3);

        Assert.AreEqual(paths[1].Nodes[0], n0);
        Assert.AreEqual(paths[1].Nodes[1], n2);
        Assert.AreEqual(paths[1].Nodes[2], n3);

        Assert.AreEqual(paths[2].Nodes[0], n0);
        Assert.AreEqual(paths[2].Nodes[1], n1);
        Assert.AreEqual(paths[2].Nodes[2], n2);
        Assert.AreEqual(paths[2].Nodes[3], n3);
    }

    [Test]
    public void dijkstraImplTest() {
        var n0 = new Node(0);
        var n1 = new Node(1);
        var n2 = new Node(2);
        var n3 = new Node(3);
        
        n0.addEdge(1, 5);
        n0.addEdge(2, 6);
        n1.addEdge(2, 7);
        n1.addEdge(3, 8);
        n2.addEdge(3, 9);
		
        var nodes = new List<Node>(){n0, n1, n2, n3 };
        var yen = new Yen();
        var path = yen.dijkstraImpl(nodes, 0, 3); // # yensImpl(nodes, fromNode, toNode, numPaths):
        //# print(paths[0].printPath()) # 1 -> 2 -> 4 (Cost: 13)
        //# print(paths[1].printPath()) # 1 -> 3 -> 4 (Cost: 15)
        //# print(paths[2].printPath()) # 1 -> 2 -> 3 -> 4 (Cost: 21)

        //Assert.AreEqual(paths.co, 3)
        Assert.AreEqual(13, path.getPathCost());
        Assert.AreEqual(path.Nodes[0], n0);
        Assert.AreEqual(path.Nodes[1], n1);
        Assert.AreEqual(path.Nodes[2], n3);
    }
}