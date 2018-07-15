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
using System.Collections.Generic;
using edu.asu.emit.algorithm.graph.abstraction;
using java_to_dotnet_translation_helpers.dot_net_types;

namespace edu.asu.emit.algorithm.graph.shortestpaths {
    /**
    * @author <a href='mailto:Yan.Qi@asu.edu'>Yan Qi</a>
    * @version $Revision: 430 $
    * @latest $Date: 2008-07-27 16:31:56 -0700 (Sun, 27 Jul 2008) $
    */
    public class DijkstraShortestPathAlg {
	    // Input
	    private readonly BaseGraph graph;

	    // Intermediate variables
	    private ISet<BaseVertex> determinedVertexSet = new HashSet<BaseVertex>();
        private java.util.PriorityQueue<BaseVertex> vertexCandidateQueue = new java.util.PriorityQueue<BaseVertex>();
	    private IDictionary<BaseVertex, Double> startVertexDistanceIndex = new Dictionary<BaseVertex, Double>();
	    private IDictionary<BaseVertex, BaseVertex> predecessorIndex = new Dictionary<BaseVertex, BaseVertex>();

	    /**
	     * Default constructor.
	     * @param graph
	     */
	    public DijkstraShortestPathAlg(BaseGraph graph) {
            this.graph = graph;
	    }

	    /**
	     * Clear intermediate variables.
	     */
	    public void Clear()	{
		    determinedVertexSet.Clear();
		    vertexCandidateQueue.clear();
		    startVertexDistanceIndex.Clear();
		    predecessorIndex.Clear();
	    }

	    /**
	     * Getter for the distance in terms of the start vertex
	     * 
	     * @return
	     */
	    public IDictionary<BaseVertex, Double> GetStartVertexDistanceIndex() {
            return startVertexDistanceIndex;
	    }

	    /**
	     * Getter for the index of the predecessors of vertices
	     * @return
	     */
	    public IDictionary<BaseVertex, BaseVertex> GetPredecessorIndex() {
            return predecessorIndex;
	    }

	    /**
	     * Construct a tree rooted at "root" with 
	     * the shortest paths to the other vertices.
	     * 
	     * @param root
	     */
	    public void GetShortestPathTree(BaseVertex root) {
            DetermineShortestPaths(root, null, true);
	    }
	
	    /**
	     * Construct a flower rooted at "root" with 
	     * the shortest paths from the other vertices.
	     * 
	     * @param root
	     */
	    public void GetShortestPathFlower(BaseVertex root) {
            DetermineShortestPaths(null, root, false);
	    }
	
	    /**
	     * Do the work
	     */
	    protected void DetermineShortestPaths(BaseVertex sourceVertex,
                                              BaseVertex sinkVertex, bool isSource2sink)	{
		    // 0. clean up variables
		    Clear();
		
		    // 1. initialize members
		    BaseVertex endVertex = isSource2sink ? sinkVertex : sourceVertex;
		    BaseVertex startVertex = isSource2sink ? sourceVertex : sinkVertex;
		    startVertexDistanceIndex.Add(startVertex, 0d);
		    startVertex.SetWeight(0d);
		    vertexCandidateQueue.add(startVertex, startVertex.GetWeight());

		    // 2. start searching for the shortest path
		    while (!vertexCandidateQueue.isEmpty()) {
			    BaseVertex curCandidate = vertexCandidateQueue.poll();

			    if (curCandidate.Equals(endVertex)) {
                    break;
                }

			    determinedVertexSet.Add(curCandidate);

			    UpdateVertex(curCandidate, isSource2sink);
		    }
	    }

	    /**
	     * Update the distance from the source to the concerned vertex.
	     * @param vertex
	     */
	    private void UpdateVertex(BaseVertex vertex, bool isSource2sink)	{
		    // 1. get the neighboring vertices 
		    ISet<BaseVertex> neighborVertexList = isSource2sink ?
			    graph.GetAdjacentVertices(vertex) : graph.GetPrecedentVertices(vertex);
			
		    // 2. update the distance passing on current vertex
		    foreach (BaseVertex curAdjacentVertex in neighborVertexList) {
			    // 2.1 skip if visited before
			    if (determinedVertexSet.Contains(curAdjacentVertex)) {
                    continue;
                }
			
			    // 2.2 calculate the new distance
			    double distance = startVertexDistanceIndex.ContainsKey(vertex)?
					    startVertexDistanceIndex[vertex] : Graph.DISCONNECTED;
					
			    distance += isSource2sink ? graph.GetEdgeWeight(vertex, curAdjacentVertex)
					    : graph.GetEdgeWeight(curAdjacentVertex, vertex);

			    // 2.3 update the distance if necessary
			    if (!startVertexDistanceIndex.ContainsKey(curAdjacentVertex)
			    || startVertexDistanceIndex[curAdjacentVertex] > distance) {
				    startVertexDistanceIndex.AddOrReplace(curAdjacentVertex, distance);

				    predecessorIndex.AddOrReplace(curAdjacentVertex, vertex);
				
				    curAdjacentVertex.SetWeight(distance);
				    vertexCandidateQueue.add(curAdjacentVertex, curAdjacentVertex.GetWeight());
			    }
		    }
	    }
	
