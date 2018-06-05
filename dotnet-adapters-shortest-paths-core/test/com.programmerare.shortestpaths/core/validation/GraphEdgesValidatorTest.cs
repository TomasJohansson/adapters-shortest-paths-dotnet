/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
//using static com.programmerare.shortestpaths.core.impl.PathImpl.createPath;
//import static com.programmerare.shortestpaths.core.impl.EdgeImpl.createEdge;
//import static com.programmerare.shortestpaths.core.validation.GraphEdgesValidator.createGraphEdgesValidator;
//import org.junit.Before;
//import org.junit.Test;
using com.programmerare.shortestpaths.core.api;
using System.Collections.Generic;
using NUnit.Framework;
using static NUnit.Framework.Assert;
//using static com.programmerare.shortestpaths.core.validation.GraphEdgesValidator;
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.api.generics;
using com.programmerare.shortestpaths.core.api;
using NUnit.Framework;
using static NUnit.Framework.Assert;
using static com.programmerare.shortestpaths.core.impl.PathImpl; // createPath
using static com.programmerare.shortestpaths.core.impl.GraphImplTest; // createEdgeGenerics
using static com.programmerare.shortestpaths.core.impl.WeightImpl; // SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS
using static com.programmerare.shortestpaths.core.impl.EdgeImpl; // createEdge
using static com.programmerare.shortestpaths.core.impl.VertexImpl; // createVertex
using System.Collections.Generic;
using com.programmerare.shortestpaths.core.api.generics;

namespace com.programmerare.shortestpaths.core.validation
{
    /**
     * @author Tomas Johansson
     */
    public class GraphEdgesValidatorTest {

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

	    private IDictionary<string, bool> mapForValidatingUniqueEdgeId;
	    private IDictionary<string, bool> mapForValidatingUniqueVerticesIds;

	    // the reason for these trivial strings below are that they provide semantic as actual parameters rather than invoking with null or "" as parameter  
	    private string stringIdNull = null;
	    private string stringIdEmpty = "";
	    private string stringIdSomeSpaces = "   ";
	    private string stringIdX = "x";
	    private string stringIdY = "y";
	    private string stringIdZ = "Z";
	
	    [SetUp]
	    public void setUp() {
		    graphEdgesValidator = GraphEdgesValidator<Path, Edge, Vertex, Weight>.createGraphEdgesValidator<Path, Edge, Vertex, Weight>();
		
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
		
		    mapForValidatingUniqueEdgeId = new Dictionary<string, bool>();
		    mapForValidatingUniqueVerticesIds = new Dictionary<string, bool>();
	    }

	
	    // ----------------------------------------------------------------------------------------------
	    // tests for validateNonNullObjects below
	
	    public void testValidateNonNullObjects_whenAllEdgePartsAreValid() {
		    graphEdgesValidator.validateNonNullObjects(createTestEdge(stringIdX, vertexA, vertexB, weight5));
	    }

        [Test]	
	    //@Test(expected = GraphValidationException.class)
	    public void testValidateNonNullObjects_whenEdgeIsNull() {
            Fail("fix this");
		    //graphEdgesValidator.validateNonNullObjects(null);
	    }
	
	    [Test]	//@Test(expected = GraphValidationException.class)
	    public void testValidateNonNullObjects_whenStartVertexIsNull() {
            Fail("fix");
		    //graphEdgesValidator.validateNonNullObjects(createTestEdge(stringIdX, null, vertexA, weight6));
	    }
	
        [Test]
	    //@Test(expected = GraphValidationException.class)
	    public void testValidateNonNullObjects_whenEndVertexIsNull() {
		    //graphEdgesValidator.validateNonNullObjects(createTestEdge(stringIdX, vertexA, null, weight6));
            Fail("fix");
	    }
	
        [Test]
	    //@Test(expected = GraphValidationException.class)
	    public void testValidateNonNullObjects_whenWeightIsNull() {
		    //graphEdgesValidator.validateNonNullObjects(createTestEdge(stringIdX, vertexA, vertexB, null));
            Fail("fix");
	    }	

	    // tests for validateNonNullObjects above
	    // ----------------------------------------------------------------------------------------------
	    // tests for validateNonBlankIds below
	    [Test]
	    public void testValidateNonBlankIds_whenAllEdgePartsAreValid() {
		    //graphEdgesValidator.validateNonBlankIds(createTestEdge(stringIdX, vertexA, vertexB, weight5));
            Fail("fix");
	    }
	
	    // - - - - - -
	    // Three tests for edge id:
	    
        [Test] // @Test(expected = GraphValidationException.class)
	    public void testValidateNonBlankIds_whenEdgeIdIsNull() {
		    //graphEdgesValidator.validateNonBlankIds(createTestEdge(stringIdNull, vertexA, vertexB, weight5));
            Fail("fix");
	    }	

