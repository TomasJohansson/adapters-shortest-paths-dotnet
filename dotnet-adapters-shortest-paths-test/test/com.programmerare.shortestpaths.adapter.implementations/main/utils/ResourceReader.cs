/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in the "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Programmerare.ShortestPaths.Utils
{
    public sealed class ResourceReader {

        // TODO: adjust the documentation below (Java) for the new C#.NET project and its paths ...
        // (see also the documentation at the method XmlFileReader.GetResourceFileAsXmlDocument)
        /**
        * Return the filenames, sorted in alphabetical order.
        *
        * Assume files like this exist:
        *      "...src\test\resources\directory_for_resourceeader_test\txtFile1.txt"
        *      "...src\test\resources\directory_for_resourceeader_test\txtFile2.txt"
        * Then the input and return examples below illustrates how the method should work.
        * @param pathToResourceFolder e.g. "irectory_for_resource_reader_test"
        * @return e.g. {  "txtFile1.txt"  ,  "txtFile2.txt" }  i.e. only the file names without any path,
        *  and the returned names are sorted in alphabetical order.
        */
	    public IList<string> GetNameOfFilesInResourcesFolder(string pathToSubFolderRelativeToResourceFolder) {
            var files = GetFilesInResourcesFolder(pathToSubFolderRelativeToResourceFolder);
            return files.Select(f => f.Name).ToList();
	    }

        public IList<FileInfo> GetFilesInResourcesFolder(string pathToSubFolderRelativeToResourceFolder) {
            string absolutePathToResourceFolder = GetAbsolutePathToResourceFolder();
            string absolutePathToSubFolderRelativeToResourceFolder  = System.IO.Path.Combine(absolutePathToResourceFolder, pathToSubFolderRelativeToResourceFolder);
            var dir = new DirectoryInfo(absolutePathToSubFolderRelativeToResourceFolder);
            if(!dir.Exists) throw new Exception("Directory could not be found: " + dir.FullName + " ||| when using the parameter pathToSubFolderRelativeToResourceFolder: " + pathToSubFolderRelativeToResourceFolder);
            List<FileInfo> files = new List<FileInfo>(dir.GetFiles());
            files.Sort( (f1,f2) => f1.Name.CompareTo(f2.Name));
            return files;
        }

        public FileInfo GetFileInResourcesFolder(string pathToFileRelativeToResourceFolder) {
            string absolutePathToResourceFolder = GetAbsolutePathToResourceFolder();
            string absolutePathToFileWithinResourceFolder  = System.IO.Path.Combine(absolutePathToResourceFolder, pathToFileRelativeToResourceFolder);
            var file = new FileInfo(absolutePathToFileWithinResourceFolder);
            if(!file.Exists) throw new Exception("File does not exist: " + file.FullName);
            return file;
        }

        private string GetAbsolutePathToResourceFolder() {
            string basePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string relativePathToResourceFolder = @"test\com.programmerare.shortestpaths.adapter.implementations\resources\";
            string absolutePathToResourceFolder = System.IO.Path.Combine(basePath, relativePathToResourceFolder);
            return absolutePathToResourceFolder;
        }

        // Java code below:
	    ////private InputStream getResourceAsStream(final String resourcePath) {
     //   private object GetResourceAsStream(string resourcePath) {
		   // //InputStream inputStream = getContextClassLoader().getResourceAsStream(resourcePath);
		   // //return inputStream == null ? getClass().getResourceAsStream(resourcePath) : inputStream;
     //       throw new Exception("TODO implement method");
	    //}

	    ////private ClassLoader getContextClassLoader() {
     //   private object GetContextClassLoader() {
     //       throw new Exception("TODO implement method");
		   // //return Thread.currentThread().getContextClassLoader();
	    //}
    }
}