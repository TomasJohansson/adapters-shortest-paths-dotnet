package com.programmerare.shortestpaths.core.api.generics;

import java.util.List;

import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;
import com.programmerare.shortestpaths.core.validation.GraphEdgesValidationDesired;

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
public interface PathFinderFactoryGenerics<F extends PathFinderGenerics<P,E,V,W> , P extends PathGenerics<E, V, W> ,  E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> {  

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
		List<E> edges, 
		GraphEdgesValidationDesired graphEdgesValidationDesired
	);
}