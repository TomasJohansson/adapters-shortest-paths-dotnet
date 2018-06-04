/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.core.parsers;

import static com.programmerare.shortestpaths.core.impl.VertexImpl.createVertex;
import static com.programmerare.shortestpaths.core.impl.WeightImpl.createWeight;

import java.util.ArrayList;
import java.util.List;

import com.programmerare.shortestpaths.core.api.Edge;
import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;
import com.programmerare.shortestpaths.core.api.generics.EdgeGenerics;
import com.programmerare.shortestpaths.core.impl.EdgeImpl;
import com.programmerare.shortestpaths.core.impl.generics.EdgeGenericsImpl;
import com.programmerare.shortestpaths.utils.StringUtility;

/**
 * See javadoc comments at the two essential methods in this class, i.e. the methods which convert between String and Edge.

 * @author Tomas Johansson
 *
 * @param <E> edge
 * @param <V> vertex
 * @param <W> weight
 */
public final class EdgeParser<E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> {

	private EdgeFactory<E, V , W> edgeFactory;
	
	/**
	 * when splitting, an regular expression can be used e.g. "\\s++" for matching one or more white space characters
	 * while at the creation you need to be precise e.g. create a string with exactly one space.
	 * However, these two should be compatible in the sense that the string used for creating should be possible 
	 * to parse back for the splitting string (which is the case with the example with a space for creation
	 * and the above mention regular expression for splitting.
	 * The things splitted/created with these strings are the separators in a string like this:
	 * "X Y 12.34" (start vertex id + separator + end vertex id + separator + weight)  
	 */
	private final String separatorBetweenEdgesAndWeightWhenSplitting;
	private final String separatorBetweenEdgesAndWeightWhenCreating;
	
	// The values below should 1,2,3 (and the current default is that order)
	// Such an order 1,2,3 means that the edge parts are in that order e.g. "X Y 12.34" 
	// wherre X (1) is the start vertex id and Y (2) is the id for end vertex and 12.34 (3) is the weight.
	private final int orderForStartVertex;
	private final int orderForEndVertex;
	private final int orderForWeight;

	/**
	 * @param <E> edge 
	 * @param <V> vertex
	 * @param <W> weight
	 * @param separatorBetweenEdgesAndWeightWhenSplitting
	 * @param separatorBetweenEdgesAndWeightWhenCreating
	 * @param orderForStartVertex
	 * @param orderForEndVertex
	 * @param orderForWeight
	 */
	private EdgeParser(
		final EdgeFactory<E, V , W> edgeFactory,
		final String separatorBetweenEdgesAndWeightWhenSplitting, 
		final String separatorBetweenEdgesAndWeightWhenCreating, 
		final int orderForStartVertex, 
		final int orderForEndVertex, 
		final int orderForWeight
	) {
		this.edgeFactory = edgeFactory;
		
		this.separatorBetweenEdgesAndWeightWhenSplitting = separatorBetweenEdgesAndWeightWhenSplitting;
		this.separatorBetweenEdgesAndWeightWhenCreating = separatorBetweenEdgesAndWeightWhenCreating;
		this.orderForStartVertex = orderForStartVertex;
		this.orderForEndVertex = orderForEndVertex;
		this.orderForWeight = orderForWeight;
		int sum = orderForStartVertex + orderForEndVertex + orderForWeight;
		if( 
			(sum != 6) // 1 + 2 + 3 
			||
			(  (orderForStartVertex < 1) || (orderForEndVertex < 1) || (orderForWeight < 1) )
			||
			( (orderForStartVertex > 3) || (orderForEndVertex > 3) || (orderForWeight > 3) )
		) { 
			throw new RuntimeException("The order for each part must be 1,2,3 and not the same order for any two parts. The order input parameters were: " + orderForStartVertex + " , " + orderForEndVertex + " , "  + orderForWeight);
		}
	}

	/**
	 * TODO: if/when this method is opened for public use, then write tests for validatinng correct input data.
	 * @return
	 */
//	public static <E extends Edge<V, W> , V extends Vertex , W extends Weight> EdgeParser<E, V, W> createEdgeParser(Class edgeClass) {
//		return createEdgeParser(edgeClass, "\\s+", " ", 1, 2, 3);
//	}

	/**
	 * @param <E> edge
	 * @param <V> vertex
	 * @param <W> weight
	 * @param edgeFactory factory object used for creating Edge instances
	 * @return an instance of EdgeParser 
	 */
	public static <E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> EdgeParser<E, V, W> createEdgeParser(final EdgeFactory<E, V , W> edgeFactory) {
		return createEdgeParser(edgeFactory, "\\s+", " ", 1, 2, 3);
	}	

	
	/**
	 * Convenience methods.
	 * @param <E> edge
	 * @param <V> vertex
	 * @param <W> weight
	 * @return an instance of EdgeParser constructed with a generics version of the edgeFactory 
	 */
	public static <E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> EdgeParser<E, V, W> createEdgeParserGenerics() {
		return createEdgeParser(new EdgeParser.EdgeFactoryGenerics<E, V, W>());
	}

	/**
	 * Convenience methods.
	 * @return an instance of EdgeParser constructed with a simple/standard version of the edgeFactory 
	 */
	public static EdgeParser<Edge, Vertex, Weight> createEdgeParserDefault() {
		return createEdgeParser(new EdgeParser.EdgeFactoryDefault());
	}	
	
	// TODO: if this method is "opened" for client code i.e. made public then write some tests with validation of input
	
