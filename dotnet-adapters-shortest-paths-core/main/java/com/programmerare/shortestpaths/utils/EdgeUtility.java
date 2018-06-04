/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.utils;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;
import com.programmerare.shortestpaths.core.api.generics.EdgeGenerics;

public final class EdgeUtility<E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> {

	private EdgeUtility() {
	}
	
	public static <E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> EdgeUtility<E, V, W> create() {
		return new EdgeUtility<E, V, W>();
	}
	
	private Map<SelectionStrategyWhenEdgesAreDuplicated, SelectionStrategy<E, V, W>> tableLookupMapForSelectionStrategies;
	
	private Map<SelectionStrategyWhenEdgesAreDuplicated, SelectionStrategy<E, V, W>> getTableLookupMapForSelectionStrategies() {
		if(tableLookupMapForSelectionStrategies == null) {
			tableLookupMapForSelectionStrategies = new HashMap<SelectionStrategyWhenEdgesAreDuplicated, SelectionStrategy<E, V, W>>();
			tableLookupMapForSelectionStrategies.put(SelectionStrategyWhenEdgesAreDuplicated.FIRST_IN_LIST_OF_EDGES, new SelectionStrategyFirst<E, V, W>());
			tableLookupMapForSelectionStrategies.put(SelectionStrategyWhenEdgesAreDuplicated.LAST_IN_LIST_OF_EDGES, new SelectionStrategyLast<E, V, W>());
			tableLookupMapForSelectionStrategies.put(SelectionStrategyWhenEdgesAreDuplicated.SMALLEST_WEIGHT, new SelectionStrategySmallestWeight<E, V, W>());
			tableLookupMapForSelectionStrategies.put(SelectionStrategyWhenEdgesAreDuplicated.LARGEST_WEIGHT, new SelectionStrategyLargestWeight<E, V, W>());			
		}
		return tableLookupMapForSelectionStrategies;
	}
	
	public List<E> getEdgesWithoutDuplicates(
		final List<E> edges, 
		final SelectionStrategyWhenEdgesAreDuplicated selectionStrategyWhenEdgesAreDuplicated
	) {
		final Map<String, List<E>> map = getMap(edges);
		final List<E> reduced = getReduced(edges, map, getTableLookupMapForSelectionStrategies().get(selectionStrategyWhenEdgesAreDuplicated));
		return reduced;
	}
	
	private List<E> getReduced(
		final List<E> edges, 
		final Map<String, List<E>> map,
		final SelectionStrategy<E, V, W> selectionStrategy
	) {
		final List<E> edgesToReturn = new ArrayList<E>(); 	
		for (final E edge : edges) {
			final String key = edge.getEdgeId();
			if(map.containsKey(key)) {
				final List<E> list = map.get(key);
				final E reduce = selectionStrategy.reduce(list);
				edgesToReturn.add(reduce);
				map.remove(key);
			}
		}
		return edgesToReturn;
	}

	private Map<String, List<E>> getMap(final List<E> edges) {
		final Map<String, List<E>> map = new HashMap<String, List<E>>();
		for (final E edge : edges) {
			final List<E> list;
			final String key = edge.getEdgeId();
			if(map.containsKey(key)) {
				list = map.get(key);	
			}
			else {
				list = new ArrayList<E>();
				map.put(key, list);
			}
			list.add(edge);
		}		
		return map;
	}

	public static enum SelectionStrategyWhenEdgesAreDuplicated {
		FIRST_IN_LIST_OF_EDGES, 
		LAST_IN_LIST_OF_EDGES,
		SMALLEST_WEIGHT,
		LARGEST_WEIGHT;
	}
	
	public static interface SelectionStrategy<E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> {
		E reduce(List<E> edges);
	}
	public static class SelectionStrategyFirst<E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> implements SelectionStrategy<E, V, W> {
		public E reduce(final List<E> edges) {
			return edges.get(0);
		}
	}
	public static class SelectionStrategyLast<E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> implements SelectionStrategy<E, V, W> {
		public E reduce(final List<E> edges) {
			return edges.get(edges.size()-1);
		}
	}
	
	public static class SelectionStrategySmallestWeight<E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> implements SelectionStrategy<E, V, W> {
		public E reduce(final List<E> edges) {
			double weightMin = Double.MAX_VALUE;
			E edgeToReturn = null;
			for (E edge : edges) {
				double w = edge.getEdgeWeight().getWeightValue();
				if(w < weightMin) {
					weightMin = w;
					edgeToReturn = edge;
				}
			}
			return edgeToReturn;
		}
	}
	// TODO: refactor above and below class to reduce duplication
	public static class SelectionStrategyLargestWeight<E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> implements SelectionStrategy<E, V, W> {
		public E reduce(final List<E> edges) {
			double weightMax = Double.MIN_VALUE;
			E edgeToReturn = null;
			for (E edge : edges) {
				double w = edge.getEdgeWeight().getWeightValue();
				if(w > weightMax) {
					weightMax = w;
					edgeToReturn = edge;
				}
			}
			return edgeToReturn;
		}
	}	
} 