/*
* Regarding the license (Apache), please find more information 
* in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and also license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

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
using edu.asu.emit.algorithm.graph.abstraction;
using System;

namespace edu.asu.emit.algorithm.graph {

    /**
     * The class defines a path in graph.
     * 
     * @author yqi
     */
    public class Path : BaseElementWithWeight {
	
	    private java.util.LinkedList<BaseVertex> vertexList = new java.util.LinkedList<BaseVertex>();
	    private double weight = -1;
	
	    public Path() { }
	
	    public Path(java.util.LinkedList<BaseVertex> vertexList, double weight) {
		    this.vertexList = vertexList;
		    this.weight = weight;
	    }

	    public double GetWeight() {
		    return weight;
	    }
	
	    public void SetWeight(double weight) {
		    this.weight = weight;
	    }
	
	    public java.util.LinkedList<BaseVertex> GetVertexList() {
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