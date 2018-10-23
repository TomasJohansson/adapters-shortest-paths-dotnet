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
     * The class defines a vertex in the graph
     * 
     * @author yqi
     * 
     * Regarding the above javadoc author statement it applies to the original Java code found here:
     * https://github.com/yan-qi/k-shortest-paths-java-version
     * Regarding the translation of that Java code to this .NET code, see the top of this source file for more information.
     */
    public class Vertex : BaseVertex
        //, Comparable<Vertex>  // used in Java, but not in this C# translated project, see comment below
    {
        // Reason for using Comparable<Vertex> :
        // Java code: Comparable<Vertex> is used because PriorityQueue is 
        // used with natural ordering in class class DijkstraShortestPathAlg:
        // private PriorityQueue<BaseVertex> vertexCandidateQueue = new PriorityQueue<BaseVertex>();
        // https://docs.oracle.com/javase/7/docs/api/java/util/PriorityQueue.html#PriorityQueue()
        // It is the method compareTo below which determines 
        // what should be returned from the priority queue.
        // HOWEVER, this is currently not needed in the C# translation 
        // of the project since a PriorityQueue is used which instead 
        // takes the weight parameter in the add method,
        // which was how the compareTo method was implemented before in this class 
        // i.e. before the method Comparable.compareTo was removed
	
	    private static int currentVertexNum = 0; // Uniquely identify each vertex
	    private int id = currentVertexNum++;
	    private double weight = 0;
	
	    public int GetId() {
		    return id;
	    }

	    public override String ToString() {
		    return "" + id;
	    }

	    public double GetWeight() {
		    return weight;
	    }
	
	    public void SetWeight(double status) {
		    weight = status;
	    }
	
        // see comment at the top of the file regarding why the below Comparable<Vertex> method has been removed
     //   private int compareToo(BaseVertex rVertex) {
	    //	double diff = this.weight - rVertex.getWeight();
	    //	if (diff > 0) {
	    //		return 1;
	    //	} else if (diff < 0) {
	    //		return -1;
	    //	} else { 
	    //		return 0;
	    //	}
	    //}
	
	    public static void Reset() {
		    currentVertexNum = 0;
	    }

        // see comment at the top of the file regarding why the below Comparable<Vertex> methods has been removed

        // Some methods related to conversion from 
        // Java code to .NET code
        // (Java Comparable and .NET IComparable)
        //public int CompareTo(Vertex other)
        //{
        //    return compareToo(other);
        //}

        //public int compareTo(BaseVertex other)
        //{
        //    return compareToo(other);
        //}

        //public int CompareTo(BaseVertex other)
        //{
        //    return compareToo(other);
        //}
    
        //public int compareTo(Vertex rVertex) {
        //    return compareToo(rVertex);
        //}

	    public override bool Equals(object obj) {
		    if (this == obj) return true;
		    if (obj == null) return false;
		    if ((obj is Vertex) || (obj is BaseVertex)) {
                BaseVertex other = (BaseVertex) obj;
                return this.GetId() == other.GetId();
            }
		    return false;
	    }

        public override int GetHashCode() {
            return GetId().GetHashCode();
        }
    }
}