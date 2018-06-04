/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/
package com.programmerare.shortestpaths.utils;

import java.util.Date;

public final class TimeMeasurer {

	public static TimeMeasurer start() {
		return new TimeMeasurer();
	}
	
	private final long startTime;
	
	private TimeMeasurer() {
		this.startTime = (new Date()).getTime();
	}
	
	public long getSeconds() {
		return ((new Date()).getTime()-startTime) / 1000;
	}
}
