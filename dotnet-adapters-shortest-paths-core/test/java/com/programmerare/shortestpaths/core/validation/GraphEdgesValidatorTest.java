/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.core.validation;

import static com.programmerare.shortestpaths.core.impl.PathImpl.createPath;
import static com.programmerare.shortestpaths.core.impl.EdgeImpl.createEdge;
import static com.programmerare.shortestpaths.core.validation.GraphEdgesValidator.createGraphEdgesValidator;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.junit.Before;
import org.junit.Test;

import com.programmerare.shortestpaths.core.api.Edge;
import com.programmerare.shortestpaths.core.api.Path;
import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;

/**
 * @author Tomas Johansson
 */
public class GraphEdgesValidatorTest {

	// <P extends Path<E,V,W> , E extends Edge<V, W> , V extends Vertex , W extends Weight>
	private GraphEdgesValidator<Path, Edge, Vertex , Weight > graphEdgesValidator;

	private Vertex vertexA;
	private Vertex vertexB;
	private Vertex vertexC;
	private Vertex vertexD;
	private Vertex vertexWithNullAsId;
	private Vertex vertexWithEmptyStringAsId;
	private Vertex vertexWithSomeSpacesAsId;

	private Weight weight5;
	private Weight weight6;
	private Weight weight7;	

	private Map<String, Boolean> mapForValidatingUniqueEdgeId;
	private Map<String, Boolean> mapForValidatingUniqueVerticesIds;

	// the reason for these trivial strings below are that they provide semantic as actual parameters rather than invoking with null or "" as parameter  
	private String stringIdNull = null;
	private String stringIdEmpty = "";
	private String stringIdSomeSpaces = "   ";
	private String stringIdX = "x";
	private String stringIdY = "y";
	private String stringIdZ = "Z";
	
	@Before
	public void setUp() throws Exception {
		graphEdgesValidator = createGraphEdgesValidator();
		
		vertexA = createTestVertex("A");
		vertexB = createTestVertex("B");
		vertexC = createTestVertex("C");
		vertexD = createTestVertex("D");
		vertexWithNullAsId = createTestVertex(null);
		vertexWithEmptyStringAsId = createTestVertex("");
		vertexWithSomeSpacesAsId = createTestVertex("   ");
		
		// interface method for Weight use primitive type as return type in method: "public double getWeightValue()"
		// i.e. since it does not return an instance of class "Double" (capital "D") we do not have any null value tests here   
		weight5 = createTestWeight(5);
		weight6 = createTestWeight(6);
		weight7 = createTestWeight(7);
		
		mapForValidatingUniqueEdgeId = new HashMap<String, Boolean>();
		mapForValidatingUniqueVerticesIds = new HashMap<String, Boolean>();
	}

	
	// ----------------------------------------------------------------------------------------------
	// tests for validateNonNullObjects below
	
	public void testValidateNonNullObjects_whenAllEdgePartsAreValid() {
		graphEdgesValidator.validateNonNullObjects(createTestEdge(stringIdX, vertexA, vertexB, weight5));
	}
	
	@Test(expected = GraphValidationException.class)
	public void testValidateNonNullObjects_whenEdgeIsNull() {
		graphEdgesValidator.validateNonNullObjects(null);
	}
	
	@Test(expected = GraphValidationException.class)
	public void testValidateNonNullObjects_whenStartVertexIsNull() {
		graphEdgesValidator.validateNonNullObjects(createTestEdge(stringIdX, null, vertexA, weight6));
	}
	
	@Test(expected = GraphValidationException.class)
	public void testValidateNonNullObjects_whenEndVertexIsNull() {
		graphEdgesValidator.validateNonNullObjects(createTestEdge(stringIdX, vertexA, null, weight6));
	}
	
	@Test(expected = GraphValidationException.class)
	public void testValidateNonNullObjects_whenWeightIsNull() {
		graphEdgesValidator.validateNonNullObjects(createTestEdge(stringIdX, vertexA, vertexB, null));
	}	

	// tests for validateNonNullObjects above
	// ----------------------------------------------------------------------------------------------
	// tests for validateNonBlankIds below
	@Test
	public void testValidateNonBlankIds_whenAllEdgePartsAreValid() {
		graphEdgesValidator.validateNonBlankIds(createTestEdge(stringIdX, vertexA, vertexB, weight5));
	}
	
	// - - - - - -
	// Three tests for edge id:
	@Test(expected = GraphValidationException.class)
	public void testValidateNonBlankIds_whenEdgeIdIsNull() {
		graphEdgesValidator.validateNonBlankIds(createTestEdge(stringIdNull, vertexA, vertexB, weight5));
	}	
	
