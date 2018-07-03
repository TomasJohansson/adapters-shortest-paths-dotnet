package com.programmerare.shortestpaths.utils;

import java.io.InputStream;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.xpath.XPath;
import javax.xml.xpath.XPathConstants;
import javax.xml.xpath.XPathExpression;
import javax.xml.xpath.XPathExpressionException;
import javax.xml.xpath.XPathFactory;

import org.w3c.dom.Document;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.xml.sax.InputSource;

/**
 * @author Tomas Johansson
 */
public final class XmlFileReader {

	private final DocumentBuilder documentBuilder;
	private final XPath xPath;

	public XmlFileReader() throws Exception {
		final DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
		final XPathFactory xPathfactory = XPathFactory.newInstance();
		documentBuilder = factory.newDocumentBuilder();
		xPath = xPathfactory.newXPath();
	}

	public NodeList getNodeListMatchingXPathExpression(final Node node, final String xPathExpressionAsString) {
		try {
			final XPathExpression xPathExpression = xPath.compile(xPathExpressionAsString);
			final Object nodesFoundByXPathExpression = xPathExpression.evaluate(node, XPathConstants.NODESET);
			return (NodeList) nodesFoundByXPathExpression;
		} catch (XPathExpressionException e) {
			throw new RuntimeException(e);
		}
	}

	/**
	 * 
	 * @param pathToResourceFile example: if you want to retrieve the file "testing the content of the file "/src/test/resources/directory_for_xmlfilereader_test/xmlFileReaderTest.xml"
	 * then the parameter value should be "directory_for_xmlfilereader_test/xmlFileReaderTest.xml"
	 * @return
	 */
	public Document getResourceFileAsXmlDocument(final String pathToResourceFile) {
		final InputStream inputStream = this.getClass().getClassLoader().getResourceAsStream(pathToResourceFile);
		final InputSource inputSource = new InputSource(inputStream);
		return getInputSourceAsDocument(inputSource);
	}

	
	private Document getInputSourceAsDocument(final InputSource xmlInputSource) {
		try {
			return documentBuilder.parse(xmlInputSource);
		} catch (Exception e) {
			throw new RuntimeException(e);
		}
	}

	/**
	 * Typically, the method is supposed to be used when there only is one subnode.
	 * The semantic "First" in the method name indicates the behaviour if there would be more than one subnodes.
	 * (since an obvious alternative could have been to throw an exception rather than simply returning the first)
	 * @param nodeParent
	 * @param nameOfSubnode
	 * @return the text content of the subnode if such subnode exists, otherwise null is returned 
	 */
	public String getTextContentNodeOfFirstSubNode(final Node nodeParent, final String nameOfSubnode) {
		// Example: nodeParent is "input" below and nameOfSubnode is  startVertex. Then "A" should be returned 
//	    <input> 
//	    <startVertex>A</startVertex>
		final NodeList childNodes = nodeParent.getChildNodes();
		for (int i = 0; i < childNodes.getLength(); i++) {
			final Node childNode = childNodes.item(i);
			if(childNode.getNodeType() == Node.ELEMENT_NODE) {
				if(nameOfSubnode.equals(childNode.getNodeName())) {
					return childNode.getTextContent();
				}
			}
		}
		return null;
	}
//	private InputSource getXmlStringAsInputSource(final String xmlString) {
//		final StringReader reader = new StringReader(xmlString);
//		return new InputSource(reader);
//	}
	
//	private NodeList getNodeListMatchingXPathExpression(String xml, String xPathExpression) {
//		final Document document = getInputSourceAsDocument(getXmlStringAsInputSource(xml));
//		return getNodeListMatchingXPathExpression(document, xPathExpression);
//	}	
}