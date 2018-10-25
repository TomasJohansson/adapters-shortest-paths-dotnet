using NUnit.Framework;

namespace Programmerare.ShortestPaths.Test.Utils
{
    [TestFixture]
    public class TargetFrameworkDetectorTest
    {
        [Test]
        public void GetTargetFrameworkEnumFromStringWithNameTest()
        {
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
                Assert.AreEqual(
                    TargetFrameworkEnum.NETSTANDARD1_0,
                    TargetFrameworkDetector.GetTargetFrameworkEnumFromStringWithName(".NETStandard,Version=v1.0")
                );

                Assert.AreEqual(
                    TargetFrameworkEnum.NETSTANDARD1_1,
                    TargetFrameworkDetector.GetTargetFrameworkEnumFromStringWithName(".NETStandard,Version=v1.1")
                );

                Assert.AreEqual(
                    TargetFrameworkEnum.NETSTANDARD1_2,
                    TargetFrameworkDetector.GetTargetFrameworkEnumFromStringWithName(".NETStandard,Version=v1.2")
                );

                Assert.AreEqual(
                    TargetFrameworkEnum.NETSTANDARD1_3,
                    TargetFrameworkDetector.GetTargetFrameworkEnumFromStringWithName(".NETStandard,Version=v1.3")
                );

                Assert.AreEqual(
                    TargetFrameworkEnum.NETSTANDARD1_4,
                    TargetFrameworkDetector.GetTargetFrameworkEnumFromStringWithName(".NETStandard,Version=v1.4")
                );

                Assert.AreEqual(
                    TargetFrameworkEnum.NETSTANDARD1_5,
                    TargetFrameworkDetector.GetTargetFrameworkEnumFromStringWithName(".NETStandard,Version=v1.5")
                );

                Assert.AreEqual(
                    TargetFrameworkEnum.NETSTANDARD1_6,
                    TargetFrameworkDetector.GetTargetFrameworkEnumFromStringWithName(".NETStandard,Version=v1.6")
                );
        }
    }
}
