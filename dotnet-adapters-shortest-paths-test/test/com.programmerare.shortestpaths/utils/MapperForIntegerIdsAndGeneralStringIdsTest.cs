/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in the "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace com.programmerare.shortestpaths.utils
{
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