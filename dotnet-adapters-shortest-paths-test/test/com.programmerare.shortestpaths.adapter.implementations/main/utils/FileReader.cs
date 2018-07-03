package com.programmerare.shortestpaths.utils;

import java.io.IOException;
import java.net.URL;
import java.nio.charset.Charset;
import java.util.List;

import com.google.common.io.Resources;

/**
 * @author Tomas Johansson
 */
public class FileReader {

	public final static Charset CHARSET_UTF_8;
	static {
		CHARSET_UTF_8 = Charset.forName("UTF-8");
	}
	
	/**
	 * Reads line from a file which is assumed to use charset UTF-8
	 * @param filePath e.g. ""test_graphs/tiny_graph_01.txt"
	 * @return
	 */
	public List<String> readLines(final String filePath)  {
		return readLines(filePath, CHARSET_UTF_8);
	}
	
	public List<String> readLines(final String filePath, Charset charsetForInputFile)  {
		try {
			final URL resource = Resources.getResource(filePath);
			final List<String> linesInFile = Resources.readLines(resource, charsetForInputFile);
			return linesInFile;
		} catch (IOException e) {
			throw new RuntimeException(e);
		}		
	}
}