/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in the "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using Programmerare.ShortestPaths.Core.Api;
using Programmerare.ShortestPaths.Core.Api.Generics;
using System;

namespace Programmerare.ShortestPaths.Graphs.Utils
{
    /**
     * The class is used for creating Edge instances from files.
     * Each line in such a file is expected to be in the format:
     * "A B 12.34"  which means an edge from Vertex A to Vertex B with Weight 12.34 
     * 
     * @author Tomas Johansson
     */
    [Obsolete] // use xml instead
    public sealed class FileReaderForGraphEdges<E, V, W>
        where E : EdgeGenerics<V, W>
        where V :Vertex
        where W : Weight
    {
	   // private readonly FileReader fileReader;
	   // private readonly EdgeParser<E, V, W> edgeParser;
	
	   // private FileReaderForGraphEdges(EdgeFactory<E, V , W> edgeFactory) {
		  //  fileReader = new FileReader();
		  //  edgeParser = EdgeParser<E, V, W>.CreateEdgeParser(edgeFactory);
	   // }
	
	   // public static FileReaderForGraphEdges<E, V, W> CreateFileReaderForGraphEdges(EdgeFactory<E, V , W> edgeFactory)
    //    {
		  //  return new FileReaderForGraphEdges<E, V, W>(edgeFactory);
	   // }

    ////	public static <E extends Edge<V, W> , V extends Vertex , W extends Weight> FileReaderForGraphEdges<E, V, W> createFileReaderForGraphEdges() {
    ////		return createFileReaderForGraphEdges(null);
    ////	}	

	   // public IList<E> ReadEdgesFromFile(string filePath) {
		  //  return ReadEdgesFromFile(filePath, Charset.CHARSET_UTF_8);		
	   // }

	   // /**
	   //  * @param filePath a path relative to the resources directory.
	   //  * Example of a file: "/src/test/resources/directory_for_filereader_test/file_for_filereader_test.txt"
	   //  * Then the filePath parameter should be "directory_for_filereader_test/file_for_filereader_test.txt" 
	   //  * @param charsetForInputFile
	   //  * @return
	   //  */
	   // public IList<E> ReadEdgesFromFile(string filePath, Charset charsetForInputFile) {
		  //  IList<E> edges = new List<E>();
		  //  IList<string> linesFromFileWithGraphEdges = fileReader.ReadLines(filePath, charsetForInputFile);
		  //  foreach (string lineInFileRepresengingEdge in linesFromFileWithGraphEdges) {
			 //   edges.Add(edgeParser.FromStringToEdge(lineInFileRepresengingEdge));
		  //  }
		  //  return edges;
	   // }
    }
}