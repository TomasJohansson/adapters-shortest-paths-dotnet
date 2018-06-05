/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
//import static com.programmerare.shortestpaths.core.impl.EdgeImpl.createEdge;
//import static com.programmerare.shortestpaths.core.impl.VertexImpl.createVertex;
//import static com.programmerare.shortestpaths.core.impl.WeightImpl.SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS;
//import static com.programmerare.shortestpaths.core.impl.WeightImpl.createWeight;
//import static com.programmerare.shortestpaths.core.impl.generics.EdgeGenericsImpl.createEdgeGenerics;
//import static com.programmerare.shortestpaths.core.parsers.EdgeParser.createEdgeParser;
//import static org.junit.Assert.*;
//import java.util.List;
//import org.junit.Before;
//import org.junit.BeforeClass;
//import org.junit.Test;
using com.programmerare.shortestpaths.core.api;
using com.programmerare.shortestpaths.core.api.generics;
using com.programmerare.shortestpaths.core.parsers;
using NUnit.Framework;
using static NUnit.Framework.Assert;
using com.programmerare.shortestpaths.core.api;
using static com.programmerare.shortestpaths.core.impl.GraphImpl;
using static com.programmerare.shortestpaths.core.impl.VertexImpl; // createVertex
using static com.programmerare.shortestpaths.core.impl.VertexImpl;
using static com.programmerare.shortestpaths.core.impl.EdgeImpl;
using static com.programmerare.shortestpaths.core.impl.WeightImpl; // createWeight
                                                                   // using static com.programmerare.shortestpaths.core.impl.generics.EdgeGenericsImpl<Vertex, Weight>; // createEdgeGenerics
using com.programmerare.shortestpaths.core.api.generics;
using com.programmerare.shortestpaths.core.validation;
using System.Collections.Generic;
using com.programmerare.shortestpaths.core.impl.generics;
using System;

namespace com.programmerare.shortestpaths.core.parsers
{
    /**
     * @author Tomas Johansson
     */
    [TestFixture]
    public class EdgeParserTest
    {
        private EdgeParser<Edge, Vertex, Weight> edgeParser;

        [SetUp]
        public void setUp()
        {
            edgeParser = EdgeParser<Edge,Vertex,Weight>.createEdgeParserDefault();
        }

        [Test]
        public void testFromStringToEdge()
        {
            Edge edge = edgeParser.fromStringToEdge("A B 3.7");
            IsNotNull(edge);
            IsNotNull(edge.getStartVertex());
            IsNotNull(edge.getEndVertex());
            IsNotNull(edge.getEdgeWeight());
            AreEqual("A", edge.getStartVertex().getVertexId());
            AreEqual("B", edge.getEndVertex().getVertexId());
            AreEqual(3.7, edge.getEdgeWeight().getWeightValue(), SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
        }
        // TODO: refactor away duplication from above and below methods
        [Test]
        public void testFromStringToEdgeGenerics()
        {
            Edge edge = edgeParser.fromStringToEdge("A B 3.7");
            IsNotNull(edge);
            IsNotNull(edge.getStartVertex());
            IsNotNull(edge.getEndVertex());
            IsNotNull(edge.getEdgeWeight());
            AreEqual("A", edge.getStartVertex().getVertexId());
            AreEqual("B", edge.getEndVertex().getVertexId());
            AreEqual(3.7, edge.getEdgeWeight().getWeightValue(), SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
        }

        [Test]
        public void testFromEdgeParserGenericsToString()
        {
            Vertex startVertex = createVertex("A");
            Vertex endVertex = createVertex("B");
            Weight weight = createWeight(3.7);
            Edge edge = createEdge(startVertex, endVertex, weight);
            AreEqual("A B 3.7", edgeParser.fromEdgeToString(edge));
        }
        // TODO: refactor away duplication from above and below methods	
        [Test]
        public void testFromEdgeToString()
        {
            Vertex startVertex = createVertex("A");
            Vertex endVertex = createVertex("B");
            Weight weight = createWeight(3.7);
            Edge edge = createEdge(startVertex, endVertex, weight);
            AreEqual("A B 3.7", edgeParser.fromEdgeToString(edge));
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
            IList<Edge> edges = edgeParser.fromMultiLinedStringToListOfEdges(multiLinedString);
            IsNotNull(edges);
            AreEqual(5, edges.Count);
            EdgeGenerics<Vertex, Weight> firstEdge = edges[0];
            EdgeGenerics<Vertex, Weight> lastEdge = edges[4];
            assertNotNulls(firstEdge);
            assertNotNulls(lastEdge);

            AreEqual("A", firstEdge.getStartVertex().getVertexId());
            AreEqual("B", firstEdge.getEndVertex().getVertexId());
            AreEqual(5, firstEdge.getEdgeWeight().getWeightValue(), SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);

            AreEqual("C", lastEdge.getStartVertex().getVertexId());
            AreEqual("D", lastEdge.getEndVertex().getVertexId());
            AreEqual(9, lastEdge.getEdgeWeight().getWeightValue(), SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
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
            IList<Edge> edges = edgeParser.fromMultiLinedStringToListOfEdges(multiLinedString);
            IsNotNull(edges);
            AreEqual(5, edges.Count);
            EdgeGenerics<Vertex, Weight> firstEdge = edges[0];
            EdgeGenerics<Vertex, Weight> lastEdge = edges[4];
            assertNotNulls(firstEdge);
            assertNotNulls(lastEdge);

            AreEqual("A", firstEdge.getStartVertex().getVertexId());
            AreEqual("B", firstEdge.getEndVertex().getVertexId());
            AreEqual(5, firstEdge.getEdgeWeight().getWeightValue(), SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);

            AreEqual("C", lastEdge.getStartVertex().getVertexId());
            AreEqual("D", lastEdge.getEndVertex().getVertexId());
            AreEqual(9, lastEdge.getEdgeWeight().getWeightValue(), SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS);
        }

        private void assertNotNulls(EdgeGenerics<Vertex, Weight> edge)
        {
            IsNotNull(edge);
            IsNotNull(edge.getStartVertex());
            IsNotNull(edge.getEndVertex());
            IsNotNull(edge.getEdgeWeight());
        }

    }
}