	/**
	 * @param <E> edge
	 * @param <V> vertex
	 * @param <W> weight
	 * @param edgeFactory
	 * @param separatorBetweenEdgesAndWeightWhenSplitting
	 * @param separatorBetweenEdgesAndWeightWhenCreating
	 * @param orderForStartVertex
	 * @param orderForEndVertex
	 * @param orderForWeight
	 * @return
	 */
	private static <E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> EdgeParser<E, V, W> createEdgeParser(
		final EdgeFactory<E, V , W> edgeFactory,
		final String separatorBetweenEdgesAndWeightWhenSplitting, 
		final String separatorBetweenEdgesAndWeightWhenCreating, 
		final int orderForStartVertex, 
		final int orderForEndVertex, 
		final int orderForWeight	
	) {
		return new EdgeParser<E, V, W>(
			edgeFactory,
			separatorBetweenEdgesAndWeightWhenSplitting, 
			separatorBetweenEdgesAndWeightWhenCreating, 
			orderForStartVertex, 
			orderForEndVertex, 
			orderForWeight
		);
	}
	
	
	/**
	 * Typical (intended) usage of the method:
	 * Read input line by line from a file, and each line represents an Edge, which then can be parsed with this method.
	 * @param stringRepresentationOfEdge format: "startVertexId [SPACE] endVertexId [SPACE] weight", 
	 * 	for example "X Y 12.34" for an edge from vertex X to vertex Y with 12.34 as the weight 
	 * @return an Edge
	 */
	public E fromStringToEdge(final String stringRepresentationOfEdge) {
		final String[] array = stringRepresentationOfEdge.split(separatorBetweenEdgesAndWeightWhenSplitting);
		// if(split.length < 3) // TODO throw
		final String startVertexId = array[orderForStartVertex-1];
		final String endVertexId = array[orderForEndVertex-1];
		final double weightValue = Double.parseDouble(array[orderForWeight-1]);
		
		final E e = createEdge(startVertexId, endVertexId, weightValue);
		return e;
	}

	// the purpose of the method name is not reduce the risk of forgetting to refactor .... 
	private E createEdge(final String startVertexId, final String endVertexId, final double weightValue) {
		final V startVertex = (V)createVertex(startVertexId);
		final V endVertex = (V)createVertex(endVertexId);
		final W weight = (W)createWeight(weightValue);
		return (E) edgeFactory.createEdge(startVertex, endVertex, weight);
	}

	/**
	 * An example usage of this method can be to generate )e.g. randomly) lots of Edges for a Graph, to be used in testing.
	 * Then we cab convert the edges to string format with this method and write them to a file, 
	 * and then create test reading from the file and recreating the edges with a corresponding method in this class
	 * which converts in the other direction i.e. from String to Edge.
	 * The reason for doing these things is that you want use regression testing with repeatable deterministic assertions,
	 * which you will not get if you randomly generate new graphs every time.
	 * Regarding how to produce assertions for a randomly generated graph written to a file,
	 * one method is to use the assertions with different implementations, and if three or more independent implementations 
	 * produce the same result, then it is reasonable to assume that the result is correct, and those expected 
	 * assertions might also be generated to a file, rather than every time only being able to assert 
	 * that different implementations produce the same result.  
	 * @param edge an Edge
	 * @return a string representation of the edge for example  "A B 3.7" for an edge from vertex A to B with weight 3.7 
	 */
	public String fromEdgeToString(final E edge) {
		// if(edge == null) // TODO throw		
		final String[] array = new String[3];
		array[orderForStartVertex-1] = edge.getStartVertex().getVertexId();
		array[orderForEndVertex-1] = edge.getEndVertex().getVertexId();
		array[orderForWeight-1] = Double.toString(edge.getEdgeWeight().getWeightValue());
		return array[0] + separatorBetweenEdgesAndWeightWhenCreating + array[1] + separatorBetweenEdgesAndWeightWhenCreating + array[2];
	}

	/**
	 * @param multiLinedString a string including linebreaks where each line defines an edge with vertices and weight. 
	 * A string like this is intended to be surrounded by  xml tags in xml files but the content will then use this method.
	    A B 5
	    A C 6
	    B C 7
	    B D 8
	    C D 9    
	 * @return a list of edges
	 */
	public List<E> fromMultiLinedStringToListOfEdges(final String multiLinedString) {
		final List<E> edges = new ArrayList<E>();
		final List<String> edgesAsStrings = StringUtility.getMultilineStringAsListOfTrimmedStringsIgnoringLinesWithOnlyWhiteSpace(multiLinedString);
		for (String string : edgesAsStrings) {
			edges.add(fromStringToEdge(string));
		}
		return edges;
	}
	
	public interface EdgeFactory<E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> {
		E createEdge(V startVertex, V endVertex, W weightValue);
	}
	
	public static class EdgeFactoryGenerics<E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> implements EdgeFactory<E, V, W> {
		public E createEdge(V startVertex, V endVertex, W weight) {
			EdgeGenerics<V, W> edge = EdgeGenericsImpl.createEdgeGenerics(startVertex, endVertex, weight);
			return (E) edge;
		}
	}
	
	public static class EdgeFactoryDefault implements EdgeFactory<Edge , Vertex , Weight> {
		public Edge createEdge(Vertex startVertex, Vertex endVertex, Weight weight) {
			return EdgeImpl.createEdge(startVertex, endVertex, weight);
		}
	}
}