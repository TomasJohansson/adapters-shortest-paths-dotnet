/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in the "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using System.Xml;
using static Programmerare.ShortestPaths.Core.Impl.VertexImpl; // createVertex
using NUnit.Framework;
using Programmerare.ShortestPaths.Adapter.Bsmock;
using Programmerare.ShortestPaths.Adapter.YanQi;
using Programmerare.ShortestPaths.Adapter.QuikGraph;
using Programmerare.ShortestPaths.Core.Api;
using Programmerare.ShortestPaths.Core.Parsers;
using Programmerare.ShortestPaths.Graphs.Utils;
using Programmerare.ShortestPaths.Utils;
using System.Collections.Generic;
using static Programmerare.ShortestPaths.Graphs.Utils.GraphShortestPathAssertionHelper;
using System;
using Programmerare.ShortestPaths.Core.Validation;

namespace Programmerare.ShortestPaths.Graphs.Tests {
    /**
     * The class can run test cases with both input data and expected output data defined in xml files.
     * See an example in the xml file ".../src/test/resources/test_graphs/small_graph_1.xml"   
     * 
     */
    [TestFixture]
    public class XmlDefinedTestCasesTest {

	    private EdgeUtility<Edge, Vertex, Weight> edgeUtility;
	    private XmlFileReader xmlFileReader;
	    private ResourceReader resourceReader;
	    private GraphShortestPathAssertionHelper graphShortestPathAssertionHelper;
	    private EdgeParser<Edge, Vertex, Weight> edgeParser;

	    private IList<PathFinderFactory> pathFinderFactoriesForAllImplementations;
	    private IList<PathFinderFactory> pathFinderFactories;

	    private IList<string> pathsToResourcesFoldersWithXmlTestFiles;

	    private const string BASE_DIRECTORY_FOR_XML_TEST_FILES = "test_graphs/";
	    /**
	     * The test data in the xml files are based on test data found in the implementation library 
	     * for the implementation "bsmock", i.e. the txt files in the following directory:
	     * https://github.com/bsmock/k-shortest-paths/tree/master/edu/ufl/cise/bsmock/graph/ksp/test 
	     * https://github.com/TomasJohansson/k-shortest-paths/tree/master/edu/ufl/cise/bsmock/graph/ksp/test
	     */
	    private const string DIRECTORY_FOR_XML_TEST_FILES_FROM_BSMOCK = BASE_DIRECTORY_FOR_XML_TEST_FILES + "origin_bsmock/";
	
	    // TODO: currently no files in this directory but when they are created here there shuuld be a comment here similar to the comment above
	    private const string DIRECTORY_FOR_XML_TEST_FILES_FROM_YANQI = BASE_DIRECTORY_FOR_XML_TEST_FILES + "origin_yanqi/";

	    /**
	     * Used for asserting that we have tested at least a certain number of xml test files,
	     * to avoid the potential problem that we iterated empty lists of files and then believe everything worked 
	     * even though we actually tested nothing.
	     */
	    private int minimumTotalNumberOfXmlTestFiles = 11; // 2 in base directory and 2 in "bsmock" (actually 3 there but one is in the exclusion list of files) and some more in yanqi directory,...  
	
	    [SetUp]
	    public void SetUp() {
		    edgeUtility = EdgeUtility<Edge, Vertex, Weight>.Create<Edge, Vertex, Weight>();
		
		    //fileReaderForGraphTestData = FileReaderForGraphEdges<Edge, Vertex, Weight>.CreateFileReaderForGraphEdges(new EdgeFactoryDefault());
            
		    graphShortestPathAssertionHelper = new GraphShortestPathAssertionHelper(false);
		
		    xmlFileReader = new XmlFileReader();
		    resourceReader = new ResourceReader();
		    edgeParser = EdgeParser<Edge, Vertex, Weight>.CreateEdgeParserDefault();
            

		    pathFinderFactoriesForAllImplementations = PathFinderFactories.CreatePathFinderFactories();
		    pathFinderFactories = new List<PathFinderFactory>(); // set to empty here before each test, so add to the list if it needs to be used
	
		    pathsToResourcesFoldersWithXmlTestFiles = new List<string>{
			    DIRECTORY_FOR_XML_TEST_FILES_FROM_BSMOCK, 
			    DIRECTORY_FOR_XML_TEST_FILES_FROM_YANQI,
			    BASE_DIRECTORY_FOR_XML_TEST_FILES // yes the base directory itself also currently has some xml test files
            };
		    graphShortestPathAssertionHelper.SetConsoleOutputDesired(ConsoleOutputDesired.NONE);
	    }


