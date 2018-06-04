/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.utils;

import static org.junit.Assert.*;

import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;

/**
 * @author Tomas Johansson
 */
public class MapperForIntegerIdsAndGeneralStringIdsTest {

//	@Before
//	public void setUp() throws Exception {
//	}

	// TODO: improve the testing below. Each method is currently doing too much.

	@Test
	public void testStartIndexZero() {
		MapperForIntegerIdsAndGeneralStringIds idMapper = MapperForIntegerIdsAndGeneralStringIds.createIdMapper(0);
		assertEquals(0, idMapper.createOrRetrieveIntegerId("A"));
		assertEquals(1, idMapper.createOrRetrieveIntegerId("B"));
		assertEquals(0, idMapper.createOrRetrieveIntegerId("A"));
		assertEquals(2, idMapper.createOrRetrieveIntegerId("C"));
		assertEquals(0, idMapper.createOrRetrieveIntegerId("A"));
		assertEquals(3, idMapper.createOrRetrieveIntegerId("D"));

		assertEquals(4, idMapper.getNumberOfVertices());
		
		assertEquals("A", idMapper.getBackThePreviouslyStoredGeneralStringIdForInteger(0));
		assertEquals("B", idMapper.getBackThePreviouslyStoredGeneralStringIdForInteger(1));
		assertEquals("C", idMapper.getBackThePreviouslyStoredGeneralStringIdForInteger(2));
		assertEquals("D", idMapper.getBackThePreviouslyStoredGeneralStringIdForInteger(3));
	}
	
	@Test
	public void testStartIndexOne() {
		MapperForIntegerIdsAndGeneralStringIds idMapper = MapperForIntegerIdsAndGeneralStringIds.createIdMapper(1);
		assertEquals(1, idMapper.createOrRetrieveIntegerId("ABC"));
		assertEquals(2, idMapper.createOrRetrieveIntegerId("DEF"));
		assertEquals(3, idMapper.createOrRetrieveIntegerId("GHI"));
		assertEquals(2, idMapper.createOrRetrieveIntegerId("DEF"));
		
		assertEquals(3, idMapper.getNumberOfVertices());
		
		assertEquals("ABC", idMapper.getBackThePreviouslyStoredGeneralStringIdForInteger(1));
		assertEquals("DEF", idMapper.getBackThePreviouslyStoredGeneralStringIdForInteger(2));
		assertEquals("GHI", idMapper.getBackThePreviouslyStoredGeneralStringIdForInteger(3));
	}	
	
//	@Test
//	public void createOrRetrieveIntegerId() {
//		fail("Not yet implemented");
//	}
//
//	@Test
//	public void testGetNumberOfVertices() {
//		fail("Not yet implemented");
//	}
//
//	@Test
//	public void testGetBackThePreviouslyStoredGeneralStringIdForInteger() {
//		fail("Not yet implemented");
//	}
}
