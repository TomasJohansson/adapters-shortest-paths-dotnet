/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in the "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using System;

namespace com.programmerare.shortestpaths.utils
{
    [Obsolete] // use xml instead
    public sealed class FileReader {

        //public final static Charset CHARSET_UTF_8;
        //static {
        //	CHARSET_UTF_8 = Charset.forName("UTF-8");
        //}
        //.NET ... currently use enum Charset.CHARSET_UTF_8

        /**
	     * Reads line from a file which is assumed to use charset UTF-8
	     * @param filePath e.g. ""test_graphs/tiny_graph_01.txt"
	     * @return
	     */
        /*
       public IList<string> ReadLines(string filePath)  {
           return ReadLines(filePath, Charset.CHARSET_UTF_8);
       }

       public IList<string> ReadLines(string filePath, Charset charsetForInputFile)  {
           throw new Exception("TODO implement ReadLines");
           // Java:
           //try {
              // URL resource = Resources.getResource(filePath);
              // IList<String> linesInFile = Resources.readLines(resource, charsetForInputFile);
              // return linesInFile;
           //} catch (IOException e) {
              // throw new RuntimeException(e);
           //}		
       }
   }

   public enum Charset { CHARSET_UTF_8 }
   */
    }
}