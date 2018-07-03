package com.programmerare.shortestpaths.utils;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNotNull;

import org.junit.Before;
import org.junit.Test;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;

/**
 * @author Tomas Johansson
 */
public class XmlFileReaderTest {

	private XmlFileReader xmlFileReader;
	private String filePathForXmlTestFile;
	private String xPathExpressionForSubelements;
	private String textContentForFirstSubelement;
	private String textContentForSecondSubelement;
	
	private String nameOfXmlRootElement;
	private String nameOfXmlSubElement;
	
	@Before
	public void setUp() throws Exception {
		xmlFileReader = new XmlFileReader();
		filePathForXmlTestFile = "directory_for_xmlfilereader_test/xmlFileReaderTest.xml";
		// testing the content of the file "/src/test/resources/directory_for_xmlfilereader_test/xmlFileReaderTest.xml":
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

	@Test
	public void testGetResourceFileAsXmlDocument() {
		final Document document = xmlFileReader.getResourceFileAsXmlDocument(filePathForXmlTestFile);
		assertNotNull(document);
		final Element documentElement = document.getDocumentElement();
		assertEquals(nameOfXmlRootElement, documentElement.getTagName());		
	}
	
	@Test
	public void testGetNodeListMatchingXPathExpression() {
		final Document document = xmlFileReader.getResourceFileAsXmlDocument(filePathForXmlTestFile);
		final NodeList nodeList = xmlFileReader.getNodeListMatchingXPathExpression(document, xPathExpressionForSubelements);
		
		assertNotNull(nodeList);
		assertEquals(2,  nodeList.getLength());
		
		final Node item1 = nodeList.item(0);
		final Node item2 = nodeList.item(1);
		assertNotNull(item1);
		assertNotNull(item2);
		
		assertEquals(textContentForFirstSubelement, item1.getTextContent());
		assertEquals(textContentForSecondSubelement, item2.getTextContent());
	}
	
	@Test
	public void testGetTextContentNodeOfFirstSubNode() {
		final Document document = xmlFileReader.getResourceFileAsXmlDocument(filePathForXmlTestFile);
		final Node rootElement = document.getDocumentElement();
		
		final String result = xmlFileReader.getTextContentNodeOfFirstSubNode(rootElement , nameOfXmlSubElement);
		assertNotNull(result);
		assertEquals(textContentForFirstSubelement,  result);
	}
	
}