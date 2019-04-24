/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in the "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using NUnit.Framework;
using Programmerare.ShortestPaths.Core.Api;
using System.Collections.Generic;
using static Programmerare.ShortestPaths.Core.Impl.WeightImpl; // SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS
using Programmerare.ShortestPaths.Core.Validation;
using System;
using System.Text;
using Programmerare.ShortestPaths.Core.Parsers;
using Programmerare.ShortestPaths.Utils;

namespace Programmerare.ShortestPaths.Graphs.Utils {
    /**
     * Used for validation of paths against each other.
     * TODO: the class should be restructured probably into to two classes with appropriate names.
     * (or at least refactored with better methods)
     * One class/method should invoke different "findShortestPaths" for different implementations 
     * and another would compare a different returned "List<Path<Edge>>" with each other.
     * Please also note that one method currently receives a parameter "List<Path<Edge>> expectedListOfPaths"
     * which is created from xml as an expected output path.
     */
    public class GraphShortestPathAssertionHelper {

	    private static string NEW_LINE = System.Environment.NewLine;
	    PathParser<Path , Edge , Vertex , Weight> pathParserUsedForPrintingToXmlFormatInAssertionMessage;
        
	    public GraphShortestPathAssertionHelper(bool isExecutingThroughTheMainMethod) {
		    this.SetConsoleOutputDesired(isExecutingThroughTheMainMethod ? ConsoleOutputDesired.ALL : ConsoleOutputDesired.NONE);
		    pathParserUsedForPrintingToXmlFormatInAssertionMessage = PathParser<Path , Edge , Vertex , Weight>.CreatePathParserDefault(new List<Edge>());
	    }

	    /**
	     * Overloaded method using null when we do not have an expected list of paths (retrieved from xml)
	     * but only want to compare results from implementations with each other
	     * See also comment at class level.
	     */
	    public void TestResultsWithImplementationsAgainstEachOther(
			    IList<Edge> edgesForBigGraph, 
			    Vertex startVertex,
			    Vertex endVertex, 
			    int numberOfPathsToFind, 
			    IList<PathFinderFactory> pathFinderFactoriesForImplementationsToTest
		    ) {
		    AssertExpectedResultsOrAssertImplementationsWithEachOther(
			    edgesForBigGraph, 
			    startVertex,
			    endVertex, 
			    numberOfPathsToFind, 
			    pathFinderFactoriesForImplementationsToTest,
			    null,
			    null
		    );		
	    }

