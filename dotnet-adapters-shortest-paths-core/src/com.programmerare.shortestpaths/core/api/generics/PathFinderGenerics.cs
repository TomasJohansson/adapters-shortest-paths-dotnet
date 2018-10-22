/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in this "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using System.Collections.Generic;

namespace Programmerare.ShortestPaths.Core.Api.Generics
{
    /// <summary>
    /// The PathFinder interface provides a method for finding the shortest path between two Vertex instances in a Graph.
    /// The Graph is NOT provided as a parameter to the find method which means that the implementations must aggregate the Graph. 
    /// The PathFinder interface corresponds to the the "Target" interface in the Adapter Design Pattern.
    /// The different "Adapter" implementations of this "Target" interface will use different third-part libraries
    /// for finding the shortest paths, i.e. those libraries play the role of the "Adaptee" in the Adapter Pattern (see URL below).
    /// https://en.wikipedia.org/wiki/Adapter_pattern
    /// </summary>
    /// <typeparam name="P">Path</typeparam>
    /// <typeparam name="E">Edge</typeparam>
    /// <typeparam name="V">Vertex</typeparam>
    /// <typeparam name="W">Weight</typeparam>
    public interface PathFinderGenerics<P, E, V, W> 
        where P : PathGenerics<E, V, W>
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {
        /// <summary>
        /// Finds the shortest paths from the start Vertex to the end Vertex.
        /// </summary>
        /// <param name="startVertex">the first Vertex of the searched Path.</param>
        /// <param name="endVertex">the last Vertex of the searched Path.</param>
        /// <param name="maxNumberOfPaths">the max number of paths the method should return.</param>
        /// <returns>
        /// a list of Path instances, where each Path represents a path from the start vertex to the end vertex.
        /// The returned paths are sorted by total weight in increasing order, i.e. the first path in the list has the very lowest total weight.
        /// Each returned Path contains a list of Edge instances. The start Vertex for the first Edge, and the end Vertex for the last Edge, 
        /// should be the same for each returned paths, i.e. those should be the same start vertex and end vertex as were used as method input parameters.     
        /// </returns>
	    IList<P> FindShortestPaths(V startVertex, V endVertex, int maxNumberOfPaths);
    }
}