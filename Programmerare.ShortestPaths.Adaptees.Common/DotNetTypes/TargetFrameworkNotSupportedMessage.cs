using System;
using System.Collections.Generic;
using System.Text;

namespace Programmerare.ShortestPaths.Adaptees.Common.DotNetTypes
{
    public class TargetFrameworkNotSupportedMessage
    {
        /// <summary>
        /// Around the usage of this constant there should probably be a 
        /// preprocessor directive such as below:
        ///     if (NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
        /// and code like this:
        ///     throw new NotImplementedException(TargetFrameworkNotSupportedMessage.NET_STANDARD_1_0_to_1_6_NOT_SUPPORTED);
        /// </summary>
        public const string NET_STANDARD_1_0_to_1_6_NOT_SUPPORTED = "The method is not supported for .NET Standard 1.0 - 1.6";
    }
}
