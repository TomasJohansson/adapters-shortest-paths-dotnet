namespace java.lang
{
    // https://docs.oracle.com/javase/7/docs/api/java/lang/StringBuffer.html
    public class StringBuffer
    {
        private System.Text.StringBuilder sb = new System.Text.StringBuilder();

        public override string ToString()
        {
            return sb.ToString();
        }

        public string toString()
        {
            return ToString();
        }

        public void append(object o)
        {
            sb.Append(o);
        }
    }
}
