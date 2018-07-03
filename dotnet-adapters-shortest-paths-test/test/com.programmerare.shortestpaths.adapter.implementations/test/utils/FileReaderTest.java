package com.programmerare.shortestpaths.utils;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNotNull;

import java.util.List;

import org.junit.Test;

/**
 * @author Tomas Johansson
 */
public class FileReaderTest {

	@Test
	public void testReadFile() {
		final String filePath = "directory_for_filereader_test/file_for_filereader_test.txt";
		// /src/test/resources/directory_for_filereader_test/file_for_filereader_test.txt
		// the above line has two rows like below:
		//first line in testfile
		//second line in testfile
		FileReader fileReader = new FileReader();
		List<String> lines = fileReader.readLines(filePath);
		assertNotNull(lines);
		assertEquals(2,  lines.size());
		assertEquals("first line in testfile", lines.get(0));
		assertEquals("second line in testfile", lines.get(1));
	}
}
