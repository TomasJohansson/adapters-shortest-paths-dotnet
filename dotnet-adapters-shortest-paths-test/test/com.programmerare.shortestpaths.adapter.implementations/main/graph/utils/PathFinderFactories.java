package com.programmerare.shortestpaths.graph.utils;

import java.util.ArrayList;
import java.util.List;

import com.programmerare.shortestpaths.adapter.bsmock.PathFinderFactoryBsmock;
import com.programmerare.shortestpaths.adapter.jgrapht.PathFinderFactoryJgrapht;
import com.programmerare.shortestpaths.adapter.mulavito.PathFinderFactoryMulavito;
import com.programmerare.shortestpaths.adapter.reneargento.PathFinderFactoryReneArgento;
import com.programmerare.shortestpaths.adapter.yanqi.PathFinderFactoryYanQi;
import com.programmerare.shortestpaths.core.api.PathFinderFactory;

/**
 * @author Tomas Johansson
 */
public final class PathFinderFactories {
	
	/**
	 * @return a list of implementations that should be used for searching the best paths,
	 * and the implementation results will be verified with each other and the test will cause a failure 
	 * if there is any mismatch found in the results.
	 */	
	public static List<PathFinderFactory>  createPathFinderFactories() {
		List<PathFinderFactory> list = new ArrayList<PathFinderFactory>();
		list.add(new PathFinderFactoryYanQi());
		list.add(new PathFinderFactoryBsmock());
		list.add(new PathFinderFactoryJgrapht());
		list.add(new PathFinderFactoryReneArgento());
		list.add(new PathFinderFactoryMulavito());
		return list;		
	}
}
