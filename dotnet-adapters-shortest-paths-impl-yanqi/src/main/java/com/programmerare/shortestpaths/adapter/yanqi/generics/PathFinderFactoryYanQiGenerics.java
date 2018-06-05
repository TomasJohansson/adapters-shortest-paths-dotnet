package com.programmerare.shortestpaths.adapter.yanqi.generics;

import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;
import com.programmerare.shortestpaths.core.api.generics.EdgeGenerics;
import com.programmerare.shortestpaths.core.api.generics.GraphGenerics;
import com.programmerare.shortestpaths.core.api.generics.PathFinderFactoryGenerics;
import com.programmerare.shortestpaths.core.api.generics.PathFinderGenerics;
import com.programmerare.shortestpaths.core.api.generics.PathGenerics;
import com.programmerare.shortestpaths.core.impl.generics.PathFinderFactoryGenericsBase;

/**
 * @author Tomas Johansson
 */
public class PathFinderFactoryYanQiGenerics<F extends PathFinderGenerics<P, E,V,W> , P extends PathGenerics<E, V, W> ,  E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> 
	extends PathFinderFactoryGenericsBase<F,P,E,V,W> 
	implements PathFinderFactoryGenerics<F,P,E,V,W> 
{
	public F createPathFinder(
		final GraphGenerics<E, V, W> graph 
	) {
		// TODO: try to get rid of the casting below ( warning: "Type safety: Unchecked cast from PathFinderYanQi<P,E,V,W> to F" )
		return (F) new PathFinderYanQiGenerics<P, E, V, W>(
			graph 
		);
	}	
}