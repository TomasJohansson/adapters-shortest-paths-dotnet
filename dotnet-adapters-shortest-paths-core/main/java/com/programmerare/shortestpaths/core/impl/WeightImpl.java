/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.core.impl;

import com.programmerare.shortestpaths.core.api.Weight;

/**
 * @author Tomas Johansson
 */
public final class WeightImpl implements Weight {

	private final double value;

	public static Weight createWeight(
		final double value			
	) {
		return new WeightImpl(
			value
		);
	}
	
	private WeightImpl(final double value) {
		this.value = value;
	}

	public double getWeightValue() {
		return value;
	}

	@Override
	public String toString() {
		return "WeightImpl [value=" + value + "]";
	}

	@Override
	public int hashCode() {
		final int prime = 31;
		int result = 1;
		long temp;
		temp = Double.doubleToLongBits(value);
		result = prime * result + (int) (temp ^ (temp >>> 32));
		return result;
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (!(obj instanceof WeightImpl))
			return false;
		WeightImpl other = (WeightImpl) obj;
		if (Double.doubleToLongBits(value) != Double.doubleToLongBits(other.value))
			return false;
		return true;
	}

	public final static double SMALL_DELTA_VALUE_FOR_WEIGHT_COMPARISONS = 0.0000000001;
	
	public String renderToString() {
		return toString();
	}

	public Weight create(double value) {
		return WeightImpl.createWeight(value);
	}

//	public Weight create(final double value) {
//		return createWeight(value);
//	}	
}
