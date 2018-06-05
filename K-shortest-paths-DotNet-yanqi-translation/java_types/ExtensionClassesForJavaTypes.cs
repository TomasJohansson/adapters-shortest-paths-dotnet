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



    }
}