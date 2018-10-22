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
using Programmerare.ShortestPaths.Core.Api.Generics;
using NUnit.Framework;
using static NUnit.Framework.Assert;
using static Programmerare.ShortestPaths.Core.Impl.VertexImpl; // createVertex
using static Programmerare.ShortestPaths.Core.Impl.EdgeImpl;
using static Programmerare.ShortestPaths.Core.Impl.WeightImpl; // createWeight
using System.Collections.Generic;
using System;

namespace Programmerare.ShortestPaths.Core.Parsers
{
    [TestFixture]
    public class EdgeParserTest
    {
        private EdgeParser<Edge, Vertex, Weight> edgeParser;

        [SetUp]
        public void setUp()
        {
            edgeParser = EdgeParser<Edge,Vertex,Weight>.CreateEdgeParserDefault();
        }

        [Test]
        public void testFromStringToEdge()
        {
            Edge edge = edgeParser.FromStringToEdge("A B 3.7");
            IsNotNull(edge);
            IsNotNull(edge.StartVertex);
            IsNotNull(edge.EndVertex);
            IsNotNull(edge.EdgeWeight);
            AreEqual("A", edge.StartVertex.VertexId);
            AreEqual("B", edge.EndVertex.VertexId);
            AreEqual(3.7, edge.EdgeWeight.WeightValue, SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
        }
        // TODO: refactor away duplication from above and below methods
        [Test]
        public void testFromStringToEdgeGenerics()
        {
            Edge edge = edgeParser.FromStringToEdge("A B 3.7");
            IsNotNull(edge);
            IsNotNull(edge.StartVertex);
            IsNotNull(edge.EndVertex);
            IsNotNull(edge.EdgeWeight);
            AreEqual("A", edge.StartVertex.VertexId);
            AreEqual("B", edge.EndVertex.VertexId);
            AreEqual(3.7, edge.EdgeWeight.WeightValue, SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
        }

        [Test]
        public void testFromEdgeParserGenericsToString()
        {
            Vertex startVertex = CreateVertex("A");
            Vertex endVertex = CreateVertex("B");
            Weight weight = CreateWeight(3.7);
            Edge edge = CreateEdge(startVertex, endVertex, weight);
            AreEqual("A B 3.7", edgeParser.FromEdgeToString(edge));
        }
        // TODO: refactor away duplication from above and below methods	
        [Test]
        public void testFromEdgeToString()
        {
            Vertex startVertex = CreateVertex("A");
            Vertex endVertex = CreateVertex("B");
            Weight weight = CreateWeight(3.7);
            Edge edge = CreateEdge(startVertex, endVertex, weight);
            AreEqual("A B 3.7", edgeParser.FromEdgeToString(edge));
        }

        [Test]
        public void testFromMultiLineStringToListOfEdgesGenerics()
        {
            //	    <graphDefinition>
            //	    A B 5
            //	    A C 6
            //	    B C 7
            //	    B D 8
            //	    C D 9    
            //	    </graphDefinition>
            const String multiLinedString = "A B 5\r\n" +
                    "A C 6\r\n" +
                    "B C 7\r\n" +
                    "B D 8\r\n" +
                    "C D 9";
            IList<Edge> edges = edgeParser.FromMultiLinedStringToListOfEdges(multiLinedString);
            IsNotNull(edges);
            AreEqual(5, edges.Count);
            EdgeGenerics<Vertex, Weight> firstEdge = edges[0];
            EdgeGenerics<Vertex, Weight> lastEdge = edges[4];
            assertNotNulls(firstEdge);
            assertNotNulls(lastEdge);

            AreEqual("A", firstEdge.StartVertex.VertexId);
            AreEqual("B", firstEdge.EndVertex.VertexId);
            AreEqual(5, firstEdge.EdgeWeight.WeightValue, SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);

            AreEqual("C", lastEdge.StartVertex.VertexId);
            AreEqual("D", lastEdge.EndVertex.VertexId);
            AreEqual(9, lastEdge.EdgeWeight.WeightValue, SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
        }
        // TODO: refactor away duplication from above and below methods	
        [Test]
        public void testFromMultiLineStringToListOfEdges()
        {
            //	    <graphDefinition>
            //	    A B 5
            //	    A C 6
            //	    B C 7
            //	    B D 8
            //	    C D 9    
            //	    </graphDefinition>
            const String multiLinedString = "A B 5\r\n" +
                    "A C 6\r\n" +
                    "B C 7\r\n" +
                    "B D 8\r\n" +
                    "C D 9";
            IList<Edge> edges = edgeParser.FromMultiLinedStringToListOfEdges(multiLinedString);
            IsNotNull(edges);
            AreEqual(5, edges.Count);
            EdgeGenerics<Vertex, Weight> firstEdge = edges[0];
            EdgeGenerics<Vertex, Weight> lastEdge = edges[4];
            assertNotNulls(firstEdge);
            assertNotNulls(lastEdge);

            AreEqual("A", firstEdge.StartVertex.VertexId);
            AreEqual("B", firstEdge.EndVertex.VertexId);
            AreEqual(5, firstEdge.EdgeWeight.WeightValue, SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);

            AreEqual("C", lastEdge.StartVertex.VertexId);
            AreEqual("D", lastEdge.EndVertex.VertexId);
            AreEqual(9, lastEdge.EdgeWeight.WeightValue, SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
        }

        private void assertNotNulls(EdgeGenerics<Vertex, Weight> edge)
        {
            IsNotNull(edge);
            IsNotNull(edge.StartVertex);
            IsNotNull(edge.EndVertex);
            IsNotNull(edge.EdgeWeight);
        }

    }
}