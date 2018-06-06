/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/

using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.validation;
using System.Collections.Generic;
using NUnit.Framework;
using static NUnit.Framework.Assert;
using static com.programmerare.shortestpaths.core.impl.WeightImpl; // SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS

namespace com.programmerare.shortestpaths.core.parsers
{
    [TestFixture]
    public class PathParserTest {

        private PathParser<Path , Edge , Vertex , Weight> pathParser;
	
	    [SetUp]
	    public void setUp()  {
		    string stringWithEdges = "A B 5\r\n" + 
				    "A C 6\r\n" + 
				    "B C 7\r\n" + 
				    "B D 8\r\n" + 
				    "C D 9";
            //	    <graphDefinition>
            //	    A B 5
            //	    A C 6
            //	    B C 7
            //	    B D 8
            //	    C D 9    
            //	 	</graphDefinition>
            var edgeParser = EdgeParser<Edge, Vertex, Weight>.CreateEdgeParserDefault();
		    IList<Edge> edges = edgeParser.FromMultiLinedStringToListOfEdges(stringWithEdges);
            pathParser = PathParser<Path , Edge , Vertex , Weight>.CreatePathParserDefault(edges);
	    }

	    [Test]
	    public void testFromStringToListOfPaths() {
    //	    <outputExpected>
    //			13 A B D
    //			15 A C D
    //			21 A B C D
    //	    </outputExpected>
		
		    var lListOfPaths = pathParser.FromStringToListOfPaths("13 A B D\r\n" + 
				    "15 A C D\r\n" + 
				    "21 A B C D");
		    IsNotNull(lListOfPaths);
		    AreEqual(3,  lListOfPaths.Count);
		
		    var path1 = lListOfPaths[0]; // 13 A B D
		    var path2 = lListOfPaths[1]; // 15 A C D 
		    var path3 = lListOfPaths[2]; // 21 A B C D
		    IsNotNull(path1);
		    IsNotNull(path2);
		    IsNotNull(path3);
		    AreEqual(13.0, path1.TotalWeightForPath.WeightValue, SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
		    AreEqual(15.0, path2.TotalWeightForPath.WeightValue, SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
		    AreEqual(21.0, path3.TotalWeightForPath.WeightValue, SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
		
		    var edgesForPath1 = path1.EdgesForPath;
		    var edgesForPath2 = path2.EdgesForPath;
		    var edgesForPath3 = path3.EdgesForPath;
		    IsNotNull(edgesForPath1);
		    IsNotNull(edgesForPath2);
		    IsNotNull(edgesForPath3);
		    AreEqual(2, edgesForPath1.Count);
		    AreEqual(2, edgesForPath2.Count);
		    AreEqual(3, edgesForPath3.Count);

		    // edgesForPath1 "13 A B D" means path "A -> B" and "B -> D"
		    AreEqual(pathParser.GetEdgeIncludingTheWeight("A", "B"), edgesForPath1[0]);
		    AreEqual(pathParser.GetEdgeIncludingTheWeight("B", "D"), edgesForPath1[1]);

		    // edgesForPath2 // 15 A C D
		    AreEqual(pathParser.GetEdgeIncludingTheWeight("A", "C"), edgesForPath2[0]);
		    AreEqual(pathParser.GetEdgeIncludingTheWeight("C", "D"), edgesForPath2[1]);
		
		    // 21 A B C D
		    AreEqual(pathParser.GetEdgeIncludingTheWeight("A", "B"), edgesForPath3[0]);
		    AreEqual(pathParser.GetEdgeIncludingTheWeight("B", "C"), edgesForPath3[1]);
		    AreEqual(pathParser.GetEdgeIncludingTheWeight("C", "D"), edgesForPath3[2]);
	    }
	
	    [Test]
	    public void testgetEdgeIncludingTheWeight_should_throw_exception_when_edge_does_not_exist() {
		    // the edges in the setup method do not contain any edge between vertices A and D
            var exceptionThrown = Assert.Throws<GraphValidationException>(() => {
                pathParser.GetEdgeIncludingTheWeight("A", "D");
            });
	    }

	    // yes, I am lazy here, with two methods tested, convenient with strings compared to creating Path and Edge objects 
	    [Test] // @Test
	    public void test_fromStringToPath_Generic_and_fromPathToString() {
		    // the pathParser is constructed in setup method with  two edges: A -> B (weight 5) and B -> D (weight 8) 
		    string inputPathString = "13 A B D";
		
		    var path = pathParser.FromStringToPath(inputPathString);
		    // TODO: test below and above methods from separate test methods
		    string outputPathString = pathParser.FromPathToString(path);
		
		    AreEqual(inputPathString, outputPathString);
	    }
	
	    [Test]
	    public void test_fromStringToPath_and_fromPathToString() {
		    // the pathParser is constructed in setup method with  two edges: A -> B (weight 5) and B -> D (weight 8) 
		    string inputPathString = "13 A B D";
		
		    //var path = pathParserGenerics.fromStringToPath(inputPathString);
            var path = pathParser.FromStringToPath(inputPathString);
		    // TODO: test below and above methods from separate test methods
		    string outputPathString = pathParser.FromPathToString(path);
		
		    AreEqual(inputPathString, outputPathString);
	    }	
    }
}