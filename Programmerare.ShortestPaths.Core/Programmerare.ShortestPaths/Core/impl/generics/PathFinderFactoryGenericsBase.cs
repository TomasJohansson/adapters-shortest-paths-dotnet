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
using Programmerare.ShortestPaths.Core.Validation;
using System.Collections.Generic;

namespace Programmerare.ShortestPaths.Core.Impl.Generics
{
public abstract class PathFinderFactoryGenericsBase<F, P, E, V, W> 
    : PathFinderFactoryGenerics<F, P, E, V, W> 
	where F : PathFinderGenerics<P, E, V, W>
    where P : PathGenerics<E, V, W>
    where E : EdgeGenerics<V, W>
    where V : Vertex
    where W : Weight
{
	protected PathFinderFactoryGenericsBase() { }

    /// <param name="edges">list of edges</param>
    /// <param name="graphEdgesValidationDesired">should be NO (for performance reason) if validation has already been done</param>
    /// <returns>an instance of a PathFinderGenerics implementation</returns>
	public F CreatePathFinder(
		IList<E> edges, 
		GraphEdgesValidationDesired graphEdgesValidationDesired
	)
    {
		GraphGenerics<E, V, W> graph = GraphGenericsImpl<E, V, W>.CreateGraphGenerics<E, V, W>(edges, graphEdgesValidationDesired);
		return CreatePathFinder(graph); // the overloaded method must be implemented by subclasses
	}

    abstract public F CreatePathFinder(
		GraphGenerics<E, V, W> graph 
	);
}
}