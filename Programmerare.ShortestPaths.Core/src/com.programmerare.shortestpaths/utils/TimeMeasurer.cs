/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in this "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/
namespace Programmerare.ShortestPaths.Utils
{
    // TODO this class is not yet translated from the Java code ... 
    public sealed class TimeMeasurer {

	    public static TimeMeasurer Start() {
		    return new TimeMeasurer();
	    }
	
	    private readonly long startTime;
	
	    private TimeMeasurer() {
		    this.startTime = 0;// TODO not yet translated from the Java code ... (new Date()).getTime();
	    }
	    
	    public long GetSeconds() {
		    return 0;// TODO((new Date()).getTime()-startTime) / 1000;
	    }
    }
}