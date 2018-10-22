/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in this "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

namespace Programmerare.ShortestPaths.Core.Api
{
    /// <summary>
    /// A Vertex represents some point in the Graph. 
    /// One instance may be part of many edges.
    /// One Vertex can be start vertex for many edges, and can also be end vertex for many edges.  
    /// </summary>
    public interface Vertex : StringRenderable {
 
        /// <summary>
        /// an id which must be unique within a Graph, i.e. a Graph should not have more than one Vertex with the same id.
        /// </summary>
        string VertexId { get; }
    }
}