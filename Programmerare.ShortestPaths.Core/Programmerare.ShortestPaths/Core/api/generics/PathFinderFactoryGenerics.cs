/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in this "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using Programmerare.ShortestPaths.Core.Validation;
using System.Collections.Generic;

namespace Programmerare.ShortestPaths.Core.Api.Generics
{
    /**
     * TODO maybe: Implement something like this for instantiating: XPathFactory xPathfactory = XPathFactory.newInstance();
     *  Maybe it should use different strategies for choosing instance.
     * 	If multiple implementations are available, then determine in some preferred order, and maybe be able to define it 
     * 	in a config file or system property... 
      */

    /// <summary>
    /// Used for creating an instance of PathFinder. 
    /// The instantiated PathFinder will be an Adapter implementation of the PathFinder, i.e. will use a third-part library for finding the shortest path. 
    /// </summary>
    /// <typeparam name="F">pathFinder</typeparam>
    /// <typeparam name="P">Path</typeparam>
    /// <typeparam name="E">Edge</typeparam>
    /// <typeparam name="V">Vertex</typeparam>
    /// <typeparam name="W">Weight</typeparam>
    public interface PathFinderFactoryGenerics<F, P, E, V, W> 
        where F : PathFinderGenerics<P,E,V,W>
        where P : PathGenerics<E, V, W>
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {  
        /// <param name="graph">the Graph which the implementation should forward to the PathFinder implementations, which must keep a reference to it,
        ///     since the PathFinder find method will not receive the Graph as parameter.
        /// </param>
        /// <returns>an Adapter implementation of the PathFinder, which will use some "Adaptee" (third-part library) for finding shortest paths.</returns>
	    F CreatePathFinder(
		    GraphGenerics<E, V, W> graph 
	    );

	    // TODO: maybe improve the docs regarding graphEdgesValidationDesired below after it has removed the parameter from the above methods
	
        /// <summary>
        /// Convenience method with a list of Edge instances parameter instead of a Graph parameter.
        /// It should be implemented as creating a Graph with the list of edges, and then invoke the overloaded method with Graph as parameter.
        /// </summary>
        /// <param name="edges">list of Edge instances to be used for creating a Graph</param>
        /// <param name="graphEdgesValidationDesired">see the documentation for the overloaded method</param>
        /// <returns>see the documentation for the overloaded method</returns>
	    F CreatePathFinder(
		    IList<E> edges, 
		    GraphEdgesValidationDesired graphEdgesValidationDesired
	    );
    }
}