/*	 
* The code in this project is based on the following Java project created by Yan Qi (https://github.com/yan-qi) :
* https://github.com/yan-qi/k-shortest-paths-java-version
* Tomas Johansson later forked the above Java project into this location:
* https://github.com/TomasJohansson/k-shortest-paths-java-version
* Tomas Johansson later translated the above Java code to C#.NET
* That C# code is currently a part of the Visual Studio solution located here:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
* The current name of the subproject (within the VS solution) with the translated C# code:
* Programmerare.ShortestPaths.Adaptee.YanQi
* 
* Regarding the latest license, Yan Qi has released (19th of Januari 2017) the code with Apache License 2.0
* https://github.com/yan-qi/k-shortest-paths-java-version/commit/d028fd873ff34efc1e851421be380d2382dcb104
* https://github.com/yan-qi/k-shortest-paths-java-version/blob/master/License.txt
* 
* You can also find license information in the files "License.txt" and "NOTICE.txt" in the project root directory.
* 
*/

/*
 * The below copyright statements are included from the original Java code found here:
 * https://github.com/yan-qi/k-shortest-paths-java-version
 * Regarding the translation of that Java code to this .NET code, see above (the top of this source file) for more information.
 * 
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
     * 
     * Regarding the above javadoc author statement it applies to the original Java code found here:
     * https://github.com/yan-qi/k-shortest-paths-java-version
     * Regarding the translation of that Java code to this .NET code, see the top of this source file for more information.
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