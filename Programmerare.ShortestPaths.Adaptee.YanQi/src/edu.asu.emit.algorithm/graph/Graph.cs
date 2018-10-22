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
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using edu.asu.emit.algorithm.graph.abstraction;
using edu.asu.emit.algorithm.utils;
using java_to_dotnet_translation_helpers.dot_net_types; 

namespace edu.asu.emit.algorithm.graph {

    /**
     * The class defines a directed graph.
     * 
     * @author yqi
     * 
     * @author Tomas Johansson, implemented (in a fork) a refactoring which extracted code from method 'importFromFile' to 
     * two methods: 'setNumberOfVertices' and 'addEdgeFromStringWithEdgeNamesAndWeight'.
     * For more information about what has changed in the forked version, see the file "NOTICE.txt".
     */
    public class Graph : BaseGraph {
	
	    public static readonly double DISCONNECTED = double.MaxValue;
	
	    // index of fan-outs of one vertex
	    protected IDictionary<int, ISet<BaseVertex>> fanoutVerticesIndex =
		    new Dictionary<int, ISet<BaseVertex>>();
	
	    // index for fan-ins of one vertex
	    protected IDictionary<int, ISet<BaseVertex>> faninVerticesIndex =
		    new Dictionary<int, ISet<BaseVertex>>();
	
	    // index for edge weights in the graph
	    protected IDictionary<Pair<int, int>, Double> vertexPairWeightIndex = 
		    new Dictionary<Pair<int, int>, Double>();
	
	    // index for vertices in the graph
	    protected IDictionary<int, BaseVertex> idVertexIndex = 
		    new Dictionary<int, BaseVertex>();
	
	    // list of vertices in the graph 
	    protected IList<BaseVertex> vertexList = new System.Collections.Generic.List<BaseVertex>();
	
	    // the number of vertices in the graph
	    protected int vertexNum = 0;
	
	    // the number of arcs in the graph
	    protected int edgeNum = 0;
	
	    /**
	     * Constructor 1 
	     * @param dataFileName
	     */
	    public Graph(String dataFileName) {
		    ImportFromFile(dataFileName);
	    }
	
	    /**
	     * Constructor 2
	     * 
	     * @param graph
	     */
	    public Graph(Graph graph) {
		    vertexNum = graph.vertexNum;
		    edgeNum = graph.edgeNum;
		    vertexList.AddAll(graph.vertexList);
		    idVertexIndex.AddAll(graph.idVertexIndex);
		    faninVerticesIndex.AddAll(graph.faninVerticesIndex);
		    fanoutVerticesIndex.AddAll(graph.fanoutVerticesIndex);
		    vertexPairWeightIndex.AddAll(graph.vertexPairWeightIndex);
	    }
	
	    /**
	     * Default constructor 
	     */
	    public Graph() { }
	
	    /**
	     * Clear members of the graph.
	     */
	    public void Clear() {
		    Vertex.Reset();
		    vertexNum = 0;
		    edgeNum = 0; 
		    vertexList.Clear();
		    idVertexIndex.Clear();
		    faninVerticesIndex.Clear();
		    fanoutVerticesIndex.Clear();
		    vertexPairWeightIndex.Clear();
	    }
	
	    /**
	     * There is a requirement for the input graph. 
	     * The ids of vertices must be consecutive. 
	     *  
	     * @param dataFileName
	     */
	    public void ImportFromFile(String dataFileName) {
		    // 0. Clear the variables 
		    Clear();
		
		    try	{
			    // 1. read the file and put the content in the buffer
                var bufRead = new StreamReader(dataFileName);


			    bool isFirstLine = true;
			    String line; 	// String that holds current file line
			    String ss = "";
			    // 2. Read first line
			    line = bufRead.ReadLine();
			    while (line != null) {
				    // 2.1 skip the empty line
				    if (line.Trim().Equals("")) {
					    line = bufRead.ReadLine();
					    continue;
				    }
				
				    // 2.2 generate nodes and edges for the graph
				    if (isFirstLine) {
					    //2.2.1 obtain the number of nodes in the graph 
					    isFirstLine = false;
					    SetNumberOfVertices(int.Parse(line.Trim()));
				    } else {
					    //2.2.2 find a new edge and put it in the graph  
					    AddEdgeFromStringWithEdgeNamesAndWeight(line);
				    }
				    //
				    line = bufRead.ReadLine();
			    }
			    bufRead.Close();

		    } catch (IOException e) {
			    // If another exception is generated, print a stack trace
                Console.WriteLine(e.StackTrace);
		    }
	    }