        [Test]	
	    //@Test(expected = GraphValidationException.class)
	    public void testValidateNonBlankIds_whenEdgeIdIsEmptyString() {
		    //graphEdgesValidator.validateNonBlankIds(createTestEdge(stringIdEmpty, vertexA, vertexB, weight5));
            Fail("fix");
	    }
	
        [Test]	
	    //@Test(expected = GraphValidationException.class)
	    public void testValidateNonBlankIds_whenEdgeIdIsSomeSpaces() {
		    graphEdgesValidator.validateNonBlankIds(createTestEdge(stringIdSomeSpaces, vertexA, vertexB, weight5));
            Fail("fix");
	    }	
	
	    // - - - - - -
	    // Three tests for start vertex id:
	    [Test]	 // @Test(expected = GraphValidationException.class)
	    public void testValidateNonBlankIds_whenStartVertexIdIsNull() {
		    //graphEdgesValidator.validateNonBlankIds(createTestEdge(stringIdNull, vertexWithNullAsId, vertexB, weight5));
            Fail("fix");
	    }	
	
	    [Test]	 // @Test(expected = GraphValidationException.class)
	    public void testValidateNonBlankIds_whenStartVertexIdIsEmptyString() {
		    graphEdgesValidator.validateNonBlankIds(createTestEdge(stringIdEmpty, vertexWithEmptyStringAsId, vertexB, weight5));
            Fail("fix");
	    }
	
	    [Test]	 // @Test(expected = GraphValidationException.class)
	    public void testValidateNonBlankIds_whenStartVertexIdIsSomeSpaces() {
		    //graphEdgesValidator.validateNonBlankIds(createTestEdge(stringIdSomeSpaces, vertexWithSomeSpacesAsId, vertexB, weight5));
            Fail("fix");
	    }
	
	    // - - - - - -
	    // Three tests for end vertex id:
	    [Test]	 // @Test(expected = GraphValidationException.class)
	    public void testValidateNonBlankIds_whenEndVertexIdIsNull() {
		    //graphEdgesValidator.validateNonBlankIds(createTestEdge(stringIdNull, vertexA, vertexWithNullAsId, weight5));
            Fail("fix");
	    }	
	
	    [Test]	 // @Test(expected = GraphValidationException.class)
	    public void testValidateNonBlankIds_whenEndVertexIdIsEmptyString() {
		    //graphEdgesValidator.validateNonBlankIds(createTestEdge(stringIdEmpty, vertexA, vertexWithEmptyStringAsId, weight5));
            Fail("fix");
	    }
	
	    [Test]	 // @Test(expected = GraphValidationException.class)
	    public void testValidateNonBlankIds_whenEndVertexIdIsSomeSpaces() {
		    graphEdgesValidator.validateNonBlankIds(createTestEdge(stringIdSomeSpaces, vertexA, vertexWithSomeSpacesAsId, weight5));
	    }
	    // tests for validateNonBlankIds above
	    // ----------------------------------------------------------------------------------------------
	
	    // tests for validateUniqueEdgeId below	
	    [Test]
	    public void testValidateUniqueEdgeId_whenAlllAreUnique() {
		    //List<Edge> edges = new List<Edge> {
			   // createTestEdge(stringIdX, vertexA, vertexB, weight5),
			   // createTestEdge(stringIdY, vertexB, vertexC, weight6),
			   // createTestEdge(stringIdZ, vertexC, vertexD, weight7)
      //      };
		    //graphEdgesValidator.validateUniqueEdgeId(edges.get(0), mapForValidatingUniqueEdgeId);
		    //graphEdgesValidator.validateUniqueEdgeId(edges.get(1), mapForValidatingUniqueEdgeId);
		    //graphEdgesValidator.validateUniqueEdgeId(edges.get(2), mapForValidatingUniqueEdgeId);
            Fail("fix");
	    }

	    //@Test(expected = GraphValidationException.class)
        [Test]
	    public void testValidateUniqueEdgeId_whenAlllAreNotUnique() {
            Fail("fix this");
		    //List<Edge> edges = new List{
			   // createTestEdge(stringIdX, vertexA, vertexB, weight5),
			   // createTestEdge(stringIdY, vertexB, vertexC, weight6),
			   // createTestEdge(stringIdX, vertexC, vertexD, weight7) // Note X again, i.e. x NOT unique, should cause Exception 
      //      };
		    //graphEdgesValidator.validateUniqueEdgeId(edges.get(0), mapForValidatingUniqueEdgeId);
		    //graphEdgesValidator.validateUniqueEdgeId(edges.get(1), mapForValidatingUniqueEdgeId);
		    //graphEdgesValidator.validateUniqueEdgeId(edges.get(2), mapForValidatingUniqueEdgeId);
	    }
	
	    // tests for validateUniqueEdgeId above
	    // ----------------------------------------------------------------------------------------------
	
