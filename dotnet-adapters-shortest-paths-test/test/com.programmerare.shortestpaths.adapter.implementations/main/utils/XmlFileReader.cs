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

namespace com.programmerare.shortestpaths.utils {
    public sealed class XmlFileReader {
        
        private ResourceReader resourceReader;

        public XmlFileReader() {
            resourceReader = new ResourceReader();
	    }

        public XmlNodeList GetNodeListMatchingXPathExpression(XmlNode node, string xPathExpressionAsString) {
            return node.SelectNodes(xPathExpressionAsString);
        }

        // /**
        //  * @param relativePathToXmlFile
        //  *   The relative path beginning from the directory "...\resources\"
        //  *   i.e. the method will find the "resources" directory 
        //  *   and then append the relative path provided in the method parameter.
        //  *   Example: if you want to retrieve the file:
        //  *   "...\dotnet-adapters-shortest-paths-test\test\com.programmerare.shortestpaths.adapter.implementations\resources\directory_for_xmlfilereader_test\xmlFileReaderTest.xml"
        //  *   (which becomes copied to "bin\Debug" if you use "Copy to output directory" in Visual Studio) i.e. to this file:
        //  *   "...\dotnet-adapters-shortest-paths-test\bin\Debug\test\com.programmerare.shortestpaths.adapter.implementations\resources\directory_for_xmlfilereader_test\xmlFileReaderTest.xml"
        //  * Then the parameter value should be "directory_for_xmlfilereader_test\xmlFileReaderTest.xml"
        //  * @return
        //  */
        public XmlDocument GetResourceFileAsXmlDocument(string relativePathToXmlFile) {
            var file = resourceReader.GetFileInResourcesFolder(relativePathToXmlFile);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(file.FullName);
            return xmlDocument;
        }

        // /**
        //  * Typically, the method is supposed to be used when there only is one subnode.
        //  * The semantic "First" in the method name indicates the behaviour if there would be more than one subnodes.
        //  * (since an obvious alternative could have been to throw an exception rather than simply returning the first)
        //  * @param nodeParent
        //  * @param nameOfSubnode
        //  * @return the text content of the subnode if such subnode exists, otherwise null is returned 
        //  */
        public string GetTextContentNodeOfFirstSubNode(
            XmlNode nodeParent, 
            string nameOfSubnode
        ) {
            // Example: nodeParent is "input" below and nameOfSubnode is  startVertex. Then "A" should be returned 
            //	    <input> 
            //	    <startVertex>A</startVertex>
            XmlNodeList childNodes = nodeParent.ChildNodes;
            for (int i = 0; i < childNodes.Count; i++) {
                XmlNode childNode = childNodes.Item(i);
                if (childNode.NodeType == XmlNodeType.Element) {
                    if (nameOfSubnode.Equals(childNode.Name)) {
                        return childNode.InnerText;
                    }
                }
            }
            return null;
        }
    }
}