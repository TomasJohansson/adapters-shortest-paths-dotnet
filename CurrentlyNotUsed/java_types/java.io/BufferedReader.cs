using System;

namespace java.io
{
    // https://docs.oracle.com/javase/7/docs/api/java/io/BufferedReader.html
    public class BufferedReader
    {
        private FileReader fileReader;

        public BufferedReader(FileReader fileReader)
        {
            this.fileReader = fileReader;
        }

        public string readLine()
        {
            return fileReader.__readLine();
        }

        public void close()
        {
            fileReader.__close();
        }
    }
}
