using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dotnet_adapters_shortest_paths_test.test.QuickGraphTest
{
    public class ExpectedTestResult
    {
        public ExpectedTestResult(string expectedPathsString)
        {
            var expectedPath = new ExpectedPath(123.34, new List<string>{ });
            IList<ExpectedPath> expectedPaths = parseExpectedPaths(expectedPathsString);
            foreach(var e in expectedPaths)
            {
                Console.WriteLine("e " + e.TotalWeightForPath);
                foreach(string vv in e.Vertices)
                {
                    Console.WriteLine("vvert " + vv);
                }
            }

            ExpectedPaths = expectedPaths;
        }

        public IList<ExpectedPath> ExpectedPaths { get; }

        private IList<ExpectedPath> parseExpectedPaths(string expectedPathsString)
        {
            var paths = Regex.Split(expectedPathsString.Trim(), Environment.NewLine);
            
            var expectedPaths = paths.Select(line => {
                var columns = Regex.Split(line.Trim(), @"\s+");
                return new ExpectedPath(
                    totalWeightForPath: double.Parse(columns[0]), 
                    vertices: columns.AsSubList(1)
                );
            }).ToList();
            return expectedPaths;
        }
    }

    public class ExpectedPath
    {
        private double v;
        private List<string> list;

        public ExpectedPath(double totalWeightForPath, IList<string> vertices)
        {
            TotalWeightForPath = totalWeightForPath;
            Vertices = vertices;
        }

        public double TotalWeightForPath { get; }
        public IList<string> Vertices { get; }
    }

    public static class MyExtensions {
    public static IList<T> AsSubList<T>(this T[] array, int startIndex)
    {
        var list = new List<T>();
        for(int i=startIndex; i<array.Length; i++)
        {
            list.Add(array[i]);
        }
        return list;
    }

    }

}
