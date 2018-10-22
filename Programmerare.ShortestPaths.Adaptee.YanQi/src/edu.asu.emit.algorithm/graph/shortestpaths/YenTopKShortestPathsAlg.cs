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
using edu.asu.emit.algorithm.utils;
using java_to_dotnet_translation_helpers.dot_net_types;
using System.Collections.Generic;

namespace edu.asu.emit.algorithm.graph.shortestpaths {
    /**
    * @author <a href='mailto:Yan.Qi@asu.edu'>Yan Qi</a>
    * @version $Revision: 783 $
    * @latest $Id: YenTopKShortestPathsAlg.java 783 2009-06-19 19:19:27Z qyan $
    */
    public class YenTopKShortestPathsAlg {
	    private VariableGraph graph = null;

	    // intermediate variables
	    private IList<Path> resultList = new List<Path>();
	    private IDictionary<Path, BaseVertex> pathDerivationVertexIndex = new Dictionary<Path, BaseVertex>();
	    private QYPriorityQueue<Path> pathCandidates = new QYPriorityQueue<Path>();
	
	    // the ending vertices of the paths
	    private BaseVertex sourceVertex = null;
	    private BaseVertex targetVertex = null;
	
	    // variables for debugging and testing
	    private int generatedPathNum = 0;
	
	    /**
	     * Default constructor.
	     * 
	     * @param graph
	     * @param k
	     */
	    public YenTopKShortestPathsAlg(BaseGraph graph): this(graph, null, null)	{
	    }
	
	    /**
	     * Constructor 2
	     * 
	     * @param graph
	     * @param sourceVertex
	     * @param targetVertex
	     */
	    public YenTopKShortestPathsAlg(BaseGraph graph, 
			    BaseVertex sourceVertex, BaseVertex targetVertex)	{
		    if (graph == null) {
			    throw new System.ArgumentException("A NULL graph object occurs!");
		    }
		    this.graph = new VariableGraph((Graph)graph);
		    this.sourceVertex = sourceVertex;
		    this.targetVertex = targetVertex;
		    Init();
	    }
	
	    /**
	     * Initiate members in the class. 
	     */
	    private void Init()	{
		    Clear();
		    // get the shortest path by default if both source and target exist
		    if (sourceVertex != null && targetVertex != null) {
			    Path shortestPath = GetShortestPath(sourceVertex, targetVertex);
			    if (shortestPath.GetVertexList().size() > 0) {
				    pathCandidates.Add(shortestPath);
				    pathDerivationVertexIndex.Add(shortestPath, sourceVertex);
			    }
		    }
	    }
	
	    /**
	     * Clear the variables of the class. 
	     */
	    public void Clear()	{
		    pathCandidates = new QYPriorityQueue<Path>();
		    pathDerivationVertexIndex.Clear();
		    resultList.Clear();
		    generatedPathNum = 0;
	    }
	
	    /**
	     * Obtain the shortest path connecting the source and the target, by using the
	     * classical Dijkstra shortest path algorithm. 
	     * 
	     * @param sourceVertex
	     * @param targetVertex
	     * @return
	     */
	    public Path GetShortestPath(BaseVertex sourceVertex, BaseVertex targetVertex)	{
		    DijkstraShortestPathAlg dijkstraAlg = new DijkstraShortestPathAlg(graph);
		    return dijkstraAlg.GetShortestPath(sourceVertex, targetVertex);
	    }
	
	    /**
	     * Check if there exists a path, which is the shortest among all candidates.  
	     * 
	     * @return
	     */
	    public bool HasNext() {
            return !pathCandidates.IsEmpty();
	    }
	
