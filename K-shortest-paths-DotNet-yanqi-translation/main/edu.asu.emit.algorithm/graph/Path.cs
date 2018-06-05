/*
 *
 * Copyright (c) 2004-2008 Arizona State University.  All rights
 * reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 *
 * 1. Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer.
 *
 * 2. Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in
 *    the documentation and/or other materials provided with the
 *    distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY ARIZONA STATE UNIVERSITY ``AS IS'' AND
 * ANY EXPRESSED OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO,
 * THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
 * PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL ARIZONA STATE UNIVERSITY
 * NOR ITS EMPLOYEES BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
 * LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *
 */
namespace edu.asu.emit.algorithm.graph
{
using java.util;

using edu.asu.emit.algorithm.graph.abstraction;
using System;

/**
 * The class defines a path in graph.
 * 
 * @author yqi
 */
public class Path : BaseElementWithWeight {
	
	private List<BaseVertex> vertexList = new Vector<BaseVertex>();
	private double weight = -1;
	
	public Path() { }
	
	public Path(List<BaseVertex> vertexList, double weight) {
		this.vertexList = vertexList;
		this.weight = weight;
	}

	public double getWeight() {
		return weight;
	}
	
	public void setWeight(double weight) {
		this.weight = weight;
	}
	
	public List<BaseVertex> getVertexList() {
		return vertexList;
	}
	
	public override bool Equals(object right) {
		
		if (right is Path) {
			Path rPath = (Path) right;
			return vertexList.Equals(rPath.vertexList);
		}
		return false;
	}

	public override int GetHashCode() {
		return vertexList.GetHashCode();
	}
	
	public override String ToString() {
		return vertexList.ToString() + ":" + weight;
	}
}
}