package com.programmerare.shortestpaths.core.api.generics;

import java.util.List;

import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;

/**
 * The PathFinder interface provides a method for finding the shortest path between two Vertex instances in a Graph.
 * The Graph is NOT provided as a parameter to the find method which means that the implementations must aggregate the Graph. 
 * 
 * The PathFinder interface corresponds to the the "Target" interface in the Adapter Design Pattern.
 * The different "Adapter" implementations of this "Target" interface will use different third-part libraries
 * for finding the shortest paths, i.e. those libraries play the role of the "Adaptee" in the Adapter Pattern.   
 
 * @see <a href="https://en.wikipedia.org/wiki/Adapter_pattern">https://en.wikipedia.org/wiki/Adapter_pattern</a> 
 * 
 * @author Tomas Johansson
 */
public interface PathFinderGenerics<P extends PathGenerics<E, V, W> , E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> {

	/**
	 * Finds the shortest paths from the start Vertex to the end Vertex.
	 * @param startVertex the first Vertex of the searched Path.  
	 * @param endVertex the last Vertex of the searched Path.
	 * @param maxNumberOfPaths the max number of paths the method should return.
	 * @return a list of Path instances, where each Path represents a path from the start vertex to the end vertex.
	 * 	The returned paths are sorted by total weight in increasing order, i.e. the first path in the list has the very lowest total weight.
	 * Each returned Path contains a list of Edge instances. The start Vertex for the first Edge, and the end Vertex for the last Edge, 
	 * should be the same for each returned paths, i.e. those should be the same start vertex and end vertex as were used as method input parameters.     
	 */
	List<P> findShortestPaths(V startVertex, V endVertex, int maxNumberOfPaths);
}