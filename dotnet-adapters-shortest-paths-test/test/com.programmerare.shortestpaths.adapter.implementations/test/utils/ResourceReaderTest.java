package com.programmerare.shortestpaths.utils;

import static org.junit.Assert.assertEquals;
import org.junit.Before;
import org.junit.Test;
import java.util.List;

/**
 * @author Tomas Johansson
 */
public class ResourceReaderTest {

	private ResourceReader resourceReader;

	@Before
	public void setUp() throws Exception {
		resourceReader = new ResourceReader();
	}

	@Test
	public void getNameOfFilesInResourcesFolder() throws Exception {
		List<String> fileNames = resourceReader.getNameOfFilesInResourcesFolder("directory_for_resourceeader_test");
		assertEquals(4, fileNames.size());
		assertEquals("txtFile1.txt", fileNames.get(0));
		assertEquals("txtFile2.txt", fileNames.get(1));
		assertEquals("xmlFile1.xml", fileNames.get(2));
		assertEquals("xmlFile2.xml", fileNames.get(3));
	}
}