using NUnit.Framework;
using System.Collections.Generic;

namespace Programmerare.ShortestPaths.Test.Utils {

    [TestFixture]
    public class TargetFrameworkDetectorTest {
    
        [Test]
        public void GetTargetFrameworkEnumFromStringWithNameTest() {
            // The below strings have been retrieved in runtime 
            // (using Console.WriteLine for targetFramework.FrameworkName 
            //  when iterating 
            //  foreach(TargetFrameworkAttribute targetFramework in typeof(SomeClassInAssembly).Assembly.GetCustomAttributes(typeof(TargetFrameworkAttribute), true)) { )
                
            // Strings (resulting when doing as mentioned above) 
            // when having used .NET Standard 1.0 - 1.6 as the targets:
            //".NETStandard,Version=v1.0"
            //".NETStandard,Version=v1.1"
            //".NETStandard,Version=v1.2"
            //".NETStandard,Version=v1.3"
            //".NETStandard,Version=v1.4"
            //".NETStandard,Version=v1.5"
            //".NETStandard,Version=v1.6"
            // TargetFrameworkDetector.GetTargetFrameworkEnumFromStringWithName(".NETStandard,Version=v1.0");

            // the key below is the input string to be used as parameter 
            // for the tested method, and the value is the expected return value for the tested method
            var testData = new Dictionary<string, TargetFrameworkEnum> {
                {".NETStandard,Version=v1.0",  TargetFrameworkEnum.NETSTANDARD1_0},
                {".NETStandard,Version=v1.1",  TargetFrameworkEnum.NETSTANDARD1_1},
                {".NETStandard,Version=v1.2",  TargetFrameworkEnum.NETSTANDARD1_2},
                {".NETStandard,Version=v1.3",  TargetFrameworkEnum.NETSTANDARD1_3},
                {".NETStandard,Version=v1.4",  TargetFrameworkEnum.NETSTANDARD1_4},
                {".NETStandard,Version=v1.5",  TargetFrameworkEnum.NETSTANDARD1_5},
                {".NETStandard,Version=v1.6",  TargetFrameworkEnum.NETSTANDARD1_6}
            };

            foreach(var keyValuePair in testData) {
                Assert.AreEqual(
                    keyValuePair.Value, // expected
                    TargetFrameworkDetector.GetTargetFrameworkEnumFromStringWithName(keyValuePair.Key)
                );
            }

        }
    }
}