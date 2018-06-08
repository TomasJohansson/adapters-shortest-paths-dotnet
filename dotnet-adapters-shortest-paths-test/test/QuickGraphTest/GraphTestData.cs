using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace dotnet_adapters_shortest_paths_impl_quickgraph.QuickGraphTest
{
    public class GraphTestData
    {
        /// <param name="graphDefinition">
        /// Each row defines an edge with the format: 
        ///     FromVertex ToVertex Weight
        /// Example with a graph with five edges:
        ///     A B 5
        ///     A C 6
        ///     B C 7
        ///     B D 8
        ///     C D 9    
        /// </param>
        /// <param name="startVertex"></param>
        /// <param name="endVertex"></param>
        /// <param name="maxNumberOfPaths"></param>
        /// <param name="expectedPaths">
        /// Each row defines a path with the format: 
        ///     TotalPathWeight FirstVertex SecondVertex ThirdVertex ...
        ///  Example with three paths from A to D:
        ///     13 A B D
        ///     15 A C D
        ///     21 A B C D
        /// </param>
        public GraphTestData(
            string graphDefinition, 
            string startVertex, 
            string endVertex, 
            int maxNumberOfPaths, 
            string expectedPaths
        )
        {
            IList<TestEdge> testEdges = parseEdges(graphDefinition);
            ISet<String> vertices = extractVertices(testEdges);
            foreach(string v in vertices)
            {
                Console.WriteLine("v : " + v);
            }
            foreach(TestEdge e in testEdges)
            {
                Console.WriteLine("e : " + e.StartVertex + " , " + e.EndVertex + " , " + e.Weight);
            }

            Edges = testEdges;
            Vertices = vertices;
            StartVertex = startVertex;
            EndVertex = endVertex;
            MaxNumberOfPaths = maxNumberOfPaths;
        }

        public IList<TestEdge> Edges { get; }
        public ISet<string> Vertices { get; }
        public string StartVertex { get; }
        public string EndVertex { get; }
        public int MaxNumberOfPaths { get; }

        private ISet<string> extractVertices(IList<TestEdge> edges)
        {
            var set = new HashSet<string>();
            foreach(TestEdge edge in edges)
            {
                set.Add(edge.StartVertex);
                set.Add(edge.EndVertex);
            }
            return set;
        }

        private IList<TestEdge> parseEdges(string graphDefinition)
        {
            var lines = Regex.Split(graphDefinition.Trim(), Environment.NewLine);
            foreach(string ss in lines)
            {
                Console.WriteLine("ss " + ss);
            }
            var testEdges = lines.Select(line => {
                var columns = Regex.Split(line.Trim(), @"\s+");
                return new TestEdge(
                    startVertex: columns[0],
                    endVertex: columns[1],
                    weight: double.Parse(columns[2])
                );
            }).ToList();
            return testEdges;
        }
    }

    public class TestEdge
    {
        public TestEdge(string startVertex, string endVertex, double weight)
        {
            StartVertex = startVertex;
            EndVertex = endVertex;
            Weight = weight;
        }
        public string StartVertex { get; }
        public string EndVertex { get; }
        public double Weight { get; }
    }
}
