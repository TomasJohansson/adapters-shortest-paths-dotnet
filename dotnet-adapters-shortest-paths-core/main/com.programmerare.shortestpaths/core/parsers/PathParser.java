/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.core.parsers;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.programmerare.shortestpaths.core.api.Edge;
import com.programmerare.shortestpaths.core.api.Path;
import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;
import com.programmerare.shortestpaths.core.api.generics.EdgeGenerics;
import com.programmerare.shortestpaths.core.api.generics.PathGenerics;
import com.programmerare.shortestpaths.core.impl.WeightImpl;
import com.programmerare.shortestpaths.core.impl.generics.EdgeGenericsImpl;
import com.programmerare.shortestpaths.core.pathfactories.PathFactory;
import com.programmerare.shortestpaths.core.pathfactories.PathFactoryDefault;
import com.programmerare.shortestpaths.core.pathfactories.PathFactoryGenerics;
import com.programmerare.shortestpaths.core.validation.GraphValidationException;
import com.programmerare.shortestpaths.utils.StringUtility;

/**
 *  TODO: write more/better documentation ...
 * 
 * {@code 
 * String representation of the "List<Path<Edge>>" i.e. the same type returned from the following method: 
 * List<Path<Edge>> shortestPaths = pathFinder.findShortestPaths(startVertex, endVertex, numberOfPathsToFind);
 * The intended purpose is to define strings within xml files with the expected result
 * 
 * Each line ns a string is first the total weight and then the sequence of vertices.
 * Example:  "13 A B D"
 * The simple representation (without weight informatin) is the reason why the list of edges is also needed,
 * i.e. to find the weights.
 * }
 * 
 * @param <P> path
 * @param <E> edge
 * @param <V> vertex
 * @param <W> weight
 
 * @author Tomas Johansson
 *
 */
public final class PathParser<P extends PathGenerics<E, V, W> , E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> {

	//<P extends Path<E,V,W> , E extends Edge<V, W> , V extends Vertex , W extends Weight> implements PathFinder<P, E, V, W>
	
	private final Map<String, E> mapWithEdgesAndVertexConcatenationAsKey;

	private PathFactory<P, E, V, W> pathFactory;// = new PathFactoryGenerics<P, E, V, W>();

	/**
	 * @param pathFactory used for creating an instance of Path<E, V, W> 
	 * @param edgesUsedForFindingTheWeightsBetweenVerticesInPath
	 * @see PathFactory
	 */
	private PathParser(
		final PathFactory<P, E, V, W> pathFactory,
		final List<E> edgesUsedForFindingTheWeightsBetweenVerticesInPath
	) {
		this.pathFactory = pathFactory;
		// TOOD: use input validator here when that branch has been merged into the same code base
//		this.edgesUsedForFindingTheWeightsBetweenVerticesInPath = edgesUsedForFindingTheWeightsBetweenVerticesInPath;
		
		mapWithEdgesAndVertexConcatenationAsKey = new HashMap<String, E>();
		for (E edge : edgesUsedForFindingTheWeightsBetweenVerticesInPath) {
			final String key = EdgeGenericsImpl.createEdgeIdValue(edge.getStartVertex().getVertexId(), edge.getEndVertex().getVertexId());
			mapWithEdgesAndVertexConcatenationAsKey.put(key, edge);
		}
	}
	
	public static <P extends PathGenerics<E, V, W> , E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> PathParser<P, E, V, W> createPathParser(
		final PathFactory<P, E, V, W> pathFactory,
		final List<E> edgesUsedForFindingTheWeightsBetweenVerticesInPath
	) {
		return new PathParser<P, E, V, W>(pathFactory, edgesUsedForFindingTheWeightsBetweenVerticesInPath);
	}
	
	public static <P extends PathGenerics<E, V, W> , E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> PathParser<P, E, V, W> createPathParserGenerics(
		final List<E> edgesUsedForFindingTheWeightsBetweenVerticesInPath
	) {
		return createPathParser(new PathFactoryGenerics<P, E, V, W>(), edgesUsedForFindingTheWeightsBetweenVerticesInPath);
	}	

	public static PathParser<Path , Edge , Vertex , Weight> createPathParserDefault(
		final List<Edge> edgesUsedForFindingTheWeightsBetweenVerticesInPath
	) {
		return createPathParser(new PathFactoryDefault(), edgesUsedForFindingTheWeightsBetweenVerticesInPath);
	}	
	
	
	public List<P> fromStringToListOfPaths(String multiLinedString) {
		final List<String> listOfLines = StringUtility.getMultilineStringAsListOfTrimmedStringsIgnoringLinesWithOnlyWhiteSpace(multiLinedString);
		return fromListOfStringsToListOfPaths(listOfLines);
	}
	
	public List<P> fromListOfStringsToListOfPaths(final List<String> listOfStrings) {
		final List<P> listOfPaths = new ArrayList<P>();
		for (String string : listOfStrings) {
			listOfPaths.add(fromStringToPath(string));
		}
		return listOfPaths;
	}

	/**
	 * @param pathString first the total weight and then the sequence of vertices.
	 * 		Example:  "13 A B D"
	 * @return
	 */
	P fromStringToPath(final String pathString) {
		final String[] array = pathString.split("\\s+");

		// TODO check "array.length" and throw exception ...
		final double totalWeight = Double.parseDouble(array[0]);
		
		final List<E> edges = new ArrayList<E>(); 
		
		for (int i = 2; i < array.length; i++) {
			final String startVertexId = array[i-1];
			final String endVertexId = array[i];
			E edge = getEdgeIncludingTheWeight(startVertexId, endVertexId);
			edges.add(edge);
		}
		W weight = (W) WeightImpl.createWeight(totalWeight);
		return this.createPath(weight, edges);
	}
	
	public String fromPathToString(final P path) {
		final StringBuilder sb = new StringBuilder();
		final double d = path.getTotalWeightForPath().getWeightValue();
		final String s = StringUtility.getDoubleAsStringWithoutZeroesAndDotIfNotRelevant(d);
		sb.append(s);
		final List<E> edgesForPath = path.getEdgesForPath();
		for (final E edge : edgesForPath) {
			sb.append(" ");			
			sb.append(edge.getStartVertex().getVertexId());
		}
		sb.append(" ");		
		sb.append(edgesForPath.get(edgesForPath.size()-1).getEndVertex().getVertexId());
		return sb.toString();
	}

	public E getEdgeIncludingTheWeight(final String startVertexId, final String endVertexId) {
		final String key = EdgeGenericsImpl.createEdgeIdValue(startVertexId, endVertexId);
		if(!mapWithEdgesAndVertexConcatenationAsKey.containsKey(key)) {
			throw new GraphValidationException("No edge with these vertices: from " + startVertexId + " to " + endVertexId);
		}
		return mapWithEdgesAndVertexConcatenationAsKey.get(key);
	}

	private P createPath(W totalWeight, List<E> edges) {
		final P path = this.pathFactory.createPath(totalWeight, edges);
		return path;
	}	
}