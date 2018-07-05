using System;

namespace com.programmerare.shortestpaths.graph.utils {
    /**
     * @author Tomas Johansson
     */
    //[TestFixture]
    [Obsolete] // use xml instead
    public class FileReaderForGraphEdgesTest {
	    //[Test]
	    //public void TestReadEdgesFromFile() {
		   // const string filePath = "directory_for_filereader_test/two_edges_A_B_7__X_Y_8.txt";
		   // // /src/test/resources/directory_for_filereader_test/file_for_filereader_test.txt
		   // // the above line has two rows like below:
		   // //A B 7
		   // //X Y 8
		
		   // FileReaderForGraphEdges<Edge, Vertex, Weight> fileReaderForGraphTestData = FileReaderForGraphEdges<Edge, Vertex, Weight>.CreateFileReaderForGraphEdges(new EdgeFactoryDefault());
		   // IList<Edge> edges = fileReaderForGraphTestData.ReadEdgesFromFile(filePath);
		   // Assert.NotNull(edges);
		   // Assert.AreEqual(2, edges.Count);
		
		   // Edge edge1 = edges[0]; // A B 7
		   // Edge edge2 = edges[1]; // X Y 8
		   // assertNonNulls(edge1);
		   // assertNonNulls(edge2);

		   // Assert.AreEqual("A", edge1.StartVertex.VertexId);
		   // Assert.AreEqual("B", edge1.EndVertex.VertexId);
		   // Assert.AreEqual(7, edge1.EdgeWeight.WeightValue, SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
		
		   // Assert.AreEqual("X", edge2.StartVertex.VertexId);
		   // Assert.AreEqual("Y", edge2.EndVertex.VertexId);
		   // Assert.AreEqual(8, edge2.EdgeWeight.WeightValue, SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);		
		
	    //}

	    //private void assertNonNulls(Edge edge) {
		   // Assert.NotNull(edge);
		   // Assert.NotNull(edge.StartVertex);
		   // Assert.NotNull(edge.EndVertex);
		   // Assert.NotNull(edge.EdgeWeight);
	    //}
    }
}