	    /**
	     * The content of the test data in this xml file is copied from the "bsmock" implementation: 
	     * /tomas-fork_bsmock_k-shortest-paths/edu/ufl/cise/bsmock/graph/ksp/test/small_road_network_01.txt
	     * It contains 28.000+ Edges, and it takes a long time for some implementations.
	     * Therefore it is excluded from the frequent testing.
	     */
	    private const string XML_FILE_BIG_TEST__SMALL_ROAD_NETWORK_01 = "small_road_network_01.xml";
	
	    /**
	     * The content of the test data in this xml file is copied from the "yanqi" implementation: 
	     * 	https://github.com/yan-qi/k-shortest-paths-java-version/blob/master/data/test_50_2
	     * It takes too long time for one of the implementations and therefore it is excluded from the frequent testing.
	     */	
	    private const string XML_FILE_BIG_TEST__50_2 = "test_50_2.xml";

	    /**
	     * The content of the test data in this xml file is copied from the "yanqi" implementation: 
	     * 	https://github.com/yan-qi/k-shortest-paths-java-version/blob/master/data/test_50
	     * It takes too long time for one of the implementations and therefore it is excluded from the frequent testing.
	     */	
	    private const string XML_FILE_BIG_TEST__50 = "test_50.xml";

        private const string XML_FILE_TEST__7 = "test_7.xml";

	    /**
	     * Regarding why these files are excluded, see the comments where the constants are defined
	     */
	    private static readonly List<string> xmlFilesToExclude = new List<string>{
		    XML_FILE_BIG_TEST__SMALL_ROAD_NETWORK_01,
		    XML_FILE_BIG_TEST__50_2,
		    XML_FILE_BIG_TEST__50
        };
	
        
	    private bool ShouldBeExcludedInFrequentTesting(string xmlFileName) {
		    return xmlFilesToExclude.Contains(xmlFileName);
	    }
	
	    /**
	     * Method for troubleshooting (or for big slow files), when you want to temporary want to focus at one 
	     * file, as opposed to normal regression testing when all files are iterated through another test method  
	     */
	    [Test] // enable this row when you want to used the method
	    public void TemporaryTest() {
		    // Either use all factories as the first row below, or add one or more to the list which is empty after the setup method 
    //		pathFinderFactories = pathFinderFactoriesForAllImplementations;
		    // Use the row above OR INSTEAD some of the rows below, to specify which implementations should be used for the test
		    //pathFinderFactories.add(new PathFinderFactoryYanQi<Edge>());
		    pathFinderFactories.Add(new PathFinderFactoryBsmock());
    //		pathFinderFactories.add(new PathFinderFactoryJgrapht<Edge>()); // 67 seconds, compaerd to less than 1 seconds for the other two implementations 
		
    //		runTestCaseDefinedInXmlFile("tiny_graph_01.xml", pathFinderFactories);
    //		runTestCaseDefinedInXmlFile("tiny_graph_02.xml", pathFinderFactories);
    //		runTestCaseDefinedInXmlFile(DIRECTORY_FOR_XML_TEST_FILES_FROM_YANQI, "network.xml", pathFinderFactories);
    //		runTestCaseDefinedInXmlFile(DIRECTORY_FOR_XML_TEST_FILES_FROM_YANQI, "test_5.xml", pathFinderFactories);
		    //runTestCaseDefinedInXmlFile(DIRECTORY_FOR_XML_TEST_FILES_FROM_YANQI, "test_50.xml", pathFinderFactories);
    //		runTestCaseDefinedInXmlFile(DIRECTORY_FOR_XML_TEST_FILES_FROM_YANQI, "test_6_1.xml", pathFinderFactories);
		    runTestCaseDefinedInXmlFile(DIRECTORY_FOR_XML_TEST_FILES_FROM_BSMOCK, "tiny_graph_01.xml", pathFinderFactories);
		    //runTestCaseDefinedInXmlFile(DIRECTORY_FOR_XML_TEST_FILES_FROM_YANQI, "test_6_2.xml", pathFinderFactories);
		    //runTestCaseDefinedInXmlFile(DIRECTORY_FOR_XML_TEST_FILES_FROM_YANQI, "test_6.xml", pathFinderFactories);
		    //runTestCaseDefinedInXmlFile(DIRECTORY_FOR_XML_TEST_FILES_FROM_YANQI, "test_7.xml", pathFinderFactories);
		    //runTestCaseDefinedInXmlFile(DIRECTORY_FOR_XML_TEST_FILES_FROM_YANQI, "test_8.xml", pathFinderFactories);
	    }

