/**
 * Core interfaces.
 * An instance of Graph contains instances of Edge.
 * Each Edge contains one Weight and two instances of Vertex (start vertex and end vertex).
 * PathFinderFactory is used for creating an instance of a PathFinder.
 * PathFinder contains a Graph and it is used for finding the best paths between two Vertices (Vertex instances).
 * The best paths are returned from PathFinder as a List of Path instances.    
 * 
 * This package should contain the simple/standard interfaces which are named without generics type parameters.
 * However, note that the interfaces may extend other interfaces with generics type parameters.
 * 
 * Interfaces with generics type parameters should be placed in the sub package "api.generics".
 * 
 * @author Tomas Johansson
 *
 */
package com.programmerare.shortestpaths.core.api;