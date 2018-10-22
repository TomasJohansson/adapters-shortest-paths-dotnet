/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in this "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

namespace Programmerare.ShortestPaths.Core.Validation
{
    /// <summary>
    /// Currently only two possbile values (but this might of course become extended with mroe fine-grained options about what to validate).
    /// The reason for using this enum (instead of boolean) is to provide semantic when reading code with the invocation
    /// instead of just sending some value of "true" or "false" 
    /// </summary>
    public enum GraphEdgesValidationDesired {
	    NO, 
        YES
    }
}