        /**
	     * Testing xml file with a big graph which takes too long time for some implementations 
	     * and therefore that file is excluded from the normal testing where all implementations are used,
	     * and instead use this method for only testing reasonable fast implementation.
	     * @throws IOException
	     */

	    //[Test] 
	    public void TestXmlFile_smallRoadNetwork01() {
		    //graphShortestPathAssertionHelper.SetConsoleOutputDesired(ConsoleOutputDesired.TIME_MEASURE);
		    pathFinderFactories.Add(new PathFinderFactoryYanQi()); // 20 seconds
		    pathFinderFactories.Add(new PathFinderFactoryBsmock()); // 475 seconds
			pathFinderFactories.Add(new PathFinderFactoryQuikGraph()); // 1032 seconds
		    runTestCaseDefinedInXmlFile(DIRECTORY_FOR_XML_TEST_FILES_FROM_BSMOCK, XML_FILE_BIG_TEST__SMALL_ROAD_NETWORK_01, pathFinderFactories);
		    // 27 seconds was the fastest with results at 2018-10-26 below (when running YanQi and Bsmock implementation i.e. the only two at that date)
		    //Xml file with test data: test_graphs/origin_bsmock/small_road_network_01.xml . Seconds: 27. Implementation: PathFinderYanQi
		    //Xml file with test data: test_graphs/origin_bsmock/small_road_network_01.xml . Seconds: 475. Implementation: PathFinderBsmock

		    // 13 seconds was the fastest with results at 2019-12-30 below (when running YanQi and QuikGraph implementation)
		    // Xml file with test data: test_graphs/origin_bsmock/small_road_network_01.xml . Seconds: 13. Implementation: PathFinderYanQi
		    // Xml file with test data: test_graphs/origin_bsmock/small_road_network_01.xml . Seconds: 1032. Implementation: PathFinderQuikGraph
	    }


        [Test]
	    public void TestXmlFile_test_50_2() {
		    graphShortestPathAssertionHelper.SetConsoleOutputDesired(ConsoleOutputDesired.ALL);
		    pathFinderFactories.Add(new PathFinderFactoryYanQi());
            pathFinderFactories.Add(new PathFinderFactoryBsmock());
            pathFinderFactories.Add(new PathFinderFactoryQuikGraph());
            //pathFinderFactories.Add(new PathFinderFactoryQuickGraph());
            //pathFinderFactories.Add(new PathFinderFactoryParrisha()); // fails
		    runTestCaseDefinedInXmlFile(DIRECTORY_FOR_XML_TEST_FILES_FROM_YANQI, XML_FILE_BIG_TEST__50_2, pathFinderFactories);
	    }

	
	
