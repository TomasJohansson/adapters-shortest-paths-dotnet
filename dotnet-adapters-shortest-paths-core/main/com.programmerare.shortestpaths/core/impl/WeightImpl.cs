/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
using com.programmerare.shortestpaths.core.api;
using System;

namespace com.programmerare.shortestpaths.core.impl
{
    /**
     * @author Tomas Johansson
     */
    public sealed class WeightImpl : Weight {

	    private readonly double value;

	    public static Weight createWeight(
	        double value			
	    ) {
		    return new WeightImpl(
			    value
		    );
	    }
	
	    private WeightImpl(double value) {
		    this.value = value;
	    }

	    public double getWeightValue() {
		    return value;
	    }

	    public override string ToString() {
		    return "WeightImpl [value=" + value + "]";
	    }

	    public override int GetHashCode() {
		    int prime = 31;
		    int result = 1;
		    long temp;
		    temp = BitConverter.DoubleToInt64Bits(value);
		    //result = prime * result + (int) (temp ^ (temp >>> 32));
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
	
	    public string renderToString() {
		    return ToString();
	    }

	    public Weight create(double value) {
		    return WeightImpl.createWeight(value);
	    }
    }
}