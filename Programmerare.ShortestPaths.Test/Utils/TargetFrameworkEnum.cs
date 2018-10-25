// see comment at the top of the file "TargetFramework.cs"

namespace Programmerare.ShortestPaths.Test.Utils {
    internal enum TargetFrameworkEnum {
        // the same name of constans as the preprocessor symbols:
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/preprocessor-directives/preprocessor-if
        NET20, NET35, NET40, NET45, NET451, NET452, NET46, NET461, NET462, NET47, NET471, NET472,

        NETSTANDARD1_0, NETSTANDARD1_1, NETSTANDARD1_2, NETSTANDARD1_3, NETSTANDARD1_4, NETSTANDARD1_5, NETSTANDARD1_6, NETSTANDARD2_0,

        NETCOREAPP1_0, NETCOREAPP1_1, NETCOREAPP2_0, NETCOREAPP2_1,

        UNKNOWN
    }
}