package com.programmerare.shortestpaths.graph.utils;

import static com.programmerare.shortestpaths.core.impl.WeightImpl.SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS;
import static org.hamcrest.MatcherAssert.assertThat;
import static org.hamcrest.number.OrderingComparison.greaterThanOrEqualTo; // artifact/jarfile "hamcrest-library" is needed, i.e. this method is NOT included in "hamcrest-core"
import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNotNull;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.programmerare.shortestpaths.core.api.Edge;
import com.programmerare.shortestpaths.core.api.Path;
import com.programmerare.shortestpaths.core.api.PathFinder;
import com.programmerare.shortestpaths.core.api.PathFinderFactory;
import com.programmerare.shortestpaths.core.api.Vertex;
import com.programmerare.shortestpaths.core.api.Weight;
import com.programmerare.shortestpaths.core.parsers.PathParser;
import com.programmerare.shortestpaths.core.validation.GraphEdgesValidationDesired;
import com.programmerare.shortestpaths.core.validation.GraphEdgesValidator;
import com.programmerare.shortestpaths.utils.TimeMeasurer;

/**
 * Used for validation of paths against each other.
 * TODO: the class should be restructured probably into to two classes with appropriate names.
 * (or at least refactored with better methods)
 * One class/method should invoke different "findShortestPaths" for different implementations 
 * and another would compare a different returned "List<Path<Edge>>" with each other.
 * Please also note that one method currently receives a parameter "List<Path<Edge>> expectedListOfPaths"
 * which is created from xml as an expected output path.
 *     
 *  
 * @author Tomas Johansson
 */
public class GraphShortestPathAssertionHelper {
	
	public GraphShortestPathAssertionHelper(final boolean isExecutingThroughTheMainMethod) {
		this.setConsoleOutputDesired(isExecutingThroughTheMainMethod ? ConsoleOutputDesired.ALL : ConsoleOutputDesired.NONE);
	}

