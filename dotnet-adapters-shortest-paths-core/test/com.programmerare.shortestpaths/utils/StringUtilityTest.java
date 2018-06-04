/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com/
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.utils;

import static org.junit.Assert.*;

import java.util.List;

import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;

public class StringUtilityTest {

	@BeforeClass
	public static void setUpBeforeClass() throws Exception {
	}

	@Before
	public void setUp() throws Exception {
	}

	@Test
	public void testGetMultilineStringAsListOfTrimmedStringsIgnoringLinesWithOnlyWhiteSpace() {
		String s = "AB\r\n" + 
				"XY\r\n" + 
				"   BX\r\n" + 
				"\r\n" + 
				"\r\n" + 
				"BA  \r\n" + 
				"  CD   \r\n" + 
				"\r\n" + 
				"\r\n" + 
				"";
		List<String> list = StringUtility.getMultilineStringAsListOfTrimmedStringsIgnoringLinesWithOnlyWhiteSpace(s);
		assertNotNull(list);
		assertEquals(5, list.size());
		assertEquals("AB", list.get(0));
		assertEquals("XY", list.get(1));
		assertEquals("BX", list.get(2));
		assertEquals("BA", list.get(3));
		assertEquals("CD", list.get(4));
		
		
	}

	@Test
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
	@Test
	public void testGetDoubleAsStringWithoutZeroesAndDotIfNotRelevant_String() {
		assertStringResult("13", "13");
		assertStringResult("13.0", "13");
		assertStringResult("13.00", "13");
		assertStringResult("13.001", "13.001");
		assertStringResult("13.0010", "13.001");
	}
	private void assertStringResult(String doubleAsString, String expected) {
		String s = StringUtility.getDoubleAsStringWithoutZeroesAndDotIfNotRelevant(doubleAsString);
		assertEquals(expected, s);		
	}
	private void assertDoubleResult(double d, String expected) {
		String s = StringUtility.getDoubleAsStringWithoutZeroesAndDotIfNotRelevant(d);
		assertEquals(expected, s);
		
	}
}