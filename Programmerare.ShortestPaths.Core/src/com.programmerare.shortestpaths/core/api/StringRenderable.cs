/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in this "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using Programmerare.ShortestPaths.Core.Validation; // to avoid warning for the XML comment refering to GraphEdgesValidator

namespace Programmerare.ShortestPaths.Core.Api
{
    /// <summary>
    /// Used for trying to enforce that implementations of core types (Vertex, Edge, Weight)
    /// will output useful string, which is used in validation methods in class <see cref="GraphEdgesValidator{P,E,V,W}"/>.
    /// Another reason for introducing the interface was to the avoid ugliness of otherwise letting a method 
    /// receive type "Object" as a parameter when you want to use "toString" when the actual objects
    /// are one of Vertex, Edge or Weight, but those three types did not have any common base type before this interface was introduced.
    /// </summary>
    public interface StringRenderable {
	    string RenderToString();
    }
}