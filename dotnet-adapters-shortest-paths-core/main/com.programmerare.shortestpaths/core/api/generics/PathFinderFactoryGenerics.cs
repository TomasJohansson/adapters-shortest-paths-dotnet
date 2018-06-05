/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/

using com.programmerare.shortestpaths.core.validation;
using System.Collections.Generic;

namespace com.programmerare.shortestpaths.core.api.generics
{
    /**
     * Used for creating an instance of PathFinder.
     * The instantiated PathFinder will be an Adapter implementation of the PathFinder, i.e. will use a third-part library for finding the shortest path. 
     * 		
     * 		TODO: Maybe implement something like this for instantiating: XPathFactory xPathfactory = XPathFactory.newInstance();
     * 				Maybe it should use different strategies for choosing instance.
     * 				If multiple implementations are available, then determine in some preferred order, and maybe be able to define it 
     * 				in a config file or system property... 
     * 
     * @author Tomas Johansson
      */
    public interface PathFinderFactoryGenerics<F, P, E, V, W> 
        where F : PathFinderGenerics<P,E,V,W>
        where P : PathGenerics<E, V, W>
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {  

	    /**
	     * @param graph the Graph which the implementation should forward to the PathFinder implementations, which must keep a reference to it,
	     * 	since the PathFinder find method will not receive the Graph as parameter.   
	     * @return an Adapter implementation of the PathFinder, which will use some "Adaptee" (third-part library) for finding shortest paths.
	     */	
	    F createPathFinder(
		    GraphGenerics<E, V, W> graph 
	    );

	    // TODO: maybe improve the javadoc regarding graphEdgesValidationDesired below after it has removed the parameter from the above methods
	
	    /**
	     * Convenience method with a list of Edge instances parameter instead of a Graph parameter.
	     * 	It should be implemented as creating a Graph with the list of edges, and then invoke the overloaded method with Graph as parameter.   
 	     * @param edges list of Edge instances to be used for creating a Graph
	     * @param graphEdgesValidationDesired see the documentation for the overloaded method
	     * @return see the documentation for the overloaded method
	     */	
	    F createPathFinder(
		    IList<E> edges, 
		    GraphEdgesValidationDesired graphEdgesValidationDesired
	    );
    }
}