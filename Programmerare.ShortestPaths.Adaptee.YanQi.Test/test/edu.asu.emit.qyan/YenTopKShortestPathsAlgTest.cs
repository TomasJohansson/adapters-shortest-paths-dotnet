/*	 
* The code in this project is based on the following Java project created by Yan Qi (https://github.com/yan-qi) :
* https://github.com/yan-qi/k-shortest-paths-java-version
* Tomas Johansson later forked the above Java project into this location:
* https://github.com/TomasJohansson/k-shortest-paths-java-version
* Tomas Johansson later translated the above Java code to C#.NET
* That C# code is currently a part of the Visual Studio solution located here:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
* The current name of the subproject (within the VS solution) with the translated C# code:
* Programmerare.ShortestPaths.Adaptee.YanQi
* 
* Regarding the latest license, Yan Qi has released (19th of Januari 2017) the code with Apache License 2.0
* https://github.com/yan-qi/k-shortest-paths-java-version/commit/d028fd873ff34efc1e851421be380d2382dcb104
* https://github.com/yan-qi/k-shortest-paths-java-version/blob/master/License.txt
* 
* You can also find license information in the files "License.txt" and "NOTICE.txt" in the project root directory.
*/

/*
 * The below copyright statements are included from the original Java code found here:
 * https://github.com/yan-qi/k-shortest-paths-java-version
 * Regarding the translation of that Java code to this .NET code, see above (the top of this source file) for more information.
 * 
 * 
 * Copyright (c) 2004-2008 Arizona State University.  All rights
 * reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 *
 * 1. Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer.
 *
 * 2. Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in
 *    the documentation and/or other materials provided with the
 *    distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY ARIZONA STATE UNIVERSITY ``AS IS'' AND
 * ANY EXPRESSED OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO,
 * THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
 * PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL ARIZONA STATE UNIVERSITY
 * NOR ITS EMPLOYEES BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
 * LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *
 */

namespace edu.asu.emit.qyan.test
{
using NUnit.Framework;
using edu.asu.emit.algorithm.graph;
using edu.asu.emit.algorithm.graph.shortestpaths;
using System.Collections.Generic;
using System;
using Programmerare.ShortestPaths.Adaptee.YanQi.Test; // GraphFactory

    /**
* TODO Need to redo!
* @author <a href='mailto:Yan.Qi@asu.edu'>Yan Qi</a>
* @version $Revision: 784 $
* @latest $Id: YenTopKShortestPathsAlgTest.java 46 2010-06-05 07:54:27Z yan.qi.asu $
* 
* Regarding the above javadoc author statement it applies to the original Java code found here:
* https://github.com/yan-qi/k-shortest-paths-java-version
* Regarding the translation of that Java code to this .NET code, see the top of this source file for more information.
*/
[TestFixture]
public class YenTopKShortestPathsAlgTest {
	// The graph should be initiated only once to guarantee the correspondence 
	// between vertex id and node id in input text file. 
	private static Graph graph = null;
	
    [SetUp]
    public void SetUp() {
        if(!IsAssemblyForAdapteeYanQiSupportingStreamReader()) {
            return;
        }
        if(graph == null) {
            graph = GraphFactory.createVariableGraph("data/test_6_2");
        }
    }

    [Test]
	public void testDijkstraShortestPathAlg()
	{
        if(!IsAssemblyForAdapteeYanQiSupportingStreamReader()) {
            Assert.Ignore(); // TODO: refactor this to a helper method "IgnoreIfTrue"
        }
		Console.WriteLine("Testing Dijkstra Shortest Path Algorithm!");
		DijkstraShortestPathAlg alg = new DijkstraShortestPathAlg(graph);
		Console.WriteLine(alg.GetShortestPath(graph.GetVertex(4), graph.GetVertex(5)));
	}
	
    [Test]
	public void testYenShortestPathsAlg()
	{		
		if(!IsAssemblyForAdapteeYanQiSupportingStreamReader()) {
			Assert.Ignore(); // TODO: refactor this to a helper method "IgnoreIfTrue"
		}
		Console.WriteLine("Testing batch processing of top-k shortest paths!");
		YenTopKShortestPathsAlg yenAlg = new YenTopKShortestPathsAlg(graph);
		IList<Path> shortest_paths_list = yenAlg.GetShortestPaths(
                graph.GetVertex(4), graph.GetVertex(5), 100);
		Console.WriteLine(":"+shortest_paths_list);
		Console.WriteLine(yenAlg.GetResultList().Count);
	}
	
    [Test]
	public void testYenShortestPathsAlg2()
	{
		if(!IsAssemblyForAdapteeYanQiSupportingStreamReader()) {
			Assert.Ignore(); // TODO: refactor this to a helper method "IgnoreIfTrue"
		}
		Console.WriteLine("Obtain all paths in increasing order! - updated!");
		YenTopKShortestPathsAlg yenAlg = new YenTopKShortestPathsAlg(
				graph, graph.GetVertex(4), graph.GetVertex(5));
		int i=0;
		while(yenAlg.HasNext())
		{
			Console.WriteLine("Path "+i+++" : "+yenAlg.Next());
		}
		
		Console.WriteLine("Result # :"+i);
		Console.WriteLine("Candidate # :"+yenAlg.GetCadidateSize());
		Console.WriteLine("All generated : "+yenAlg.GetGeneratedPathSize());
	}
	
	[Test]
	public void testYenShortestPathsAlg4MultipleGraphs()
	{
		if(!IsAssemblyForAdapteeYanQiSupportingStreamReader()) {
			Assert.Ignore(); // TODO: refactor this to a helper method "IgnoreIfTrue"
		}
		Console.WriteLine("Graph 1 - ");
		YenTopKShortestPathsAlg yenAlg = new YenTopKShortestPathsAlg(
				graph, graph.GetVertex(4), graph.GetVertex(5));
		int i=0;
		while(yenAlg.HasNext())
		{
			Console.WriteLine("Path "+i+++" : "+yenAlg.Next());
		}
		
		Console.WriteLine("Result # :"+i);
		Console.WriteLine("Candidate # :"+yenAlg.GetCadidateSize());
		Console.WriteLine("All generated : "+yenAlg.GetGeneratedPathSize());
		
		///
		Console.WriteLine("Graph 2 - ");
		graph = GraphFactory.createVariableGraph("data/test_6_1");
		YenTopKShortestPathsAlg yenAlg1 = new YenTopKShortestPathsAlg(graph);
		IList<Path> shortest_paths_list = yenAlg1.GetShortestPaths(
                graph.GetVertex(4), graph.GetVertex(5), 100);
		Console.WriteLine(":"+shortest_paths_list);
		Console.WriteLine(yenAlg1.GetResultList().Count);
	}

	private bool IsAssemblyForAdapteeYanQiSupportingStreamReader() {
		return Programmerare.ShortestPaths.Adaptee.YanQi.Test.YenTopKShortestPathsAlgTest.IsAssemblyForAdapteeYanQiSupportingStreamReader();
	}
}
}