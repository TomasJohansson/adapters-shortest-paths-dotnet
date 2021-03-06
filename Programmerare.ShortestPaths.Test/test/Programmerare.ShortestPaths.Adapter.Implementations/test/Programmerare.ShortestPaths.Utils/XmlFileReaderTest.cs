/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in the "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using System.Xml;
using NUnit.Framework;

namespace Programmerare.ShortestPaths.Utils
{
    public class XmlFileReaderTest {

	    private XmlFileReader xmlFileReader;

	    private string filePathForXmlTestFile;
	    private string xPathExpressionForSubelements;
	    private string textContentForFirstSubelement;
	    private string textContentForSecondSubelement;
	    private string nameOfXmlRootElement;
	    private string nameOfXmlSubElement;
	
	    [SetUp]
	    public void SetUp() {
		    xmlFileReader = new XmlFileReader();
            filePathForXmlTestFile = @"directory_for_xmlfilereader_test\xmlFileReaderTest.xml";
		    // testing the content of the file "/.../resources/directory_for_xmlfilereader_test/xmlFileReaderTest.xml":
    //		<myRoot>
    //		    <mySubElement>abc</mySubElement>
    //		    <mySubElement>def</mySubElement>
    //		</myRoot>
		    xPathExpressionForSubelements = "myRoot/mySubElement";
		    nameOfXmlRootElement = "myRoot";
		    nameOfXmlSubElement = "mySubElement";
		
		    textContentForFirstSubelement = "abc";
		    textContentForSecondSubelement = "def";		
	    }

	    [Test]
	    public void TestGetResourceFileAsXmlDocument() {
		    XmlDocument xmlDocument = xmlFileReader.GetResourceFileAsXmlDocument(filePathForXmlTestFile);
		    Assert.NotNull(xmlDocument);
		    XmlElement documentElement = xmlDocument.DocumentElement;
		    Assert.AreEqual(nameOfXmlRootElement, documentElement.Name);
	    }
	
	    [Test]
	    public void TestGetNodeListMatchingXPathExpression() {
            XmlDocument xmlDocument = xmlFileReader.GetResourceFileAsXmlDocument(filePathForXmlTestFile);
		    XmlNodeList nodeList = xmlFileReader.GetNodeListMatchingXPathExpression(xmlDocument, xPathExpressionForSubelements);
		    Assert.NotNull(nodeList);
		    Assert.AreEqual(2,  nodeList.Count);
		
		    XmlNode item1 = nodeList.Item(0);
		    XmlNode item2 = nodeList[1];
		    Assert.NotNull(item1);
		    Assert.NotNull(item2);
		    Assert.AreEqual(textContentForFirstSubelement, item1.InnerText);
		    Assert.AreEqual(textContentForSecondSubelement, item2.InnerText);
	    }
	
	    [Test]
	    public void TestGetTextContentNodeOfFirstSubNode() {
		    XmlDocument xmlDocument = xmlFileReader.GetResourceFileAsXmlDocument(filePathForXmlTestFile);
		    XmlElement rootElement = xmlDocument.DocumentElement;
		    string result = xmlFileReader.GetTextContentNodeOfFirstSubNode(rootElement , nameOfXmlSubElement);
		    Assert.NotNull(result);
		    Assert.AreEqual(textContentForFirstSubelement,  result);
	    }
    }
}