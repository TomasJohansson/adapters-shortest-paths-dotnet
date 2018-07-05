using com.programmerare.shortestpaths.adapter.bsmock;
using com.programmerare.shortestpaths.adapter.yanqi;
using com.programmerare.shortestpaths.adapter.quickgraph;
using com.programmerare.shortestpaths.adapter.parrisha;
using com.programmerare.shortestpaths.core.api;
using System.Collections.Generic;

namespace com.programmerare.shortestpaths.graph.utils
{
    /**
     * @author Tomas Johansson
     */
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