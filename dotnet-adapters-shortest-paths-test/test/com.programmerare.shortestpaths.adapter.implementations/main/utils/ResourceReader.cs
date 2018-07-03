package com.programmerare.shortestpaths.utils;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

/**
 * @author Tomas Johansson
 */
public final class ResourceReader {

    /**
    * Return the filenames, sorted in alphabetical order.
    *
    * Assume files like this exist:
    *      "...src\test\resources\directory_for_resourceeader_test\txtFile1.txt"
    *      "...src\test\resources\directory_for_resourceeader_test\txtFile2.txt"
    * Then the input and return examples below illustrates how the method should work.
    * @param pathToResourceFolder e.g. "irectory_for_resourceeader_test"
    * @return e.g. {  "txtFile1.txt"  ,  "txtFile2.txt" }  i.e. only the file names without any path,
    *  and the returned names are sorted in alphabetical order.
    */
	public List<String> getNameOfFilesInResourcesFolder(final String pathToResourceFolder) {
		// if Spring Framework in the future will become included as a dependency then consider using something like below
		// (but currently it seems unnecessary to include Spring only because of this)
		// PathMatchingResourcePatternResolver resolver = new PathMatchingResourcePatternResolver();
		// resolver.getResources("classpath*:some/package/name/**/*.xml");

		final List<String> filenames = new ArrayList<String>();
		try {
			final InputStream inputStream = getResourceAsStream(pathToResourceFolder);
			final BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream));
			String resourceName;
			while( (resourceName = bufferedReader.readLine()) != null ) {
				filenames.add(resourceName);
			}
			Collections.sort(filenames);
			return filenames;
		}
		catch(Exception e) {
			throw new RuntimeException(e);
		}
	}

	private InputStream getResourceAsStream(final String resourcePath) {
		final InputStream inputStream = getContextClassLoader().getResourceAsStream(resourcePath);
		return inputStream == null ? getClass().getResourceAsStream(resourcePath) : inputStream;
	}

	private ClassLoader getContextClassLoader() {
		return Thread.currentThread().getContextClassLoader();
	}
}