/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in this "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using Programmerare.ShortestPaths.Core.Api.Generics;

namespace Programmerare.ShortestPaths.Core.Api
{
    /// <summary>
    /// See <see cref="PathFinderFactoryGenerics{F,P,E,V,W}"/>
    /// </summary>
    public interface PathFinderFactory 
	    : 
        PathFinderFactoryGenerics<
            PathFinder,
            Path,
            Edge,
            Vertex,
            Weight
        >
    { }
}