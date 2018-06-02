namespace java.lang
{
    // https://docs.oracle.com/javase/7/docs/api/java/lang/Double.html

    // .NET has a struct named Double in namespace System:
    // https://msdn.microsoft.com/en-us/library/system.double(v=vs.110).aspx
    // so to avoid conflict with it, the suffix "J" is used below in class name DoubleJ
    public class DoubleJ
    {
        // https://docs.oracle.com/javase/7/docs/api/java/lang/Double.html#parseDouble(java.lang.String)
        public static double parseDouble(string s)
        {
            return double.Parse(s);
        }
    }
}
