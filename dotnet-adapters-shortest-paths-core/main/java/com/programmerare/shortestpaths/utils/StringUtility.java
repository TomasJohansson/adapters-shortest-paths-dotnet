/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.utils;

import java.util.ArrayList;
import java.util.List;

public final class StringUtility {
	public static List<String> getMultilineStringAsListOfTrimmedStringsIgnoringLinesWithOnlyWhiteSpace(String multilinedStringWithLineBreaks) {
		final String lines[] = multilinedStringWithLineBreaks.split("[\r\n]+");
		final List<String> listOfLines = new ArrayList<String>(); 
		for (String line : lines) {
			if(line != null && !line.trim().equals("") ) {
				listOfLines.add(line.trim());	
			}
		}
		return listOfLines;	
	}

	/**
	 * Used for testing since the input can then be controlled better with the current implementation.
	 * For example, if you want to assert that 13.0010 (double value) becomes "13.001" then 
	 * you can test with "13.0010" (string) as input, since you otherwise may be incorrectly 
	 * believe that you tested strign behaviour while it was Double.toString which eliminated a zero. 
	 * @param s
	 * @return
	 */
	static String getDoubleAsStringWithoutZeroesAndDotIfNotRelevant(String s) {
		s = s.replaceFirst("\\.0+$", "").replaceFirst("^([0-9]*\\.[0-9]*)(0+)$", "$1");
		// TODO overkill to implement with regexp as above. instead use some formatter https://stackoverflow.com/questions/703396/how-to-nicely-format-floating-numbers-to-string-without-unnecessary-decimal-0
		return s;		
	}
	
	public static String getDoubleAsStringWithoutZeroesAndDotIfNotRelevant(double d) {
		return getDoubleAsStringWithoutZeroesAndDotIfNotRelevant(Double.toString(d));
	}
}
