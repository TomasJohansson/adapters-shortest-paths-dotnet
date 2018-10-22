/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in this "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using Programmerare.ShortestPaths.Core.Api;
using Programmerare.ShortestPaths.Core.Api.Generics;
using System.Collections.Generic;

namespace Programmerare.ShortestPaths.Core.PathFactories
{
    /// <summary>
    /// Used for creating an instance of Path{E, V, W}.
    /// Example of path types which can be created by an implementation class:
    ///     Path{Edge, Vertex, Weight} (and note that all these types are part of the library)
    ///     Path{Road, City, WeightDeterminedByRoadLengthAndQuality}
    ///         (and note that the above three types within brackets are NOT part of the library but 
    ///          examples of types which client code can create as subtypes)
    /// </summary>
    /// <typeparam name="P">Path or subtype</typeparam>
    /// <typeparam name="E">Edge or subtype</typeparam>
    /// <typeparam name="V">Vertex or subtype</typeparam>
    /// <typeparam name="W">Weight or subtype</typeparam>
    public interface PathFactory<P, E, V, W> 
        where P : PathGenerics<E, V, W>
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {
	    P CreatePath(W totalWeight, IList<E> edges);
    }
}