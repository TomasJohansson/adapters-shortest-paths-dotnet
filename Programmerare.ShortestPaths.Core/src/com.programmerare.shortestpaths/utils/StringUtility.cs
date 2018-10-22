/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in this "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Programmerare.ShortestPaths.Utils
{
    public sealed class StringUtility {
	    public static IList<string> GetMultilineStringAsListOfTrimmedStringsIgnoringLinesWithOnlyWhiteSpace(string multilinedStringWithLineBreaks) {
		    string[] lines = Regex.Split(multilinedStringWithLineBreaks, "[\r\n]+");
		    IList<string> listOfLines = new List<string>(); 
		    foreach (string line in lines) {
			    if(line != null && !line.Trim().Equals("") ) {
				    listOfLines.Add(line.Trim());	
			    }
		    }
		    return listOfLines;	
	    }

        /// <summary>
        /// Used for testing since the input can then be controlled better with the current implementation.
        /// For example, if you want to assert that 13.0010 (double value) becomes "13.001" then 
        /// you can test with "13.0010" (string) as input, since you otherwise may be incorrectly 
        /// believe that you tested strign behaviour while it was Double.toString which eliminated a zero.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
	    public static string GetDoubleAsStringWithoutZeroesAndDotIfNotRelevant(string s) {
            s = Regex.Replace(s, "\\.0+$", "");
		    s = Regex.Replace(s, "^([0-9]*\\.[0-9]*)(0+)$", "$1");
		    // TODO overkill to implement with regexp as above. instead use some formatter https://stackoverflow.com/questions/703396/how-to-nicely-format-floating-numbers-to-string-without-unnecessary-decimal-0
		    return s;		
	    }
	
	    public static string GetDoubleAsStringWithoutZeroesAndDotIfNotRelevant(double d) {
		    return GetDoubleAsStringWithoutZeroesAndDotIfNotRelevant(d.ToString());
	    }
    }
}