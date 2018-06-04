/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.core.impl.generics;

/**
 * Testing general behaviour independent of implementation.
 * Of course, since the base class is abstract, some kind of subclass need to be instantiated.
 * 
 * @author Tomas Johansson
 *
 */
public class PathFinderFactoryGenericsBaseTest {

	// The Graph validation tests have moved to GraphImplTest
	
//	// TODO: refactor duplication ... the same test class as below is duplicated in another test class file
//	public final class PathFinderConcreteTest extends PathFinderBase 
//		<
//		PathGenerics<EdgeGenerics<Vertex, Weight>, Vertex, Weight>,
//		EdgeGenerics<Vertex , Weight> , 
//		Vertex , 
//		Weight 
//		>
//	{
//		protected PathFinderConcreteTest(GraphGenerics<EdgeGenerics<Vertex, Weight>, Vertex, Weight> graph
////				GraphEdgesValidationDesired graphEdgesValidationDesired
//		) {
//			//super(graph, graphEdgesValidationDesired);
//			super(graph);
//			Assert.fail("move test to other class since all validation is now done in Graph construction");
//		}
//
//		@Override
//		protected List<PathGenerics<EdgeGenerics<Vertex, Weight>, Vertex, Weight>> findShortestPathHook(Vertex startVertex,
//				Vertex endVertex, int maxNumberOfPaths) {
//			// TODO Auto-generated method stub
//			return null;
//		}
//
//	}
//	
//	public final class PathFinderFactoryConcreteForTest extends PathFinderFactoryGenericsBase
//	< PathFinderGenerics<PathGenerics<EdgeGenerics<Vertex,Weight>,Vertex,Weight> , EdgeGenerics<Vertex,Weight>,Vertex,Weight> , PathGenerics<EdgeGenerics<Vertex,Weight>,Vertex,Weight> , EdgeGenerics<Vertex , Weight> , Vertex , Weight>
//	{
//
//		public PathFinderGenerics<PathGenerics<EdgeGenerics<Vertex, Weight>, Vertex, Weight>, EdgeGenerics<Vertex, Weight>, Vertex, Weight> createPathFinder(
//			GraphGenerics<EdgeGenerics<Vertex, Weight>, Vertex, Weight> graph
//		) {
//			Assert.fail("move test to other class since all validation is now done in Graph construction");
//			return new PathFinderConcreteTest(
//				graph 
//				//graphEdgesValidationDesired
//			);
//		}
//		//
//		
////		public PathFinder<Edge<Vertex, Weight>, Vertex, Weight> createPathFinder(
////				Graph<Edge<Vertex, Weight>, Vertex, Weight> graph,
////				GraphEdgesValidationDesired graphEdgesValidationDesired) {
////			return new PathFinderConcreteTest(
////				graph, 
////				graphEdgesValidationDesired
////			);
////		}
//	}	
}