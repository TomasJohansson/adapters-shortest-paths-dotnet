using System;

// java: System.out and System.err
// but .NET has a keyword out and a core namespace System
// so to avaoid problems with those, instead 
// the classes SystemErr and SystemOut are used
namespace java.lang
{
    // class System is located in package java.lang
    // https://docs.oracle.com/javase/7/docs/api/java/lang/System.html

    public class SystemOut
    {
        public static void println(object o)
        {
            Console.WriteLine(o);
        }
    }
}
