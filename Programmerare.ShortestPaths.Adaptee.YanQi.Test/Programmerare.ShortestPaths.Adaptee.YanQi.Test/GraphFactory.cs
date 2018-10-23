using edu.asu.emit.algorithm.graph;
using Programmerare.ShortestPaths.Adaptees.Common;

namespace Programmerare.ShortestPaths.Adaptee.YanQi.Test
{
    /// <summary>
    /// Original java code at the URL below: 
    ///     graph = new Graph("data/test_50"); // https://github.com/yan-qi/k-shortest-paths-java-version/blob/master/src/test/java/edu/asu/emit/qyan/test/ShortestPathAlgTest.java 
    /// This C# project instead uses the following code:
    ///     graph = GraphFactory.createGraph("data/test_50");
    /// (to resolve the relative path provided as parameter)
    /// </summary>
    class GraphFactory
    {
        internal static Graph createGraph(string fileNamePathRelativeFromProjectDirectory)
        {
            return new Graph(GetFullPath(fileNamePathRelativeFromProjectDirectory));
        }

        internal static VariableGraph createVariableGraph(string fileNamePathRelativeFromProjectDirectory)
        {
            return new VariableGraph(GetFullPath(fileNamePathRelativeFromProjectDirectory));
        }

        internal static string GetFullPath(string fileNamePathRelativeFromProjectDirectory)
        {
            return FileUtil.GetFullPath(fileNamePathRelativeFromProjectDirectory);
        }
    }
}