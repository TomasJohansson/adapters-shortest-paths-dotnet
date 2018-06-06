/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
namespace com.programmerare.shortestpaths.utils
{
    public sealed class TimeMeasurer {

	    public static TimeMeasurer Start() {
		    return new TimeMeasurer();
	    }
	
	    private readonly long startTime;
	
	    private TimeMeasurer() {
		    this.startTime = 0;// TODO(new Date()).getTime();
	    }
	
	    public long GetSeconds() {
		    return 0;// TODO((new Date()).getTime()-startTime) / 1000;
	    }
    }
}