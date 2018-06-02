using System;

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
            throw new NotImplementedException();
        }

    }
}