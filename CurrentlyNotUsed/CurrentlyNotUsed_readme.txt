Some comments about why QuickGraph and "parrisha" implementations are currently not used:

QuickGraph implementation (based on QuickGraph version 3.6.6) seem to work quite good
but not perfectly. It is currently not used because it fails for the test data in the following file 
test_graphs/origin_yanqi/test_7.xml
(test method XmlDefinedTestCasesTest.TestXmlFile_test_7)

"parrisha" implementation (translation from a Python code base) seems to not work generally.
The test passed for the class SmallGraphTest but when later adding more tests it does not work.