	    [Test]
	    public void TestXmlFile_test_50() {
		    graphShortestPathAssertionHelper.SetConsoleOutputDesired(ConsoleOutputDesired.ALL);
            pathFinderFactories.Add(new PathFinderFactoryYanQi());
            pathFinderFactories.Add(new PathFinderFactoryBsmock());
            pathFinderFactories.Add(new PathFinderFactoryQuikGraph());
            //pathFinderFactories.Add(new PathFinderFactoryQuickGraph());
            //pathFinderFactories.Add(new PathFinderFactoryParrisha());  // fails
            runTestCaseDefinedInXmlFile(DIRECTORY_FOR_XML_TEST_FILES_FROM_YANQI, XML_FILE_BIG_TEST__50, pathFinderFactories);
	    }
	    [Test]
	    public void TestXmlFile_test_7() {
		    graphShortestPathAssertionHelper.SetConsoleOutputDesired(ConsoleOutputDesired.ALL);
            pathFinderFactories.Add(new PathFinderFactoryYanQi());
            pathFinderFactories.Add(new PathFinderFactoryBsmock());
            pathFinderFactories.Add(new PathFinderFactoryQuikGraph());
            //pathFinderFactories.Add(new PathFinderFactoryQuickGraph()); // fails
            //pathFinderFactories.Add(new PathFinderFactoryParrisha()); // fails
            runTestCaseDefinedInXmlFile(DIRECTORY_FOR_XML_TEST_FILES_FROM_YANQI, XML_FILE_TEST__7, pathFinderFactories);
	    }

	    [Test]
	    public void Test_all_xml_files_in_test_graphs_directory() {
		    // the advantage with iterating xml files is this method is that you do not have to add a new test method
		    // for each new xml file with test cases, but the disadvantage is that you do not automatically see which file failed
		    // but that problem is handled in the loop below with a try/catch/throw

		    int counterForNumberOfXmlFilesTested = 0;
		    foreach (string pathToResourcesFoldersWithXmlTestFiles in pathsToResourcesFoldersWithXmlTestFiles) {
			    IList<string> fileNames = resourceReader.GetNameOfFilesInResourcesFolder(pathToResourcesFoldersWithXmlTestFiles);
			    foreach(string fileName in fileNames) {
				    if(fileName.ToLower().EndsWith(".xml") && !ShouldBeExcludedInFrequentTesting(fileName)) {
					    try {
						    runTestCaseDefinedInXmlFile(pathToResourcesFoldersWithXmlTestFiles, fileName, pathFinderFactoriesForAllImplementations);
						    counterForNumberOfXmlFilesTested++;
					    }
					    catch(Exception e) {
						    // Without try/catch here we would fail without seeing which test file caused the failure
						    // We might use the method 'Assert.fail' here but then we do not see the stack trace, so therefore throw exception here 
						    throw new Exception("Failure for the test defined in file " + fileName + " , " + e.Message, e);
					    }
				    }
			    }
		    }
		    Assert.That(counterForNumberOfXmlFilesTested >= minimumTotalNumberOfXmlTestFiles, "Too few files tested"); // TODO java hamcrest Assert.That(counterForNumberOfXmlFilesTested, greaterThanOrEqualTo(minimumTotalNumberOfXmlTestFiles));

	    }

	    private void runTestCaseDefinedInXmlFile(
		    string pathToResourcesFoldersIncludingTrailingSlash, 
		    string nameOfXmlFileWithoutDirectoryPath,
		    IList<PathFinderFactory> pathFinderFactories
	    )  {
		    runTestCaseDefinedInXmlFileWithPathIncludingDirectory(
			    pathToResourcesFoldersIncludingTrailingSlash + nameOfXmlFileWithoutDirectoryPath, 
			    pathFinderFactories
		    );
	    }
	
