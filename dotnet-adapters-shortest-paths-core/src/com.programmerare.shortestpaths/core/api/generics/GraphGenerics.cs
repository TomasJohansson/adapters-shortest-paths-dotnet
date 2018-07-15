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
    /// A Graph represents a collection of Edge instances.
    /// </summary>
    /// <typeparam name="E">Edge</typeparam>
    /// <typeparam name="V">Vertex</typeparam>
    /// <typeparam name="W">Weight</typeparam>
    public interface GraphGenerics<E , V , W>
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {
        /// <summary>
        /// all edges in the graph
        /// </summary>
        IList<E> Edges { get; }

        /// <summary>
        /// all vertices in the graph. 
        /// The vertices can be derived from the edges, i.e. the method might be implemented as iterating the edges 
        /// as a way to create a list of all vertices.
        /// However, for performance reasons should it not be done every time the method is invoked, but preferably implemented with lazy loading.  
        /// </summary>
        IList<V> Vertices { get; }

        /// <param name="vertex">an instance of a Vertex. When the method is implemented, it should use the Vertex id for the comparison, 
        ///     i.e. the implementation should NOT use object equality when determining whether the Vertex is part of the Graph or not.
        /// </param>
        /// <returns>true if there is a Vertex in the graph with the same id as the Vertex parameter.</returns>
        bool ContainsVertex(V vertex);

        /// <param name="edge">edge an instance of an Edge. When the method is implemented, it should use the Edge id for the comparison,  
	    ///     i.e. the implementation should NOT use object equality when determining whether the Edge is part of the Graph or not.
        /// </param>
        /// <returns>true if there is an Edge in the graph with the same id as the Edge parameter.</returns>
	    bool ContainsEdge(E edge);
    }
}