	    /**
	     * Overloaded method. Note that the last parameter can be null if we only want to compare 
	     * results from different implementations.
	     * The second last parameter is used when we also have an expected path retrieved for example from an xml file.
	     * The last parameter can be used temporarirly for "debugging" purposes when we want to display the results to the console
	     * See comment at class level.
	     */	
	    public void AssertExpectedResultsOrAssertImplementationsWithEachOther(
		    IList<Edge> edgesForBigGraph, 
		    Vertex startVertex,
		    Vertex endVertex, 
		    int numberOfPathsToFind, 
		    IList<PathFinderFactory> pathFinderFactoriesForImplementationsToTest,
		    IList<Path> expectedListOfPaths,
		    string optionalPathToResourceXmlFile,
            bool shouldTestResultsWithImplementationsAgainstEachOther = false
	    ) {
            // TODO: clean up this method e.g. regarding "shouldAlsoTestResultsWithImplementationsAgainstEachOther"
		    //this.SetConsoleOutputDesired(ConsoleOutputDesired.TIME_MEASURE);
		    string messagePrefixWithInformationAboutXmlSourcefileWithTestData = optionalPathToResourceXmlFile == null ? "" : "Xml file with test data: " + optionalPathToResourceXmlFile + " . ";
		    output("Number of edges in the graph to be tested : " + edgesForBigGraph.Count);
		    IDictionary<string, IList<Path>> shortestPathsPerImplementation = new Dictionary<string, IList<Path>>();

		    // the parameter GraphEdgesValidationDesired.NO will be used so therefore do the validation once externally here first
		    GraphEdgesValidator<Path, Edge, Vertex, Weight>.ValidateEdgesForGraphCreation<Path, Edge, Vertex, Weight>(edgesForBigGraph);
		
		    PathParser<Path, Edge, Vertex, Weight> pathParser = PathParser<Path, Edge, Vertex, Weight>.CreatePathParserDefault(edgesForBigGraph);

		    //assertThat("At least some implementation should be used", pathFinderFactoriesForImplementationsToTest.size(), greaterThanOrEqualTo(1));
            // TODO: "hamcrest" syntax similar to above java code
            Assert.That(pathFinderFactoriesForImplementationsToTest.Count >= 1, "At least some implementation should be used");
		    for (int i = 0; i < pathFinderFactoriesForImplementationsToTest.Count; i++) {
			    PathFinderFactory pathFinderFactory = pathFinderFactoriesForImplementationsToTest[i];
			    output("Will now test file " + optionalPathToResourceXmlFile + " with impl " + pathFinderFactory.GetType().Name);
			    TimeMeasurer tm = TimeMeasurer.Start(); 			
			    PathFinder pathFinder = pathFinderFactory.CreatePathFinder(
				    edgesForBigGraph,
				    GraphEdgesValidationDesired.NO // do the validation one time instead of doing it for each pathFinderFactory
			    );
 			    IList<Path> shortestPaths = pathFinder.FindShortestPaths(startVertex, endVertex, numberOfPathsToFind);
 			    Assert.IsNotNull(shortestPaths);
			    //assertThat("At least some path should be found", shortestPaths.size(), greaterThanOrEqualTo(1));
                Assert.That(shortestPaths.Count >= 1, "At least some path should be found"); // TODO "hamcrest" syntax as java above
			    output(
					    messagePrefixWithInformationAboutXmlSourcefileWithTestData
						    + "Seconds: " + tm.GetSeconds() 
						    + ". Implementation: " + pathFinder.GetType().Name, 
					    ConsoleOutputDesired.TIME_MEASURE
			    );
			    if(isAllConsoleOutputDesired()) {
				    DisplayListOfShortestPath(shortestPaths);
				    output("Implementation class for above and below output: " + pathFinderFactory.GetType().Name);
				    DisplayAsPathStringsWhichCanBeUsedInXml(shortestPaths, pathParser);
			    }
			    shortestPathsPerImplementation.Add(pathFinder.GetType().Name, shortestPaths);
		    }
		    IList<string> nameOfImplementations = new List<string>(shortestPathsPerImplementation.Keys);
			if(expectedListOfPaths != null) {
	            AssertResultsWithExpectedPaths(
		            expectedListOfPaths,
		            optionalPathToResourceXmlFile,
                    shortestPathsPerImplementation
	            );
			}
            else // if(expectedListOfPaths != null) {
            if(shouldTestResultsWithImplementationsAgainstEachOther) {
	            AssertResultsWithImplementationsAgainstEachOther(
		            optionalPathToResourceXmlFile,
                    shortestPathsPerImplementation
	            );
            }
	    }

	    private void AssertResultsWithExpectedPaths(
		    IList<Path> expectedListOfPaths,
		    string optionalPathToResourceXmlFile,
            IDictionary<string, IList<Path>> shortestPathsPerImplementation
	    ) {
            IList<string> nameOfImplementations = new List<string>(shortestPathsPerImplementation.Keys);
            for (int i = 0; i < nameOfImplementations.Count; i++) {
			string nameOfImplementation_1 = nameOfImplementations[i];
			IList<Path> pathsFoundByImplementation_1 = shortestPathsPerImplementation[nameOfImplementation_1];
				string failureMessage = nameOfImplementation_1 + " failed when comparing with expected result according to xml file " + optionalPathToResourceXmlFile; 
				Assert.AreEqual(expectedListOfPaths.Count, pathsFoundByImplementation_1.Count, "Mismatching number of paths, " + failureMessage);
				for (int m = 0; m < pathsFoundByImplementation_1.Count; m++) {
					AssertEqualPaths(failureMessage + " , path with index " + m , expectedListOfPaths[m], pathsFoundByImplementation_1[m]);
				}					
            }
        }

