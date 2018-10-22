using System;

namespace java.io
{
    // https://docs.oracle.com/javase/7/docs/api/java/io/File.html
    public class File
    {
        private string fileName;

        public File(string fileName)
        {
            this.fileName = fileName;
        }

        public object getCanonicalPath()
        {
            throw new NotImplementedException();
        }
    }
}
