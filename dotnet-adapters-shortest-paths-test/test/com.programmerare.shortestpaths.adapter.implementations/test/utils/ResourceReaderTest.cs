using NUnit.Framework;
using System.Collections.Generic;

namespace com.programmerare.shortestpaths.utils
{
    /**
     * @author Tomas Johansson
     */
    [TestFixture]
    public class ResourceReaderTest {

	    private ResourceReader resourceReader;

	    [SetUp]
	    public void SetUp() {
		    resourceReader = new ResourceReader();
	    }

        // TODO: document (more detailed than here) the two assumptions for this test ...
        //      1. Assumptions about the hardcoded base path to "resources" directory (relative to the root folder)
        //      2. Visual Studio: "Copy to output directory" for each of the files
	    [Test]
	    public void GetNameOfFilesInResourcesFolder() {
		    IList<string> fileNames = resourceReader.GetNameOfFilesInResourcesFolder("directory_for_resource_reader_test");
		    Assert.AreEqual(4, fileNames.Count);
		    Assert.AreEqual("txtFile1.txt", fileNames[0]);
		    Assert.AreEqual("txtFile2.txt", fileNames[1]);
		    Assert.AreEqual("xmlFile1.xml", fileNames[2]);
		    Assert.AreEqual("xmlFile2.xml", fileNames[3]);
	    }
    }
}