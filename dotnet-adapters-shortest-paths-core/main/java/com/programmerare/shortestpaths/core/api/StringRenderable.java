/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.core.api;

import com.programmerare.shortestpaths.core.validation.GraphEdgesValidator;

/**
 * Used for trying to enforce that implementations of core types (Vertex, Edge, Weight)
 * will output useful string, which is used in validation methods in class {@link GraphEdgesValidator}.
 * Another reason for introducing the interface was to the avoid ugliness of otherwise letting a method 
 * receive type "Object" as a parameter when you want to use "toString" when the actual objects
 * are one of Vertex, Edge or Weight, but those three types did not have any common base type before this interface was introduced.
 *  
 * @author Tomas Johansson
 */
public interface StringRenderable {
	String renderToString();
}