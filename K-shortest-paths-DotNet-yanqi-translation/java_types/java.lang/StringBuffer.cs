using System;
using System.Text;

namespace java.lang
{
    // https://docs.oracle.com/javase/7/docs/api/java/lang/StringBuffer.html
    public class StringBuffer
    {
        private StringBuilder sb = new StringBuilder();

        public override string ToString()
        {
            return sb.ToString();
        }

        public void append(string s)
        {
            sb.Append(s);
        }
    }
}
