using static com.programmerare.shortestpaths.core.impl.WeightImpl; // SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.api.generics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
namespace com.programmerare.shortestpaths.core.impl.generics
{
    /**
     * @author Tomas Johansson
     */
    public class PathGenericsImpl<E, V, W> : PathGenerics<E, V, W> 
        where E : EdgeGenerics<V, W>
        where V :Vertex
        where W : Weight
    {
	    private readonly W totalWeight;
	    private readonly IList<E> edges;

	    public static PathGenerics<E, V, W> createPathGenerics<E, V, W>(
		    W totalWeight, 
            IList<E> edges
	    )
            where E : EdgeGenerics<V, W>
            where V :Vertex
            where W : Weight            
        {
		    return createPathGenerics<E, V, W>(totalWeight, edges, false, false);
	    }

	    /**
	     * 
	     * @param totalWeight total weight
	     * @param edges list of edges
	     * @param shouldThrowExceptionIfTotalWeightIsMismatching boolean flag for deciding if exception is desired if there is a mismatch in the total weight (which is redundant available in a convenience method but also summable from the list of edges and therefore might mismatch) 
	     * @param shouldThrowExceptionIfAnyMismatchingVertex boolean flag for deciding if exception is desired if there is a mismatch in the vertices of the path edges (i.e. if not the end vertex is the same as the start vertex for the next edge)
	     * @param <E> edge 
	     * @param <V> vertex
	     * @param <W> weight
	     * @return an instance of PathGenericsImpl
	     */
	    public static PathGenerics<E, V, W> createPathGenerics<E, V, W>(
		    W totalWeight, 
		    IList<E> edges, 
		    bool shouldThrowExceptionIfTotalWeightIsMismatching, 
		    bool shouldThrowExceptionIfAnyMismatchingVertex
	    )
            where E : EdgeGenerics<V, W>
            where V :Vertex
            where W : Weight            
        {
		    if(shouldThrowExceptionIfTotalWeightIsMismatching) {
			    if(isTotalWeightNotCorrect<E, V, W>(edges, totalWeight)) {
				    throw new Exception("Incorrect weight " + totalWeight + " not mathcing the sum of the edges " + edges);
			    }			
		    }
		    if(shouldThrowExceptionIfAnyMismatchingVertex) {
			    if(isAnyVertexMismatching<E, V, W>(edges)) {
				    throw new Exception("Mismatching vertices detected " + edges);
			    }
		    }
		    return new PathGenericsImpl<E, V, W>(totalWeight, edges);
	    }	
	
	    private static bool isTotalWeightNotCorrect<E, V, W>(IList<E> edges, W totalWeight)
            where E : EdgeGenerics<V, W>
            where V : Vertex
            where W : Weight
        {
		    double tot = 0;
		    foreach (E edge in edges) {
			    tot += edge.getEdgeWeight().getWeightValue();
		    }
		    return Math.Abs(totalWeight.getWeightValue() - tot) > SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS;
	    }

	    private static bool isAnyVertexMismatching<E, V, W>(IList<E> edges)
            where E : EdgeGenerics<V, W>
            where V : Vertex
            where W : Weight
        {
		    for (int i = 1; i < edges.Count; i++) {
			    E edge = edges[i-1];
			    E nextEdge = edges[i];
			    if(!edge.getEndVertex().Equals(nextEdge.getStartVertex())) {
				    return true;
			    }
		    }
		    return false;
	    }

	    protected PathGenericsImpl(
		    W totalWeight, 
		    IList<E> edges
	    ) {
		    this.totalWeight = totalWeight;
		    this.edges = new ReadOnlyCollection<E>(edges);
	    }	
	
	    /**
	     * {@inheritDoc}
	     */
	    public W getTotalWeightForPath() {
		    return totalWeight;
	    }

	    /**
	     * {@inheritDoc}
	     */	
	    public IList<E> getEdgesForPath() {
		    return edges;
	    }

	    // The three methods below were generated by Eclipse
	
	    public override string ToString() {
		    return "PathImpl [totalWeight=" + totalWeight + ", edges=" + edges + "]";
	    }

	    public override int GetHashCode() {
		    int prime = 31;
		    int result = 1;
		    result = prime * result + ((edges == null) ? 0 : edges.GetHashCode());
		    result = prime * result + ((totalWeight == null) ? 0 : totalWeight.GetHashCode());
		    return result;
	    }

	    public override bool Equals(object obj) {
		    if (this == obj)
			    return true;
		    if (obj == null)
			    return false;
		    if (!(obj is PathGenericsImpl<E, V, W>))
			    return false;
		    PathGenericsImpl<E, V, W> other = (PathGenericsImpl<E, V, W>) obj;
		    if (edges == null) {
			    if (other.edges != null)
				    return false;
		    } else if (!edges.Equals(other.edges))
			    return false;
		    if (totalWeight == null) {
			    if (other.totalWeight != null)
				    return false;
		    } else if (!totalWeight.Equals(other.totalWeight))
			    return false;
		    return true;
	    }
	
    }

}