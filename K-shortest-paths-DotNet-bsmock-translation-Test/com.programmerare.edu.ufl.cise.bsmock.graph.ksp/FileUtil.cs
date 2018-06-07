using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.programmerare.edu.ufl.cise.bsmock.graph.ksp
{
    class FileUtil
    {

        // TODO: move the currently duplicated method "GetFullPath"
        // to some reusable place...
        // current two locations:
        // ...\K-shortest-paths-DotNet-bsmock-translation-Test\com.programmerare.edu.ufl.cise.bsmock.graph.ksp\FileUtil.cs
        // ...\K-shortest-paths-DotNet-yanqi-translation-Test\test\programmerare\GraphFactory.cs

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
