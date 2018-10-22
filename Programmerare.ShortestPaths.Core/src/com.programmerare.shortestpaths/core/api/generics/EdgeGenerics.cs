/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in this "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

namespace Programmerare.ShortestPaths.Core.Api.Generics
{
    /// <summary>
    /// An Edge represents a path between two vertices,  the start Vertex and the end Vertex,  with an associated weight. 
    /// The edge path is directed (i.e. with direction) and direct (i.e. without roundabout through other vertices in between).
    /// </summary>
    /// <typeparam name="V">Vertex</typeparam>
    /// <typeparam name="W">Weight</typeparam>
    public interface EdgeGenerics<V , W> : StringRenderable 
        where V : Vertex
        where W : Weight
    {
        /// <summary>
        /// an id which must be unique within a Graph, i.e. a Graph should not have more than one Edge with the same id. 
        /// One possible way of implementing it is to return a concatenation of the id's for the start vertex and the end vertex, with a separator between (e.g. an underscore).
        /// Such an implementation will make it unique since each vertex id should also be unique within a graph. 
        /// </summary>
        string EdgeId { get; }

        /// <summary>
        /// the Vertex where the directed Edge starts
        /// </summary>
        V StartVertex { get; }

        /// <summary>
        /// the Vertex where the directed Edge ends
        /// </summary>
        V EndVertex { get; }

        /// <summary>
        /// the Weight for the edge, i.e. some kind of 'cost' (e.g. time or distance) for going from the start Vertex to the end Vertex. 
        /// </summary>
        W EdgeWeight { get; }
    }
}