	/**
	 * Overloaded method using null when we do not have an expected list of paths (retrieved from xml)
	 * but only want to compare results from implementations with each other
	 * See also comment at class level.
	 */
	public void testResultsWithImplementationsAgainstEachOther(
			final List<Edge> edgesForBigGraph, 
			final Vertex startVertex,
			final Vertex endVertex, 
			final int numberOfPathsToFind, 
			final List<PathFinderFactory> pathFinderFactoriesForImplementationsToTest
		) {
		testResultsWithImplementationsAgainstEachOther(
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
	public void testResultsWithImplementationsAgainstEachOther(
		final List<Edge> edgesForBigGraph, 
		final Vertex startVertex,
		final Vertex endVertex, 
		final int numberOfPathsToFind, 
		final List<PathFinderFactory> pathFinderFactoriesForImplementationsToTest,
		final List<Path> expectedListOfPaths,
		final String optionalPathToResourceXmlFile
	) {
		final String messagePrefixWithInformationAboutXmlSourcefileWithTestData = optionalPathToResourceXmlFile == null ? "" : "Xml file with test data: " + optionalPathToResourceXmlFile + " . ";
		output("Number of edges in the graph to be tested : " + edgesForBigGraph.size());
		final Map<String, List<Path>> shortestPathsPerImplementation = new HashMap<String, List<Path>>();

		// the parameter GraphEdgesValidationDesired.NO will be used so therefore do the validation once externally here first
		GraphEdgesValidator.validateEdgesForGraphCreation(edgesForBigGraph);
		
		final PathParser<Path, Edge, Vertex, Weight> pathParser = PathParser.createPathParserDefault(edgesForBigGraph);
		
		assertThat("At least some implementation should be used", pathFinderFactoriesForImplementationsToTest.size(), greaterThanOrEqualTo(1));
		for (int i = 0; i < pathFinderFactoriesForImplementationsToTest.size(); i++) {
			final PathFinderFactory pathFinderFactory = pathFinderFactoriesForImplementationsToTest.get(i);
			
			final TimeMeasurer tm = TimeMeasurer.start(); 			
			final PathFinder pathFinder = pathFinderFactory.createPathFinder(
				edgesForBigGraph,
				GraphEdgesValidationDesired.NO // do the validation one time instead of doing it for each pathFinderFactory
			);
			List<Path> shortestPaths = pathFinder.findShortestPaths(startVertex, endVertex, numberOfPathsToFind);
			assertNotNull(shortestPaths);
			assertThat("At least some path should be found", shortestPaths.size(), greaterThanOrEqualTo(1));
			output(
					messagePrefixWithInformationAboutXmlSourcefileWithTestData					
						+ "Seconds: " + tm.getSeconds() 
						+ ". Implementation: " + pathFinder.getClass().getName(), 
					ConsoleOutputDesired.TIME_MEASURE
			);
			if(isAllConsoleOutputDesired()) {
				displayListOfShortestPath(shortestPaths);
				output("Implementation class for above and below output: " + pathFinderFactory.getClass().getSimpleName());				
				displayAsPathStringsWhichCanBeUsedInXml(shortestPaths, pathParser);
			}
			shortestPathsPerImplementation.put(pathFinder.getClass().getName(), shortestPaths);
		}
		
		final List<String> nameOfImplementations = new ArrayList<String>(shortestPathsPerImplementation.keySet());
		for (int i = 0; i < nameOfImplementations.size(); i++) {
			final String nameOfImplementation_1 = nameOfImplementations.get(i);
			final List<Path> pathsFoundByImplementation_1 = shortestPathsPerImplementation.get(nameOfImplementation_1);
			if(expectedListOfPaths != null) {
				final String failureMessage = nameOfImplementation_1 + " failed when comparing with expected result according to xml file"; 
				assertEquals("Mismatching number of paths, " + failureMessage, expectedListOfPaths.size(), pathsFoundByImplementation_1.size());
				for (int m = 0; m < pathsFoundByImplementation_1.size(); m++) {
					assertEqualPaths(failureMessage + " , path with index " + m , expectedListOfPaths.get(m), pathsFoundByImplementation_1.get(m));
				}					
			}
			
			for (int j = i+1; j < nameOfImplementations.size(); j++) {
				final String nameOfImplementation_2 = nameOfImplementations.get(j);
				final String comparedImplementations = nameOfImplementation_1 + " vs " + nameOfImplementation_2 + " , "; 
				final List<Path> pathsFoundByImplementation_2 = shortestPathsPerImplementation.get(nameOfImplementation_2);
				assertEquals(pathsFoundByImplementation_1.size(), pathsFoundByImplementation_2.size());
				for (int k = 0; k < pathsFoundByImplementation_2.size(); k++) {
					assertEqualPaths(comparedImplementations + "fail for i,j,k " + i + " , " + j + " , " + k , pathsFoundByImplementation_1.get(k), pathsFoundByImplementation_2.get(k));
				}
				output("-----------------");
				output("Now the results from these two implementations have been compaerd with each other: ");
				output(nameOfImplementation_1 + " vs " + nameOfImplementation_2);
			}
		}
	}

	private void displayAsPathStringsWhichCanBeUsedInXml(List<Path> shortestPaths, PathParser<Path, Edge, Vertex, Weight> pathParser) {
		output("-----");
		output("The below output is in a format which can be used in xml files with test cases defining the expected output");
		for (Path path : shortestPaths) {
			output(pathParser.fromPathToString(path));
		}
		output("-----");
	}

	private void output(Object o) {
		if(isAllConsoleOutputDesired()) {
			System.out.println(o);
		}
	}
	
	private void output(Object o, ConsoleOutputDesired consoleOutputDesired) {
		if(isAllConsoleOutputDesired() || consoleOutputDesired == this.consoleOutputDesired) {
			System.out.println(o);
		}
	}
	
	private ConsoleOutputDesired consoleOutputDesired = ConsoleOutputDesired.NONE;

	public void setConsoleOutputDesired(final ConsoleOutputDesired consoleOutputDesired) {
		this.consoleOutputDesired = consoleOutputDesired;
	}
	private boolean  isAllConsoleOutputDesired() {
		return consoleOutputDesired == ConsoleOutputDesired.ALL;
	}	

	private void assertEqualPaths(final String message, final Path expectedPath, final Path actualPath) {
		assertNotNull(expectedPath); // the expected list SHOULD not be null but you never know for sure, since it might originate from an xml file which was not properly defined or read
		assertNotNull(actualPath);
		assertNotNull(expectedPath.getTotalWeightForPath());
		assertNotNull(actualPath.getTotalWeightForPath());
		
		final List<Edge> expectedEdges = expectedPath.getEdgesForPath(); 
		final List<Edge> actualEdges = actualPath.getEdgesForPath();
		assertNotNull(message, expectedEdges); // same comment as above, regarding why the expected value is asserted
		assertNotNull(message, actualEdges);

		final String actualPathAsString = getPathAsPrettyPrintedStringForConsoleOutput(actualPath);
		final String expectedPathAsString = getPathAsPrettyPrintedStringForConsoleOutput(expectedPath);
		final String messageIncludingActualAndExpectedPath = message + " , actualPath : " + actualPathAsString +  " expectedPath :  " + expectedPathAsString;

		assertEquals(
			"Mismatching number of vertices/edges in the path, " + messageIncludingActualAndExpectedPath, 
			expectedEdges.size(), 
			actualEdges.size()
		);
		for (int i = 0; i < actualEdges.size(); i++) {
			final Edge actualEdge = actualEdges.get(i);
			final Edge expectedEdge = expectedEdges.get(i);
			assertNotNull(messageIncludingActualAndExpectedPath, expectedEdge); // same comment as above, regarding why the expected value is asserted 
			assertNotNull(messageIncludingActualAndExpectedPath, actualEdge);
			assertEquals(messageIncludingActualAndExpectedPath, expectedEdge.getStartVertex(), actualEdge.getStartVertex());
			assertEquals(messageIncludingActualAndExpectedPath, expectedEdge.getEndVertex(), actualEdge.getEndVertex());
			assertEquals(
				messageIncludingActualAndExpectedPath, 
				expectedEdge.getEdgeWeight().getWeightValue(), 
				actualEdge.getEdgeWeight().getWeightValue(), 
				SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS
			);			
			assertEquals(messageIncludingActualAndExpectedPath, expectedEdge, actualEdge);			
		}

		double weightTotal = 0;
		for (Edge edge : actualEdges) {
			assertNotNull(edge.getEdgeWeight());
			weightTotal += edge.getEdgeWeight().getWeightValue();
		}
		assertEquals(
			messageIncludingActualAndExpectedPath, 
			weightTotal, 
			actualPath.getTotalWeightForPath().getWeightValue(), 
			SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS
		);
		
		assertEquals(
			messageIncludingActualAndExpectedPath, 
			expectedPath.getTotalWeightForPath().getWeightValue(), 
			actualPath.getTotalWeightForPath().getWeightValue(), 
			SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS
		);
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
	private static void displayListOfShortestPath(List<Path> shortestPaths) {
		for (Path path : shortestPaths) {
			System.out.println(getPathAsPrettyPrintedStringForConsoleOutput(path));
		}
		System.out.println("-------------------------------------------------------------");		
	}
	
	private static String getPathAsPrettyPrintedStringForConsoleOutput(Path path) {
		StringBuffer sb = new StringBuffer();
		sb.append(path.getTotalWeightForPath().getWeightValue() + " ( ");
		List<Edge> edges = path.getEdgesForPath();
		for (int i = 0; i < edges.size(); i++) {
			if(i > 0) {
				sb.append(" + ");		
			}
			sb.append(getEdgeAsPrettyPrintedStringForConsoleOutput(edges.get(i)));
		}
		sb.append(")");
		return sb.toString();
	}
	
	private static String getEdgeAsPrettyPrintedStringForConsoleOutput(Edge edge) {
		return edge.getEdgeWeight().getWeightValue()  + "[" + edge.getStartVertex().getVertexId() + "--->" + edge.getEndVertex().getVertexId() + "] ";		
	}
	// ---------------------------------------------------------------------------------------
	
	public enum ConsoleOutputDesired {
		NONE,
		PATH_RESULTS,
		TIME_MEASURE,
		ALL
	}	
}