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
namespace edu.asu.emit.algorithm.graph.shortestpaths
{
using System;
using System.Collections.Generic;
using edu.asu.emit.algorithm.graph;
using edu.asu.emit.algorithm.graph.abstraction;
using java_to_dotnet_translation_helpers.dot_net_types;

    /**
* @author <a href='mailto:Yan.Qi@asu.edu'>Yan Qi</a>
* @version $Revision: 430 $
* @latest $Date: 2008-07-27 16:31:56 -0700 (Sun, 27 Jul 2008) $
*/
    public class DijkstraShortestPathAlg
{
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
	public void clear()	{
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
	public IDictionary<BaseVertex, Double> getStartVertexDistanceIndex() {
        return startVertexDistanceIndex;
	}

	/**
	 * Getter for the index of the predecessors of vertices
	 * @return
	 */
	public IDictionary<BaseVertex, BaseVertex> getPredecessorIndex() {
        return predecessorIndex;
	}

	/**
	 * Construct a tree rooted at "root" with 
	 * the shortest paths to the other vertices.
	 * 
	 * @param root
	 */
	public void getShortestPathTree(BaseVertex root) {
        determineShortestPaths(root, null, true);
	}
	
	/**
	 * Construct a flower rooted at "root" with 
	 * the shortest paths from the other vertices.
	 * 
	 * @param root
	 */
	public void getShortestPathFlower(BaseVertex root) {
        determineShortestPaths(null, root, false);
	}
	
	/**
	 * Do the work
	 */
	protected void determineShortestPaths(BaseVertex sourceVertex,
                                          BaseVertex sinkVertex, bool isSource2sink)	{
		// 0. clean up variables
		clear();
		
		// 1. initialize members
		BaseVertex endVertex = isSource2sink ? sinkVertex : sourceVertex;
		BaseVertex startVertex = isSource2sink ? sourceVertex : sinkVertex;
		startVertexDistanceIndex.Add(startVertex, 0d);
		startVertex.setWeight(0d);
		vertexCandidateQueue.add(startVertex, startVertex.getWeight());

		// 2. start searching for the shortest path
		while (!vertexCandidateQueue.isEmpty()) {
			BaseVertex curCandidate = vertexCandidateQueue.poll();

			if (curCandidate.Equals(endVertex)) {
                break;
            }

			determinedVertexSet.Add(curCandidate);

			updateVertex(curCandidate, isSource2sink);
		}
	}

	/**
	 * Update the distance from the source to the concerned vertex.
	 * @param vertex
	 */
	private void updateVertex(BaseVertex vertex, bool isSource2sink)	{
		// 1. get the neighboring vertices 
		ISet<BaseVertex> neighborVertexList = isSource2sink ?
			graph.getAdjacentVertices(vertex) : graph.getPrecedentVertices(vertex);
			
		// 2. update the distance passing on current vertex
		foreach (BaseVertex curAdjacentVertex in neighborVertexList) {
			// 2.1 skip if visited before
			if (determinedVertexSet.Contains(curAdjacentVertex)) {
                continue;
            }
			
			// 2.2 calculate the new distance
			double distance = startVertexDistanceIndex.ContainsKey(vertex)?
					startVertexDistanceIndex[vertex] : Graph.DISCONNECTED;
					
			distance += isSource2sink ? graph.getEdgeWeight(vertex, curAdjacentVertex)
					: graph.getEdgeWeight(curAdjacentVertex, vertex);

			// 2.3 update the distance if necessary
			if (!startVertexDistanceIndex.ContainsKey(curAdjacentVertex)
			|| startVertexDistanceIndex[curAdjacentVertex] > distance) {
				startVertexDistanceIndex.AddOrReplace(curAdjacentVertex, distance);

				predecessorIndex.AddOrReplace(curAdjacentVertex, vertex);
				
				curAdjacentVertex.setWeight(distance);
				vertexCandidateQueue.add(curAdjacentVertex, curAdjacentVertex.getWeight());
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
	public Path getShortestPath(BaseVertex sourceVertex, BaseVertex sinkVertex)	{
		determineShortestPaths(sourceVertex, sinkVertex, true);
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
	public Path updateCostForward(BaseVertex vertex) {
		double cost = Graph.DISCONNECTED;
		
		// 1. get the set of successors of the input vertex
		ISet<BaseVertex> adjVertexSet = graph.getAdjacentVertices(vertex);
		
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
			distance += graph.getEdgeWeight(vertex, curVertex);
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
			subPath.setWeight(cost);
			java.util.LinkedList<BaseVertex> vertexList = subPath.getVertexList();
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
	public void correctCostBackward(BaseVertex vertex) {
		// 1. initialize the list of vertex to be updated
		var vertexList = new java.util.LinkedList<BaseVertex>();
		vertexList.add(vertex);
		
		// 2. update the cost of relevant precedents of the input vertex
		while (!vertexList.isEmpty()) {
			BaseVertex curVertex = vertexList.remove(0);
			double costOfCurVertex = startVertexDistanceIndex[curVertex];
			
			ISet<BaseVertex> preVertexSet = graph.getPrecedentVertices(curVertex);
			foreach (BaseVertex preVertex in preVertexSet) {
				double costOfPreVertex = startVertexDistanceIndex.ContainsKey(preVertex) ?
						startVertexDistanceIndex[preVertex] : Graph.DISCONNECTED;
						
				double freshCost = costOfCurVertex + graph.getEdgeWeight(preVertex, curVertex);
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