	@Test(expected = GraphValidationException.class)
	public void testValidateNonBlankIds_whenEdgeIdIsEmptyString() {
		graphEdgesValidator.validateNonBlankIds(createTestEdge(stringIdEmpty, vertexA, vertexB, weight5));
	}
	
	@Test(expected = GraphValidationException.class)
	public void testValidateNonBlankIds_whenEdgeIdIsSomeSpaces() {
		graphEdgesValidator.validateNonBlankIds(createTestEdge(stringIdSomeSpaces, vertexA, vertexB, weight5));
	}	
	
	// - - - - - -
	// Three tests for start vertex id:
	@Test(expected = GraphValidationException.class)
	public void testValidateNonBlankIds_whenStartVertexIdIsNull() {
		graphEdgesValidator.validateNonBlankIds(createTestEdge(stringIdNull, vertexWithNullAsId, vertexB, weight5));
	}	
	
	@Test(expected = GraphValidationException.class)
	public void testValidateNonBlankIds_whenStartVertexIdIsEmptyString() {
		graphEdgesValidator.validateNonBlankIds(createTestEdge(stringIdEmpty, vertexWithEmptyStringAsId, vertexB, weight5));
	}
	
	@Test(expected = GraphValidationException.class)
	public void testValidateNonBlankIds_whenStartVertexIdIsSomeSpaces() {
		graphEdgesValidator.validateNonBlankIds(createTestEdge(stringIdSomeSpaces, vertexWithSomeSpacesAsId, vertexB, weight5));
	}
	
	// - - - - - -
	// Three tests for end vertex id:
	@Test(expected = GraphValidationException.class)
	public void testValidateNonBlankIds_whenEndVertexIdIsNull() {
		graphEdgesValidator.validateNonBlankIds(createTestEdge(stringIdNull, vertexA, vertexWithNullAsId, weight5));
	}	
	
	@Test(expected = GraphValidationException.class)
	public void testValidateNonBlankIds_whenEndVertexIdIsEmptyString() {
		graphEdgesValidator.validateNonBlankIds(createTestEdge(stringIdEmpty, vertexA, vertexWithEmptyStringAsId, weight5));
	}
	
	@Test(expected = GraphValidationException.class)
	public void testValidateNonBlankIds_whenEndVertexIdIsSomeSpaces() {
		graphEdgesValidator.validateNonBlankIds(createTestEdge(stringIdSomeSpaces, vertexA, vertexWithSomeSpacesAsId, weight5));
	}
	// tests for validateNonBlankIds above
	// ----------------------------------------------------------------------------------------------
	
	// tests for validateUniqueEdgeId below	
	@Test
	public void testValidateUniqueEdgeId_whenAlllAreUnique() {
		List<Edge> edges = Arrays.asList(
			createTestEdge(stringIdX, vertexA, vertexB, weight5),
			createTestEdge(stringIdY, vertexB, vertexC, weight6),
			createTestEdge(stringIdZ, vertexC, vertexD, weight7)
		);
		graphEdgesValidator.validateUniqueEdgeId(edges.get(0), mapForValidatingUniqueEdgeId);
		graphEdgesValidator.validateUniqueEdgeId(edges.get(1), mapForValidatingUniqueEdgeId);
		graphEdgesValidator.validateUniqueEdgeId(edges.get(2), mapForValidatingUniqueEdgeId);
	}

	@Test(expected = GraphValidationException.class)
	public void testValidateUniqueEdgeId_whenAlllAreNotUnique() {
		List<Edge> edges = Arrays.asList(
			createTestEdge(stringIdX, vertexA, vertexB, weight5),
			createTestEdge(stringIdY, vertexB, vertexC, weight6),
			createTestEdge(stringIdX, vertexC, vertexD, weight7) // Note X again, i.e. x NOT unique, should cause Exception 
		);
		graphEdgesValidator.validateUniqueEdgeId(edges.get(0), mapForValidatingUniqueEdgeId);
		graphEdgesValidator.validateUniqueEdgeId(edges.get(1), mapForValidatingUniqueEdgeId);
		graphEdgesValidator.validateUniqueEdgeId(edges.get(2), mapForValidatingUniqueEdgeId);
	}
	
	// tests for validateUniqueEdgeId above
	// ----------------------------------------------------------------------------------------------
	
