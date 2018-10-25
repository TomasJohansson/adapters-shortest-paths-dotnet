// This class is located in project "Programmerare.ShortestPaths.Adaptee.YanQi.Test"
// but also added as link from project "Programmerare.ShortestPaths.Adaptee.Bsmock.Test"
// Reason: Reusing it while overkill to create a separate project only because of this one file.
namespace Programmerare.ShortestPaths.Adaptees.Common
{
    // TODO: Move this class to a test library 
    // since it is currently only used from test libraries
    // and then less important if .NET framework is used.
    // But for the real libraries (i.e. non-test libraries)
    // support for .NET Standard is desirable e.g. 1.0
    // but 1.0 does not seem to support retrieving the current path
    // e.g. "Assembly.GetExecutingAssembly()"
    // or AppDomain.CurrentDomain.BaseDirectory

    public class FileUtil
    {
        // https://docs.microsoft.com/sv-se/dotnet/api/system.reflection.assembly.getexecutingassembly?view=netframework-4.7.2
        // Assembly.GetExecutingAssembly 
        // .NET Framework 1.1+
        // .NET Standard 2.0

        // https://docs.microsoft.com/en-us/dotnet/api/system.appdomain.basedirectory?view=netframework-4.7.2
        // AppDomain.CurrentDomain.BaseDirectory
        // .NET Framework 1.1+
        // .NET Standard 2.0

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