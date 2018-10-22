using NUnit.Framework;
namespace Programmerare.ShortestPaths.Example.Roadrouting {
    // TODO: fix old Java doumentation below ...
    /**
     * This is not a real test class which is doing any assertions, but it is a convenient 
     * way of running the code to write "mvn test" instead of writing (finding and pasting) 
     * the following command:
     * mvn exec:java -Dexec.mainClass="roadrouting.RoadRoutingMain" -Dexec.args="1"
     * 
     * @author Tomas Johansson
     */
    [TestFixture]
    public class RoadRoutingMainTest {

        //private RoadRoutingMain roadRoutingMain;
        private bool useDatabase;

        [SetUp]
        public void SetUp() {
            //roadRoutingMain = new RoadRoutingMain();
        }

        [Test]
        public void TestMainWithDatabase() {
            useDatabase = true;
            RoadRoutingMain.MainMethod(useDatabase);
        }

        [Test]
        public void TestMainWithoutDatabase() {
            useDatabase = false;
            RoadRoutingMain.MainMethod(useDatabase);
        }	
    }
}
