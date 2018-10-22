/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in the "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

namespace Programmerare.ShortestPaths.Core.Impl.Generics
{
    //public class PathFinderBaseTest {

	   // private GraphGenerics<EdgeGenerics<Vertex,Weight>,Vertex,Weight> graph;
	   // private IList<PathGenerics<EdgeGenerics<Vertex,Weight>,Vertex,Weight>> pathWithAllEdgesBeingPartOfTheGraph;
	   // private IList<PathGenerics<EdgeGenerics<Vertex,Weight>,Vertex,Weight>> pathWithAllEdgesNOTbeingPartOfTheGraph;
	
	   // private const string NEWLINE = " \r\n";
	
	   // [Test]
	   // public void testCreateWeightInstance() {
		
		  //  PathFinderConcrete<PathGenerics<EdgeGenerics<Vertex,Weight>,Vertex,Weight>, EdgeGenerics<Vertex,Weight>,Vertex,Weight> pathFinderConcrete = new PathFinderConcrete<PathGenerics<EdgeGenerics<Vertex,Weight>,Vertex,Weight> , EdgeGenerics<Vertex,Weight>,Vertex,Weight>(graph, GraphEdgesValidationDesired.YES);
		  //  // TDOO: refactor duplicated creations as above
		
		  //  List<EdgeGenerics<Vertex, Weight>> edges = graph.getEdges();
		  //  Weight weightForFirstEdge = edges[0].getEdgeWeight();
		
		  //  Weight createdWeightInstance = pathFinderConcrete.createInstanceWithTotalWeight(12.456, edges);
		  //  IsNotNull(createdWeightInstance);
		  //  AreEqual(weightForFirstEdge.GetType(), createdWeightInstance.GetType());
		  //  AreEqual(12.456, createdWeightInstance.getWeightValue(), 0.0001);
	   // }

	   // [SetUp]
	   // public void setUp()  {
		  //  EdgeParser<EdgeGenerics<Vertex, Weight>, Vertex, Weight> edgeParser = EdgeParserEdgeParser<EdgeGenerics<Vertex, Weight>, Vertex, Weight>.createEdgeParserGenerics();
		  //  IList<EdgeGenerics<Vertex,Weight>> edges = edgeParser.fromMultiLinedStringToListOfEdges(
				//    "A B 5" + NEWLINE +  
				//    "B C 6" + NEWLINE +
				//    "C D 7" + NEWLINE +
				//    "D E 8" + NEWLINE);
		  //  // TODO: fix below
    //        // graph = GraphGenericsImpl<Edge, Vertex, Weight>.createGraphGenerics<Edge, Vertex, Weight>(edges);	
		
		  //  IList<EdgeGenerics<Vertex,Weight>> edgeForPath1 = edgeParser.fromMultiLinedStringToListOfEdges(
				//    "A B 5" + NEWLINE +  
				//    "B C 7" + NEWLINE);
		  //  PathGenerics<EdgeGenerics<Vertex,Weight>, Vertex, Weight> path1 = createPathGenerics(createWeight(1234), edgeForPath1);
		
		  //  IList<EdgeGenerics<Vertex,Weight>> edgeForPath2 = edgeParser.fromMultiLinedStringToListOfEdges(
				//    "B C 5" + NEWLINE +  
				//    "C D 7" + NEWLINE);
		  //  PathGenerics<EdgeGenerics<Vertex,Weight>, Vertex, Weight>  path2 = createPathGenerics(createWeight(1234), edgeForPath2);

		  //  IList<EdgeGenerics<Vertex,Weight>> edgeForPath3 = edgeParser.fromMultiLinedStringToListOfEdges(
				//    "A B 5" + NEWLINE +  
				//    "E F 7" + NEWLINE); // NOT part of the graph
		  //  PathGenerics<EdgeGenerics<Vertex,Weight>, Vertex, Weight>  path3 = createPathGenerics(createWeight(1234), edgeForPath3);
		
		  //  pathWithAllEdgesBeingPartOfTheGraph = new List {path1, path2 };
		  //  pathWithAllEdgesNOTbeingPartOfTheGraph = new List<Path> {path2, path3 };
	   // }

	
	   // [Test]
	   // public void validateThatAllEdgesInAllPathsArePartOfTheGraph_should_NOT_throw_exception() {
		  //  //var pathFinderConcrete = new PathFinderConcrete<PathGenerics<EdgeGenerics<Vertex,Weight>,Vertex,Weight>, EdgeGenerics<Vertex,Weight>,Vertex,Weight>(graph, GraphEdgesValidationDesired.YES);
		  //  //pathFinderConcrete.validateThatAllEdgesInAllPathsArePartOfTheGraph(this.pathWithAllEdgesBeingPartOfTheGraph);
    //        Fail("fix");
	   // }
	
	   // //@Test(expected = GraphValidationException.class)
    //    [Test]
	   // public void validateThatAllEdgesInAllPathsArePartOfTheGraph_SHOULD_throw_exception() {
    //        Fail("fix exception test");
		  //  //PathFinderConcrete<PathGenerics<EdgeGenerics<Vertex,Weight>,Vertex,Weight>, EdgeGenerics<Vertex,Weight>,Vertex,Weight> pathFinderConcrete = new PathFinderConcrete<PathGenerics<EdgeGenerics<Vertex,Weight>,Vertex,Weight>, EdgeGenerics<Vertex,Weight>,Vertex,Weight>(graph, GraphEdgesValidationDesired.YES);
		  //  //pathFinderConcrete.validateThatAllEdgesInAllPathsArePartOfTheGraph(this.pathWithAllEdgesNOTbeingPartOfTheGraph);
	   // }

	   // // TODO: refactor duplication ... the same etst class as below is duplicated in another test class file
	   // public sealed class PathFinderConcrete<P, E, V, W> : PathFinderBase<P, E, V, W>
    //        where P : PathGenerics<E, V, W>
    //        where E : EdgeGenerics<V, W>
    //        where V : Vertex
    //        where W : Weight
    //    {
		  //  protected PathFinderConcrete(GraphGenerics<E, V, W> graph, GraphEdgesValidationDesired graphEdgesValidationDesired): base(graph) {
		  //  }

    //        protected override List<P> findShortestPathHook(V startVertex, V endVertex, int maxNumberOfPaths)
    //        {
    //            return null;
    //        }
    //    }
    //}
}