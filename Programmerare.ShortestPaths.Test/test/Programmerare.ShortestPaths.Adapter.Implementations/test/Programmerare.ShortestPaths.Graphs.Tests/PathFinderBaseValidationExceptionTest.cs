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
using NUnit.Framework;
using static Programmerare.ShortestPaths.Core.Impl.WeightImpl; // SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS
using static Programmerare.ShortestPaths.Core.Impl.EdgeImpl; // createEdge
using static Programmerare.ShortestPaths.Core.Impl.VertexImpl; // createVertex
using System.Collections.Generic;
using Programmerare.ShortestPaths.Core.Validation;
using Programmerare.ShortestPaths.Graphs.Utils;

namespace Programmerare.ShortestPaths.Graphs.Tests {
    /**
     * PathFinderBase is an abstract base class, and this test class verifies that the subclasses throw validation exceptions implemented in the base class. 
     * @author Tomas Johansson
     */
    [TestFixture]
    public class PathFinderBaseValidationExceptionTest {
	    private IList<PathFinderFactory> pathFinderFactories;
	    private Edge edgeAB, edgeBC;
	    private IList<Edge> edges_A_B_and_B_C;
	    private Vertex vertexA, vertexB, vertexC, vertexX_notPartOfGraph;

	    [SetUp]
	    public void SetUp() {
		    pathFinderFactories = PathFinderFactories.CreatePathFinderFactories();

		    vertexA = CreateVertex("A");
		    vertexB = CreateVertex("B");
		    vertexC = CreateVertex("C");
		
		    vertexX_notPartOfGraph = CreateVertex("X");
		
		    edgeAB = CreateEdge(vertexA, vertexB, CreateWeight(123));
		    edgeBC = CreateEdge(vertexB, vertexC, CreateWeight(456));
		
		    edges_A_B_and_B_C = new List<Edge>();
		    edges_A_B_and_B_C.Add(edgeAB);
		    edges_A_B_and_B_C.Add(edgeBC);
	    }


        // -------------------------------------------------------------
        // Test with start vertex not part of the graph
        [Test]
        public void incorrect_startVertex_shouldThrowException() {
            foreach (PathFinderFactory pathFinderFactory in pathFinderFactories) {
                shouldThrowExceptionIfAnyOfTheVerticesIsNotPartOfTheGraph(
                    pathFinderFactory,
                    vertexX_notPartOfGraph,
                    vertexC,
                    isExceptionExpected: true
                );
            }
        }

        // -------------------------------------------------------------
        // Test with end vertex not part of the graph 
        [Test]
        public void incorrect_endVertex_shouldThrowException() {
            foreach (PathFinderFactory pathFinderFactory in pathFinderFactories) {
                shouldThrowExceptionIfAnyOfTheVerticesIsNotPartOfTheGraph(
                    pathFinderFactory,
                    vertexA,
                    vertexX_notPartOfGraph,
                    isExceptionExpected: true
                );
            }
        }


        // -------------------------------------------------------------
        // Test with start and vertex both part of the graph
        // The purpose of the test is simply to show that these do not throw an exception (as the other tests do) and thus no assertions are done about the found paths
        [Test]
        public void correct_startAndEndVertex_should_NOT_ThrowException() {
            foreach (PathFinderFactory pathFinderFactory in pathFinderFactories) {
                shouldThrowExceptionIfAnyOfTheVerticesIsNotPartOfTheGraph(
                    pathFinderFactory,
                    vertexA,
                    vertexC,
                    isExceptionExpected: false
                );
            }
        }

        // -------------------------------------------------------------
        private void FindShortestPaths(
		    PathFinderFactory pathFinderFactory, 
		    Vertex startVertex, 
		    Vertex endVertex
	    ) {
		    PathFinder pathFinder = pathFinderFactory.CreatePathFinder(edges_A_B_and_B_C, GraphEdgesValidationDesired.YES);
		    IList<Path> shortestPaths = pathFinder.FindShortestPaths(startVertex, endVertex, maxNumberOfPaths);
	    }

	    private void shouldThrowExceptionIfAnyOfTheVerticesIsNotPartOfTheGraph(
		    PathFinderFactory pathFinderFactory, 
		    Vertex startVertex, 
		    Vertex endVertex,
            bool isExceptionExpected
	    ) {
            if(isExceptionExpected) {
                Assert.Throws<GraphValidationException>( () => {
                    FindShortestPaths(pathFinderFactory, startVertex, endVertex);
                });
                //ActualValueDelegate<object> testDelegate = () => FindShortestPaths(pathFinderFactory, startVertex, endVertex);
                //Assert.That( () => {
                //    FindShortestPaths(pathFinderFactory, startVertex, endVertex);
                //},
                //Throws.TypeOf<GraphValidationException>
                //);
            }
            else
            {
                FindShortestPaths(pathFinderFactory, startVertex, endVertex);
            }
	    }
	
	    private const int maxNumberOfPaths = 1;
    }
}