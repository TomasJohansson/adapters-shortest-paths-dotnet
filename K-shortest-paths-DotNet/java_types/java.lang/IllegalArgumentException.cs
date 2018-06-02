using System;

namespace java.lang
{
    // https://docs.oracle.com/javase/7/docs/api/java/lang/IllegalArgumentException.html
    public class IllegalArgumentException : ArgumentException
    {
        public IllegalArgumentException(string message) : base(message)
        {
        }
    }
}
