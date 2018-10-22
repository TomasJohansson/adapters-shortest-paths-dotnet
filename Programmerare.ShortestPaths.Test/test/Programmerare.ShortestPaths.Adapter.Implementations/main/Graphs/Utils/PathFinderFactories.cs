/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in the "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using Programmerare.ShortestPaths.Adapter.Bsmock;
using Programmerare.ShortestPaths.Adapter.YanQi;
using Programmerare.ShortestPaths.Core.Api;
using System.Collections.Generic;

namespace Programmerare.ShortestPaths.Graphs.Utils
{
    public sealed class PathFinderFactories {
	
	    /**
	     * @return a list of implementations that should be used for searching the best paths,
	     * and the implementation results will be verified with each other and the test will cause a failure 
	     * if there is any mismatch found in the results.
	     */	
	    public static IList<PathFinderFactory>  CreatePathFinderFactories() {
		    IList<PathFinderFactory> list = new List<PathFinderFactory>();
		    list.Add(new PathFinderFactoryYanQi());
		    list.Add(new PathFinderFactoryBsmock());
		    // The other implementations below do not currently seem to work for all tests and therefore disabled
		    //list.Add(new PathFinderFactoryQuickGraph());
		    //list.Add(new PathFinderFactoryParrisha());
		    return list;		
	    }
    }
}