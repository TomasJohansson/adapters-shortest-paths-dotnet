package com.programmerare.shortestpaths.graph.utils;

import java.nio.charset.Charset;
import java.util.ArrayList;
import java.util.List;

import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;
import com.programmerare.shortestpaths.core.api.generics.EdgeGenerics;
import com.programmerare.shortestpaths.core.parsers.EdgeParser;
import com.programmerare.shortestpaths.core.parsers.EdgeParser.EdgeFactory;
import com.programmerare.shortestpaths.utils.FileReader;

/**
 * The class is used for creating Edge instances from files.
 * Each line in such a file is expected to be in the format:
 * "A B 12.34"  which means an edge from Vertex A to Vertex B with Weight 12.34 
 * 
 * @author Tomas Johansson
 */
public final class FileReaderForGraphEdges<E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> {
	
	private final FileReader fileReader;
	private final EdgeParser<E, V, W> edgeParser;
	
	private FileReaderForGraphEdges(final EdgeFactory<E, V , W> edgeFactory) {
		fileReader = new FileReader();
		edgeParser = EdgeParser.createEdgeParser(edgeFactory);
	}
	
	public static <E extends EdgeGenerics<V, W> , V extends Vertex , W extends Weight> FileReaderForGraphEdges<E, V, W> createFileReaderForGraphEdges(final EdgeFactory<E, V , W> edgeFactory) {
		return new FileReaderForGraphEdges<E, V, W>(edgeFactory);
	}

//	public static <E extends Edge<V, W> , V extends Vertex , W extends Weight> FileReaderForGraphEdges<E, V, W> createFileReaderForGraphEdges() {
//		return createFileReaderForGraphEdges(null);
//	}	

	public List<E> readEdgesFromFile(final String filePath) {
		return readEdgesFromFile(filePath, FileReader.CHARSET_UTF_8);		
	}

	/**
	 * @param filePath a path relative to the resources directory.
	 * Example of a file: "/src/test/resources/directory_for_filereader_test/file_for_filereader_test.txt"
	 * Then the filePath parameter should be "directory_for_filereader_test/file_for_filereader_test.txt" 
	 * @param charsetForInputFile
	 * @return
	 */
	public List<E> readEdgesFromFile(final String filePath, final Charset charsetForInputFile) {
		final List<E> edges = new ArrayList<E>();
		final List<String> linesFromFileWithGraphEdges = fileReader.readLines(filePath, charsetForInputFile);
		for (String lineInFileRepresengingEdge : linesFromFileWithGraphEdges) {
			edges.add(edgeParser.fromStringToEdge(lineInFileRepresengingEdge));
		}
		return edges;
	}
}