	    /**
	     * Note that, the source should not be as same as the sink! (we could extend 
	     * this later on)
	     *  
	     * @param sourceVertex
	     * @param sinkVertex
	     * @return
	     */
	    public Path GetShortestPath(BaseVertex sourceVertex, BaseVertex sinkVertex)	{
		    DetermineShortestPaths(sourceVertex, sinkVertex, true);
		    //
		    java.util.LinkedList<BaseVertex> vertexList = new java.util.LinkedList<BaseVertex>();
		    double weight = startVertexDistanceIndex.ContainsKey(sinkVertex) ?
			    startVertexDistanceIndex[sinkVertex] : Graph.DISCONNECTED;
		    if (weight != Graph.DISCONNECTED) {
			    BaseVertex curVertex = sinkVertex;
			    do {
				    vertexList.add(curVertex);
				    curVertex = predecessorIndex[curVertex];
			    } while (curVertex != null && curVertex != sourceVertex);
			    vertexList.add(sourceVertex);
                vertexList.reverse();
		    }
		    return new Path(vertexList, weight);
	    }

	    /**
	     * Calculate the distance from the target vertex to the input 
	     * vertex using forward star form. 
	     * (FLOWER)
	     * 
	     * @param vertex
	     */
	    public Path UpdateCostForward(BaseVertex vertex) {
		    double cost = Graph.DISCONNECTED;
		
		    // 1. get the set of successors of the input vertex
		    ISet<BaseVertex> adjVertexSet = graph.GetAdjacentVertices(vertex);
		
		    // 2. make sure the input vertex exists in the index
		    if (!startVertexDistanceIndex.ContainsKey(vertex)) {
			    startVertexDistanceIndex.Add(vertex, Graph.DISCONNECTED);
		    }
		
		    // 3. update the distance from the root to the input vertex if necessary
		    foreach (BaseVertex curVertex in adjVertexSet) {
			    // 3.1 get the distance from the root to one successor of the input vertex
			    double distance = startVertexDistanceIndex.ContainsKey(curVertex)?
					    startVertexDistanceIndex[curVertex] : Graph.DISCONNECTED;
					
			    // 3.2 calculate the distance from the root to the input vertex
			    distance += graph.GetEdgeWeight(vertex, curVertex);
			    //distance += ((VariableGraph)graph).get_edge_weight_of_graph(vertex, curVertex);
			
			    // 3.3 update the distance if necessary 
			    double costOfVertex = startVertexDistanceIndex[vertex];
			    if(costOfVertex > distance)	{
				    startVertexDistanceIndex.AddOrReplace(vertex, distance);
				    predecessorIndex.AddOrReplace(vertex, curVertex);
				    cost = distance;
			    }
		    }
		
		    // 4. create the subPath if exists
		    Path subPath = null;
		    if (cost < Graph.DISCONNECTED) {
			    subPath = new Path();
			    subPath.SetWeight(cost);
			    java.util.LinkedList<BaseVertex> vertexList = subPath.GetVertexList();
			    vertexList.add(vertex);
			
			    BaseVertex selVertex = predecessorIndex[vertex];
			    while (selVertex != null) {
				    vertexList.add(selVertex);
				    selVertex = predecessorIndex.GetValueIfExists(selVertex);
			    }
		    }
		
		    return subPath;
	    }
	
	    /**
	     * Correct costs of successors of the input vertex using backward star form.
	     * (FLOWER)
	     * 
	     * @param vertex
	     */
	    public void CorrectCostBackward(BaseVertex vertex) {
		    // 1. initialize the list of vertex to be updated
		    var vertexList = new java.util.LinkedList<BaseVertex>();
		    vertexList.add(vertex);
		
		    // 2. update the cost of relevant precedents of the input vertex
		    while (!vertexList.isEmpty()) {
			    BaseVertex curVertex = vertexList.remove(0);
			    double costOfCurVertex = startVertexDistanceIndex[curVertex];
			
			    ISet<BaseVertex> preVertexSet = graph.GetPrecedentVertices(curVertex);
			    foreach (BaseVertex preVertex in preVertexSet) {
				    double costOfPreVertex = startVertexDistanceIndex.ContainsKey(preVertex) ?
						    startVertexDistanceIndex[preVertex] : Graph.DISCONNECTED;
						
				    double freshCost = costOfCurVertex + graph.GetEdgeWeight(preVertex, curVertex);
				    if (costOfPreVertex > freshCost) {
					    startVertexDistanceIndex.AddOrReplace(preVertex, freshCost);
					    predecessorIndex.AddOrReplace(preVertex, curVertex);
					    vertexList.add(preVertex);
				    }
			    }
		    }
	    }
	
    }
}
