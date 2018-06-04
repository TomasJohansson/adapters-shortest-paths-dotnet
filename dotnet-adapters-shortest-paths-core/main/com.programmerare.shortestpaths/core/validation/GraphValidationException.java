/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.core.validation;

/**
 * @see GraphEdgesValidator
 *    
 * @author Tomas Johansson
*/
public class GraphValidationException extends RuntimeException {
	public GraphValidationException(String message) {
		super(message);
	}
}