	    private void AssertResultsWithImplementationsAgainstEachOther(
		    string optionalPathToResourceXmlFile,// TODO use in assertion messag
            IDictionary<string, IList<Path>> shortestPathsPerImplementation
        ) {
            IList<string> nameOfImplementations = new List<string>(shortestPathsPerImplementation.Keys);
		    for (int i = 0; i < nameOfImplementations.Count; i++) {
			    string nameOfImplementation_1 = nameOfImplementations[i];
			    IList<Path> pathsFoundByImplementation_1 = shortestPathsPerImplementation[nameOfImplementation_1];
			    for (int j = i+1; j < nameOfImplementations.Count; j++) {
				    string nameOfImplementation_2 = nameOfImplementations[j];
				    string comparedImplementations = nameOfImplementation_1 + " vs " + nameOfImplementation_2 + " , "; 
				    IList<Path> pathsFoundByImplementation_2 = shortestPathsPerImplementation[nameOfImplementation_2];
				    Assert.AreEqual(pathsFoundByImplementation_1.Count, pathsFoundByImplementation_2.Count, nameOfImplementation_2 + " vs " + comparedImplementations);
				    string fileNamePrefix = optionalPathToResourceXmlFile == null ? "" : "Xml file which defined the data: " + optionalPathToResourceXmlFile + " , ";
				    for (int k = 0; k < pathsFoundByImplementation_2.Count; k++) {
					    AssertEqualPaths(fileNamePrefix + comparedImplementations + "fail for i,j,k " + i + " , " + j + " , " + k , pathsFoundByImplementation_1[k], pathsFoundByImplementation_2[k]);
				    }
				    output("-----------------");
				    output("Now the results from these two implementations have been compaerd with each other: ");
				    output(nameOfImplementation_1 + " vs " + nameOfImplementation_2);
			    }
		    }
        }

 	    private void DisplayAsPathStringsWhichCanBeUsedInXml(IList<Path> shortestPaths, PathParser<Path, Edge, Vertex, Weight> pathParser) {
		    output("-----");
		    output("The below output is in a format which can be used in xml files with test cases defining the expected output");
		    foreach(Path path in shortestPaths) {
			    output(pathParser.FromPathToString(path));
		    }
		    output("-----");
	    }

	    private void output(object o) {
		    if(isAllConsoleOutputDesired()) {
			    Console.WriteLine(o);
		    }
	    }
	
	    private void output(Object o, ConsoleOutputDesired consoleOutputDesired) {
		    if(isAllConsoleOutputDesired() || consoleOutputDesired == this.consoleOutputDesired) {
			    Console.WriteLine(o);
		    }
	    }
	
	    private ConsoleOutputDesired consoleOutputDesired = ConsoleOutputDesired.NONE;

	    public void SetConsoleOutputDesired(ConsoleOutputDesired consoleOutputDesired) {
		    this.consoleOutputDesired = consoleOutputDesired;
	    }
	    private bool  isAllConsoleOutputDesired() {
		    return consoleOutputDesired == ConsoleOutputDesired.ALL;
	    }	

	    private void AssertEqualPaths(string message, Path expectedPath, Path actualPath) {
		    Assert.NotNull(expectedPath); // the expected list SHOULD not be null but you never know for sure, since it might originate from an xml file which was not properly defined or read
		    Assert.NotNull(actualPath);
		    Assert.NotNull(expectedPath.TotalWeightForPath);
		    Assert.NotNull(actualPath.TotalWeightForPath);
		
		    IList<Edge> expectedEdges = expectedPath.EdgesForPath; 
		    IList<Edge> actualEdges = actualPath.EdgesForPath;
		    Assert.NotNull(expectedEdges, message); // same comment as above, regarding why the expected value is asserted
		    Assert.NotNull(actualEdges, message);

            string messageIncludingActualAndExpectedPath = message + GetMessageIncludingActualAndExpectedPath(actualPath, expectedPath);

		    Assert.AreEqual(
			    expectedEdges.Count, 
			    actualEdges.Count,
			    "Mismatching number of vertices/edges in the path, " + messageIncludingActualAndExpectedPath
		    );
		    for (int i = 0; i < actualEdges.Count; i++) {
			    Edge actualEdge = actualEdges[i];
			    Edge expectedEdge = expectedEdges[i];
			    Assert.NotNull(expectedEdge, messageIncludingActualAndExpectedPath); // same comment as above, regarding why the expected value is asserted 
			    Assert.NotNull(actualEdge, messageIncludingActualAndExpectedPath);
			    Assert.AreEqual(expectedEdge.StartVertex, actualEdge.StartVertex, messageIncludingActualAndExpectedPath);
			    Assert.AreEqual(expectedEdge.EndVertex, actualEdge.EndVertex, messageIncludingActualAndExpectedPath);
			    Assert.AreEqual(
				    expectedEdge.EdgeWeight.WeightValue, 
				    actualEdge.EdgeWeight.WeightValue, 
				    SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS,
				    messageIncludingActualAndExpectedPath
			    );			
			    Assert.AreEqual(expectedEdge, actualEdge, messageIncludingActualAndExpectedPath);
		    }

		    double weightTotal = 0;
		    foreach(Edge edge in actualEdges) {
			    Assert.NotNull(edge.EdgeWeight);
			    weightTotal += edge.EdgeWeight.WeightValue;
		    }
		    Assert.AreEqual(
			    weightTotal, 
			    actualPath.TotalWeightForPath.WeightValue, 
			    SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS,
                messageIncludingActualAndExpectedPath
		    );
		
		    Assert.AreEqual(
			    expectedPath.TotalWeightForPath.WeightValue,
			    actualPath.TotalWeightForPath.WeightValue, 
			    SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS,
			    messageIncludingActualAndExpectedPath
		    );
	    }
	

