using NUnit.Framework;
using Programmerare.ShortestPaths.Test.Utils;

namespace dotnet_adapters_shortest_paths_test.test.Utils {

    [TestFixture]
    public class TargetFrameworkTest {

        [Test]
        public void IsSupportingFileStreamReaderTest() {
            var netStandard_1_0 = new TargetFramework(TargetFrameworkEnum.NETSTANDARD1_0);
            var netStandard_1_6 = new TargetFramework(TargetFrameworkEnum.NETSTANDARD1_6);
            var netStandard_2_0 = new TargetFramework(TargetFrameworkEnum.NETSTANDARD2_0);
            var netFramework_4_0 = new TargetFramework(TargetFrameworkEnum.NET40);

            Assert.IsFalse(netStandard_1_0.IsSupportingFileStreamReader());
            Assert.IsFalse(netStandard_1_6.IsSupportingFileStreamReader());
            
            Assert.IsTrue(netStandard_2_0.IsSupportingFileStreamReader());
            Assert.IsTrue(netFramework_4_0.IsSupportingFileStreamReader());
        }
    }
}
