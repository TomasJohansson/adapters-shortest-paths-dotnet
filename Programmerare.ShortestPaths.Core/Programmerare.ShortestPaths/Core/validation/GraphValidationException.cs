/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in this "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/
using System;

namespace Programmerare.ShortestPaths.Core.Validation
{
    /// <summary>
    /// See <see cref="GraphEdgesValidator"/>
    /// </summary>
    public class GraphValidationException : Exception {
	    public GraphValidationException(string message): base(message) {
	    }
    }
}