	    /**
	     * Get the shortest path among all that connecting source with targe. 
	     * 
	     * @return
	     */
	    public Path Next() {
		    //3.1 prepare for removing vertices and arcs
		    Path curPath = pathCandidates.Poll();
		    resultList.Add(curPath);

		    BaseVertex curDerivation = pathDerivationVertexIndex[curPath];
		    int curPathHash =
			    curPath.GetVertexList().subList(0, curPath.GetVertexList().indexOf(curDerivation)).GetHashCode();
		
		    int count = resultList.Count;
		
		    //3.2 remove the vertices and arcs in the graph
		    for (int i = 0; i < count-1; ++i) {
			    Path curResultPath = resultList[i];
							
			    int curDevVertexId =
				    curResultPath.GetVertexList().indexOf(curDerivation);
			
			    if (curDevVertexId < 0) {
                    continue;
                }

			    // Note that the following condition makes sure all candidates should be considered. 
			    /// The algorithm in the paper is not correct for removing some candidates by mistake. 
			    int pathHash = curResultPath.GetVertexList().subList(0, curDevVertexId).GetHashCode();
			    if (pathHash != curPathHash) {
                    continue;
                }
			
			    BaseVertex curSuccVertex =
				    curResultPath.GetVertexList().get(curDevVertexId + 1);
			
			    graph.DeleteEdge(new Pair<int, int>(
                        curDerivation.GetId(), curSuccVertex.GetId()));
		    }
		
		    int pathLength = curPath.GetVertexList().size();
		    java.util.LinkedList<BaseVertex> curPathVertexList = curPath.GetVertexList();
		    for (int i = 0; i < pathLength-1; ++i) {
			    graph.DeleteVertex(curPathVertexList.get(i).GetId());
			    graph.DeleteEdge(new Pair<int, int>(
                        curPathVertexList.get(i).GetId(),
                        curPathVertexList.get(i + 1).GetId()));
		    }
		
		    //3.3 calculate the shortest tree rooted at target vertex in the graph
		    DijkstraShortestPathAlg reverseTree = new DijkstraShortestPathAlg(graph);
		    reverseTree.GetShortestPathFlower(targetVertex);
		
		    //3.4 recover the deleted vertices and update the cost and identify the new candidate results
		    bool isDone = false;
		    for (int i=pathLength-2; i>=0 && !isDone; --i)	{
			    //3.4.1 get the vertex to be recovered
			    BaseVertex curRecoverVertex = curPathVertexList.get(i);
			    graph.RecoverDeletedVertex(curRecoverVertex.GetId());
			
			    //3.4.2 check if we should stop continuing in the next iteration
			    if (curRecoverVertex.GetId() == curDerivation.GetId()) {
				    isDone = true;
			    }
			
			    //3.4.3 calculate cost using forward star form
			    Path subPath = reverseTree.UpdateCostForward(curRecoverVertex);
			
			    //3.4.4 get one candidate result if possible
			    if (subPath != null) {
				    ++generatedPathNum;
				
				    //3.4.4.1 get the prefix from the concerned path
				    double cost = 0; 
				    IList<BaseVertex> prePathList = new List<BaseVertex>();
				    reverseTree.CorrectCostBackward(curRecoverVertex);
				
				    for (int j=0; j<pathLength; ++j) {
					    BaseVertex curVertex = curPathVertexList.get(j);
					    if (curVertex.GetId() == curRecoverVertex.GetId()) {
						    j=pathLength;
					    } else {
						    cost += graph.GetEdgeWeightOfGraph(curPathVertexList.get(j),
								    curPathVertexList.get(j+1));
						    prePathList.Add(curVertex);
					    }
				    }
				    prePathList.AddAll(subPath.GetVertexList());

				    //3.4.4.2 compose a candidate
				    subPath.SetWeight(cost + subPath.GetWeight());
				    subPath.GetVertexList().clear();
				    subPath.GetVertexList().addAll(prePathList);
				
				    //3.4.4.3 put it in the candidate pool if new
				    if (!pathDerivationVertexIndex.ContainsKey(subPath)) {
					    pathCandidates.Add(subPath);
					    pathDerivationVertexIndex.Add(subPath, curRecoverVertex);
				    }
			    }
			
			    //3.4.5 restore the edge
			    BaseVertex succVertex = curPathVertexList.get(i + 1);
			    graph.RecoverDeletedEdge(new Pair<int, int>(
                        curRecoverVertex.GetId(), succVertex.GetId()));
			
			    //3.4.6 update cost if necessary
			    double cost1 = graph.GetEdgeWeight(curRecoverVertex, succVertex)
				    + reverseTree.GetStartVertexDistanceIndex()[succVertex];
			
			    if (reverseTree.GetStartVertexDistanceIndex()[curRecoverVertex] >  cost1) {
				    reverseTree.GetStartVertexDistanceIndex().AddOrReplace(curRecoverVertex, cost1);
				    reverseTree.GetPredecessorIndex().AddOrReplace(curRecoverVertex, succVertex);
				    reverseTree.CorrectCostBackward(curRecoverVertex);
			    }
		    }
		
		    //3.5 restore everything
		    graph.RecoverDeletedEdges();
		    graph.RecoverDeletedVertices();
		
		    return curPath;
	    }
	
	    /**
	     * Get the top-K shortest paths connecting the source and the target.  
	     * This is a batch execution of top-K results.
	     * 
	     * @param source
	     * @param sink
	     * @param k
	     * @return
	     */
	    public IList<Path> GetShortestPaths(BaseVertex source,
                                           BaseVertex target, int k) {
		    sourceVertex = source;
		    targetVertex = target;
		
		    Init();
		    int count = 0;
		    while (HasNext() && count < k) {
			    Next();
			    ++count;
		    }
		
		    return resultList;
	    }
		
	    /**
	     * Return the list of results generated on the whole.
	     * (Note that some of them are duplicates)
	     * @return
	     */
	    public IList<Path> GetResultList() {
            return resultList;
	    }

	    /**
	     * The number of distinct candidates generated on the whole. 
	     * @return
	     */
	    public int GetCadidateSize() {
		    return pathDerivationVertexIndex.Count;
	    }

	    public int GetGeneratedPathSize() {
		    return generatedPathNum;
	    }
    }
}