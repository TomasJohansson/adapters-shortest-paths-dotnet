/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in this "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using com.programmerare.shortestpaths.core.api;
using System;

namespace com.programmerare.shortestpaths.core.impl
{
    public sealed class WeightImpl : Weight {

	    private readonly double value;

	    public static Weight CreateWeight(
	        double value			
	    ) {
		    return new WeightImpl(
			    value
		    );
	    }
	
	    private WeightImpl(double value) {
		    this.value = value;
	    }

        public double WeightValue => value;

        public override string ToString() {
		    return "WeightImpl [value=" + value + "]";
	    }

	    public override int GetHashCode() {
		    // int prime = 31;
		    int result = 1;
		    long temp;
		    temp = BitConverter.DoubleToInt64Bits(value);
		    //result = prime * result + (int) (temp ^ (temp >>> 32));
            // The above "disabled" line (i.e. marked as comment with "//") originates from the original java code which then has been translated to C#
            result = (int)temp; // TODO: Java >>> is not valid in C#
		    return result;
	    }

	    public override bool Equals(object obj) {
		    if (this == obj)
			    return true;
		    if (obj == null)
			    return false;
		    if (!(obj is WeightImpl))
			    return false;
		    WeightImpl other = (WeightImpl) obj;
		    if (BitConverter.DoubleToInt64Bits(value) != BitConverter.DoubleToInt64Bits(other.value))
			    return false;
		    return true;
	    }

	    public const double SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS = 0.0000000001;
	
	    public string RenderToString() {
		    return ToString();
	    }

	    public Weight Create(double value) {
		    return WeightImpl.CreateWeight(value);
	    }
    }
}