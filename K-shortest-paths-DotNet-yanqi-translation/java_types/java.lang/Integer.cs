namespace java.lang
{
    // https://docs.oracle.com/javase/7/docs/api/java/lang/Integer.html
    // .NET does not have a class named Integer (though Double exists)
    // but instead there is a class Int32 (int) :
    // https://msdn.microsoft.com/en-us/library/system.int32(v=vs.110).aspx
    public class Integer
    {
        public static int parseInt(string s)
        {
            return int.Parse(s);
        }
    }
}
