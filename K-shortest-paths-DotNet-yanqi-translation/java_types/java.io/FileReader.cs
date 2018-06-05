using System;
using System.IO;

namespace java.io
{
    // https://docs.oracle.com/javase/7/docs/api/java/io/FileReader.html
    public class FileReader
    {
        private string fileName;
        private StreamReader streamReader;

        public FileReader(string fileName)
        {
            this.fileName = fileName;
            this.streamReader = new StreamReader(fileName);
        }

        internal string __readLine()
        {
            return streamReader.ReadLine();
        }

        internal void __close()
        {
            streamReader.Close();
        }
    }
}
