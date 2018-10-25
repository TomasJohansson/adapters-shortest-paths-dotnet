using System;
using System.Collections.Generic;
using System.Runtime.Versioning;

// see comment at the top of the file "TargetFramework.cs"

namespace Programmerare.ShortestPaths.Test.Utils {

    internal class TargetFrameworkDetector {
        
        private static IDictionary<String, TargetFrameworkEnum> dictionaryWithFrameworkNameAsKey;
        
        static TargetFrameworkDetector() {
            const string NetStandardPrefix = ".NETStandard,Version=v"; // e.g. "...Version=v1.0"
            dictionaryWithFrameworkNameAsKey = new Dictionary<String, TargetFrameworkEnum>
            {
                {NetStandardPrefix + "1.0",  TargetFrameworkEnum.NETSTANDARD1_0 },
                {NetStandardPrefix + "1.1",  TargetFrameworkEnum.NETSTANDARD1_1 },
                {NetStandardPrefix + "1.2",  TargetFrameworkEnum.NETSTANDARD1_2 },
                {NetStandardPrefix + "1.3",  TargetFrameworkEnum.NETSTANDARD1_3 },
                {NetStandardPrefix + "1.4",  TargetFrameworkEnum.NETSTANDARD1_4 },
                {NetStandardPrefix + "1.5",  TargetFrameworkEnum.NETSTANDARD1_5 },
                {NetStandardPrefix + "1.6",  TargetFrameworkEnum.NETSTANDARD1_6 },
            };
        }

        internal static TargetFramework GetTargetFrameworkForAssembly(Type typeInTheAssembly) {
            var assembly = typeInTheAssembly.Assembly;
            //Console.WriteLine("typeof(Graph).Assembly.ImageRuntimeVersion " + assembly.ImageRuntimeVersion);
            object[] customAttributes = assembly.GetCustomAttributes(typeof(TargetFrameworkAttribute), true);
            foreach(TargetFrameworkAttribute targetFramework in customAttributes) {
                //Console.WriteLine("targetFramework.TypeId : " + targetFramework.TypeId);
                //Console.WriteLine("targetFramework.FrameworkDisplayName : " + targetFramework.FrameworkDisplayName);
                Console.WriteLine("targetFramework.FrameworkName : " + targetFramework.FrameworkName);
                // The below strings have been tested when having used .NET Standard 1.0 - 1.6 as the targets
                // Tested strings so far (by changing the target framework and watching the output):
                //".NETStandard,Version=v1.0"
                //".NETStandard,Version=v1.1"
                //".NETStandard,Version=v1.2"
                //".NETStandard,Version=v1.3"
                //".NETStandard,Version=v1.4"
                //".NETStandard,Version=v1.5"
                //".NETStandard,Version=v1.6"
                TargetFrameworkEnum framework = GetTargetFrameworkEnumFromStringWithName(targetFramework.FrameworkName);
                if(framework != TargetFrameworkEnum.UNKNOWN) {
                    return new TargetFramework(framework);
                }
            }
            return new TargetFramework(TargetFrameworkEnum.UNKNOWN);
        }

        /// <summary>
        /// </summary>
        /// <param name="frameworkName">
        /// The name of a framework, originating from the property
        /// TargetFrameworkAttribute.FrameworkName
        /// </param>
        /// <returns></returns>
        internal static TargetFrameworkEnum GetTargetFrameworkEnumFromStringWithName(string frameworkName) {
            if(dictionaryWithFrameworkNameAsKey.ContainsKey(frameworkName))
            {
                return dictionaryWithFrameworkNameAsKey[frameworkName];
            }
            return TargetFrameworkEnum.UNKNOWN;
        }
    }
}