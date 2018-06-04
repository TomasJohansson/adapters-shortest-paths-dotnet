/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.core.impl;

import com.programmerare.shortestpaths.core.api.Vertex;

/**
 * @author Tomas Johansson
 */
public final class VertexImpl implements Vertex {

	private final String id;
	
	public static Vertex createVertex(
		final String id		
	) {
		return new VertexImpl(
			id
		);
	}

	public static Vertex createVertex(
		final int id			
	) {
		return createVertex(Integer.toString(id));
	}
	

	
	private VertexImpl(final String id) {
		this.id = id;
	}

	public String getVertexId() {
		return id;
	}

	@Override
	public String toString() {
		return "VertexImpl [id=" + id + "]";
	}

	@Override
	public int hashCode() {
		final int prime = 31;
		int result = 1;
		result = prime * result + ((id == null) ? 0 : id.hashCode());
		return result;
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (!(obj instanceof VertexImpl))
			return false;
		VertexImpl other = (VertexImpl) obj;
		if (id == null) {
			if (other.id != null)
				return false;
		} else if (!id.equals(other.id))
			return false;
		return true;
	}

	public String renderToString() {
		return toString();
	}

//	public Vertex create(final String id) {
//		return createVertex(id);
//	}
	
}