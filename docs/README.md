# License Notice
Notice that the "core" library with the API and general code is released with MIT License.
However, the libraries which are implementing adapters are licensed in the same way as the adapted libraries.
Currently there are **two** such adapter libraries, and **both of them are released with license Apache 2.0 **
* [YanQi](https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/tree/master/Programmerare.ShortestPaths.Adapter.YanQi)
* [Bsmock](https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/tree/master/Programmerare.ShortestPaths.Adapter.Bsmock)
Both of the above libraries are bundled in [NuGet package Programmerare.ShortestPaths 1.0.0](https://www.nuget.org/packages/Programmerare.ShortestPaths/1.0.0) and thus the license should be considered as Apache 2.0 for that binary distribution.

## Adapters for C#.NET implementations of Graph algorithms useful for finding the shortest paths in travel routing.

The purpose of this project is to provide Adapters for C#.NET implementations finding the shortest path**s** (and note the plural of the word 'path**s**').<br>
This is useful for travel routing  when you want to minimize the total time or total distance.<br>
The project might also be useful for other situations, but travel routing is the main kind of application I have in mind.<br>
Regarding graph theory applicable for finding the shortest paths in travel routing, see more information about that further down in a separate section at this page.
  
**Currently there are two implemented Adapters**, i.e. two different implementations can be used.
Since the Client code is using the same Target interface (see the [Adapter Design Pattern](https://en.wikipedia.org/wiki/Adapter_pattern)) it is possible to **reuse the same test code for the different implementations**.
Therefore you can assert their results against each other, which could help finding bugs. If one implementation would produce a different result than the others, then it is likely a bug that should be reported and hopefully fixed. However, note that the tested graph need to be constructed in such a way that there must not be more than one path (among the first shortest paths you use test assertions for) with the same total weight. If multiple paths have the same total weight then it is not obvious which should be sorted first, and then it would not be surprising if different implementations produce different results.

When you run such tests with the same test data for different implementations then you can also easily **compare the performance for the different implementations**.

Currently there are only two implementations in this C# version of the [Java project adapters-shortest-paths](https://github.com/TomasJohansson/adapters-shortest-paths) 
but the test files being used in the C# version have also been tested with the five different implementations in the Java project with the same results, so therefore it is very likely that the implementations are correct.

### Example of how to use this C# shortest paths adapter library:

The C# code example below uses the following graph with four vertices (A,B,C,D) and five edges with weights.<br>(A to B (5) , A to C (6) , B to C (7)  , B to D (8) , C to D (9) ).<br>![alt text](images/shortest_paths_getting_started_example.gif "Logo Title Text 1")<br>
There are three possible paths from A to D , with the total weight within parenthesis : 
* A to B to D (total cost: 13 = 5 + 8)
* A to C to D (total cost: 15 = 6 + 9)
* A to B to C to D (total cost: 21 = 5 + 7 + 9)

For example, the vertices might represent cities, and the edges might represent roads with distances as weights.

The C# code below can be used for finding the shortest paths (sorted with the shortest first) from A to D :<br>

```C#
using Programmerare.ShortestPaths.Core.Api;
using Programmerare.ShortestPaths.Adapter.Bsmock;
using Programmerare.ShortestPaths.Adapter.YanQi;
using static Programmerare.ShortestPaths.Core.Impl.VertexImpl;	// CreateVertex
using static Programmerare.ShortestPaths.Core.Impl.WeightImpl;	// CreateWeight
using static Programmerare.ShortestPaths.Core.Impl.EdgeImpl;	// CreateEdge
using static Programmerare.ShortestPaths.Core.Impl.GraphImpl;	// CreateGraph
using System;
using System.Text;
using System.Collections.Generic;
...

	PathFinderFactory pathFinderFactory = new PathFinderFactoryYanQi();
	// Alternative implementation:
	//PathFinderFactory pathFinderFactory = new PathFinderFactoryBsmock();

	Vertex a = CreateVertex("A");
	Vertex b = CreateVertex("B");
	Vertex c = CreateVertex("C");
	Vertex d = CreateVertex("D");

	Edge edgeAB5 = CreateEdge(a, b, CreateWeight(5));
	Edge edgeAC6 = CreateEdge(a, c, CreateWeight(6));
	Edge edgeBC7 = CreateEdge(b, c, CreateWeight(7));
	Edge edgeBD8 = CreateEdge(b, d, CreateWeight(8));
	Edge edgeCD9 = CreateEdge(c, d, CreateWeight(9));

	IList<Edge> edges = new List<Edge> {
		edgeAB5,
		edgeAC6,
		edgeBC7,
		edgeBD8,
		edgeCD9
	};

	Graph graph = CreateGraph(edges);

	PathFinder pathFinder = pathFinderFactory.CreatePathFinder(graph);

	IList<Path> shortestPathsFromAtoD = pathFinder.FindShortestPaths(a, d, 5);

	foreach(Path pathFromAtoD in shortestPathsFromAtoD) {
		Console.WriteLine(pathFromAtoD.GetPathAsPrettyPrintedStringForConsoleOutput());
	}
	/*
	 Resulting output from the above loop:
		13 ( 5 [A--->B]  + 8 [B--->D] )
		15 ( 6 [A--->C]  + 9 [C--->D] )
		21 ( 5 [A--->B]  + 7 [B--->C]  + 9 [C--->D] )             
	 */
	 
	 ...
	 
	 
	public static class MyExtensionMethods {
		public static string GetPathAsPrettyPrintedStringForConsoleOutput(this Path path) {
			var sb = new StringBuilder();
			IList<Edge> edges = path.EdgesForPath;
			sb.Append(path.TotalWeightForPath.WeightValue + " ( ");
			for (int i = 0; i < edges.Count; i++) {
				if(i > 0) sb.Append(" + ");
				sb.Append(edges[i].GetEdgeAsPrettyPrintedStringForConsoleOutput());
			}
			sb.Append(")");
			return sb.ToString();
		}
		private static string GetEdgeAsPrettyPrintedStringForConsoleOutput(this Edge edge) {
			return edge.EdgeWeight.WeightValue  + " [" + edge.StartVertex.VertexId + "--->" + edge.EndVertex.VertexId + "] ";		
		}
	}  
```
If you are using [NuGet](https://www.nuget.org/) and want to try the above code, you can use the NuGet package [Programmerare.ShortestPaths 1.0.0](https://www.nuget.org/packages/Programmerare.ShortestPaths/1.0.0).
After the installation, your project file (.csproj) should have become updated with the following content:
```xml
    <PackageReference Include="Programmerare.ShortestPaths">
      <Version>1.0.0</Version>
    </PackageReference>
```
An alternative to use NuGet as above is to download [released binaries](https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/releases)

### .NET versions

Currently the code is compiled for two target frameworks:
* .NET Framework 2.0
* .NET Framework 4.0
     

#### Some concepts in graph theory:

See the section with the same name as above at the bottom of the Java version of this library:
[adapters-shortest-paths](https://github.com/TomasJohansson/adapters-shortest-paths)