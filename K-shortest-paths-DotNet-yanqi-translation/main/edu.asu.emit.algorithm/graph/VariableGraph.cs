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
using System;
using edu.asu.emit.algorithm.graph.abstraction;
using edu.asu.emit.algorithm.graph.shortestpaths;
using edu.asu.emit.algorithm.utils;
using java.lang;

/**
 * The class defines a graph which can be changed constantly.
 *  
 * @author yqi
 */
public class VariableGraph : Graph {
	private Set<int> remVertexIdSet = new HashSet<int>();
	private Set<Pair<int, int>> remEdgeSet = new HashSet<Pair<int, int>>();

	/**
	 * Default constructor
	 */
	public VariableGraph() { }
	
	/**
	 * Constructor 1
	 * 
	 * @param dataFileName
	 */
	public VariableGraph(String dataFileName): base(dataFileName)	{
	}
	
	/**
	 * Constructor 2
	 * 
	 * @param graph
	 */
	public VariableGraph(Graph graph): base(graph) {
	}

	/**
	 * Set the set of vertices to be removed from the graph
	 * 
	 * @param remVertexList
	 */
	public void setDelVertexIdList(Collection<int> remVertexList) {
		this.remVertexIdSet.addAll(remVertexList);
	}

	/**
	 * Set the set of edges to be removed from the graph
	 * 
	 * @param _rem_edge_hashcode_set
	 */
	public void setDelEdgeHashcodeSet(Collection<Pair<int, int>> remEdgeCollection) {
		remEdgeSet.addAll(remEdgeCollection);
	}
	
	/**
	 * Add an edge to the set of removed edges
	 * 
	 * @param edge
	 */
	public void deleteEdge(Pair<int, int> edge) {
		remEdgeSet.add(edge);
	}
	
	/**
	 * Add a vertex to the set of removed vertices
	 * 
	 * @param vertexId
	 */
	public void deleteVertex(int vertexId) {
		remVertexIdSet.add(vertexId);
	}
	
	public void recoverDeletedEdges() {
		remEdgeSet.clear();
	}

	public void recoverDeletedEdge(Pair<int, int> edge)	{
		remEdgeSet.remove(edge);
	}
	
	public void recoverDeletedVertices() {
		remVertexIdSet.clear();
	}
	
	public void recoverDeletedVertex(int vertexId) {
		remVertexIdSet.remove(vertexId);
	}
	
	/**
	 * Return the weight associated with the input edge.
	 * 
	 * @param source
	 * @param sink
	 * @return
	 */
	public override double getEdgeWeight(BaseVertex source, BaseVertex sink)	{
		int sourceId = source.getId();
		int sinkId = sink.getId();
		
		if (remVertexIdSet.contains(sourceId) || remVertexIdSet.contains(sinkId) ||
		   remEdgeSet.contains(new Pair<int, int>(sourceId, sinkId))) {
			return Graph.DISCONNECTED;
		}
		return base.getEdgeWeight(source, sink);
	}

	/**
	 * Return the weight associated with the input edge.
	 * 
	 * @param source
	 * @param sink
	 * @return
	 */
	public double getEdgeWeightOfGraph(BaseVertex source, BaseVertex sink) {
		return base.getEdgeWeight(source, sink);
	}
	
	/**
	 * Return the set of fan-outs of the input vertex.
	 * 
	 * @param vertex
	 * @return
	 */
	public override Set<BaseVertex> getAdjacentVertices(BaseVertex vertex) {
		Set<BaseVertex> retSet = new HashSet<BaseVertex>();
		int startingVertexId = vertex.getId();
		if (!remVertexIdSet.contains(startingVertexId))	{
			Set<BaseVertex> adjVertexSet = base.getAdjacentVertices(vertex);
			foreach (BaseVertex curVertex in adjVertexSet) {
				int endingVertexId = curVertex.getId();
				if (remVertexIdSet.contains(endingVertexId) ||
					remEdgeSet.contains(new Pair<int, int>(startingVertexId, endingVertexId))) {
					continue;
				}
				// 
				retSet.add(curVertex);
			}
		}
		return retSet;
	}

	/**
	 * Get the set of vertices preceding the input vertex.
	 * 
	 * @param vertex
	 * @return
	 */
	public override Set<BaseVertex> getPrecedentVertices(BaseVertex vertex) {
		Set<BaseVertex> retSet = new HashSet<BaseVertex>();
		if (!remVertexIdSet.contains(vertex.getId())) {
			int endingVertexId = vertex.getId();
			Set<BaseVertex> preVertexSet = base.getPrecedentVertices(vertex);
			foreach (BaseVertex curVertex in preVertexSet) {
				int startingVertexId = curVertex.getId();
				if (remVertexIdSet.contains(startingVertexId) ||
					remEdgeSet.contains(new Pair<int, int>(startingVertexId, endingVertexId))) {
					continue;
				}
				//
				retSet.add(curVertex);
			}
		}
		return retSet;
	}

	/**
	 * Get the list of vertices in the graph, except those removed.
	 * @return
	 */
	public override List<BaseVertex> getVertexList() {
		List<BaseVertex> retList = new Vector<BaseVertex>();
		foreach (BaseVertex curVertex in base.getVertexList()) {
			if (remVertexIdSet.contains(curVertex.getId())) {
				continue;
			}
			retList.add(curVertex);
		}
		return retList;
	}

	/**
	 * Get the vertex corresponding to the input 'id', if exist. 
	 * 
	 * @param id
	 * @return
	 */
	public override BaseVertex getVertex(int id)	{
		if (remVertexIdSet.contains(id)) {
			return null;
		} else {
			return base.getVertex(id);
		}
	}

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		SystemOut.println("Welcome to the class VariableGraph!");
		
		VariableGraph graph = new VariableGraph("data/test_50");
		graph.deleteVertex(13);
		graph.deleteVertex(12);
		graph.deleteVertex(10);
		graph.deleteVertex(23);
		graph.deleteVertex(47);
		graph.deleteVertex(49);
		graph.deleteVertex(3);
		graph.deleteEdge(new Pair<int, int>(26, 41));
		DijkstraShortestPathAlg alg = new DijkstraShortestPathAlg(graph);
		SystemOut.println(alg.getShortestPath(graph.getVertex(0), graph.getVertex(20)));
	}
}
}