	    [Test]
	    public void testValidateUniqueVerticesIds_whenAlllAreUnique() {
            Fail("todo fix");
		 //   List<Edge> edges = new List{
			//	createTestEdge(stringIdX, vertexA, vertexB, weight5),
			//	createTestEdge(stringIdY, vertexB, vertexC, weight6),
			//	createTestEdge(stringIdZ, vertexC, vertexD, weight7)
   //         };
			//graphEdgesValidator.validateUniqueVerticesIds(edges.get(0), mapForValidatingUniqueVerticesIds);
			//graphEdgesValidator.validateUniqueVerticesIds(edges.get(1), mapForValidatingUniqueVerticesIds);
			//graphEdgesValidator.validateUniqueVerticesIds(edges.get(2), mapForValidatingUniqueVerticesIds);		
	    }	
	
	    [Test] // @Test(expected = GraphValidationException.class)
	    public void testValidateUniqueVerticesIds_whenAlllAreNotUnique() {
            Fail("todo fix");
		    //List<Edge> edges = new List{
			   // createTestEdge(stringIdX, vertexA, vertexB, weight5),
			   // createTestEdge(stringIdY, vertexB, vertexC, weight6),
			   // createTestEdge(stringIdZ, vertexB, vertexC, weight7) //  // Note "B to C" again, i.e. x NOT unique, should cause Exception
      //      };
		    //graphEdgesValidator.validateUniqueVerticesIds(edges.get(0), mapForValidatingUniqueVerticesIds);
		    //graphEdgesValidator.validateUniqueVerticesIds(edges.get(1), mapForValidatingUniqueVerticesIds);
		    //graphEdgesValidator.validateUniqueVerticesIds(edges.get(2), mapForValidatingUniqueVerticesIds);
	    }

	    private Vertex createTestVertex(string id) {
		    return new VertexTestImpl(id);
	    }
	    private Weight createTestWeight(double value) {
		    return new WeightTestImpl(value);
	    }
	
	    private Edge createTestEdge(string edgeId, Vertex startVertex, Vertex endVertex, Weight weight) {
		    return createEdge(edgeId, startVertex, endVertex, weight);
	    }
	
	    // Test implementations are defined  below, since we are above testimg the behaviour of interface,
	    // which may be constructed incorrectly by implementors while the "standard" (in core library) implementations 
	    // may contain some validation and not be possible to create in n incorrect way
	    private class VertexTestImpl : Vertex {
		    private string id;
		    public VertexTestImpl(string id) {
			    this.id = id;
		    }
		    public string getVertexId() {
			    return id;
		    }
		    public string renderToString() {
			    return ToString();
		    }
		    
		    public override string ToString() {
			    return "VertexTestImpl [id=" + id + "]";
		    }
		
	    }
	    private sealed class WeightTestImpl : Weight {
		    private readonly double value;
		    public WeightTestImpl(double value) {
			    this.value = value;
		    }		
		    public double getWeightValue() {
			    return value;
		    }
		    public string renderToString() {
			    return ToString();
		    }
		    
		    public override string ToString() {
			    return "WeightTestImpl [value=" + value + "]";
		    }
		    public Weight create(double value) {
			    return new WeightTestImpl(value);
		    }
	    }

	    private sealed class EdgeTestImpl : Edge {
		    private readonly string id;
		    private readonly Vertex startVertex;
		    private readonly Vertex endVertex;
		    private readonly Weight weight;
		    private EdgeTestImpl(
			    string edgeId,
			    Vertex startVertex, 
			    Vertex endVertex, 
			    Weight weight
		    ) {
			    this.startVertex = startVertex;
			    this.endVertex = endVertex;
			    this.weight = weight;
			    this.id = edgeId;
		    }
		    public string getEdgeId() {
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
		    public string renderToString() {
			    return ToString();
		    }
		    
		    public override string ToString() {
			    return "EdgeTestImpl [id=" + id + ", startVertex=" + startVertex + ", endVertex=" + endVertex + ", weight="
					    + weight + "]";
		    }
	    }

	    // ----------------------------------------------------------------------------------------------
	    [Test] // @Test(expected = GraphValidationException.class)
	    public void testValidateAllPathsOnlyContainEdgesDefinedInGraph() {
		    IList<Edge> allEdgesForGraph = new List<Edge>();
		    allEdgesForGraph.Add(createTestEdge("11", createTestVertex("a"), createTestVertex("b"), createTestWeight(1)));

		    IList<Edge> edgesForPath = new List<Edge>();
		    edgesForPath.Add(createTestEdge("11", createTestVertex("a"), createTestVertex("c"), createTestWeight(1)));
		    Path path = createPath(createTestWeight(1), edgesForPath);
		    List<Path> paths = new List<Path>{ path };

		    graphEdgesValidator.validateAllPathsOnlyContainEdgesDefinedInGraph(paths, allEdgesForGraph);

		    // TODO maybe: introduce a new base class for GraphJgrapht and for the other implementations of Graph, 
		    // and put the validation there, to ensure a reasonable output paths by using the above validation method, 
		    // but then maybe such an validation also should be optional 
	    }
	    // ----------------------------------------------------------------------------------------------
    }
}