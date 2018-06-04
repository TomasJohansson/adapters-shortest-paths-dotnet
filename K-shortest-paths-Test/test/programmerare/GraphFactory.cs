using edu.asu.emit.algorithm.graph;

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

        // e.g. fileNamePathRelativeFromProjectDirectory = "data/test_50"
        // (which is the path used in the Java project this project was ported from)
        internal static string GetFullPath(string fileNamePathRelativeFromProjectDirectory)
        {
            string basePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string fileNamePathRelativePart = fileNamePathRelativeFromProjectDirectory.Replace('/', '\\');
            return System.IO.Path.Combine(basePath, fileNamePathRelativePart);
        }
    }
}