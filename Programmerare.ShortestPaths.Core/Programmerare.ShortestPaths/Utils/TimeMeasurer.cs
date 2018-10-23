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

namespace Programmerare.ShortestPaths.Utils
{
    // TODO this class is not yet translated from the Java code ... 
    public sealed class TimeMeasurer {

	    public static TimeMeasurer Start() {
		    return new TimeMeasurer(DateTime.Now);
	    }

	    private readonly DateTime startTime;
	
	    private TimeMeasurer(DateTime startTime) {
            this.startTime = startTime;
	    }
	    

	    public int GetSeconds() {
            return GetNumberOfSecondsBetweenTwoDates(this.startTime, DateTime.Now);
        }

        /// <summary>
        /// The number of seconds between the two DateTime instances.
        /// The returned value is an integer and is rounded by removing the decimal 
        /// part as when using casting from double to int.
        /// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/explicit-numeric-conversions-table
        /// </summary>
        /// <returns></returns>
        public static int GetNumberOfSecondsBetweenTwoDates(DateTime startTime, DateTime endTime)
        {
            long elapsedTicks = endTime.Ticks - startTime.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            return (int) elapsedSpan.TotalSeconds;
        }
    }
}