        private string GetMessageIncludingActualAndExpectedPath(Path actualPath, Path expectedPath)
        {
		    string actualPathAsString = getPathAsPrettyPrintedStringForConsoleOutput(actualPath);
		    string expectedPathAsString = getPathAsPrettyPrintedStringForConsoleOutput(expectedPath);
		    string messageIncludingActualAndExpectedPath = " , actualPath : " + actualPathAsString +  " expectedPath :  " + expectedPathAsString;	
		    messageIncludingActualAndExpectedPath += NEW_LINE + "actual path in xml format:" + NEW_LINE + pathParserUsedForPrintingToXmlFormatInAssertionMessage.FromPathToString(actualPath);
		    messageIncludingActualAndExpectedPath += NEW_LINE + "expected path in xml format:" + NEW_LINE + pathParserUsedForPrintingToXmlFormatInAssertionMessage.FromPathToString(expectedPath);
		    return messageIncludingActualAndExpectedPath;
        }
	
	    // ---------------------------------------------------------------------------------------
	    // TODO: these methods was copied from "/adapters-shortest-paths-example-project/src/main/java/shortest_paths_getting_started_example/ExampleMain.java"
	    // and should be refactored into a reusable utiltity method (probably in core project)
    //	private static void displayShortestPathBetweenEdges(Vertex startVertex, Vertex endVertex, List<Edge> edgesInput, PathFinderFactory<Edge> pathFinderFactory) {
    //		System.out.println("Implementation " + pathFinderFactory.getClass().getName());
    //		Graph<Edge> graph = pathFinderFactory.createGraph(edgesInput);
    //		List<Path<Edge>> shortestPaths = pathFinder.findShortestPaths(startVertex, endVertex, 10); // 10 is max but in this case there are only 3 possible paths to return
    //		displayListOfShortestPath(shortestPaths);
    //	}
	    private static void DisplayListOfShortestPath(IList<Path> shortestPaths) {
		    foreach(Path path in shortestPaths) {
			    Console.WriteLine(getPathAsPrettyPrintedStringForConsoleOutput(path));
		    }
		    Console.WriteLine("-------------------------------------------------------------");		
	    }
	
	    private static String getPathAsPrettyPrintedStringForConsoleOutput(Path path) {
		    StringBuilder sb = new StringBuilder();
		    sb.Append(path.TotalWeightForPath.WeightValue + " ( ");
		    IList<Edge> edges = path.EdgesForPath;
		    for (int i = 0; i < edges.Count; i++) {
			    if(i > 0) {
				    sb.Append(" + ");		
			    }
			    sb.Append(getEdgeAsPrettyPrintedStringForConsoleOutput(edges[i]));
		    }
		    sb.Append(")");
		    return sb.ToString();
	    }
	
	    private static String getEdgeAsPrettyPrintedStringForConsoleOutput(Edge edge) {
		    return edge.EdgeWeight.WeightValue  + "[" + edge.StartVertex.VertexId + "--->" + edge.EndVertex.VertexId + "] ";		
	    }
	    // ---------------------------------------------------------------------------------------
	
	    public enum ConsoleOutputDesired {
		    NONE,
		    PATH_RESULTS,
		    TIME_MEASURE,
		    ALL
	    }	
    }
}