	    private void runTestCaseDefinedInXmlFileWithPathIncludingDirectory(
		    string pathToResourceXmlFile, 
		    IList<PathFinderFactory> pathFinderFactories,
		    bool shouldAlsoTestResultsWithImplementationsAgainstEachOther = true
	    ) {
		    XmlDocument document = xmlFileReader.GetResourceFileAsXmlDocument(pathToResourceXmlFile);
		    XmlNodeList nodeList = xmlFileReader.GetNodeListMatchingXPathExpression(document, "graphTestData/graphDefinition");
		    Assert.NotNull(nodeList);
		    Assert.AreEqual(1, nodeList.Count); // should only be one graph definition per file
		
		    XmlNode nodeWithGraphDefinition = nodeList.Item(0);
		    Assert.NotNull(nodeWithGraphDefinition);

		    IList<Edge> edgesForGraphPotentiallyIncludingDuplicatedEdges = edgeParser.FromMultiLinedStringToListOfEdges(nodeWithGraphDefinition.InnerText);
    //		System.out.println("efetr fromMultiLinedStringToListOfEdges  " + edgesForGraphPotentiallyIncludingDuplicatedEdges.get(0).getClass());
		    //System.out.println("edgesForGraphPotentiallyIncludingDuplicatedEdges " + edgesForGraphPotentiallyIncludingDuplicatedEdges.size());
		    // There can be duplicates in the list of edges, whcih would cause exception at validation,
		    // so therefore below instead remove duplicated with a chosen strategy 
		    // and one example file with many duplicated edges is "small_road_network_01.xml"
		    IList<Edge> edgesForGraph = edgeUtility.GetEdgesWithoutDuplicates(edgesForGraphPotentiallyIncludingDuplicatedEdges, SelectionStrategyWhenEdgesAreDuplicated.FIRST_IN_LIST_OF_EDGES);
		    //System.out.println("edgesForGraph " + edgesForGraph.size());
		    // edgesForGraphPotentiallyIncludingDuplicatedEdges 28524
		    // edgesForGraph 28320
		    // The above output was the result when running tests for the file "small_road_network_01.xml" 
		
    //		System.out.println("efetr getEdgesWithoutDuplicates  " + edgesForGraph.get(0).getClass());
		
		    PathParser<Path, Edge, Vertex, Weight> pathParser = PathParser<Path, Edge, Vertex, Weight>.CreatePathParserDefault(edgesForGraph);
		
		    XmlNodeList nodeListWithTestCases = xmlFileReader.GetNodeListMatchingXPathExpression(document, "graphTestData/testCase");
		    Assert.NotNull(nodeListWithTestCases); // shouold be zero rather than null i.e. this is okay to test
		    //assertEquals(1, nodeListWithTestCases.getLength()); // can be many (or zero, then just validat implementatins against each other)
		    for (int i = 0; i < nodeListWithTestCases.Count; i++) {
			    XmlNode itemWithTestCase = nodeListWithTestCases.Item(i);
			    XmlNodeList nodeListWithInput = xmlFileReader.GetNodeListMatchingXPathExpression(itemWithTestCase, "input");
			    string outputExpectedAsMultiLinedString  = xmlFileReader.GetTextContentNodeOfFirstSubNode(itemWithTestCase, "outputExpected");
    //			System.out.println("outputExpectedAsMultiLinedString " + outputExpectedAsMultiLinedString);
			    //List fromStringToListOfPaths = pathParser.fromStringToListOfPaths(outputExpectedAsMultiLinedString);			
			    IList<Path> expectedListOfPaths  = outputExpectedAsMultiLinedString == null || outputExpectedAsMultiLinedString.Trim().Equals("") ? null : pathParser.FromStringToListOfPaths(outputExpectedAsMultiLinedString);
			
			    GraphEdgesValidator<Path, Edge, Vertex, Weight> edgeGraphEdgesValidator = GraphEdgesValidator<Path, Edge, Vertex, Weight>.CreateGraphEdgesValidator<Path, Edge, Vertex, Weight>();
			    edgeGraphEdgesValidator.ValidateEdgesAsAcceptableInputForGraphConstruction(edgesForGraph);
			    if(expectedListOfPaths != null) {
				    edgeGraphEdgesValidator.ValidateAllPathsOnlyContainEdgesDefinedInGraph(expectedListOfPaths, edgesForGraph);	
			    }
			
			    Assert.AreEqual(1, nodeListWithInput.Count); // should only be one input element
			    XmlNode nodeWithInputForTestCase  = nodeListWithInput.Item(0);
			    //final NodeList nodeListWithInput = xmlFileReader.getNodeListMatchingXPathExpression(itemWithTestCase, "input");
			    string startVertexId = xmlFileReader.GetTextContentNodeOfFirstSubNode(nodeWithInputForTestCase, "startVertex");
			    string endVertexId = xmlFileReader.GetTextContentNodeOfFirstSubNode(nodeWithInputForTestCase, "endVertex");
    //			System.out.println("startVertexId " + startVertexId);
    //			System.out.println("endVertexId " + endVertexId);
			    string maxNumberOfPathsAsString = xmlFileReader.GetTextContentNodeOfFirstSubNode(nodeWithInputForTestCase, "maxNumberOfPaths");
			    int maxNumberOfPaths = int.Parse(maxNumberOfPathsAsString);
                
    //			System.out.println("maxNumberOfPaths " + maxNumberOfPaths);
    //			System.out.println("innan graphShortestPathAssertionHelper.testResultsWithImplementationsAgainstEachOther " + edgesForGraph.get(0).getClass());
		
			    // pathToResourceXmlFile
		        graphShortestPathAssertionHelper.AssertExpectedResultsOrAssertImplementationsWithEachOther(
				    edgesForGraph, 
				    CreateVertex(startVertexId), 
				    CreateVertex(endVertexId), 
				    maxNumberOfPaths, 
				    pathFinderFactories,
				    expectedListOfPaths, // null, // expectedListOfPaths , use null when we do not want to fail because of expected output according to xml but maybe instyead want to print output with below paaraeter
				    pathToResourceXmlFile,
                    shouldAlsoTestResultsWithImplementationsAgainstEachOther
			    );
		    }
	    }
	
