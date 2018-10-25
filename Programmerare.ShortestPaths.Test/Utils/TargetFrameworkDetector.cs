using System;
using System.Runtime.Versioning;

// see comment at the top of the file "TargetFramework.cs"

namespace Programmerare.ShortestPaths.Test.Utils {

    internal class TargetFrameworkDetector {
        
        internal static TargetFramework GetTargetFrameworkForAssembly(Type typeInTheAssembly) {
            var assembly = typeInTheAssembly.Assembly;
            //Console.WriteLine("typeof(Graph).Assembly.ImageRuntimeVersion " + assembly.ImageRuntimeVersion);
            object[] customAttributes = assembly.GetCustomAttributes(typeof(TargetFrameworkAttribute), true);
            foreach(TargetFrameworkAttribute targetFramework in customAttributes) {
                //Console.WriteLine("targetFramework.TypeId : " + targetFramework.TypeId);
                //Console.WriteLine("targetFramework.FrameworkDisplayName : " + targetFramework.FrameworkDisplayName);
                Console.WriteLine("targetFramework.FrameworkName : " + targetFramework.FrameworkName); 
                // Tested strings so far (by changing the target framework and watching the output):
                //".NETStandard,Version=v1.0"
                //".NETStandard,Version=v1.1"
                //".NETStandard,Version=v1.2"
                //".NETStandard,Version=v1.3"
                //".NETStandard,Version=v1.4"
                const string NetStandardPrefix = ".NETStandard,Version=v"; // e.g. "...Version=v1.0"
                const string v10 = NetStandardPrefix + "1.0";
                const string v11 = NetStandardPrefix + "1.1";
                const string v12 = NetStandardPrefix + "1.2";
                const string v13 = NetStandardPrefix + "1.3";
                const string v14 = NetStandardPrefix + "1.4";
                string s = targetFramework.FrameworkName;
                if(s == v10) return new TargetFramework(TargetFrameworkEnum.NETSTANDARD1_0);
                if(s == v11) return new TargetFramework(TargetFrameworkEnum.NETSTANDARD1_1);
                if(s == v12) return new TargetFramework(TargetFrameworkEnum.NETSTANDARD1_2);
                if(s == v13) return new TargetFramework(TargetFrameworkEnum.NETSTANDARD1_3);
                if(s == v14) return new TargetFramework(TargetFrameworkEnum.NETSTANDARD1_4);
                // The above strings are tested when having used .NET Standard 1.0 - 1.4 as the targets.
                // But the below code are currently only assumptions ...
                // TODO: verify the code below by testing the different .NET Standard versions !
                const string v15 = NetStandardPrefix + "1.5";
                const string v16 = NetStandardPrefix + "1.6";
                if(s == v15) return new TargetFramework(TargetFrameworkEnum.NETSTANDARD1_5);
                if(s == v16) return new TargetFramework(TargetFrameworkEnum.NETSTANDARD1_6);
            }
            return new TargetFramework(TargetFrameworkEnum.UNKNOWN);
        }
    }
}