using System;
using System.Collections.Generic;
using System.Text;

namespace Programmerare.ShortestPaths.Adaptees.Common.DotNetTypes.DotNet20
{
    class SupportForExtensionMethods
    {
    }
}
#if ( NET20 || NET30 )
// To support extension methods with when targeting .NET 2.0
// https://stackoverflow.com/questions/2280716/is-it-possible-to-create-extension-methods-with-2-0-framework
// https://stackoverflow.com/questions/707145/extension-method-in-c-sharp-2-0
namespace System.Runtime.CompilerServices
{
    //[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
    public class ExtensionAttribute : Attribute { }
}
#endif
