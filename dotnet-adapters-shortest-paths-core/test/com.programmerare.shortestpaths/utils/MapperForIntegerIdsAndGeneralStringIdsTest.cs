/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
//import static org.junit.Assert.*;
//import org.junit.After;
//import org.junit.AfterClass;
//import org.junit.Before;
//import org.junit.BeforeClass;
//import org.junit.Test;
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
/**
 * @author Tomas Johansson
 */
[TestFixture]
public class MapperForIntegerIdsAndGeneralStringIdsTest {

//	@Before
//	public void setUp() throws Exception {
//	}

	// TODO: improve the testing below. Each method is currently doing too much.

	[Test]
	public void testStartIndexZero() {
		MapperForIntegerIdsAndGeneralStringIds idMapper = MapperForIntegerIdsAndGeneralStringIds.createIdMapper(0);
		AreEqual(0, idMapper.createOrRetrieveIntegerId("A"));
		AreEqual(1, idMapper.createOrRetrieveIntegerId("B"));
		AreEqual(0, idMapper.createOrRetrieveIntegerId("A"));
		AreEqual(2, idMapper.createOrRetrieveIntegerId("C"));
		AreEqual(0, idMapper.createOrRetrieveIntegerId("A"));
		AreEqual(3, idMapper.createOrRetrieveIntegerId("D"));

		AreEqual(4, idMapper.getNumberOfVertices());
		
		AreEqual("A", idMapper.getBackThePreviouslyStoredGeneralStringIdForInteger(0));
		AreEqual("B", idMapper.getBackThePreviouslyStoredGeneralStringIdForInteger(1));
		AreEqual("C", idMapper.getBackThePreviouslyStoredGeneralStringIdForInteger(2));
		AreEqual("D", idMapper.getBackThePreviouslyStoredGeneralStringIdForInteger(3));
	}
	
	[Test]
	public void testStartIndexOne() {
		MapperForIntegerIdsAndGeneralStringIds idMapper = MapperForIntegerIdsAndGeneralStringIds.createIdMapper(1);
		AreEqual(1, idMapper.createOrRetrieveIntegerId("ABC"));
		AreEqual(2, idMapper.createOrRetrieveIntegerId("DEF"));
		AreEqual(3, idMapper.createOrRetrieveIntegerId("GHI"));
		AreEqual(2, idMapper.createOrRetrieveIntegerId("DEF"));
		
		AreEqual(3, idMapper.getNumberOfVertices());
		
		AreEqual("ABC", idMapper.getBackThePreviouslyStoredGeneralStringIdForInteger(1));
		AreEqual("DEF", idMapper.getBackThePreviouslyStoredGeneralStringIdForInteger(2));
		AreEqual("GHI", idMapper.getBackThePreviouslyStoredGeneralStringIdForInteger(3));
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
}