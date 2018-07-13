using System;
using System.Text.RegularExpressions;

namespace extensionClassesForJavaTypes
{
    public static class ExtensionClassesForJavaTypes
    {
        public static string trim(this String s)
        {
            return s.Trim();
        }

        public static String[] split(this String s, string regularExpression)
        {
            return Regex.Split(s, regularExpression);
        }
        public static void printStackTrace(this Exception e)
        {
            Console.Error.WriteLine(e);
        }

        //public static string toString(this object o)
        //{
        //    return o.ToString();
        //}

    }
}