package com.programmerare.shortestpaths.graph.utils;

import static com.programmerare.shortestpaths.core.impl.WeightImpl.SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS;
import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNotNull;

import java.util.List;

import org.junit.Test;

import com.programmerare.shortestpaths.core.api.Edge;
import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;
import com.programmerare.shortestpaths.core.parsers.EdgeParser;

/**
 * @author Tomas Johansson
 */
public class FileReaderForGraphEdgesTest {

	@Test
	public void testReadEdgesFromFile() {
		final String filePath = "directory_for_filereader_test/two_edges_A_B_7__X_Y_8.txt";
		// /src/test/resources/directory_for_filereader_test/file_for_filereader_test.txt
		// the above line has two rows like below:
		//A B 7
		//X Y 8
		
		final FileReaderForGraphEdges<Edge, Vertex, Weight> fileReaderForGraphTestData = FileReaderForGraphEdges.createFileReaderForGraphEdges(new EdgeParser.EdgeFactoryDefault());
		final List<Edge> edges = fileReaderForGraphTestData.readEdgesFromFile(filePath);
		assertNotNull(edges);
		assertEquals(2, edges.size());
		
		final Edge edge1 = edges.get(0); // A B 7
		final Edge edge2 = edges.get(1); // X Y 8
		assertNonNulls(edge1);
		assertNonNulls(edge2);

		assertEquals("A", edge1.getStartVertex().getVertexId());
		assertEquals("B", edge1.getEndVertex().getVertexId());
		assertEquals(7, edge1.getEdgeWeight().getWeightValue(), SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
		
		assertEquals("X", edge2.getStartVertex().getVertexId());
		assertEquals("Y", edge2.getEndVertex().getVertexId());
		assertEquals(8, edge2.getEdgeWeight().getWeightValue(), SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);		
		
	}

	private void assertNonNulls(Edge edge) {
		assertNotNull(edge);
		assertNotNull(edge.getStartVertex());
		assertNotNull(edge.getEndVertex());
		assertNotNull(edge.getEdgeWeight());
	}
}