using NUnit.Framework;

namespace Programmerare.ShortestPaths.Adaptee.YanQi.Test
{
    [TestFixture]
    class GraphFactoryTest
    {
        [Test]
        public void TestGetFullPath_ExistingFile()
        {
            Assert.IsTrue(
                System.IO.File.Exists(
                    GraphFactory.GetFullPath("data/test_50")
                )
            );
        }

        [Test]
        public void TestGetFullPath_NotExistingFile()
        {
            Assert.IsFalse(
                System.IO.File.Exists(
                    GraphFactory.GetFullPath("data/nameOfNonExistinFile")
                )
            );
        }
    }
}
