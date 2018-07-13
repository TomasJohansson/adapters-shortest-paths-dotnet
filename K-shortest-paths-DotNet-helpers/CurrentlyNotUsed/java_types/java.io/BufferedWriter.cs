namespace java.io
{
    // https://docs.oracle.com/javase/7/docs/api/java/io/BufferedWriter.html
    public class BufferedWriter : Writer
    {
        private FileWriter fileWriter;

        public BufferedWriter(FileWriter fileWriter)
        {
            this.fileWriter = fileWriter;
        }
    }
}
