using edu.asu.emit.algorithm.graph;
using Programmerare.ShortestPaths.Adaptees.Common;

namespace programmerare
{
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