	@Test
	public void testValidateUniqueVerticesIds_whenAlllAreUnique() {
		List<Edge> edges = Arrays.asList(
				createTestEdge(stringIdX, vertexA, vertexB, weight5),
				createTestEdge(stringIdY, vertexB, vertexC, weight6),
				createTestEdge(stringIdZ, vertexC, vertexD, weight7)
			);
			graphEdgesValidator.validateUniqueVerticesIds(edges.get(0), mapForValidatingUniqueVerticesIds);
			graphEdgesValidator.validateUniqueVerticesIds(edges.get(1), mapForValidatingUniqueVerticesIds);
			graphEdgesValidator.validateUniqueVerticesIds(edges.get(2), mapForValidatingUniqueVerticesIds);		
	}	
	
	@Test(expected = GraphValidationException.class)
	public void testValidateUniqueVerticesIds_whenAlllAreNotUnique() {
		List<Edge> edges = Arrays.asList(
			createTestEdge(stringIdX, vertexA, vertexB, weight5),
			createTestEdge(stringIdY, vertexB, vertexC, weight6),
			createTestEdge(stringIdZ, vertexB, vertexC, weight7) //  // Note "B to C" again, i.e. x NOT unique, should cause Exception
		);
		graphEdgesValidator.validateUniqueVerticesIds(edges.get(0), mapForValidatingUniqueVerticesIds);
		graphEdgesValidator.validateUniqueVerticesIds(edges.get(1), mapForValidatingUniqueVerticesIds);
		graphEdgesValidator.validateUniqueVerticesIds(edges.get(2), mapForValidatingUniqueVerticesIds);
	}

	private Vertex createTestVertex(String id) {
		return new VertexTestImpl(id);
	}
	private Weight createTestWeight(double value) {
		return new WeightTestImpl(value);
	}
	
	private Edge createTestEdge(String edgeId, Vertex startVertex, Vertex endVertex, Weight weight) {
		return createEdge(edgeId, startVertex, endVertex, weight);
	}
	
	// Test implementations are defined  below, since we are above testimg the behaviour of interface,
	// which may be constructed incorrectly by implementors while the "standard" (in core library) implementations 
	// may contain some validation and not be possible to create in n incorrect way
	private final static class VertexTestImpl implements Vertex {
		private String id;
		public VertexTestImpl(String id) {
			this.id = id;
		}
		public String getVertexId() {
			return id;
		}
		public String renderToString() {
			return toString();
		}
		@Override
		public String toString() {
			return "VertexTestImpl [id=" + id + "]";
		}
		
	}
	private final static class WeightTestImpl implements Weight {
		private final double value;
		public WeightTestImpl(final double value) {
			this.value = value;
		}		
		public double getWeightValue() {
			return value;
		}
		public String renderToString() {
			return toString();
		}
		@Override
		public String toString() {
			return "WeightTestImpl [value=" + value + "]";
		}
		public Weight create(double value) {
			return new WeightTestImpl(value);
		}
	}

	private final static class EdgeTestImpl implements Edge {
		private final String id;
		private final Vertex startVertex;
		private final Vertex endVertex;
		private final Weight weight;
		private EdgeTestImpl(
			final String edgeId,
			final Vertex startVertex, 
			final Vertex endVertex, 
			final Weight weight
		) {
			this.startVertex = startVertex;
			this.endVertex = endVertex;
			this.weight = weight;
			this.id = edgeId;
		}
		public String getEdgeId() {
			return id;
		}
		public Vertex getStartVertex() {
			return startVertex;
		}
		public Vertex getEndVertex() {
			return endVertex;
		}
		public Weight getEdgeWeight() {
			return weight;
		}
		public String renderToString() {
			return toString();
		}
		@Override
		public String toString() {
			return "EdgeTestImpl [id=" + id + ", startVertex=" + startVertex + ", endVertex=" + endVertex + ", weight="
					+ weight + "]";
		}
	}

	// ----------------------------------------------------------------------------------------------
	@Test(expected = GraphValidationException.class)
	public void testValidateAllPathsOnlyContainEdgesDefinedInGraph() {
		final List<Edge> allEdgesForGraph = new ArrayList<Edge>();
		allEdgesForGraph.add(createTestEdge("11", createTestVertex("a"), createTestVertex("b"), createTestWeight(1)));

		final List<Edge> edgesForPath = new ArrayList<Edge>();
		edgesForPath.add(createTestEdge("11", createTestVertex("a"), createTestVertex("c"), createTestWeight(1)));
		final Path path = createPath(createTestWeight(1), edgesForPath);
		List<Path> paths = Arrays.asList(path);

		graphEdgesValidator.validateAllPathsOnlyContainEdgesDefinedInGraph(paths, allEdgesForGraph);

		// TODO maybe: introduce a new base class for GraphJgrapht and for the other implementations of Graph, 
		// and put the validation there, to ensure a reasonable output paths by using the above validation method, 
		// but then maybe such an validation also should be optional 
	}
	// ----------------------------------------------------------------------------------------------
}