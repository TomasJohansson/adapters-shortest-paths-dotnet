using NUnit.Framework;
using static NUnit.Framework.Assert;
using edu.asu.emit.algorithm.graph;
using edu.asu.emit.algorithm.graph.shortestpaths;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace com.programmerare.edu.asu.emit.algorithm.graph
{
    /**
     * @author Tomas Johansson
     * @see GraphPossibleToCreateProgrammatically
     */
    [TestFixture]
    public class GraphPossibleToCreateProgrammaticallyTest {

	    [Test]
	    public void testGraphPossibleToCreateProgrammatically() {
		    // The method YenTopKShortestPathsAlgTest.testYenShortestPathsAlg4MultipleGraphs is using 
		    // similar test data as below but retrieving it from a file, and it was not doing any assertions 
		    // but just printing the results to the output window.
		    // However, the weights have been modified a little bit to make sure that no total weights will be 
		    // the same, since equal values make the sort order less obvious when you use assertions.  
		
		    // Each edge in the strings below are specified with three parts separated by a space as below:
		    // "startVertexId endVertexId weight"		
		    IList<string> edgeData = new List<string>{
			    "0 1 1",
			    "1 3 1",
			    "1 2 1",
			    "4 0 0",
			    "4 1 0",
			    "4 3 0",
			    "1 5 0.2",
			    "3 5 0.1",
			    "2 5 0"
            };
		    var edges = getEdges(edgeData);
		
		    GraphPossibleToCreateProgrammatically graph = new GraphPossibleToCreateProgrammatically(
			    6, // the number of vertices
			    edges
		    );
		
		    YenTopKShortestPathsAlg yenAlg = new YenTopKShortestPathsAlg(graph, graph.GetVertex(4), graph.GetVertex(5));
		    // The output below is copied from the output window after running the algorithm for graph with data in file "data/test_6_2"
		    // with the test method "testYenShortestPathsAlg4MultipleGraphs" in test file "edu.asu.emit.qyan.test.YenTopKShortestPathsAlgTest"
		    //	Path 0 : [4, 1, 5]:0.0
		    //	Path 1 : [4, 3, 5]:0.0
		    //	Path 2 : [4, 1, 2, 5]:1.0
		    //	Path 3 : [4, 0, 1, 5]:1.0
		    //	Path 4 : [4, 1, 3, 5]:1.0
		    //	Path 5 : [4, 0, 1, 2, 5]:2.0
		    //	Path 6 : [4, 0, 1, 3, 5]:2.0
		    //	Result # :7

		    // The assertions below are based on the output above
		    assertExpectedPath(yenAlg.Next(), 0.1, 4,3,5);
		    assertExpectedPath(yenAlg.Next(), 0.2, 4,1,5);		
		    assertExpectedPath(yenAlg.Next(), 1.0, 4,1,2,5);
		    assertExpectedPath(yenAlg.Next(), 1.1, 4,1,3,5);
		    assertExpectedPath(yenAlg.Next(), 1.2, 4,0,1,5);		
		    assertExpectedPath(yenAlg.Next(), 2.0, 4,0,1,2,5);
		    assertExpectedPath(yenAlg.Next(), 2.1, 4,0,1,3,5);
		    IsFalse(yenAlg.HasNext());
	    }

	    private void assertExpectedPath(Path path, double expectedTotalCost, params int[] nodenames) {
		    AreEqual(path.GetWeight(), expectedTotalCost, SMALL_DELTA_VALUE_FOR_DOUBLE_CMOPARISONS);
		    var vertices = path.GetVertexList();
		    for (int i = 0; i < nodenames.Length; i++) {
			    AreEqual(vertices.get(i).GetId(), nodenames[i]);
		    }
	    }

	    private const double SMALL_DELTA_VALUE_FOR_DOUBLE_CMOPARISONS = 0.00000001;
	
	    /** 
	     * The string splitting code in this method are the same as in a method in class 'Graph'
	     * but since I am not the owner of that project I do not want to make many refactorings  
	     * that makes it deviate more from the original. Therefore the line splitting is duplicated here.     
	     * @see Graph#addEdgeFromStringWithEdgeNamesAndWeight(String)
	     */
	    private IList<EdgeYanQi> getEdges(IList<string> lines) {
		    IList<EdgeYanQi> edges = new List<EdgeYanQi>(); 
		    foreach (string line in lines) {
			    string[] strList = Regex.Split(line.Trim(), "\\s");
			    int startVertexId = int.Parse(strList[0]);
			    int endVertexId = int.Parse(strList[1]);
			    double weight = double.Parse(strList[2]);
			    edges.Add(new EdgeYanQi(startVertexId, endVertexId, weight));
		    }
		    return edges;
	    }	
    }
}