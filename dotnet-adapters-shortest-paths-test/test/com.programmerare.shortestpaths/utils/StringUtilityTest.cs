/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com/
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
using com.programmerare.shortestpaths.core.api;
using NUnit.Framework;
using static NUnit.Framework.Assert;
using static com.programmerare.shortestpaths.core.impl.GraphImplTest; // createEdgeGenerics
using static com.programmerare.shortestpaths.core.impl.WeightImpl; // SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS
using static com.programmerare.shortestpaths.core.impl.EdgeImpl; // createEdge
using static com.programmerare.shortestpaths.core.impl.VertexImpl; // createVertex
using System.Collections.Generic;
using com.programmerare.shortestpaths.core.api.generics;


namespace com.programmerare.shortestpaths.utils
{
    [TestFixture]
    public class StringUtilityTest {

	    [SetUp]
	    public void setUp() {
	    }

	    [Test]
	    public void testGetMultilineStringAsListOfTrimmedStringsIgnoringLinesWithOnlyWhiteSpace() {
		    string s = "AB\r\n" + 
				    "XY\r\n" + 
				    "   BX\r\n" + 
				    "\r\n" + 
				    "\r\n" + 
				    "BA  \r\n" + 
				    "  CD   \r\n" + 
				    "\r\n" + 
				    "\r\n" + 
				    "";
		    IList<string> list = StringUtility.GetMultilineStringAsListOfTrimmedStringsIgnoringLinesWithOnlyWhiteSpace(s);
		    IsNotNull(list);
		    AreEqual(5, list.Count);
		    AreEqual("AB", list[0]);
		    AreEqual("XY", list[1]);
		    AreEqual("BX", list[2]);
		    AreEqual("BA", list[3]);
		    AreEqual("CD", list[4]);
	    }

	    [Test]
	    public void testGetDoubleAsStringWithoutZeroesAndDotIfNotRelevant() {
		    assertDoubleResult(13, "13");
		    assertDoubleResult(13.0, "13");
		    assertDoubleResult(13.00, "13");
		    assertDoubleResult(13.001, "13.001");
		    assertDoubleResult(13.0010, "13.001");
		
		    assertDoubleResult(1, "1");
		    assertDoubleResult(10, "10"); // should NOT remove the zero at the end
		    assertDoubleResult(100, "100");
	    }
	    // TODO: refactor above and below... maybe better names of the methods but maybe also 
	    // better implementation of the tested methdo without the regular expressions ...
	    [Test]
	    public void testGetDoubleAsStringWithoutZeroesAndDotIfNotRelevant_String() {
		    assertStringResult("13", "13");
		    assertStringResult("13.0", "13");
		    assertStringResult("13.00", "13");
		    assertStringResult("13.001", "13.001");
		    assertStringResult("13.0010", "13.001");
	    }
	    private void assertStringResult(string doubleAsString, string expected) {
            string s = StringUtility.GetDoubleAsStringWithoutZeroesAndDotIfNotRelevant(doubleAsString);
            AreEqual(expected, s);
        }
	    private void assertDoubleResult(double d, string expected) {
		    string s = StringUtility.GetDoubleAsStringWithoutZeroesAndDotIfNotRelevant(d);
		    AreEqual(expected, s);
		
	    }
    }
}