	    /**
	     * Note that this may not be used externally, because some other members in the class
	     * should be updated at the same time. 
	     * 
	     * @param startVertexId
	     * @param endVertexId
	     * @param weight
	     */
	    protected void AddEdge(int startVertexId, int endVertexId, double weight) {
		    // actually, we should make sure all vertices ids must be correct. 
		    if (!idVertexIndex.ContainsKey(startVertexId) || 
			    !idVertexIndex.ContainsKey(endVertexId) || 
			    startVertexId == endVertexId) {
			    throw new ArgumentException("The edge from " + startVertexId +
					    " to " + endVertexId + " does not exist in the graph.");
		    }
		
		    // update the adjacent-list of the graph
		    ISet<BaseVertex> fanoutVertexSet = new System.Collections.Generic.HashSet<BaseVertex>();
		    if (fanoutVerticesIndex.ContainsKey(startVertexId)) {
			    fanoutVertexSet = fanoutVerticesIndex[startVertexId];
		    }
		    fanoutVertexSet.Add(idVertexIndex[endVertexId]);
		    fanoutVerticesIndex.AddOrReplace(startVertexId, fanoutVertexSet);
		    //
		    ISet<BaseVertex> faninVertexSet = new System.Collections.Generic.HashSet<BaseVertex>();
		    if (faninVerticesIndex.ContainsKey(endVertexId)) {
			    faninVertexSet = faninVerticesIndex[endVertexId];
		    }
		    faninVertexSet.Add(idVertexIndex[startVertexId]);
		    faninVerticesIndex.AddOrReplace(endVertexId, faninVertexSet);
		    // store the new edge 
		    vertexPairWeightIndex.Add(
				    new Pair<int, int>(startVertexId, endVertexId), 
				    weight);
		    ++edgeNum;
	    }
	
	    /**
	     * Store the graph information into a file. 
	     * 
	     * @param fileName
	     */
	    public void ExportToFile(String fileName) {
		    //1. prepare the text to export
		    StringBuilder sb = new StringBuilder();
		    sb.Append(vertexNum + "\n\n");
		    foreach (Pair<int, int> curEdgePair in vertexPairWeightIndex.Keys) {
			    int startingPtId = curEdgePair.First();
			    int endingPtId = curEdgePair.Second();
			    double weight = vertexPairWeightIndex[curEdgePair];
			    sb.Append(startingPtId + "	" + endingPtId + "	" + weight + "\n");
		    }

            throw new NotImplementedException();
            // Java code below not yet translated to .NET

		    //2. open the file and put the data into the file. 
		    //Writer output = null;
		    //try {
		    //	// FileWriter always assumes default encoding is OK!
		    //	output = new BufferedWriter(new FileWriter(new File(fileName)));
		    //	output.write(sb.ToString());
		    //} catch (FileNotFoundException e) {
		    //	e.printStackTrace();
		    //} catch (IOException e) {
		    //	e.printStackTrace();
		    //} finally {
		    //	// flush and close both "output" and its underlying FileWriter
		    //	try {
		    //		if (output != null) {
		    //			output.close();
		    //		}
		    //	} catch (IOException e) {
		    //		e.printStackTrace();
		    //	}
		    //}
	    }
	
	    public virtual ISet<BaseVertex> GetAdjacentVertices(BaseVertex vertex) {
		    return fanoutVerticesIndex.ContainsKey(vertex.GetId()) 
				    ? fanoutVerticesIndex[vertex.GetId()] 
				    : new System.Collections.Generic.HashSet<BaseVertex>();
	    }

	    public virtual ISet<BaseVertex> GetPrecedentVertices(BaseVertex vertex) {
		    return faninVerticesIndex.ContainsKey(vertex.GetId()) 
				    ? faninVerticesIndex[vertex.GetId()] 
				    : new System.Collections.Generic.HashSet<BaseVertex>();
	    }
	
	    public virtual double GetEdgeWeight(BaseVertex source, BaseVertex sink)	{
		    return vertexPairWeightIndex.ContainsKey(
					    new Pair<int, int>(source.GetId(), sink.GetId()))? 
							    vertexPairWeightIndex[
									    new Pair<int, int>(source.GetId(), sink.GetId())]
						      : DISCONNECTED;
	    }

	    /**
	     * Set the number of vertices in the graph
	     * @param num
	     */
	    public void SetVertexNum(int num) {
		    vertexNum = num;
	    }
	
	    /**
	     * Return the vertex list in the graph.
	     */
	    public virtual IList<BaseVertex> GetVertexList() {
		    return vertexList;
	    }
	
	    /**
	     * Get the vertex with the input id.
	     * 
	     * @param id
	     * @return
	     */
	    public virtual BaseVertex GetVertex(int id) {
		    return idVertexIndex[id];
	    }

	    /**
	    * @author Tomas Johansson, added this method as a refactoring, by extracting code from method 'importFromFile' into this method. 
	    * Fork: https://github.com/TomasJohansson/k-shortest-paths-java-version
	    */	
	    protected void AddEdgeFromStringWithEdgeNamesAndWeight(String line) {
            String[] strList = Regex.Split(line.Trim(), @"\s");
		    int startVertexId = int.Parse(strList[0]);
		    int endVertexId = int.Parse(strList[1]);
		    double weight = double.Parse(strList[2]);
		    AddEdge(startVertexId, endVertexId, weight);
	    }

	    /**
	    * @author Tomas Johansson, added this method as a refactoring, by extracting code from method 'importFromFile' into this method. 
	    * Fork: https://github.com/TomasJohansson/k-shortest-paths-java-version
	    */	
	    protected void SetNumberOfVertices(int numberOfVertices) {
		    vertexNum = numberOfVertices;
		    for (int i=0; i<vertexNum; ++i) {
			    BaseVertex vertex = new Vertex();
			    vertexList.Add(vertex);
			    idVertexIndex.Add(vertex.GetId(), vertex);
		    }
	    }	
    }
}