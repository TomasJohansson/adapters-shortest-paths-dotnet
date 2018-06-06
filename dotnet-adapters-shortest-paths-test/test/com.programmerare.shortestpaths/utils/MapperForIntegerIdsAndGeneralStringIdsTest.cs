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
		MapperForIntegerIdsAndGeneralStringIds idMapper = MapperForIntegerIdsAndGeneralStringIds.CreateIdMapper(0);
		AreEqual(0, idMapper.CreateOrRetrieveIntegerId("A"));
		AreEqual(1, idMapper.CreateOrRetrieveIntegerId("B"));
		AreEqual(0, idMapper.CreateOrRetrieveIntegerId("A"));
		AreEqual(2, idMapper.CreateOrRetrieveIntegerId("C"));
		AreEqual(0, idMapper.CreateOrRetrieveIntegerId("A"));
		AreEqual(3, idMapper.CreateOrRetrieveIntegerId("D"));

		AreEqual(4, idMapper.GetNumberOfVertices());
		
		AreEqual("A", idMapper.GetBackThePreviouslyStoredGeneralStringIdForInteger(0));
		AreEqual("B", idMapper.GetBackThePreviouslyStoredGeneralStringIdForInteger(1));
		AreEqual("C", idMapper.GetBackThePreviouslyStoredGeneralStringIdForInteger(2));
		AreEqual("D", idMapper.GetBackThePreviouslyStoredGeneralStringIdForInteger(3));
	}
	
	[Test]
	public void testStartIndexOne() {
		MapperForIntegerIdsAndGeneralStringIds idMapper = MapperForIntegerIdsAndGeneralStringIds.CreateIdMapper(1);
		AreEqual(1, idMapper.CreateOrRetrieveIntegerId("ABC"));
		AreEqual(2, idMapper.CreateOrRetrieveIntegerId("DEF"));
		AreEqual(3, idMapper.CreateOrRetrieveIntegerId("GHI"));
		AreEqual(2, idMapper.CreateOrRetrieveIntegerId("DEF"));
		
		AreEqual(3, idMapper.GetNumberOfVertices());
		
		AreEqual("ABC", idMapper.GetBackThePreviouslyStoredGeneralStringIdForInteger(1));
		AreEqual("DEF", idMapper.GetBackThePreviouslyStoredGeneralStringIdForInteger(2));
		AreEqual("GHI", idMapper.GetBackThePreviouslyStoredGeneralStringIdForInteger(3));
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