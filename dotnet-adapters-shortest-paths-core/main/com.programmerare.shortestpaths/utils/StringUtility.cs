/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace com.programmerare.shortestpaths.utils
{
    public sealed class StringUtility {
	    public static IList<string> getMultilineStringAsListOfTrimmedStringsIgnoringLinesWithOnlyWhiteSpace(string multilinedStringWithLineBreaks) {
		    string[] lines = Regex.Split(multilinedStringWithLineBreaks, "[\r\n]+");
		    IList<string> listOfLines = new List<string>(); 
		    foreach (string line in lines) {
			    if(line != null && !line.Trim().Equals("") ) {
				    listOfLines.Add(line.Trim());	
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
	    static string getDoubleAsStringWithoutZeroesAndDotIfNotRelevant(string s) {
            s = Regex.Replace(s, "\\.0+$", "");
		    s = Regex.Replace(s, "^([0-9]*\\.[0-9]*)(0+)$", "$1");
		    // TODO overkill to implement with regexp as above. instead use some formatter https://stackoverflow.com/questions/703396/how-to-nicely-format-floating-numbers-to-string-without-unnecessary-decimal-0
		    return s;		
	    }
	
	    public static string getDoubleAsStringWithoutZeroesAndDotIfNotRelevant(double d) {
		    return getDoubleAsStringWithoutZeroesAndDotIfNotRelevant(d.ToString());
	    }
    }
}