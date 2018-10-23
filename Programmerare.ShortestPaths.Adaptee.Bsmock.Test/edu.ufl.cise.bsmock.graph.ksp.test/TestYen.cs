/*	 
* The code in this project is based on the following Java project created by Brandon Smock:
* https://github.com/bsmock/k-shortest-paths/
* Tomas Johansson later forked the above Java project into this location:
* https://github.com/TomasJohansson/k-shortest-paths/
* Tomas Johansson later translated the above Java code to C#.NET
* That C# code is currently a part of the Visual Studio solution located here:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
* The current name of the subproject (within the VS solution) with the translated C# code:
* Programmerare.ShortestPaths.Adaptee.Bsmock.Test
* 
* Regarding the latest license, Brandon Smock has released (13th of November 2017) the code with Apache License 2.0
* https://github.com/bsmock/k-shortest-paths/commit/b0af3f4a66ab5e4e741a5c9faffeb88def752882
* https://github.com/bsmock/k-shortest-paths/pull/4
* https://github.com/bsmock/k-shortest-paths/blob/master/LICENSE
* 
* You can also find license information in the files "License.txt" and "NOTICE.txt" in the project root directory.
* 
*/


using System;
using edu.ufl.cise.bsmock.graph;
using edu.ufl.cise.bsmock.graph.ksp;
using edu.ufl.cise.bsmock.graph.util;
using NUnit.Framework;
using com.programmerare.edu.ufl.cise.bsmock.graph.ksp;
using System.Collections.Generic;
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
        Console.WriteLine("Reading data from file... ");
        Graph graph = new Graph(graphFilename);

        Console.WriteLine("complete.");
        printGraph(graph);
        /* Compute the K shortest paths and record the completion time */
        Console.WriteLine("Computing the " + k + " shortest paths from [" + source + "] to [" + target + "] ");
        Console.WriteLine("using Yen's algorithm... ");
        IList<Path> ksp;
        long timeStart = 0;// TODO System.currentTimeMillis();
        Yen yenAlgorithm = new Yen();
        ksp = yenAlgorithm.Ksp(graph, source, target, k);
        long timeFinish = 0;// TODO SystemJ.currentTimeMillis();
        Console.WriteLine("complete.");
        Console.WriteLine("Operation took " + (timeFinish - timeStart) / 1000.0 + " seconds.");
        /* Output the K shortest paths */
        Console.WriteLine("k) cost: [path]");
        int n = 0;
        foreach (Path p in ksp) {
            Console.WriteLine(++n + ") " + p);
        }
    }

    private static void printGraph(Graph graph)
    {
        Console.WriteLine("graph looping starts");
        var nodes = graph.GetNodes();
        var keys = nodes.Keys;
        foreach(var k in keys)
        {
            Console.WriteLine("node key " + k);
        }

        var edges = graph.GetEdgeList();
        foreach(Edge e in edges) {
            Console.WriteLine("edge weight " + e.GetWeight());
        }
        Console.WriteLine("graph looping ends");

        
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

    // TODO: the test method below was created by Tomas Johansson
    // i.e. not part of the original project, so move it to some other file
    // since the "created by" at the beginning of this file above does not apply to the below method
    [Test]
    public void YenTest()
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
