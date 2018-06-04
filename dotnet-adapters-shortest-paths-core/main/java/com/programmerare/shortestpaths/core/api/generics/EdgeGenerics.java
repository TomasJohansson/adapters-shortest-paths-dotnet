package com.programmerare.shortestpaths.core.api.generics;

import com.programmerare.shortestpaths.core.api.StringRenderable;
import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;

/**
 * An Edge represents a path between two vertices,  the start Vertex and the end Vertex,  with an associated weight.
 * The edge path is directed (i.e. with direction) and direct (i.e. without roundabout through other vertices in between).
 * 
 * @author Tomas Johansson
 */
public interface EdgeGenerics<V extends Vertex , W extends Weight> extends StringRenderable { 

	/**
	 * @return an id which must be unique within a Graph, i.e. a Graph should not have more than one Edge with the same id. 
	 * 	One possible way of implementing it is to return a concatenation of the id's for the start vertex and the end vertex, with a separator between (e.g. an underscore).
	 *  Such an implementation will make it unique since each vertex id should also be unique within a graph. 
	 */
	String getEdgeId();
	
	/**
	 * @return the Vertex where the directed Edge starts
	 */
	V getStartVertex();

	/**
	 * @return the Vertex where the directed Edge ends
	 */
	V getEndVertex();

	/**
	 * @return the Weight for the edge, i.e. some kind of 'cost' (e.g. time or distance) for going from the start Vertex to the end Vertex. 
	 */	
	W getEdgeWeight();
}