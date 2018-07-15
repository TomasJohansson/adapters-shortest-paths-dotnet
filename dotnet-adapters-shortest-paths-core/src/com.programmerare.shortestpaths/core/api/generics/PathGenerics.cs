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

namespace com.programmerare.shortestpaths.core.api.generics
{
    /// <summary>
    /// A path represents a connection between two Vertices, the start Vertex in the first Edge of the edge List, 
    /// and the end Vertex in the last Edge of the edge List. 
    /// </summary>
    /// <typeparam name="E">Edge</typeparam>
    /// <typeparam name="V">Vertex</typeparam>
    /// <typeparam name="W">Weight</typeparam>
    public interface PathGenerics<E, V, W> 
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {
        /// <summary>
        /// a list of Edge instances which together represent the Path.
        /// For each Edge in the list, the start Vertex should the same Vertex as the end Vertex for the previous Edge in the List.
        /// (but of course not for the first Edge in the List, since it has not previous Edge)
        /// </summary>
        IList<E> EdgesForPath { get; }

        /// <summary>
        /// the sum of all the weights in the Weight instances aggregated by all the Edge instances in the Path. 
        /// Obviously, the client code could calculate the sum through the method returning a List of Edge instance, but 
        /// it is provided here as a convenience method in the interface.
        /// </summary>
        W TotalWeightForPath { get; }
    }
}