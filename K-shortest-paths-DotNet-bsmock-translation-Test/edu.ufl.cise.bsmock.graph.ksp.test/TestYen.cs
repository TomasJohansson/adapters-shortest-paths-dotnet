using System;
using java.util;
using edu.ufl.cise.bsmock.graph;
using edu.ufl.cise.bsmock.graph.ksp;
using edu.ufl.cise.bsmock.graph.util;
using edu.ufl.cise.bsmock.graph.ksp;
using java.lang;
using extensionClassesForJavaTypes;
using NUnit.Framework;
using com.programmerare.edu.ufl.cise.bsmock.graph.ksp;
/**
 * Test of Yen's algorithm for computing the K shortest loopless paths between two nodes in a graph.
 *
 * Copyright (C) 2015  Brandon Smock (dr.brandon.smock@gmail.com, GitHub: bsmock)
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *
 * Created by Brandon Smock on September 23, 2015.
 * Last updated by Brandon Smock on December 24, 2015.
 */
[TestFixture]
public class TestYen {

    public static void main(String[] args) {
        /* Uncomment any of these example tests */
        String graphFilename, sourceNode, targetNode;
        int K;

        /* Example 1 */
        graphFilename = "edu/ufl/cise/bsmock/graph/ksp/test/tiny_graph_01.txt";
        sourceNode = "1";
        targetNode = "10";
        K = 50;

        /* Example 2 */
        graphFilename = "edu/ufl/cise/bsmock/graph/ksp/test/tiny_graph_02.txt";
        sourceNode = "1";
        targetNode = "9";
        K = 10;

        /* Example 3 */
        //graphFilename = "edu/ufl/cise/bsmock/graph/ksp/test/small_road_network_01.txt";
        //sourceNode = "5524";
        //targetNode = "7239";
        //K = 5;

        string graphFilenameFullPath = GetModifiedPath(graphFilename);
        usageExample1(graphFilenameFullPath,sourceNode,targetNode,K);
    }

    public static void usageExample1(String graphFilename, String source, String target, int k) {
        /* Read graph from file */
        SystemOut.println("Reading data from file... ");
        Graph graph = new Graph(graphFilename);

        SystemOut.println("complete.");
        printGraph(graph);
        /* Compute the K shortest paths and record the completion time */
        SystemOut.println("Computing the " + k + " shortest paths from [" + source + "] to [" + target + "] ");
        SystemOut.println("using Yen's algorithm... ");
        List<Path> ksp;
        long timeStart = SystemJ.currentTimeMillis();
        Yen yenAlgorithm = new Yen();
        ksp = yenAlgorithm.ksp(graph, source, target, k);
        long timeFinish = SystemJ.currentTimeMillis();
        SystemOut.println("complete.");
        SystemOut.println("Operation took " + (timeFinish - timeStart) / 1000.0 + " seconds.");
        /* Output the K shortest paths */
        SystemOut.println("k) cost: [path]");
        int n = 0;
        foreach (Path p in ksp) {
            SystemOut.println(++n + ") " + p);
        }
    }

    private static void printGraph(Graph graph)
    {
        SystemOut.println("graph looping starts");
        var nodes = graph.getNodes();
        var keys = nodes.keySet();
        foreach(var k in keys)
        {
            SystemOut.println("node key " + k);
        }

        var edges = graph.getEdgeList();
        foreach(Edge e in edges) {
            SystemOut.println("edge weight " + e.getWeight());
        }
        SystemOut.println("graph looping ends");

        
    }

    private static string GetModifiedPath(string graphFilename)
    {
        // example input:
        // "edu/ufl/cise/bsmock/graph/ksp/test/tiny_graph_01.txt"
        // then the expected output should be:
        // "test_files_edu.ufl.cise.bsmock.graph.ksp.test/tiny_graph_01.txt"
        int indexOfLastForwardSlash = graphFilename.LastIndexOf("/");
        string directory = graphFilename.Substring(0, indexOfLastForwardSlash);
        string fileNameOnly = graphFilename.Substring(indexOfLastForwardSlash);
        string directoryReplacedWithDots = directory.Replace('/', '.');
        string modifiedPath = "test_files_" + directoryReplacedWithDots + fileNameOnly;
        string graphFilenameFullPath = FileUtil.GetFullPath(modifiedPath);
        Console.WriteLine("graphFilenameFullPath: " + graphFilenameFullPath);
        return graphFilenameFullPath;
    }


    // No assertions, i.e. no real test
    // but just a way of triggering the main method 
    // from a library
    [Test]
    public void YenMain()
    {
        TestYen.main(null);
    }

    [Test]
    public void YenTest()
    {
        const double deltaValue = 0.0000001;
        var graph = new Graph();
        graph.addEdge("A", "B", 5);
        graph.addEdge("A", "C", 6);
        graph.addEdge("B", "C", 7);
        graph.addEdge("B", "D", 8);
        graph.addEdge("C", "D", 9);

        Yen yenAlgorithm = new Yen();
        List<Path> paths = yenAlgorithm.ksp(graph, "A", "D", 5);
        Assert.AreEqual(3, paths.size());

        Path path1 = paths.get(0);
        Path path2 = paths.get(1);
        Path path3 = paths.get(2);
        Assert.AreEqual(13, path1.getTotalCost(), deltaValue);
        Assert.AreEqual(15, path2.getTotalCost(), deltaValue);
        Assert.AreEqual(21, path3.getTotalCost(), deltaValue);

        List<String> nodes1 = path1.getNodes();
        Assert.AreEqual(3, nodes1.size());
        Assert.AreEqual("A", nodes1.get(0));
        Assert.AreEqual("B", nodes1.get(1));
        Assert.AreEqual("D", nodes1.get(2));
        
        List<String> nodes2 = path2.getNodes();
        Assert.AreEqual(3, nodes2.size());
        Assert.AreEqual("A", nodes2.get(0));
        Assert.AreEqual("C", nodes2.get(1));
        Assert.AreEqual("D", nodes2.get(2));

        List<String> nodes3 = path3.getNodes();
        Assert.AreEqual(4, nodes3.size());
        Assert.AreEqual("A", nodes3.get(0));
        Assert.AreEqual("B", nodes3.get(1));
        Assert.AreEqual("C", nodes3.get(2));
        Assert.AreEqual("D", nodes3.get(3));
    }
}
