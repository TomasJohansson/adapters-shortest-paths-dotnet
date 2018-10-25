using System;

namespace Programmerare.ShortestPaths.Adaptees.Common.DotNetTypes {
    // https://docs.microsoft.com/en-us/dotnet/api/system.console?view=netframework-4.7.2
    //Console class applies to:
    //.NET Framework
    //4.7.2 4.7.1 4.7 4.6.2 4.6.1 4.6 4.5.2 4.5.1 4.5 4.0 3.5 3.0 2.0 1.1
    //.NET Standard
    //2.0 1.6 1.4 1.3

    // Probably it is a mistake that 1.5 is currently missing at the above web page ?
    // Note that Console is NOT supported by:
    // .NET Standard 1.2 , 1.1 , 1.0

    public class ConsoleUtility {
        public static void WriteLine(object o) {
#if !(NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2)
            Console.WriteLine(o);
#endif
        }
    }
}