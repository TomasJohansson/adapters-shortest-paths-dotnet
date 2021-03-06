/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in this "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using Programmerare.ShortestPaths.Core.Api;
using Programmerare.ShortestPaths.Core.Impl;
using System.Collections.Generic;

namespace Programmerare.ShortestPaths.Core.PathFactories
{
    public sealed class PathFactoryDefault
	    : PathFactory<Path , Edge , Vertex , Weight>
    {
	    public Path CreatePath(Weight totalWeight, IList<Edge> edges) {
		    Path pathDefault = PathImpl.CreatePath((Weight)totalWeight, edges);
		    return pathDefault;
	    }
    }
}