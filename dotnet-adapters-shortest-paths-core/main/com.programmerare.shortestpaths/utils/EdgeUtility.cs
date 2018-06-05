/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.api.generics;
using System.Collections.Generic;

namespace com.programmerare.shortestpaths.utils
{
    public sealed class EdgeUtility<E, V, W>
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {
	    private EdgeUtility() {
	    }
	
	    public static EdgeUtility<E, V, W> create<E, V, W>()
            where E : EdgeGenerics<V, W>
            where V : Vertex
            where W : Weight
        {
		    return new EdgeUtility<E, V, W>();
	    }
	
	    private IDictionary<SelectionStrategyWhenEdgesAreDuplicated, SelectionStrategy<E, V, W>> tableLookupMapForSelectionStrategies;
	
	    private IDictionary<SelectionStrategyWhenEdgesAreDuplicated, SelectionStrategy<E, V, W>> getTableLookupMapForSelectionStrategies() {
		    if(tableLookupMapForSelectionStrategies == null) {
			    tableLookupMapForSelectionStrategies = new Dictionary<SelectionStrategyWhenEdgesAreDuplicated, SelectionStrategy<E, V, W>>();
			    tableLookupMapForSelectionStrategies.Add(SelectionStrategyWhenEdgesAreDuplicated.FIRST_IN_LIST_OF_EDGES, new SelectionStrategyFirst<E, V, W>());
			    tableLookupMapForSelectionStrategies.Add(SelectionStrategyWhenEdgesAreDuplicated.LAST_IN_LIST_OF_EDGES, new SelectionStrategyLast<E, V, W>());
			    tableLookupMapForSelectionStrategies.Add(SelectionStrategyWhenEdgesAreDuplicated.SMALLEST_WEIGHT, new SelectionStrategySmallestWeight<E, V, W>());
			    tableLookupMapForSelectionStrategies.Add(SelectionStrategyWhenEdgesAreDuplicated.LARGEST_WEIGHT, new SelectionStrategyLargestWeight<E, V, W>());			
		    }
		    return tableLookupMapForSelectionStrategies;
	    }
	
	    public IList<E> getEdgesWithoutDuplicates(
		    IList<E> edges, 
		    SelectionStrategyWhenEdgesAreDuplicated selectionStrategyWhenEdgesAreDuplicated
	    ) {
		    IDictionary<string, IList<E>> map = getMap(edges);
		    IList<E> reduced = getReduced(edges, map, getTableLookupMapForSelectionStrategies()[selectionStrategyWhenEdgesAreDuplicated] );
		    return reduced;
	    }
	
	    private IList<E> getReduced(
		    IList<E> edges, 
		    IDictionary<string, IList<E>> map,
		    SelectionStrategy<E, V, W> selectionStrategy
	    ) {
		    IList<E> edgesToReturn = new List<E>(); 	
		    foreach (E edge in edges) {
			    string key = edge.getEdgeId();
			    if(map.ContainsKey(key)) {
				    IList<E> list = map[key];
				    E reduce = selectionStrategy.reduce(list);
				    edgesToReturn.Add(reduce);
				    map.Remove(key);
			    }
		    }
		    return edgesToReturn;
	    }

	    private IDictionary<string, IList<E>> getMap(IList<E> edges) {
		    IDictionary<string, IList<E>> map = new Dictionary<string, IList<E>>();
		    foreach (E edge in edges) {
			    IList<E> list;
			    string key = edge.getEdgeId();
			    if(map.ContainsKey(key)) {
				    list = map[key];	
			    }
			    else {
				    list = new List<E>();
				    map.Add(key, list);
			    }
			    list.Add(edge);
		    }		
		    return map;
	    }
    } 
	public enum SelectionStrategyWhenEdgesAreDuplicated {
		FIRST_IN_LIST_OF_EDGES, 
		LAST_IN_LIST_OF_EDGES,
		SMALLEST_WEIGHT,
		LARGEST_WEIGHT
	}
	
	public interface SelectionStrategy<E, V, W> 
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {
		E reduce(IList<E> edges);
	}
	public class SelectionStrategyFirst<E, V, W> : SelectionStrategy<E, V, W> 
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {
		public E reduce(IList<E> edges) {
			return edges[0];
		}
	}
	public class SelectionStrategyLast<E, V, W> : SelectionStrategy<E, V, W> 
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {
		public E reduce(IList<E> edges) {
			return edges[edges.Count-1];
		}
	}
	
	public class SelectionStrategySmallestWeight<E, V, W> : SelectionStrategy<E, V, W> 
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {
		public E reduce(IList<E> edges) {
			double weightMin = double.MaxValue;
			E edgeToReturn = default(E); // null in the Java version
			foreach (E edge in edges) {
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
	public class SelectionStrategyLargestWeight<E, V, W> : SelectionStrategy<E, V, W> 
        where E : EdgeGenerics<V, W>
        where V : Vertex
        where W : Weight
    {
		public E reduce(IList<E> edges) {
			double weightMax = double.MaxValue;
			E edgeToReturn = default(E); // null in the Java version
			foreach (E edge in edges) {
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