    //    [Obsolete]
	   // //@Deprecated // use xml instead
	   // [Test]
	   // public void Test_graph_datafile__small_graph_1() {
		  //  string filePath = "test_graphs/small_graph_1.txt";
    ////		A B 5
    ////		A C 6
    ////		B C 7
    ////		B D 8
    ////		C D 9
		  //  String startVertexId = "A";
		  //  String endVertexId = "D";
		  //  int maxNumberOfPathsToTryToFind = 5;
		  //  // TODO: improve the usage of files with test data.
		  //  // currently only the edges are specified in the test file 
		  //  // while the tests from A to B is hardcoded as above
		
		  //  // TODO: use xml reader instead to use an xml file instead 
		
		  //  TestPathResultsForImplementationsAgainstEachOther(filePath, startVertexId, endVertexId, maxNumberOfPathsToTryToFind);
	   // }

	   // //@Deprecated // use xml instead
    //    [Obsolete]
	   // [Test]
	   // public void Test_graph_datafile__small_graph_2() throws IOException {
		  //  string filePath = "test_graphs/small_graph_2.txt";
    ////		F G 13
    ////		F H 14
    ////		F I 15
    ////		F J 16
    ////		G H 17
    ////		G I 18
    ////		G J 19
    ////		H I 20
    ////		H J 21
		  //  string startVertexId = "F";
		  //  string endVertexId = "J";
		  //  int maxNumberOfPathsToTryToFind = 5;
		  //  TestPathResultsForImplementationsAgainstEachOther(filePath, startVertexId, endVertexId, maxNumberOfPathsToTryToFind);		
	   // }

	   // private void TestPathResultsForImplementationsAgainstEachOther(
		  //  string filePath, 
		  //  string startVertexId, 
		  //  string endVertexId, 
		  //  int maxNumberOfPathsToTryToFind
	   // ) {
		  //  Vertex startVertex = CreateVertex(startVertexId);
		  //  Vertex endVertex = CreateVertex(endVertexId);
		  //  IList<Edge> edgesForGraph = fileReaderForGraphTestData.ReadEdgesFromFile(filePath);
    ////		System.out.println("edgesForGraph " + edgesForGraph.get(0).getClass());
		  //  graphShortestPathAssertionHelper.TestResultsWithImplementationsAgainstEachOther(
			 //   edgesForGraph, 
			 //   startVertex, 
			 //   endVertex, 
			 //   maxNumberOfPathsToTryToFind, 
			 //   pathFinderFactoriesForAllImplementations
		  //  );
	   // }
    }
}