using System;

namespace com.programmerare.shortestpaths.utils
{
    /**
     * @author Tomas Johansson
     */
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