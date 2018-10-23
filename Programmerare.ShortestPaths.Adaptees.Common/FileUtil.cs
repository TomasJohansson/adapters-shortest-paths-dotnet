namespace Programmerare.ShortestPaths.Adaptees.Common
{
    public class FileUtil
    {
        // e.g. fileNamePathRelativeFromProjectDirectory = "data/test_50"
        // (which is the path used in the Java project this project was ported from)
        public static string GetFullPath(string fileNamePathRelativeFromProjectDirectory)
        {
            string basePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string fileNamePathRelativePart = fileNamePathRelativeFromProjectDirectory.Replace('/', '\\');
            return System.IO.Path.Combine(basePath, fileNamePathRelativePart);
        }
    }
}